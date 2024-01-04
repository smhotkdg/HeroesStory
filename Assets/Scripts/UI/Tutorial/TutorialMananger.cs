using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMananger : MonoBehaviour
{

    public GameObject CompleteTutorialPanel;
    public GameObject TutorialCanvas;


    /// <summary>
    /// 1번 튜토리얼
    /// </summary>
    public GameObject Tutorial_Click;

    /// 2번 튜토리얼
    public GameObject Tutorial_UpgradeClick_1;
    public GameObject Tutorial_UpgradeClick_2;
    
    //3번 튜토리얼
    public GameObject Tutorial_buff_1;
    public GameObject Tutorial_buff_2;

    //4번 튜토리얼
    public GameObject Tutorial_shop_1;
    public GameObject Tutorial_shop_2;
    public GameObject Tutorial_shop_3;
    public GameObject Tutorial_shop_4;

    //5번 튜토리얼
    public GameObject Tutorial_SelectHero_1;
    public GameObject Tutorial_SelectHero_2;
    public GameObject Tutorial_SelectHero_3;
    public GameObject Tutorial_SelectHero_4;

    //6번 튜토리얼
    public GameObject Tutorial_HeroLIst;

    //7번 튜토리얼
    public GameObject Tutorial_AutoMove;

    //8번 -> 원정대

    //9번 -> 레이드

    //10번 -> 재단

    //11번 -> 초월

    public GameObject TutorialRewardPanel;

    private void Start()
    {
        //튜토리얼이 필요한가?확인하고~
        bool bTutorial = false;
        for (int i = 0; i < 6; i++)
        {
            if(GameManager.Instance.TutorialList[i] ==0)
            {
                bTutorial = true;
            }
        }
        if(bTutorial == true)
        {
            TutorialRewardPanel.GetComponent<TutorialRewardSrc>().OnCompleteEventHandler += TutorialMananger_OnCompleteEventHandler;
            TutorialCanvas.SetActive(true);
            if (GameManager.Instance.TutorialList[0] ==0)
            {                
                Tutorial_Click.SetActive(true);
            }
            else if(GameManager.Instance.TutorialList[1] == 0)
            {
                Tutorial_UpgradeClick_1.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[2] == 0)
            {
                GameManager.Instance.TutorialList[2] = 1;
                GameManager.Instance.Save(GameManager.saveType.TutorialList);
                UiManager.Instance.CheckTurorialLock();
                Tutorial_shop_1.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[3] == 0)
            {
                Tutorial_shop_1.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[4] == 0)
            {
                Tutorial_SelectHero_1.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[5] == 0)
            {
                Tutorial_HeroLIst.SetActive(true);
            }
        }
        else
        {
            //튜토리얼이 필요없음
            TutorialCanvas.SetActive(false);
        }    
    }

    private void TutorialMananger_OnCompleteEventHandler()
    {
        if (GameManager.Instance.TutorialList[6] == 1)
        {
            CompleteTutorialPanel.SetActive(true);
        }
        bool bTutorial = false;
        for (int i = 0; i < 6; i++)
        {
            if (GameManager.Instance.TutorialList[i] == 0)
            {
                bTutorial = true;
            }
        }
        if (bTutorial == true)
        {            
            TutorialCanvas.SetActive(true);
            if (GameManager.Instance.TutorialList[0] == 0)
            {
                Tutorial_Click.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[1] == 0)
            {
                Tutorial_UpgradeClick_1.SetActive(true);
            }            
            else if(GameManager.Instance.TutorialList[2] ==0)
            {
                //Tutorial_buff_1.SetActive(true);
                GameManager.Instance.TutorialList[2] = 1;
                GameManager.Instance.Save(GameManager.saveType.TutorialList);
                UiManager.Instance.CheckBottomUI();
                ShopTutorial();
                
            }
            else if (GameManager.Instance.TutorialList[4] == 0)
            {
                Tutorial_SelectHero_1.SetActive(true);
            }
            else if (GameManager.Instance.TutorialList[5] == 0)
            {
                Tutorial_HeroLIst.SetActive(true);
            }
        }
    }
    public void EndHeroList()
    {
        Tutorial_HeroLIst.SetActive(false);
        GameManager.Instance.TutorialList[5] = 1;
        GameManager.Instance.Save(GameManager.saveType.TutorialList);
        TutorialRewardPanel.SetActive(true);
        TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(20, TutorialRewardSrc.ItemType.Gem);
        UiManager.Instance.CheckTurorialLock();
    }
    public void CheckAutoMoveTutorial()
    {
        if(GameManager.Instance.TutorialList[6]  ==0)
        {
            GameManager.Instance.TutorialList[6] = 1;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            Tutorial_AutoMove.SetActive(true);
            UiManager.Instance.CheckTurorialLock(true);
        }
    }
    public void EndAutoMoveTutorial()
    {
        TutorialRewardPanel.SetActive(true);
        TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(20, TutorialRewardSrc.ItemType.Gem);
    }
    int ClickCount = 0;
    public void TutorialClick()
    {
        ClickCount++;
        if(ClickCount >10)
        {
            Tutorial_Click.SetActive(false);
            GameManager.Instance.TutorialList[0] = 1;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            TutorialRewardPanel.SetActive(true);
            TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(100, TutorialRewardSrc.ItemType.Gold);
            UiManager.Instance.CheckTurorialLock();
        }
    }
    public void UpgradeTutorial_1()
    {
        Tutorial_UpgradeClick_1.SetActive(false);
        Tutorial_UpgradeClick_2.SetActive(true);
        GameManager.Instance.TutorialList[1] = 1;
        GameManager.Instance.Save(GameManager.saveType.TutorialList);
        UiManager.Instance.CheckTurorialLock();
    }
    int UpgradeCount = 0;
    public void UpgradeTutorial_2()
    {        
        UpgradeCount++;        
        if (UpgradeCount>2)
        {
            Tutorial_UpgradeClick_2.SetActive(false);
            TutorialRewardPanel.SetActive(true);            
            TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(20, TutorialRewardSrc.ItemType.Gem);
        }        
    }

    public void BuffTutorial_1()
    {        
        Tutorial_buff_1.SetActive(false);
        Tutorial_buff_2.SetActive(true);
        GameManager.Instance.TutorialList[2] = 1;
        GameManager.Instance.Save(GameManager.saveType.TutorialList);
        UiManager.Instance.CheckTurorialLock();
    }
    public void BuffTutorial_2()
    {
        Tutorial_buff_2.SetActive(false);        
    }

    public void ShopTutorial()
    {
        if (GameManager.Instance.TutorialList[3] == 0)
        {
            Tutorial_shop_1.SetActive(true);
        }
    }
    public void ShopTutorial_1()
    {
        Tutorial_shop_1.SetActive(false);
        Tutorial_shop_2.SetActive(true);
    }
    public void ShopTutorial_2()
    {
        if (GameManager.Instance.TutorialList[3] == 0)
        {
            GameManager.Instance.TutorialList[3] = 1;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            UiManager.Instance.CheckTurorialLock();
            Tutorial_shop_2.SetActive(false);
            Tutorial_shop_3.SetActive(true);
        }
    }
    public void ShopTutorial_3()
    {
        Tutorial_shop_3.SetActive(false);
        Tutorial_shop_4.SetActive(true);
    }
    public void ShopTutorial_4()
    {
        Tutorial_shop_4.SetActive(false);
        TutorialRewardPanel.SetActive(true);

        TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(20, TutorialRewardSrc.ItemType.Gem);
    }
    public void EndTutorialSelect()
    {
        TutorialRewardPanel.SetActive(true);
        TutorialRewardPanel.GetComponent<TutorialRewardSrc>().SetValue(100, TutorialRewardSrc.ItemType.Gold);
    }
    public void StartPlaceTutorial()
    {
        GameManager.Instance.TutorialList[4] = 1;
        GameManager.Instance.Save(GameManager.saveType.TutorialList);
        UiManager.Instance.CheckTurorialLock();
    }
}
