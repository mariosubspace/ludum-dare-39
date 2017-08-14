using System;
using System.Collections;
using UnityEngine;

public class StreetLamp : PowerHolder
{
    [Tooltip("Seconds for lamp to go dark.")]
    public float timeToDecay = 10f;
    float powerDecayRate; // amount of power to take per second.
    public AnimationCurve decayAnimation;
    new Light light;

    public GameObject ghostAgentPrefab;
    public float secondsBetweenGhostSpawnAttempts = 2f;

    Material lightMatInstance;
    Color emissionColor;

    Material powerLevelMatInstance;

    Transform powerSink;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        powerDecayRate = maxPower / timeToDecay;
        lightMatInstance = transform.Find("Lamp Light").GetComponent<MeshRenderer>().material;
        emissionColor = lightMatInstance.GetColor("_EmissionColor");
        powerLevelMatInstance = transform.Find("Power Level Indicator").GetComponent<MeshRenderer>().material;
        powerSink = transform.Find("Power Sink");
        SetToMaxPower();
    }

    private void Start()
    {
        StartCoroutine(SpawnGhostOnChanceAfterWait(secondsBetweenGhostSpawnAttempts));
    }

    private void Update()
    {
        TakePower(powerDecayRate * Time.deltaTime);
        UpdateVisualsToMatchCurrentPower();
    }

    private void UpdateVisualsToMatchCurrentPower()
    {
        float intensity = decayAnimation.Evaluate(GetFillFraction());
        light.intensity = intensity;
        lightMatInstance.SetColor("_EmissionColor", emissionColor * intensity);
        powerLevelMatInstance.SetFloat("_PowerLevel", intensity);
    }

    public override Transform GetPowerSink()
    {
        return powerSink;
    }

    IEnumerator SpawnGhostOnChanceAfterWait(float waitTime)
    {
        // Wait for time to spawn ghost.
        float startTime = Time.time;
        while (Time.time - startTime < waitTime)
        {
            yield return null;
        }

        // Roll dice and see if we should spawn the ghost.
        float diceRoll = UnityEngine.Random.value; 
        if (diceRoll < GetGhostSpawnProbability())
        {
            // Determine where to spawn ghost.
            Vector3 position = transform.position;

            // Spawn the ghost.
            var ghostAgent = Instantiate(ghostAgentPrefab);
            ghostAgent.transform.position = position;

            Debug.LogFormat("StreetLamp:: Ghost spawned under lamp {0} at distance {1} from lamp.", name, (position - transform.position).magnitude);
        }

        // Start waiting to spawn again.
        StartCoroutine(SpawnGhostOnChanceAfterWait(waitTime));
    }

    float GetGhostSpawnProbability()
    {
        return 1 - GetFillFraction();
    }
}
