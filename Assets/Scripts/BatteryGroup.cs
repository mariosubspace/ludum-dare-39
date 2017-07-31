using UnityEngine;
using Mario;

public class BatteryGroup : MonoBehaviour
{
    Player player;
    GameManager gameManager;

    float powerGiveAmount;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();

        powerGiveAmount = 100f / gameManager.maxBatteriesInScene;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("BatteryGroup:: Collided with battery in house {0}", transform.parent.name);
        gameObject.SetActive(false);
        player.GivePower(powerGiveAmount);
    }
}
