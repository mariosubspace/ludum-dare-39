
using System;
using UnityEngine;

public class Ghost : PowerHolder
{
    Transform powerSink;
    Material healthBarMat;

    private void Awake()
    {
        powerSink = transform.Find("Power Sink");
        healthBarMat = transform.Find("Health Bar").GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        healthBarMat.SetFloat("_PowerLevel", 1f - GetFillFraction());

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
