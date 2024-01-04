using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class OfflineRewardController : MonoBehaviour
{
    public Text CostText;
    public GameObject AdsButton;
    public GameObject NormalButton;

    InfiniCoin m_value = new InfiniCoin();
    public void Set(InfiniCoin cost)
    {
        m_value = cost;
        AdsButton.SetActive(false);
        NormalButton.SetActive(false);
        CostText.text = UiManager.Instance.SetCost(cost);
        StartCoroutine(ButtonRoutine());
    }
    private void OnDisable()
    {
        if(GameManager.Instance !=null)
            GameManager.Instance.CheckCollection();
    }
    IEnumerator ButtonRoutine()
    {
        AdsButton.SetActive(true);        
        yield return new WaitForSeconds(1f);
        NormalButton.SetActive(true);
    }
    public void Ok()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GameManager.Instance.TotalGold += m_value;
        GameManager.Instance.Save(GameManager.saveType.TotalGold);
        UiManager.Instance.SetGoldText();
        
    }
    public void ShowAd()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GameManager.Instance.adsType = GameManager.AdsType.offlinereward;
        GameManager.Instance.m_OfflineRewardCost = m_value;
        AdManager.Instance.ShowRewardedAds();
    }
}
