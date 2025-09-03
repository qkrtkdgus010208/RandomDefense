using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("# BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.03f;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;
    AudioListener audioListener;

    [Header("# SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.1f;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    private const string KEY_BGM = "VOL_BGM";
    private const string KEY_SFX = "VOL_SFX";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        // 저장된 값 불러오기
        bgmVolume = PlayerPrefs.GetFloat(KEY_BGM, 0.03f);
        sfxVolume = PlayerPrefs.GetFloat(KEY_SFX, 0.1f);

        // bgmPlayer 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
        audioListener = Camera.main.GetComponent<AudioListener>();

        // sfxPlayers 초기화
        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }

    public void EffectBgm(bool isPlay)
    {
        audioListener.enabled = false; // Unity의 DSP(Digital Signal Processing) 체인 업데이트 문제 때문에 AudioListener를 껐다 켜며 DSP 체인을 강제로 다시 계산
        bgmEffect.enabled = isPlay;
        audioListener.enabled = true;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void SetBgmVolume(float volume)
    {
        float v = Mathf.Clamp01(volume);
        bgmVolume = v;
        if (bgmPlayer != null) bgmPlayer.volume = v;
        PlayerPrefs.SetFloat(KEY_BGM, v);   // 저장
    }

    public void SetSfxVolume(float volume)
    {
        float v = Mathf.Clamp01(volume);
        sfxVolume = v;
        if (sfxPlayers == null) return;
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            if (sfxPlayers[i] != null) sfxPlayers[i].volume = v;
        }
        PlayerPrefs.SetFloat(KEY_SFX, v);   // 저장
    }

}
