using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubQuestController : MonoBehaviour
{
 

    public Text QuestText;
    public GameObject GetObject;
    public GameObject RewardPanel;
    
    private void Start()
    {
        CheckSubQuest();
    }
    void CheckSubQuest()
    {
        if (GameManager.Instance.subQuestIndex > 9)
        {
            GameManager.Instance.subQuestIndex = 0;
            GameManager.Instance.Save(GameManager.saveType.subQuestIndex);
            GameManager.Instance.subQuestLevel++;
            GameManager.Instance.Save(GameManager.saveType.subQuestLevel);
        }
        int subQuestCount = 0;
        switch (GameManager.Instance.subQuestIndex)
        {
            case 0:

                GameManager.Instance.subquestType = GameManager.SubQuestType.click;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 50;
                QuestText.text = "<color=red>터치 공격</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                
                
                break;
            case 1:
                GameManager.Instance.subquestType = GameManager.SubQuestType.clickLevelup;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 10;
                QuestText.text = "<color=red>터치 파워 레벨업</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                
                break;
            case 2:
                GameManager.Instance.subquestType = GameManager.SubQuestType.heroLevelup;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 20;
                QuestText.text = "<color=red>히어로 레벨업</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";

                break;
            case 3:
                GameManager.Instance.subquestType = GameManager.SubQuestType.gatchaHero;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel);
                QuestText.text = "<color=red>히어로 뽑기</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 4:

                GameManager.Instance.subquestType = GameManager.SubQuestType.stage;
                subQuestCount = GameManager.Instance.GetSubQuestMaxStage();
                QuestText.text = "<color=red>" + subQuestCount + " 스테이지 달성</color>";
                               
                break;
            case 5:
                GameManager.Instance.subquestType = GameManager.SubQuestType.expedtionGo;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel);
                QuestText.text = "<color=red>원정대 출정</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";                                
                break;

            case 6:
                GameManager.Instance.subquestType = GameManager.SubQuestType.heroGet;
                subQuestCount = (6);
                QuestText.text = "<color=red>히어로 수집 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                GameManager.Instance.CheckTutorial();
                break;

            case 7:
                GameManager.Instance.subquestType = GameManager.SubQuestType.raidBossKill;
                subQuestCount = (1);
                QuestText.text = "<color=red>레이드 보스 제압</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;       
            case 8:
                subQuestCount = (1);
                GameManager.Instance.subquestType = GameManager.SubQuestType.altarUpgrade;
                QuestText.text = "<color=red>재단 강화</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 9:
                GameManager.Instance.subquestType = GameManager.SubQuestType.aweaking;
                subQuestCount = 1;
                QuestText.text = "<color=red>히어로 각성</color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
        }

        if (GameManager.Instance.subquestType != GameManager.SubQuestType.stage)
        {
            if (GameManager.Instance.subQuestNow >= subQuestCount)
            {
                SetGetSometing();
            }
        }
    }

    public void SetText()
    {
        int subQuestCount = 0;
        switch (GameManager.Instance.subQuestIndex)
        {
            case 0:
                GameManager.Instance.subquestType = GameManager.SubQuestType.click;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 50;
                QuestText.text = "<color=red>터치 공격 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";                
                break;
            case 1:
                GameManager.Instance.subquestType = GameManager.SubQuestType.clickLevelup;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 10;
                QuestText.text = "<color=red>터치 파워 레벨업 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                
                break;
            case 2:
                GameManager.Instance.subquestType = GameManager.SubQuestType.heroLevelup;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel) * 20;
                QuestText.text = "<color=red>히어로 레벨업 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 3:
                GameManager.Instance.subquestType = GameManager.SubQuestType.gatchaHero;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel);
                QuestText.text = "<color=red>히어로 뽑기 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";                
                break;
            case 4:
                GameManager.Instance.subquestType = GameManager.SubQuestType.stage;
                subQuestCount = GameManager.Instance.GetSubQuestMaxStage();
                QuestText.text = "<color=red>" + subQuestCount + " 스테이지 달성</color>";                
                break;
            case 5:
                GameManager.Instance.subquestType = GameManager.SubQuestType.expedtionGo;
                subQuestCount = (1 + GameManager.Instance.subQuestLevel);
                QuestText.text = "<color=red>원정대 출정 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 6:
                GameManager.Instance.subquestType = GameManager.SubQuestType.heroGet;
                subQuestCount = (6);
                QuestText.text = "<color=red>히어로 수집 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";                
                break;
            case 7:
                GameManager.Instance.subquestType = GameManager.SubQuestType.raidBossKill;
                subQuestCount = (1);
                QuestText.text = "<color=red>레이드 보스 제압 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 8:
                subQuestCount = (1);
                GameManager.Instance.subquestType = GameManager.SubQuestType.altarUpgrade;
                QuestText.text = "<color=red>재단 강화 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
            case 9:
                GameManager.Instance.subquestType = GameManager.SubQuestType.aweaking;
                subQuestCount = 1;
                QuestText.text = "<color=red>히어로 각성 </color>" + GameManager.Instance.subQuestNow + " / " + subQuestCount + "번 달성";
                break;
        }
        if(GameManager.Instance.subquestType != GameManager.SubQuestType.stage)
        {
            if(GameManager.Instance.subQuestNow >= subQuestCount)
            {
                SetGetSometing();
            }
        }
    }
    void SetGetSometing()
    {
        GetObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance.subquestType == GameManager.SubQuestType.stage)
        {
            if(GameManager.Instance.MaxLevel >= GameManager.Instance.GetSubQuestMaxStage())
            {
                //여기서 최대층 깸
                SetGetSometing();
            }
        }
    }
    public void GetSomething()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GetObject.SetActive(false);
        RewardPanel.SetActive(true);
        switch (GameManager.Instance.subQuestIndex)
        {
            case 0:
                if (GameManager.Instance.MaxLevel_rank < 10)
                {
                    RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(300, TutorialRewardSrc.ItemType.Gold);
                }
                else
                {
                    RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(GameManager.Instance.GetGoldTick()* 4000, TutorialRewardSrc.ItemType.Gold);
                }
                
                break;
            case 1:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.Gem);
                break;
            case 2:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.altar);
                break;
            case 3:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(1, TutorialRewardSrc.ItemType.Box);                
                break;
            case 4:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(5, TutorialRewardSrc.ItemType.soul);
                break;
            case 5:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.Gem);                
                break;
            case 6:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.Gem);
                break;
            case 7:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.altar);
                break;
            case 8:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(1, TutorialRewardSrc.ItemType.Box);                
                break;
            case 9:
                RewardPanel.GetComponent<TutorialRewardSrc>().SetValue(10, TutorialRewardSrc.ItemType.altar);
                break;
        }
        if(GameManager.Instance.subQuestLevel ==0)
        {
            if(GameManager.Instance.subquestType == GameManager.SubQuestType.stage)
            {
                UiManager.Instance.NewContentEventPanel.SetActive(true);
                UiManager.Instance.NewContentEventPanel.GetComponent<NewContentEventSrc>().SetData("ex");
                GameManager.Instance.TutorialList[7] = 1;
                GameManager.Instance.Save(GameManager.saveType.TutorialList);
                UiManager.Instance.CheckTurorialLock();
            }          
            if(GameManager.Instance.subquestType == GameManager.SubQuestType.heroGet)
            {
                UiManager.Instance.NewContentEventPanel.SetActive(true);
                UiManager.Instance.NewContentEventPanel.GetComponent<NewContentEventSrc>().SetData("raid");
                GameManager.Instance.TutorialList[8] = 1;
                GameManager.Instance.Save(GameManager.saveType.TutorialList);
                UiManager.Instance.CheckTurorialLock();
            }  
        }
        GameManager.Instance.subQuestIndex++;
        GameManager.Instance.Save(GameManager.saveType.subQuestIndex);
        GameManager.Instance.subQuestNow = 0;
        GameManager.Instance.Save(GameManager.saveType.subQuestNow);
        CheckSubQuest();
        if(GameManager.Instance.subQuestLevel >= 2)
        {
            UiManager.Instance.SubQuestObject.SetActive(false);
        }
    }
}
