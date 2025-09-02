using TMPro;
using UnityEngine;

public class LuckyController : MonoBehaviour
{
    public TextMeshProUGUI requireCoin0;
    public TextMeshProUGUI requireCoin1;
    public TextMeshProUGUI requireCoin2;

    public HeroSpawner heroSpawner;

    private GameController controller;

    private void Awake()
    {
        controller = GameController.Instance;
    }

    private void Start()
    {
        requireCoin0.text = "5";
        requireCoin1.text = "20";
        requireCoin2.text = "50";
    }

    public void OnClickLucky(int id)
    {
        int needCoin = 0;
        switch (id)
        {
            case 0:
                needCoin = int.Parse(requireCoin0.text);
                if (Random.Range(1, 101) <= 10)
                    heroSpawner.SpawnBoss();
                break;

            case 1:
                needCoin = int.Parse(requireCoin1.text);
                if (Random.Range(1, 101) <= 30)
                    heroSpawner.SpawnBoss();
                break;

            case 2:
                needCoin = int.Parse(requireCoin2.text);
                if (Random.Range(1, 101) <= 60)
                    heroSpawner.SpawnBoss();
                break;
        }

        if (controller.coin >= needCoin)
        {
            controller.coin -= needCoin;
        }
        else
        {
            return;
        }
    }
}
