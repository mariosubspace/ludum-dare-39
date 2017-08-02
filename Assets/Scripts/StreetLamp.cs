using UnityEngine;

public class StreetLamp : MonoBehaviour, IPowerHolder
{
    [Tooltip("Seconds for lamp to go dark.")]
    public float timeToDecay = 10f;
    float powerDecayRate; // amount of power to take per second.

    new Light light;

    public AnimationCurve decayAnimation;
    [Tooltip("The maximum power stored, balance this with the power batteries give.")]
    public float maxPower = 30f;
    float currentPower = 30f;

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
        float intensity = decayAnimation.Evaluate(currentPower / maxPower);
        light.intensity = intensity;
        lightMatInstance.SetColor("_EmissionColor", emissionColor * intensity);
        powerLevelMatInstance.SetFloat("_PowerLevel", intensity);
    }

#region IPowerHolder

    public void GivePower(float power)
    {
        Debug.Log("We got power! " + power);
        currentPower = Mathf.Clamp(currentPower + power, 0f, maxPower);
    }

    public float TakePower(float power)
    {
        float startPower = currentPower;
        currentPower = Mathf.Clamp(currentPower - power, 0f, maxPower);
        return currentPower - startPower;
    }

    public float GetAmountDepleted()
    {
        return maxPower - currentPower;
    }

    public float GetCurrentPower()
    {
        return currentPower;
    }

    public float GetMaxPower()
    {
        return maxPower;
    }

#endregion
}
