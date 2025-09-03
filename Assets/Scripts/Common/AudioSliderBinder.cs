using UnityEngine;
using UnityEngine.UI;

public class AudioSliderBinder : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void OnEnable()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(OnBgmChanged);
            bgmSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("VOL_BGM", 0.03f));
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(OnSfxChanged);
            sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("VOL_SFX", 0.1f));
        }
    }

    private void OnDisable()
    {
        if (bgmSlider != null) bgmSlider.onValueChanged.RemoveListener(OnBgmChanged);
        if (sfxSlider != null) sfxSlider.onValueChanged.RemoveListener(OnSfxChanged);
    }

    private void OnBgmChanged(float v)
    {
        if (AudioManager.Instance != null) AudioManager.Instance.SetBgmVolume(v);
    }

    private void OnSfxChanged(float v)
    {
        if (AudioManager.Instance != null) AudioManager.Instance.SetSfxVolume(v);
    }
}
