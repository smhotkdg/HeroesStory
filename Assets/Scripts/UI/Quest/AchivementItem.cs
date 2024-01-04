using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchivementItem : MonoBehaviour
{
    public Text TitleText;
    public Button GetButton;
    public Text GetImageText;
    public enum achivementType
    {
        levelup,
        gatcha,
        expedition,        
        altar,
        raid,
        fame,
        ads,
        time,
        stage,
        craft,
        altar_Levelup
    };
    public int defaultValue;
    public achivementType AchivementType = achivementType.altar;
    private void OnEnable()
    {
        CheckAhicvement();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.achivementData.AchivementCount[(int)AchivementType] >= GameManager.Instance.achivementData.AchivementMax[(int)AchivementType])
        {
            GetButton.image.color = UiManager.Instance.enableButtonColor;
            GetImageText.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            GetButton.image.color = UiManager.Instance.disableButtonColor;
            GetImageText.color = UiManager.Instance.disableButtonColor;
        }
        if(AchivementType == achivementType.time)
        {
            TitleText.text = "접속 시간\n<color=red> (" + GameManager.Instance.getTime((float)GameManager.Instance.achivementData.AchivementCount[7]) + " / " +
                GameManager.Instance.getTime((float)GameManager.Instance.achivementData.AchivementMax[7]) + ")</color>";
        }
    }
    public void GetReward()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.achivementData.AchivementCount[(int)AchivementType] >= GameManager.Instance.achivementData.AchivementMax[(int)AchivementType])
        {
            //GetSometing
            if(AchivementType == achivementType.levelup)
            {
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
            }
            else if(AchivementType == achivementType.altar_Levelup)
            {              
                
                GameManager.Instance.SetMatEffect(5, GetButton.transform);
                GameManager.Instance.materialCount += 5;
                GameManager.Instance.Save(GameManager.saveType.materialCount);
                
            }
            else
            {
                GameManager.Instance.SetGemEffect(5, GetButton.transform);
                GameManager.Instance.TotalGem += 5;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                UiManager.Instance.SetGemText();                
            }

            GameManager.Instance.achivementData.AchivementLevel[(int)AchivementType]++;
            GameManager.Instance.CheckAchivement();
            GameManager.Instance.Save(GameManager.saveType.achivementData);
            CheckAhicvement();

        }
        else
        {
            UiManager.Instance.SetNotification("아직 달성하지 못했습니다.");
        }
        
    }
    void CheckAhicvement()
    {
        switch(AchivementType)
        {
            case achivementType.levelup:
                TitleText.text = "히어로 레벨업\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[0]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[0]) + ")</color>";
                break;
            case achivementType.gatcha:
                TitleText.text = "히어로 뽑기\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[1]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[1]) + ")</color>";
                break;
            case achivementType.expedition:
                TitleText.text = "원정대 보상 획득\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[2]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[2]) + ")</color>";
                break;
            case achivementType.altar:
                TitleText.text = "재물 등록\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[3]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[3]) + ")</color>";
                break;
            case achivementType.raid:
                TitleText.text = "레이드 보스사냥\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[4]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[4]) + ")</color>";
                break;
            case achivementType.fame:
                TitleText.text = "영웅 각성\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[5]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[5]) + ")</color>";
                break;
            case achivementType.ads:
                TitleText.text = "광고 시청\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[6]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[6]) + ")</color>";
                break;
            case achivementType.time:
                TitleText.text = "접속 시간\n<color=red> (" + GameManager.Instance.getTime((float)GameManager.Instance.achivementData.AchivementCount[7]) + " / " +
                    GameManager.Instance.getTime((float)GameManager.Instance.achivementData.AchivementMax[7]) + ")</color>";
                break;
            case achivementType.stage:
                TitleText.text = "스테이지 클리어\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[8]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[8]) + ")</color>";
                break;
            case achivementType.craft:
                TitleText.text = "아이템 제작\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[9]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[9]) + ")</color>";
                break;
            case achivementType.altar_Levelup:
                TitleText.text = "제단 레벨업\n<color=red> (" + UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementCount[10]) + " / " +
                    UiManager.Instance.SetCost(GameManager.Instance.achivementData.AchivementMax[10]) + ")</color>";
                break;
        }
    }
}
