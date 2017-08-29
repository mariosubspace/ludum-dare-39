using UnityEngine;

public class Player : PowerHolder
{
    public float maxHealth = 40f;
    float health;

    private void Awake()
    {
        health = maxHealth;
    }

    // This is not used for the player.
    public override Transform GetPowerSink()
    {
        return transform;
    }

    /// <summary>
    /// Inflict damage on the player...
    /// </summary>
    /// <param name="amt">How much would you like to hurt the player?</param>
    public void HurtPlayer(float amt)
    {
        float debug_lostHealth = health;

        health = Mario.MathTools.Max(health - amt, 0f);

        debug_lostHealth = debug_lostHealth - health;
        Debug.LogFormat("Ouch! Lost health: {0}", debug_lostHealth);
    }

    /// <summary>
    /// Heal the player. 
    /// </summary>
    /// <param name="amt">Some amount to heal the player...</param>
    [System.Obsolete]
    public void HealPlayer(float amt)
    {
        health = Mario.MathTools.Min(health + amt, maxHealth);
    }
}
