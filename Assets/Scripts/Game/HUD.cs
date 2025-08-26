using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Gold, Kill, Time, MyHP, YourHP }
    public InfoType type;

    TextMeshProUGUI myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Gold:
                myText.text = $"{GameController.Instance.gold:F0}";
                break;

            case InfoType.Kill:
                myText.text = $"{GameController.Instance.kill:F0}";
                break;

            case InfoType.Time:
                float remainTime = GameController.Instance.maxGameTime - GameController.Instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = $"{min:D2}:{sec:D2}";
                break;
        }
    }
}
