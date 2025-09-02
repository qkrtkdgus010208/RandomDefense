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

    public void OnClickUpgrade(int id)
    {
        int needGold = 0;
        switch (id)
        {
            case 0:
                needGold = int.Parse(requireGold0.text);
                break;
            case 1:
                needGold = int.Parse(requireGold1.text);
                break;
            case 2:
                needGold = int.Parse(requireGold2.text);
                break;
        }

        if (controller.gold >= needGold)
        {
            controller.gold -= needGold;

            switch (id)
            {
                case 0:
                    needGold += 30;
                    requireGold0.text = needGold.ToString();
                    level0++;
                    textLevel0.text = $"Lv.{level0 + 1}";
                    upgrade0 += 0.2f;
                    break;

                case 1:
                    needGold += 50;
                    requireGold1.text = needGold.ToString();
                    level1++;
                    textLevel1.text = $"Lv.{level1 + 1}";
                    upgrade1 += 0.2f;
                    break;

                case 2:
                    needGold += 100;
                    requireGold2.text = needGold.ToString();
                    level2++;
                    textLevel2.text = $"Lv.{level2 + 1}";
                    upgrade2 += 0.2f;
                    break;
            }
        }
        else
        {
            return;
        }

        UpdateUpgradeData();
    }

    private void UpdateUpgradeData()
    {
        OnUpgradeChanged?.Invoke();
    }
}
