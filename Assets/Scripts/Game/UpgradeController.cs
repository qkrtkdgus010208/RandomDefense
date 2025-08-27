using TMPro;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController Instance { get; private set; }

    public TextMeshProUGUI requireGold0;
    public TextMeshProUGUI requireGold1;
    public TextMeshProUGUI requireGold2;

    public float upgrade0 = 1.0f;
    public float upgrade1 = 1.0f;
    public float upgrade2 = 1.0f;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickUpgrade0()
    {
        upgrade0 += 0.2f;
    }

    public void OnClickUpgrade1()
    {
        upgrade1 += 0.2f;
    }

    public void OnClickUpgrade2()
    {
        upgrade2 += 0.2f;
    }
}
