using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public TMP_Text powerText;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        powerText = GetComponentInChildren<TMP_Text>();
    }

    private void SetPowerAmount(float power)
    {
        powerText.text = "Power Level: " + (int)power;
    }

    private void Update()
    {
        SetPowerAmount(player.GetCurrentPowerLevel());
    }
}
