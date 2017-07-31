using UnityEngine;

public class House : MonoBehaviour
{
    private GameObject batteryGroup;

    private void Awake()
    {
        batteryGroup = transform.Find("Battery Group").gameObject;
    }

    public void SetBatteryActive(bool b)
    {
        batteryGroup.SetActive(b);
    }

    public bool HasBattery()
    {
        return batteryGroup.activeInHierarchy;
    }
}
