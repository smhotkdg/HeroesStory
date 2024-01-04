using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
using System.Globalization;
public class ShopPanelController : MonoBehaviour
{
    public GameObject SoldOutObject;
    public List<Text> CostList;
    public BuyCompletePanel completePanel;
    public GameObject FreeNormalBoxObject;
    public GameObject FreeSpeacialBoxObject;

    public GameObject NormalBoxButton;
    public GameObject SpeacalBoxButton;

    public Text NormalTimeText;
    public Text SpeacialTimeText;

    public Text CoinText_1;
    public Text CoinText_2;
    public Text CoinText_3;

    InfiniCoin Coin_1;
    InfiniCoin Coin_2;
    InfiniCoin Coin_3;

    List<float> JaponCost = new List<float>();
    List<float> KoreanCost = new List<float>();
    List<float> USACost = new List<float>();
    List<float> EUCost = new List<float>();
    private void Start()
    {
#if UNITY_ANDROID
        KoreanCost.Add(1200);
        JaponCost.Add(100);
        USACost.Add(0.99f);
        EUCost.Add(1.09f);

        KoreanCost.Add(5900);
        JaponCost.Add(520);
        USACost.Add(4.99f);
        EUCost.Add(5.49f);

        KoreanCost.Add(12000);
        JaponCost.Add(960);
        USACost.Add(8.99f);
        EUCost.Add(9.99f);

        KoreanCost.Add(25000);
        JaponCost.Add(2800);
        USACost.Add(25.99f);
        EUCost.Add(27.99f);

        KoreanCost.Add(65000);
        JaponCost.Add(4720);
        USACost.Add(43.99f);
        EUCost.Add(47.99f);

        KoreanCost.Add(119000);
        JaponCost.Add(9600);
        USACost.Add(89.99f);
        EUCost.Add(99.99f);

        KoreanCost.Add(5900);
        JaponCost.Add(520);
        USACost.Add(4.99f);
        EUCost.Add(5.49f);
#endif
#if UNITY_IOS
           //IOS
        KoreanCost.Add(1200);
        JaponCost.Add(120);
        USACost.Add(0.99f);
        EUCost.Add(1.09f);

        KoreanCost.Add(5900);
        JaponCost.Add(610);
        USACost.Add(4.99f);
        EUCost.Add(5.49f);

        KoreanCost.Add(11000);
        JaponCost.Add(1100);
        USACost.Add(8.99f);
        EUCost.Add(9.99f);

        KoreanCost.Add(32000);
        JaponCost.Add(3180);
        USACost.Add(25.99f);
        EUCost.Add(28.99f);

        KoreanCost.Add(54000);
        JaponCost.Add(5380);
        USACost.Add(43.99f);
        EUCost.Add(48.99f);

        KoreanCost.Add(109000);
        JaponCost.Add(11000);
        USACost.Add(89.99f);
        EUCost.Add(99.99f);

        KoreanCost.Add(5900);
        JaponCost.Add(610);
        USACost.Add(4.99f);
        EUCost.Add(5.49f);
        //
#endif
        NumberFormatInfo numberFormat;        
        for (int i =0; i< CostList.Count; i++)
        {
            numberFormat = new CultureInfo("ko-KR", false).NumberFormat;
            CostList[i].text = KoreanCost[i].ToString("c", numberFormat);
        }
    }
    private void OnEnable()
    {
        SetCoinData();   
    }
    void SetCoinData()
    {
        InfiniCoin coin = GameManager.Instance.GetShopCoin();
        Coin_1 = coin * 4000*5;
        CoinText_1.text = UiManager.Instance.SetCost(Coin_1);

        Coin_2 = coin * 80000*5;
        CoinText_2.text = UiManager.Instance.SetCost(Coin_2);

        Coin_3 = coin * 300000*5;
        CoinText_3.text = UiManager.Instance.SetCost(Coin_3);
    }
    private void FixedUpdate()
    {
        CheckTimer();
        if(GameManager.Instance.Pack1 ==true)
        {
            SoldOutObject.SetActive(true);
        }
        else
        {
            SoldOutObject.SetActive(false);
        }
    }
    public void BuyGold(int index)
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        switch (index)
        {
            case 0:
                //광고
                if (GameManager.Instance.timerCotroller.bAdsGoldTime == true)
                {
                    UiManager.Instance.SetNotification("광고 시간이 남았습니다.");
                    return;
                }
                GameManager.Instance.adsType = GameManager.AdsType.gold;
                GameManager.Instance.AdsGold = Coin_1;
                AdManager.Instance.ShowRewardedAds();
                break;
            case 1:
                if(GameManager.Instance.TotalGem >= 80)
                {
                    GameManager.Instance.TotalGem -= 80;
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                    UiManager.Instance.SetGemText();
                    UiManager.Instance.SetBuyComplete(Coin_2, BuyCompletePanel.buyType.gold);
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
                }
                
                break;
            case 2:
                if (GameManager.Instance.TotalGem >= 200)
                {
                    GameManager.Instance.TotalGem -= 200;
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                    UiManager.Instance.SetGemText();
                    UiManager.Instance.SetBuyComplete(Coin_3, BuyCompletePanel.buyType.gold);
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
                }
                break;

        }

    }
    void CheckTimer()
    {
        
        if (GameManager.Instance.timerCotroller.bStart_normal ==false)
        {
            FreeNormalBoxObject.SetActive(true);
            NormalBoxButton.SetActive(false);
            NormalTimeText.text = "무료 획득가능";
        }
        else
        {
            FreeNormalBoxObject.SetActive(false);
            NormalBoxButton.SetActive(true);
            NormalTimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.NormalBoxTime) + " 후 무료 획득";
        }

        if (GameManager.Instance.timerCotroller.bStart_Speacial == false)
        {
            SpeacialTimeText.text = "무료 획득가능";
            FreeSpeacialBoxObject.SetActive(true);
            SpeacalBoxButton.SetActive(false);
        }
        else
        {
            FreeSpeacialBoxObject.SetActive(false);
            SpeacalBoxButton.SetActive(true);
            SpeacialTimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.SpeacialBoxTime) + " 후 무료 획득";
        }
    }
    public void GetNormalAds()
    {
        //광고 보고 하는거임
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GameManager.Instance.adsType = GameManager.AdsType.normalBox;
        AdManager.Instance.ShowRewardedAds();
        
    }
    public void GetFreeNormal()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        UiManager.Instance.GatchaPanel.SetActive(true);
        UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.tutorial;

    }
    public void GetSpeacialBox()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        GameManager.Instance.setSpecialBoxTime();
        UiManager.Instance.GatchaPanel.SetActive(true);
        UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
    }
    public void BuyNormalBox()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.TotalGem >= GameManager.Instance.NormalBoxCost)
        {
            GameManager.Instance.TotalGem -= GameManager.Instance.NormalBoxCost;
            GameManager.Instance.Save(GameManager.saveType.TotalGem);
            UiManager.Instance.SetGemText();

            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
        }
    }
    public void BuySpeacialBox()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.TotalGem >= GameManager.Instance.SpeacialBoxCost)
        {
            GameManager.Instance.TotalGem -= GameManager.Instance.SpeacialBoxCost;
            GameManager.Instance.Save(GameManager.saveType.TotalGem);
            UiManager.Instance.SetGemText();

            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
        }
    }
}
