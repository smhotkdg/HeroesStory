using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class ExpeditionItem : MonoBehaviour
{
    public Image GetItem_1;
    public Image GetItem_2;
    public Text GetItemText_1;
    public Text GetItemText_2;

    public Text ExpeditionText;
    public GameObject ExpedtionButton;
    public GameObject GemCompleteButton;
    public Button GemCompleteButton_object;
    public GameObject CompleteButton;

    public Text GemText;

    public GameObject LockObject;
    public GameObject NormalObject;
    public Animator BGAnim;
    public Image HeroImage;

    public Text BuyCost;
    public Image BuyImage;
    public Button BuyButton;
    int heroindex = -1;
    InfiniCoin Cost;

    public delegate void buyPosition();
    public event buyPosition buyPositionEvent;


    public delegate void selectHero(int iheroIndex);
    public event selectHero selectHeroEvent;

    public delegate void GetSometingEvent(int iheroIndex,int exPos);
    public event GetSometingEvent GetSometingEventHandler;


    public delegate void GemCompleteEvent(int ipos,int gemcount);
    public event GemCompleteEvent GemCompleteEventHandler;

    void setData()
    {

    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {        
        CheckData();
    }
    public void BuyExpPos()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (heroindex != 6 && heroindex != 7)
        {
            if (GameManager.Instance.TotalGold >= Cost)
            {
                GameManager.Instance.TotalGold -= Cost;
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
                UiManager.Instance.SetGoldText();

                GameManager.Instance.heroExpeditionPos[heroindex] = -1;
                GameManager.Instance.Save(GameManager.saveType.heroExpeditionPos);
                CheckData();
                buyPositionEvent?.Invoke();
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gold);
            }
        }
        else
        {
            if (GameManager.Instance.TotalGem >= Cost)
            {
                GameManager.Instance.TotalGem -= Cost;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                UiManager.Instance.SetGemText();

                GameManager.Instance.heroExpeditionPos[heroindex] = -1;
                GameManager.Instance.Save(GameManager.saveType.heroExpeditionPos);
                CheckData();
                buyPositionEvent?.Invoke();
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
            }
        }
       
    }
    void Test()
    {
        
    }
    void initData()
    {
        heroindex = int.Parse(this.name);
        if(heroindex==55)
        {
            return;
        }
        Cost = GameManager.Instance.GetExpCost(heroindex);
        switch (heroindex)
        {

            case 0:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.gold);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.gold);

                GetItemText_1.text = "골드";
                GetItemText_2.text = "골드";
                break;
            case 1:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.nBox);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.nBox);

                GetItemText_1.text = "상자";
                GetItemText_2.text = "상자";
                break;
            case 2:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.gem);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.gem);

                GetItemText_1.text = "보석";
                GetItemText_2.text = "보석";
                break;
            case 3:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.altar);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.altar);

                GetItemText_1.text = "영혼의 조각";
                GetItemText_2.text = "영혼의 조각";
                break;
            case 4:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.mat);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.mat);

                GetItemText_1.text = "재료";
                GetItemText_2.text = "재료";
                break;
            case 5:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.soul);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.soul);

                GetItemText_1.text = "번개";
                GetItemText_2.text = "번개";
                break;
            case 6:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.gem);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.gem);

                GetItemText_1.text = "보석";
                GetItemText_2.text = "보석";
                break;
            case 7:
                UiManager.Instance.SetIcon(GetItem_1, UiManager.iconType.gold);
                UiManager.Instance.SetIcon(GetItem_2, UiManager.iconType.gold);

                GetItemText_1.text = "골드";
                GetItemText_2.text = "골드";
                break;
            case 8:
                break;
        }

    }
    void SetEnable()
    {
        ExpeditionText.text = "탐험 대기중....";
        CompleteButton.gameObject.SetActive(false);
        GemCompleteButton.gameObject.SetActive(false);
        ExpedtionButton.gameObject.SetActive(true);
        HeroImage.gameObject.SetActive(false);
        BGAnim.Play("idle");
        NormalObject.SetActive(true);
        LockObject.SetActive(false);
    }
    public void SelectHero()
    {
        selectHeroEvent?.Invoke(heroindex);
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    void SetDisable()
    {
        NormalObject.SetActive(false);
        LockObject.SetActive(true);
        BuyCost.text = UiManager.Instance.SetCost(Cost);
        if (heroindex != 6 && heroindex != 7)
        {
            UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.gold);
        }
        else
        {
            UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.gem);
        }
        if(heroindex -1 >= 0)
        {
            if(GameManager.Instance.heroExpeditionPos[heroindex-1] >-2)
            {
                BuyButton.gameObject.SetActive(true);
            }
            else
            {
                BuyButton.gameObject.SetActive(false);
            }
        }
        else
        {
            BuyButton.gameObject.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if(LockObject.activeSelf ==true)
        {
            if (heroindex != 6 && heroindex != 7)
            {
                if (GameManager.Instance.TotalGold >= Cost)
                {
                    BuyButton.image.color = UiManager.Instance.enableButtonColor;
                    BuyCost.color = UiManager.Instance.enableButtonColor;
                }
                else
                {
                    BuyButton.image.color = UiManager.Instance.disableButtonColor;
                    BuyCost.color = UiManager.Instance.disableButtonColor;
                }
            }
            else
            {
                if (GameManager.Instance.TotalGem >= Cost)
                {
                    BuyButton.image.color = UiManager.Instance.enableButtonColor;
                    BuyCost.color = UiManager.Instance.enableButtonColor;
                }
                else
                {
                    BuyButton.image.color = UiManager.Instance.disableButtonColor;
                    BuyCost.color = UiManager.Instance.disableButtonColor;
                }
            }           
        }
        
        
        if (GameManager.Instance.timerCotroller.ExpedtionTime[heroindex] <=0)
        {
            MissionComplete();
        }
        if(GameManager.Instance.timerCotroller.ExpedtionTime[heroindex]>0 && GameManager.Instance.timerCotroller.bExpedtionTime[heroindex]==true &&
            GameManager.Instance.heroExpeditionComplete[heroindex]<0)
        {
            timestr = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.ExpedtionTime[heroindex]);
            ExpeditionText.text = "탐험 중....[ " + timestr + " ]";
            float Gemf = GameManager.Instance.timerCotroller.ExpedtionTime[heroindex] / 60;
            gemCount = (int)(Gemf);
            if(gemCount <=0)
            {
                gemCount = 1;
            }
            GemText.text = gemCount.ToString("N0");

            if (GameManager.Instance.TotalGem >= gemCount)
            {
                GemCompleteButton_object.image.color = UiManager.Instance.enableButtonColor;
                GemText.color = UiManager.Instance.enableButtonColor;
            }
            else
            {
                GemCompleteButton_object.image.color = UiManager.Instance.disableButtonColor;
                GemText.color = UiManager.Instance.disableButtonColor;
            }
        }
    }
    int gemCount;
    public void GetSometing()
    {
        GetSometingEventHandler?.Invoke(heroindex,int.Parse(this.name));
        
        SetEnable();
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.ExpedtionComplete);
        GameManager.Instance.achivementData.AchivementCount[2]++;
        GameManager.Instance.Save(GameManager.saveType.achivementData);
    }
    public void MissionComplete()
    {
        GameManager.Instance.timerCotroller.bExpedtionTime[heroindex] = false;
        GameManager.Instance.timerCotroller.ExpedtionTime[heroindex] = GameManager.Instance.defaultExpeditionTime;        
        ExpeditionText.text = "탐험 완료 !!";        
        CompleteButton.SetActive(true);
        GemCompleteButton.gameObject.SetActive(false);
        HeroImage.gameObject.GetComponent<Animator>().Play("idle");
        GameManager.Instance.heroExpeditionComplete[heroindex] = GameManager.Instance.heroExpeditionPos[heroindex];
        GameManager.Instance.heroExpeditionPos[heroindex] = -1;
        GameManager.Instance.Save(GameManager.saveType.heroExpeditionPos);
        GameManager.Instance.Save(GameManager.saveType.heroExpeditionComplete);
        BGAnim.Play("idle");
    }
    void InitComplete()
    {
        ExpedtionButton.SetActive(false);
        GemCompleteButton.gameObject.SetActive(false);
        CompleteButton.SetActive(true);
        ExpeditionText.text = "탐험 완료 !!";
        HeroImage.gameObject.SetActive(true);
        HeroImage.sprite = GameManager.Instance.HeroList[GameManager.Instance.heroExpeditionComplete[heroindex]].GetComponent<SpriteRenderer>().sprite;
        if (HeroImage.GetComponent<Animator>() == null)
        {
            HeroImage.gameObject.AddComponent<Animator>();
        }

        Animator temp = GameManager.Instance.HeroList[GameManager.Instance.heroExpeditionComplete[heroindex]].GetComponent<Animator>();
        HeroImage.gameObject.GetComponent<Animator>().runtimeAnimatorController = temp.runtimeAnimatorController;
        HeroImage.gameObject.GetComponent<Animator>().Play("idle");
        BGAnim.Play("idle");
    }
    public void BuyCompleteGem()
    {
        if(GameManager.Instance.TotalGem >= gemCount)
        {
            GemCompleteEventHandler?.Invoke(int.Parse(this.name),gemCount);
            //GameManager.Instance.TotalGem -= gemCount;
            //UiManager.Instance.SetGemText();
            //MissionComplete();            
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
        }
    }
    string timestr;
    float deltaTime;
    void SetMoveHero()
    {
        //Debug.Log(heroindex);
        ExpeditionText.text = "탐험 중....[ " + timestr +" ]";        
        CompleteButton.gameObject.SetActive(false);
        GemCompleteButton.gameObject.SetActive(true);
        ExpedtionButton.gameObject.SetActive(false);
        HeroImage.gameObject.SetActive(true);        
        HeroImage.sprite = GameManager.Instance.HeroList[GameManager.Instance.heroExpeditionPos[heroindex]].GetComponent<SpriteRenderer>().sprite;        
        if(HeroImage.GetComponent<Animator>() ==null)
        {
            HeroImage.gameObject.AddComponent<Animator>();
        }        

        Animator temp = GameManager.Instance.HeroList[GameManager.Instance.heroExpeditionPos[heroindex]].GetComponent<Animator>();
        HeroImage.gameObject.GetComponent<Animator>().runtimeAnimatorController = temp.runtimeAnimatorController;
        float rand = Random.Range(0.9f, 1.2f);
        HeroImage.gameObject.GetComponent<Animator>().Play("move");
        HeroImage.gameObject.GetComponent<Animator>().speed = rand;
        BGAnim.Play("move");
        BGAnim.speed = rand;
        //StartCoroutine(AnimRoutine());


    }

 
    public void CheckData()
    {   
        if(heroindex ==-1)
        {
            initData();
        }    
        if (GameManager.Instance.heroExpeditionPos[heroindex] ==-2)
        {
            //구매 해야함
            SetDisable();
        }
        else if(GameManager.Instance.heroExpeditionPos[heroindex] ==-1)
        {
            //구매는 완료함 -> 원정대 아직 안보냄
            if(GameManager.Instance.heroExpeditionComplete[heroindex] <0)
            {
                SetEnable();
            }
            else
            {
                //완료후 아직 안받은 상태
                InitComplete();
            }
            
        }
        else
        {
            SetMoveHero();
            //원정대 진행중
        }
    }
}
