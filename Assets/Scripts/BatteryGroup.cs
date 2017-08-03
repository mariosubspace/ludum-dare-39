using UnityEngine;

public class BatteryGroup : MonoBehaviour
{
    Player player;

    public float powerGiveAmount = 20f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("BatteryGroup:: Collided with battery group for house {0}", other.transform.parent.name);
        gameObject.SetActive(false);
        player.GivePower(powerGiveAmount);
    }
}
