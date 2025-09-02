using BackEnd;
using System.Collections;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[DefaultExecutionOrder(-1000)]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("# Game Control")]
    public bool isPlay;
    public float gameTime;
    public float maxGameTime = 180f;

    [Header("# Game Info")]
    public int gameId; // 난이도(Easy:0, Normal:1, Hard:2, PvP:3)
    public int gold;
    public int coin;
    public TextMeshProUGUI requireGold;
    public int kill;

    [Header("# Gameobject")]
    public PoolManager heroPool;
    public PoolManager enemyPool;
    public PoolManager projectilePool;
    public PoolManager damagePopupPool;
    public Result gameResult;
    public GameObject overlayBackground;
    public GameObject buttonGroup;
    public UpgradeController upgradeController;

    [SerializeField]
    private RankRegister rank;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        if (!isPlay)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameOver();
        }
    }

    public void GameStart()
    {
        gold = 20000;
        coin = 10000;
        requireGold.text = "50";
        kill = 0;

        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isPlay = false;  

        yield return new WaitForSeconds(0.5f);

        overlayBackground.SetActive(true);
        buttonGroup.SetActive(false);
        gameResult.gameObject.SetActive(true);
        gameResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isPlay = false;

        // 현재 잔여 시간을 바탕으로 랭킹 데이터 갱신
        float remainTime = GameController.Instance.maxGameTime - GameController.Instance.gameTime;
        rank.Process((int)remainTime);

        yield return new WaitForSeconds(0.5f);

        overlayBackground.SetActive(true);
        buttonGroup.SetActive(false);
        gameResult.gameObject.SetActive(true);
        gameResult.Win();
        Stop();
    }

    public void OnClickLobby()
    {
        Utils.LoadScene(SceneNames.Lobby);
    }

    public void Stop()
    {
        isPlay = false;
        Time.timeScale = 0;
    }

    public void Exit()
    {
        Utils.LoadScene("Lobby");
    }

    public void Resume()
    {
        isPlay = true;
        Time.timeScale = 1;
    }
}
