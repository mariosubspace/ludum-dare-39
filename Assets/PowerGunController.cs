using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGunController : MonoBehaviour
{
    public Image reticle;
    Ray r = new Ray();
    public float maxDistance = 10f;
    public LayerMask layers;
    Transform exitPoint;
    Transform hitRaycastStart;

    private void Awake()
    {
        exitPoint = transform.Find("Exit Point");
        hitRaycastStart = transform.Find("Hit Raycast Start");
    }

    private void Update()
    {
        RaycastHit hit;
        r.origin = hitRaycastStart.position;
        r.direction = hitRaycastStart.up;

        Debug.DrawRay(exitPoint.position, r.direction, Color.blue);
        if (Physics.Raycast(r, out hit, maxDistance, layers))
        {
            reticle.color = Color.red;
        }
        else
        {
            reticle.color = Color.white;
        }
    }
}
