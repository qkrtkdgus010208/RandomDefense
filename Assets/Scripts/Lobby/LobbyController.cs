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
}
