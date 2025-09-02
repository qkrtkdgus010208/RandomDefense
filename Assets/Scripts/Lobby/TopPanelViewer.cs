﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopPanelViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNickname;
    //[SerializeField]
    //private TextMeshProUGUI textLevel;
    //[SerializeField]
    //private Slider sliderExperience;
    //[SerializeField]
    //private TextMeshProUGUI textHeart;
    //[SerializeField]
    //private TextMeshProUGUI textJewel;
    //[SerializeField]
    //private TextMeshProUGUI textGold;

    private void Awake()
    {
        BackendGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);
    }

    public void UpdateNickname()
    {
        // 닉네임이 없으면 gamer_id를 출력하고, 닉네임이 있으면 닉네임 출력
        textNickname.text = UserInfo.Data.nickname == null ?
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;
    }

    public void UpdateGameData()
    {
        //int currentLevel = BackendGameData.Instance.UserGameData.level;

        //textLevel.text = currentLevel.ToString();
        //sliderExperience.value = BackendGameData.Instance.UserGameData.experience /
        //                              BackendChartData.levelChart[currentLevel - 1].maxExperience;
        //textHeart.text = $"{BackendGameData.Instance.UserGameData.heart} / 30";
        //textJewel.text = $"{BackendGameData.Instance.UserGameData.jewel}";
        //textGold.text = $"{BackendGameData.Instance.UserGameData.gold}";
    }
}

