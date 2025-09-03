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
                break;
            case 1:
                needCoin = int.Parse(requireCoin1.text);
                break;
            case 2:
                needCoin = int.Parse(requireCoin2.text);
                break;
        }

        if (controller.coin >= needCoin)
        {
            controller.coin -= needCoin;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);

            switch (id)
            {
                case 0:
                    if (Random.Range(1, 101) <= 10)
                    {
                        heroSpawner.SpawnBoss();
                        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    }
                    break;

                case 1:
                    if (Random.Range(1, 101) <= 30)
                    {
                        heroSpawner.SpawnBoss();
                        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    }
                    break;

                case 2:
                    if (Random.Range(1, 101) <= 60)
                    {
                        heroSpawner.SpawnBoss();
                        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
                    }
                    break;
            }
        }
        else
        {
            return;
        }
    }
}
