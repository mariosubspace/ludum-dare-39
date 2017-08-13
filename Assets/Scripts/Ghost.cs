
using System;
using UnityEngine;

public class Ghost : PowerHolder
{
    Transform powerSink;

    private void Awake()
    {
        powerSink = transform.Find("Power Sink");
    }

    void Update()
    {
        // Destroy when the ghost get's 'charged' to max power.
        if (currentPower >= maxPower)
        {
            Destroy(gameObject);
        }
    }

    public override Transform GetPowerSink()
    {
        return powerSink;
    }
}
