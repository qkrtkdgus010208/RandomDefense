using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float spawnTime = 3;
    public float bossSpawnTime = 30;

    float timer;
    float timerBoss;

    private void Update()
    {
        if (!GameController.Instance.isPlay)
            return;

        // 몬스터 스폰
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            timer = 0f;
            int rand = Random.Range(1, 101); // 1 ~ 100
            int id;
            switch (rand)
            {
                case int n when n <= 50: // Normal 50%
                    Debug.Log("Enemy_Normal");
                    id = Random.Range(0, 3); // 0 ~ 2
                    break;

                case int n when n <= 75: // Rare 25%
                    Debug.Log("Enemy_Rare");
                    id = Random.Range(3, 6); // 3 ~ 5
                    break;

                case int n when n <= 90: // Epic 15%
                    Debug.Log("Enemy_Epic");
                    id = Random.Range(6, 9); // 6 ~ 8
                    break;

                case int n when n <= 97: // Legendary 7%
                    Debug.Log("Enemy_Legendary");
                    id = Random.Range(9, 12); // 9 ~ 11
                    break;

                default:                 // Mystic 3%
                    Debug.Log("Enemy_Mystic");
                    id = Random.Range(12, 15); // 12 ~ 14
                    break;
            }
            Spawn(id);
        }

        // 보스 스폰
        timerBoss += Time.deltaTime;
        if (timerBoss > bossSpawnTime)
        {
            timerBoss = 0f;
            int id = Random.Range(15, 18); // 15 ~ 17
            SpawnBoss(id);
        }
    }

    private void Spawn(int id)
    {
        GameObject enemy = GameController.Instance.enemyPool.Get(id);
        enemy.transform.GetChild(0).position = spawnPoint.position;
    }

    private void SpawnBoss(int id)
    {
        GameObject enemy = GameController.Instance.enemyPool.Get(id);
        enemy.transform.GetChild(0).position = spawnPoint.position;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Warning);
    }
}
