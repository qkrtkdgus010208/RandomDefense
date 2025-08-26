using BackEnd;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Effect1 : MonoBehaviour
{
    public static GameManager_Effect1 Instance { get; private set; }

    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject login;
    [SerializeField]
    private GameObject titleStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 뒤끝 자동 로그인 유무 판단
    public void TryAutoLogin()
    {
        if (!PlayerPrefs.HasKey("AutoLogin"))
            PlayerPrefs.SetInt("AutoLogin", 0);

        bool isAuto = PlayerPrefs.GetInt("AutoLogin") == 1;

        var bro = Backend.BMember.LoginWithTheBackendToken();

        if (bro.IsSuccess() && isAuto)
        {
            Debug.Log("[Backend] 토큰 자동 로그인 성공");
            title.SetActive(false);
            titleStart.SetActive(true);
        }
        else
        {
            Debug.Log("[Backend] 토큰 자동 로그인 실패");
            title.SetActive(false);
            login.SetActive(true);
        }
    }
}
