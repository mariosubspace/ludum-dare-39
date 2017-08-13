using UnityEngine;

public abstract class PowerHolder : MonoBehaviour
{
    public float maxPower;
    protected float currentPower;

    /// <summary>
    /// Add power.
    /// </summary>
    /// <param name="amount">The amount of power to add.</param>
    /// <returns>The amount of power actually added.</returns>
    public float GivePower(float amount)
    {
        // Validate input.
        if (amount < 0)
        {
            Debug.LogError("PowerHolder:: GivePower() cannot accept a negative value. [" + amount + "]");
            return 0f;
        }

        // Add amount to power up to maximum.
        float startPower = currentPower;
        currentPower = Mathf.Clamp(currentPower + amount, 0f, maxPower);

        // Return the amount added.
        return currentPower - startPower;
    }

    /// <summary>
    /// Remove power.
    /// </summary>
    /// <param name="amount">The amount of power to remove.</param>
    /// <returns>The amount of power actually removed.</returns>
    public float TakePower(float amount)
    {
        // Validate input.
        if (amount < 0)
        {
            Debug.LogError("PowerHolder:: TakePower() cannot accept a negative value. [" + amount + "]");
            return 0f;
        }

        // Remove amount of power.
        float startPower = currentPower;
        currentPower = Mathf.Clamp(currentPower - amount, 0f, maxPower);

        // Return the amount removed.
        return startPower - currentPower;
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

    /// <summary>
    /// Get the fill fraction (the amount the battery is full as a fraction).
    /// </summary>
    /// <returns>The fill fraction.</returns>
    public float GetFillFraction()
    {
        return currentPower / maxPower;
    }

    public void SetToMaxPower()
    {
        currentPower = maxPower;
    }

    public abstract Transform GetPowerSink();
}
