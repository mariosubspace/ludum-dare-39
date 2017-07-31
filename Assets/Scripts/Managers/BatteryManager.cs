using UnityEngine;
using Mario;

public class BatteryManager : MonoBehaviour
{
    public GameManager gameManager;
    // add event for battery count changed?
    // or poll for changes every frame (egh)...

    private House[] houses;

    private void Awake()
    {
        houses = FindObjectsOfType<House>();
        Debug.LogFormat("BatteryManager:: Found {0} houses.", this.houses.Length);
    }

    private void OnEnable()
    {
        int count = CountActiveBatteries();
        Debug.LogFormat("BatteryManager:: There are {0} batteries active.", count);
    }

    private void Update()
    {
        // let's poll for now.
        if (CountActiveBatteries() < gameManager.maxBatteriesInScene)
        {
            int houseIndex = Random.Range(0, houses.Length);
            houses[houseIndex].SetBatteryActive(true);

            // Looks like it's spawning batteries correctly,
            // need to have the batteries able to be consumed now.
            //Debug.LogFormat("BatteryManager:: Randomly spawned battery at house {0}", houses[houseIndex].name);
        }
    }

    private int CountActiveBatteries()
    {
        int count = 0;
        for (int i = 0; i < houses.Length; i++)
        {
            if (houses[i].HasBattery())
            {
                ++count;
            }
        }
        return count;
    }
}
