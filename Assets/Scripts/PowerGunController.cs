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

    [Tooltip("Amount of power to transfer per second.")]
    public float powerTransferRate = 10;

    ArcGenerator arcGenerator;

    Transform exitPoint;
    Transform hitRaycastStart;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        exitPoint = transform.Find("Exit Point");
        hitRaycastStart = transform.Find("Hit Raycast Start");
        arcGenerator = transform.Find("Arc Generator").GetComponent<ArcGenerator>();
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

            if (Input.GetMouseButton(0))
            {
                var hitPowerHolder = hit.collider.gameObject.GetComponent<PowerHolder>();
                if (hitPowerHolder != null)
                {
                    // Get the default amount to transfer.
                    float powerRequest = powerTransferRate * Time.deltaTime;

                    // If the depletion amount is less than the default amount, don't waste all
                    // the energy. Use the less of the default and depleted amounts.
                    powerRequest = Mathf.Min(powerRequest, hitPowerHolder.GetAmountDepleted());

                    // Take this amount from the player.
                    // The player might not have enough power, so we will save the actual amount taken.
                    float powerTaken = player.TakePower(powerRequest);

                    if (Mathf.Approximately(powerTaken, 0f))
                    {
                        arcGenerator.TurnOff();
                    }
                    else
                    {
                        arcGenerator.SetTarget(hitPowerHolder.GetPowerSink());
                        arcGenerator.TurnOn();
                    }

                    // Transfer the power taken from the player to the hit PowerHolder.
                    hitPowerHolder.GivePower(powerTaken);
                }
            }
            else
            {
                arcGenerator.TurnOff();
            }
        }
        else
        {
            reticle.color = Color.white;
            arcGenerator.TurnOff();
        }
    }
}
