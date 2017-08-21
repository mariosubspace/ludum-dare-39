using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGhost : MonoBehaviour
{
    public Ghost target;
    public float powerGiveAmount = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            target.GivePower(powerGiveAmount);
        }
    }
}
