using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    private GameController controller;

    private void Awake()
    {
        controller = GameController.Instance;
    }

    public void OnClickSpawn()
    {
        int needGold = int.Parse(controller.requireGold.text);
        if (controller.gold >= needGold)
        {
            controller.gold -= needGold;

            needGold += 5;
            controller.requireGold.text = needGold.ToString();
        }
        else
        {
            return;
        }

        int rand = Random.Range(1, 101); // 1 ~ 100
        int id;
        switch (rand)
        {
            case int n when n <= 50: // Normal 50%
                Debug.Log("Hero_Normal");
                id = 0;
                break;

            case int n when n <= 75: // Rare 25%
                Debug.Log("Hero_Rare");
                id = 1;
                break;

            case int n when n <= 90: // Epic 15%
                Debug.Log("Hero_Epic");
                id = 2;
                break;

            case int n when n <= 97: // Legendary 7%
                Debug.Log("Hero_Legendary");
                id = 3;
                break;

            default:                 // Mystic 3%
                Debug.Log("Hero_Mystic");
                id = 4;
                break;
        }
        Spawn(id);
    }

    private void Spawn(int id)
    {
        GameObject hero = GameController.Instance.heroPool.Get(id);
        hero.transform.GetChild(0).position = spawnPoint.position;
    }

    public void SpawnBoss()
    {
        GameObject hero = GameController.Instance.heroPool.Get(5);
        hero.transform.GetChild(0).position = spawnPoint.position;
    }
}
