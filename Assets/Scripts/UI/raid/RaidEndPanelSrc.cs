using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class RaidEndPanelSrc : MonoBehaviour
{
    public GameObject GetEffect;
    public GameObject GoldObejct;
    public GameObject BoxObject;
    public GameObject AltarObejct;
    public GameObject RewardObject;
    public GameObject OkButton;
    public GameObject AdsButton;
    private void OnDisable()
    {
        if (BoxObject.activeSelf == true)
        {
            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
        }
        if(GoldObejct.activeSelf ==true)
        {
            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
        }
        GoldObejct.SetActive(false);
        BoxObject.SetActive(false);
        AltarObejct.SetActive(false);
        RewardObject.SetActive(true);
    }
    bool bGood = false;
    InfiniCoin RewardCount = new InfiniCoin();
    int TotalstageCount;
    InfiniCoin TotalGetGold;
    float m_buff=0;
    public void ShowReward(int stageCount,float buff,bool isMax)
    {
        TotalstageCount = stageCount;
        m_buff = buff;
        bGood = false;
        if (isMax == true)
        {            
            if(stageCount ==0)
            {
                //max stage 도전 실패
                //Debug.Log("맥스 도전 실패");
            }
            else
            {
                //max stage 도전 성공 // 리워드 좋게줌
                //Debug.Log("맥스 도전 성공");
                bGood = true;
                RewardCount = (stageCount * 10) +((stageCount * 10) *buff);
            }
        }
        else
        {
            if(stageCount ==0)
            {
                //하위 스테이지에서 못깸
                //Debug.Log("하위 스테이지 못깸");
            }
            else
            {
                //하위 스테이지 몇개 깸
                //Debug.Log("하위 스테이지 몇개 깸");
            }
        }        

    }
    private void OnEnable()
    {
        OkButton.SetActive(false);
        AdsButton.SetActive(false);
    
    }
    public void GetReward()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GetEffect.SetActive(true);
        StartCoroutine(GetRewardRutine());
    }
    public void GetAdsReward()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GameManager.Instance.adsType = GameManager.AdsType.altar;
        AdManager.Instance.ShowRewardedAds();
    }
    IEnumerator GetRewardRutine()
    {        
        
        RewardObject.SetActive(false);
        if(bGood ==true)
        {
            AltarObejct.SetActive(true);
            AltarObejct.transform.Find("CostText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(RewardCount);
            GameManager.Instance.TotalAltarCoin += RewardCount;
            GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
            GameManager.Instance.adAltarCount = RewardCount;
        }
        else
        {
            GameManager.GetRaidItemType getRaidItemType =  GameManager.Instance.GetrandomRaidItem();
            if(TotalstageCount ==0)
            {
                TotalstageCount = 1;
            }
            InfiniCoin RewardCost = new InfiniCoin();
            
            switch (getRaidItemType)
            {
                case GameManager.GetRaidItemType.Box1:
                    GoldObejct.SetActive(true);
                    break;

                case GameManager.GetRaidItemType.Box2:
                    BoxObject.SetActive(true);
                    break;
                case GameManager.GetRaidItemType.Altar:
                    AltarObejct.SetActive(true);

                    int power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        int AltarCount = 5 + (5 * power);
                        AltarObejct.transform.Find("CostText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(AltarCount);
                        GameManager.Instance.TotalAltarCoin += AltarCount;
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                        GameManager.Instance.adAltarCount = AltarCount;
                    }
                    else
                    {
                        AltarObejct.transform.Find("CostText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(5);
                        GameManager.Instance.TotalAltarCoin += 5;
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                        GameManager.Instance.adAltarCount = 5;
                    }
                
                    break;
            }

        }
        Vector3 buttonPos = OkButton.transform.localPosition;
        if (AltarObejct.activeSelf ==true)
        {
            AdsButton.SetActive(true);
            yield return new WaitForSeconds(1f);
            buttonPos.x = 110;
            OkButton.transform.localPosition = buttonPos;
            OkButton.SetActive(true);
        }
        else
        {
            buttonPos.x = 0;
            OkButton.transform.localPosition = buttonPos;
            OkButton.SetActive(true);
        }
        
    }
    
}
