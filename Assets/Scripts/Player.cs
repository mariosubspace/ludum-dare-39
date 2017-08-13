using UnityEngine;

public class Player : PowerHolder
{
    // This is not used for the player.
    public override Transform GetPowerSink()
    {
        return transform;
    }
}
