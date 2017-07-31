using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public float ySpeed; // really dumb, but just need horizontal rotation right now.

    Vector3 UP = Vector3.up;

    private void Update()
    {
        transform.Rotate(UP, ySpeed * Time.deltaTime, Space.World);
    }
}
