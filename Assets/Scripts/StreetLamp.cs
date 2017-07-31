using UnityEngine;

public class StreetLamp : MonoBehaviour
{
    [Tooltip("Seconds for lamp to go dark.")]
    public float timeToDecay = 10f;
    float powerDecayRate; // amount of power to take per second.

    new Light light;

    public AnimationCurve decayAnimation;
    float currentPower = 100f;

    Material lightMatInstance;
    Color emissionColor;

    Material powerLevelMatInstance;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        powerDecayRate = 100f / timeToDecay;
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
        float intensity = decayAnimation.Evaluate(currentPower / 100f);
        light.intensity = intensity;
        lightMatInstance.SetColor("_EmissionColor", emissionColor * intensity);
        powerLevelMatInstance.SetFloat("_PowerLevel", intensity);
    }

    public void GivePower(float power)
    {
        currentPower = Mathf.Clamp(currentPower + power, 0f, 100f);
    }

    public void TakePower(float power)
    {
        currentPower = Mathf.Clamp(currentPower - power, 0f, 100f);
    }
}
