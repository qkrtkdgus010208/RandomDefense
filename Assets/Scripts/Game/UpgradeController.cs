using System;
using TMPro;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public TextMeshProUGUI requireGold0;
    public TextMeshProUGUI requireGold1;
    public TextMeshProUGUI requireGold2;

    public TextMeshProUGUI textLevel0;
    public TextMeshProUGUI textLevel1;
    public TextMeshProUGUI textLevel2;

    public int level0;
    public int level1;
    public int level2;

    public float upgrade0 = 1.0f;
    public float upgrade1 = 1.0f;
    public float upgrade2 = 1.0f;

    public event Action OnUpgradeChanged;

    private GameController controller;

    private void Awake()
    {
        controller = GameController.Instance;
    }

    private void Start()
    {
        requireGold0.text = "30";
        requireGold1.text = "50";
        requireGold2.text = "100";

        textLevel0.text = $"Lv.{level0 + 1}";
        textLevel1.text = $"Lv.{level1 + 1}";
        textLevel2.text = $"Lv.{level2 + 1}";

        UpdateUpgradeData();
    }

    public void OnClickUpgrade0()
    {
        int needGold = int.Parse(requireGold0.text);
        if (controller.gold >= needGold)
        {
            controller.gold -= needGold;

            needGold += 30;
            requireGold0.text = needGold.ToString();

            level0++;
            textLevel0.text = $"Lv.{level0 + 1}";
        }
        else
        {
            return;
        }

        upgrade0 += 0.2f;
        UpdateUpgradeData();
    }

    public void OnClickUpgrade1()
    {
        int needGold = int.Parse(requireGold1.text);
        if (controller.gold >= needGold)
        {
            controller.gold -= needGold;

            needGold += 50;
            requireGold1.text = needGold.ToString();

            level1++;
            textLevel1.text = $"Lv.{level1 + 1}";
        }
        else
        {
            return;
        }

        upgrade1 += 0.2f;
        UpdateUpgradeData();
    }

    public void OnClickUpgrade2()
    {
        int needGold = int.Parse(requireGold2.text);
        if (controller.gold >= needGold)
        {
            controller.gold -= needGold;

            needGold += 100;
            requireGold2.text = needGold.ToString();

            level2++;
            textLevel2.text = $"Lv.{level2 + 1}";
        }
        else
        {
            return;
        }

        upgrade2 += 0.2f;
        UpdateUpgradeData();
    }

    private void UpdateUpgradeData()
    {
        OnUpgradeChanged?.Invoke();
    }
}
