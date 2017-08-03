using UnityEngine;

public class StreetLamp : PowerHolder
{
    [Tooltip("Seconds for lamp to go dark.")]
    public float timeToDecay = 10f;
    float powerDecayRate; // amount of power to take per second.
    public AnimationCurve decayAnimation;
    new Light light;

    Material lightMatInstance;
    Color emissionColor;

    Material powerLevelMatInstance;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        powerDecayRate = maxPower / timeToDecay;
        lightMatInstance = transform.Find("Lamp Light").GetComponent<MeshRenderer>().material;
        emissionColor = lightMatInstance.GetColor("_EmissionColor");
        powerLevelMatInstance = transform.Find("Power Level Indicator").GetComponent<MeshRenderer>().material;
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
}
