using UnityEngine;

public class Player : MonoBehaviour
{
    private float currentPowerLevel = 0f;

    [Tooltip("The amount of power lost per second.")]
    public float powerLossRate = 0.5f;

    public void GivePower(float amount)
    {
        currentPowerLevel = Mathf.Clamp(currentPowerLevel + amount, 0f, 100f);
    }

    public void TakePower(float amount)
    {
        currentPowerLevel = Mathf.Clamp(currentPowerLevel - amount, 0f, 100f);
    }

    public float GetCurrentPowerLevel()
    {
        return currentPowerLevel;
    }

    private void Update()
    {
        TakePower(powerLossRate * Time.deltaTime);
    }
}
