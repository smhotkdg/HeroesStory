using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TranscendenceOpenPanelSrc : MonoBehaviour
{
    public TierLevelupPanelSrc tierLevelupPanelSrc;
    public GameObject TransTutorialObject;
    public GameObject BuyTicketObject;
    public Image ButtonImage;
    public Text TimeText;
    public GameObject Effect;
    public Text TierText;
    public Text TierMonsterCount;
    public Image FillMonsterCount;
    public Text TierXText;
    public GameObject XTier;
    public GameObject Fill;

    // Start is called before the first frame update
    void Start()
    {
        tierLevelupPanelSrc.OnDIsablePanelEventHandler += TierLevelupPanelSrc_OnDIsablePanelEventHandler;
    }

    private void TierLevelupPanelSrc_OnDIsablePanelEventHandler()
    {
        StartCoroutine(TierEffectRoutine());
    }

    private void OnEnable()
    {
        if(GameManager.Instance.transTutorial==false)
        {
            TransTutorialObject.SetActive(true);
            GameManager.Instance.transTutorial = true;
            GameManager.Instance.Save(GameManager.saveType.transTutorial);
        }
        SetTier();
    }
    public void SetTierEndGame()
    {
        if (GameManager.Instance.TransTier > 20)
            return;
        bool bUpgarde = false;
        while(true)
        {
            int tier = GameManager.Instance.TransTier + 1;
            int montserCount = tier * 10;
            if (GameManager.Instance.TransTierMonsterCount >= montserCount)
            {
                GameManager.Instance.TransTierMonsterCount -= montserCount;
                GameManager.Instance.TransTier++;
                GameManager.Instance.Save(GameManager.saveType.TransTier);
                GameManager.Instance.Save(GameManager.saveType.TransTierMonsterCount);
                bUpgarde = true;
            }
            else
            {
                break;
            }
        }
        if(bUpgarde ==true)
        {

            tierLevelupPanelSrc.gameObject.SetActive(true);
            tierLevelupPanelSrc.StartRoutine();
        }
        else
        {
            SetTier();
        }
    }
    
    IEnumerator TierEffectRoutine()
    {
        int tier = GameManager.Instance.TransTier + 1;
        int montserCount = tier * 10;
        

        yield return new WaitForSeconds(0.1f);                
        TierXText.text = "x " + tier;
        XTier.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBounce).From(false);
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.TransTier > 20)
        {
            TierText.text = "Tier. Max";
            TierMonsterCount.text = "Max";
            FillMonsterCount.fillAmount = 1;
        }
        else
        {
            TierText.text = "Tier. " + tier;
            TierMonsterCount.text = GameManager.Instance.TransTierMonsterCount + " / " + montserCount;
            FillMonsterCount.fillAmount = (float)(GameManager.Instance.TransTierMonsterCount) / (float)(montserCount);
        }

        
        Fill.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBounce).From(false);
    }
  
    void SetTier()
    {
        int tier = GameManager.Instance.TransTier + 1;
        if (GameManager.Instance.TransTier>20)
        {
            TierXText.text = "x " + tier;
            TierText.text = "Tier. Max";
            TierMonsterCount.text = "Max";
            FillMonsterCount.fillAmount = 1;
        }
        else
        {
            
            TierXText.text = "x " + tier;
            TierText.text = "Tier. " + tier;
            int montserCount = tier * 10;
            TierMonsterCount.text = GameManager.Instance.TransTierMonsterCount + " / " + montserCount;
            FillMonsterCount.fillAmount = (float)(GameManager.Instance.TransTierMonsterCount) / (float)(montserCount);
        }
   
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.timerCotroller.bTrasnTicketTime ==true)
        {
            TimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.TransTicketTime);
            //ButtonImage.color = UiManager.Instance.disableButtonColor;                
        }
        else
        {
            TimeText.text = "x 1";
            //ButtonImage.color = UiManager.Instance.enableButtonColor;
        }
    }
    public void StartDungeon()
    {
        if(GameManager.Instance.timerCotroller.bTrasnTicketTime ==false)
        {
            if(GameManager.Instance.heroTransPos ==-1)
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.NeedHero);
                return;
            }
            GameManager.Instance.timerCotroller.TransTicketTime = 3600 * 4;
            GameManager.Instance.timerCotroller.bTrasnTicketTime = true;            
            //this.gameObject.SetActive(false);
            Effect.SetActive(true);
            Effect.transform.Find("Image").gameObject.SetActive(true);
        }
        else
        {
            BuyTicketObject.SetActive(true);
        }
    }
    public void BuyTicket_Gem()
    {
        if(GameManager.Instance.TotalGem >= 200)
        {
            GameManager.Instance.TotalGem -= 200;
            GameManager.Instance.Save(GameManager.saveType.TotalGem);
            UiManager.Instance.SetGemText();

            GameManager.Instance.timerCotroller.bTrasnTicketTime = false;
            GameManager.Instance.timerCotroller.TransTicketTime = 0;

            BuyTicketObject.SetActive(false);
            UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.ticket, 1);

        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
        }
    }
}
