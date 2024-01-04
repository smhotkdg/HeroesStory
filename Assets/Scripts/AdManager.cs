using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
public class AdManager : MonoBehaviour
{
    private static AdManager _instance = null;
    AudienceNetworkClientImpl fbadClient;    
    UnityAdsClientImpl unityClinet;
    public static AdManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton AdManager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
        //DontDestroyOnLoad(gameObject);
    }



    void Start()
    {
        fbadClient = Advertising.AudienceNetworkClient;

        fbadClient.RewardedAdCompleted += FbanClient_RewardedAdCompleted;
        fbadClient.RewardedAdSkipped += FbadClient_RewardedAdSkipped;
        fbadClient.RewardedVideoAdDidClose += FbadClient_RewardedVideoAdDidClose;


        unityClinet = Advertising.UnityAdsClient;

        unityClinet.RewardedAdCompleted += UnityClinet_RewardedAdCompleted;
        unityClinet.RewardedAdSkipped += UnityClinet_RewardedAdSkipped;
        
    }

    private void UnityClinet_RewardedAdSkipped(IAdClient arg1, AdPlacement arg2)
    {
#if UNITY_IOS
        Time.timeScale = 1;
        AudioListener.pause = false;
#endif
    }

    private void UnityClinet_RewardedAdCompleted(IAdClient arg1, AdPlacement arg2)
    {
        GameManager.Instance.RewardAds();
#if UNITY_IOS
        Time.timeScale = 1;
        AudioListener.pause = false;
#endif
    }


    private void FbadClient_RewardedVideoAdDidClose()
    {
#if UNITY_IOS
        Time.timeScale = 1;
        AudioListener.pause = false;
#endif
    }

    private void FbadClient_RewardedAdSkipped(IAdClient arg1, AdPlacement arg2)
    {
#if UNITY_IOS
        Time.timeScale = 1;
        AudioListener.pause = false;
#endif

    }


    private void FbanClient_RewardedAdCompleted(IAdClient arg1, AdPlacement arg2)
    {
        //FireBaseRewardComplete
        GameManager.Instance.RewardAds();
#if UNITY_IOS
        Time.timeScale = 1;
        AudioListener.pause = false;
#endif
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (fbadClient != null)
        {
            if (fbadClient.IsRewardedAdReady())
            {
                //Debug.Log("페북 로딩완료");
            }
            else
            {

            }
        }
        else
        {
            //Debug.Log("null fb");
        }
        if (unityClinet != null)
        {
            if (unityClinet.IsRewardedAdReady())
            {
                //Debug.Log("유니티 로딩완료");
            }
            else
            {

            }
        }
        else
        {
            //Debug.Log("null unityClinet");
        }

    }
    public void ShowAttackBuff()
    {
        GameManager.Instance.adsType = GameManager.AdsType.powerBuff;
        ShowRewardedAds();
    }
    public void FreeAttackBuff()
    {
        GameManager.Instance.adsType = GameManager.AdsType.powerBuff;
        GameManager.Instance.RewardAds();
    }
    public void BuyTicket()
    {
        if (GameManager.Instance.timerCotroller.bAdsTicketTime == true)
        {
            UiManager.Instance.SetNotification("광고 시간이 남았습니다.");
            return;
        }
        GameManager.Instance.adsType = GameManager.AdsType.ticket;
        ShowRewardedAds();
    }
    public void ShowRewardedAds()
    {      
        if(GameManager.Instance.adsType== GameManager.AdsType.non)
        {
            return;
        }


#if UNITY_EDITOR
        GameManager.Instance.RewardAds();
#endif
        if (fbadClient.IsRewardedAdReady())
        {
            fbadClient.ShowRewardedAd();
#if UNITY_IOS
            //Time.timeScale = 0;
            AudioListener.pause = true;
#endif
        }
        else
        {
            if (unityClinet.IsRewardedAdReady())
            {
                unityClinet.ShowRewardedAd();
#if UNITY_IOS
                //Time.timeScale = 0;
                AudioListener.pause = true;
#endif
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.ads);

            }
        }

#if !UNITY_EDITOR
     
#endif
    }
}
