using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public void OnClickGame()
    {
        Utils.LoadScene(SceneNames.Game);
    }
}
