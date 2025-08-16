using UnityEngine;

public class InitScenario : MonoBehaviour
{
    [SerializeField]
    private Progress progress;

    private void Awake()
    {
        SystemSetup();
    }

    private void SystemSetup()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 로딩 애니메이션 시작, 재생 완료시 OnAfterProgress() 메소드 실행
        progress.Play(OnAfterProgress);
    }

    private void OnAfterProgress()
    {
        GameManager.Instance.TryAutoLogin();
    }

    public void OnClickLobby()
    {
        Utils.LoadScene(SceneNames.Lobby);
    }
}
