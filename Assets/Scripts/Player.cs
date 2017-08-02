using UnityEngine;

public class Player : MonoBehaviour, IPowerHolder
{
    private float currentPower = 0f;

    [Tooltip("The amount of power lost per second.")]
    public float powerLossRate = 0.5f;

    public float maxPower = 100f;

    public void GivePower(float amount)
    {
        currentPower = Mathf.Clamp(currentPower + amount, 0f, maxPower);
    }

    /// <summary>
    /// Take power from the Player.
    /// </summary>
    /// <param name="amount">The amount to subtract.</param>
    /// <returns>The actual amount that was subtracted in case the player
    /// doesn't have that much power.</returns>
    public float TakePower(float amount)
    {
        float startPower = currentPower;
        currentPower = Mathf.Clamp(currentPower - amount, 0f, maxPower);
        return startPower - currentPower;
    }

    public float GetCurrentPowerLevel()
    {
        return currentPower;
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
}
