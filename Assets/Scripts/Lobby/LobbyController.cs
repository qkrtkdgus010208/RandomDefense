using BackEnd;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
    }

    private void Start()
    {
        BackendGameData.Instance.GameDataLoad();
    }

    public void OnClickGame()
    {
        Utils.LoadScene(SceneNames.Game);
    }

    public void OnClickLogout()
    {
        var bro = Backend.BMember.Logout();

        if (bro.IsSuccess())
        {
            Debug.Log("로그아웃 성공");
        }
        else
        {
            Debug.Log("로그아웃 실패");
        }

        OnClickQuit();
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
