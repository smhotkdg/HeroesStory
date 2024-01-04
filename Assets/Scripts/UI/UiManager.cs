using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
using System.IO;
public class UiManager : MonoBehaviour
{
    public GameObject DataSavePanel;
    public Text APILevelText;
    public GameObject AppQuitPanel;
    public GameObject CollectionObject;
    public Text CollectionText;
    public Text StageNameText;
    public GameObject TransDungeonCanvas;
    public GameObject BreakLimitPanelObject;
    public Text MatChanceText;
    public Text ScrollChanceText;
    public GameObject LockCraft;
    public GameObject InventoryObject;

    public GameObject StageClearPanel;
    public SubQuestController subQuestController;
    public GameObject RestoreObject;
    public Text ToogleLable;
    public GameObject BossStageStatusPanel;
    public GameObject BG_ShowMoveObject_1;
    public GameObject BG_ShowMoveObject_2;
    public GameObject BG_ShowMoveObject_3;

    public GameObject CompleteStagePanel;
    public List<GameObject> TutorialObjects;

    
    public GameObject SubQuestObject;
    public GameObject AchivementIcon;
    public GameObject RankObject;
    public GameObject NewContentEventPanel;
    public GameObject ConnectFail;
    public GameObject MailBox;
    public GameObject MapObject;
    public GameObject TicketAdsPanel;
    public GameObject OfflineRewardPanel;
    public GameObject bossEffectAnim;
    public GameObject BuffImage;
    public GameObject EnableBuffImage;
    public GameObject CollectionIcon;

    public Text EnableBuffText;

    public GameObject BuyCompletePanel;
    public GameObject InappProcess;
    public GameObject TransEffectObjectr;
    public GameObject AutoPlayImage;
    public GameObject RaidCanvas;
    public CanvasGroup MainCanvasGroup;
    public GameObject Notification;
    public Text NotificationText;
    public List<GameObject> BottomObjects;
    public GameObject GatchaPanel;
    public GameObject unlockSlotPanel;
    public SelectHeroPanelSrc HeroListPanel;
    public UpgradePanelSrc UpgradePanel;
    public Toggle toggleAutoPlay;
    public SelectHeroPanelSrc SelectHero;
    public HeroInfoPanelSrc heroInfoPanelSrc;
    public GameObject PrevObject;
    public GameObject NextObject;
    public Text GemText;
    public Text TimerText;
    public Text GoldText;
    public Text SoulText;
    public GameObject HpObject;
    public Image HpFillImage;
    public Text HpText;
    public Text DpsText;
    public Text ClickText;
    public Text LevelText;    
    public Text MonsterCountText;

    public TextPoolController raidTextPoolController;
    public TextPoolController textPoolController;
    public TextPoolController textPollControllerTrans;
    GameManager gameManager;
    //baseClicker baseClicker;
    public StageClearConroller clearConroller;

    public Color AttackColor;
    public Color32 normalColor = new Color32(255, 255, 255, 255);
    public Color32 advanceColor = new Color32(15, 210, 0, 255);
    public Color32 rareColor = new Color32(0, 120, 255, 255);
    public Color32 heroColor = new Color32(255, 0, 60, 255);
    public Color32 lgendColor = new Color32(255, 220, 0, 255);

    public Color EnableColor = new Color(1, 1, 1,1);
    public Color DisableColor = new Color(0, 0, 0, 1);

    public Color disableButtonColor;
    public Color enableButtonColor;

    public int MoveStage_count = 1;
    public void SetdataSavePanel(bool flag)
    {
        DataSavePanel.SetActive(flag);
    }
    public void setAPILevel(int level)
    {
        APILevelText.text = level.ToString();
    }
    public void Move_Stage()
    {
        int yPos = MoveStage_count * 4;
        //int yPos = MoveStage_count * 2;
        Vector3 newPos = new Vector3(0, -yPos, 0);
        BG_ShowMoveObject_1.transform.DOMove(newPos, 0.9f).SetEase(Ease.Linear);
        BG_ShowMoveObject_2.transform.DOMove(newPos, 0.9f).SetEase(Ease.Linear).OnComplete(EndObjectMove);
        //BG_ShowMoveObject_3.transform.DOMove(newPos, 0.7f).SetEase(Ease.Linear);
        MoveStage_count++;        
    }
    void EndObjectMove()
    {
        GameManager.Instance.enemyManager.SetAttackPointsList(true);
        GameManager.Instance.enemyManager.SetMakeEnemy();
        //GameManager.Instance.CheckMap();
        
    }
    public void SetInit_StageMove()
    {
        Vector3 newPos = new Vector3(0, 0, 0);
        BG_ShowMoveObject_1.transform.position = newPos;
        BG_ShowMoveObject_2.transform.position = newPos;
        //BG_ShowMoveObject_3.transform.position = newPos;
        MoveStage_count=1;
    }
    public void CheckTurorialLock(bool bSubQuest = false)
    {
        int index = 0;
        for (int i = 0; i < GameManager.Instance.TutorialList.Count; i++)
        {
            if (GameManager.Instance.TutorialList[i] == 0)
            {
                if (i > 0)
                {
                    if(i - 1 ==0 || i - 1 == 2 || i - 1 ==6 || i - 1==7|| i - 1==8|| i - 1==9)
                    {
                        TutorialObjects[i - 1].SetActive(true);
                    }
                    else
                    {
                        TutorialObjects[i - 1].SetActive(false);
                    }
                    
                }

            }
            else
            {
                if (i > 0)
                {
                    if (i - 1 == 0 || i - 1 == 2 || i - 1 == 6 || i - 1 == 7 || i - 1 == 8 || i - 1 == 9)
                    {
                        TutorialObjects[i - 1].SetActive(false);
                    }
                    else
                    {
                        TutorialObjects[i - 1].SetActive(true);
                    }
                    index++;
                }

            }
        }
        if (index >= 6)
        {
            AchivementIcon.SetActive(true);
            RankObject.SetActive(true);
            MapObject.SetActive(true);
            CollectionIcon.SetActive(true);
            if (GameManager.Instance.subQuestLevel >= 2)
            {
                SubQuestObject.SetActive(false);
            }
            else if(bSubQuest ==false)
            {
                SubQuestObject.SetActive(true);
            }
            

            CheckGift();
        }
        else
        {
            AchivementIcon.SetActive(false);
            RankObject.SetActive(false);
            SubQuestObject.SetActive(false);
            MapObject.SetActive(false);
            CollectionIcon.SetActive(false);
        }

        if(GameManager.Instance.IsNewSmith !=0)
        {
            LockCraft.SetActive(false);
            InventoryObject.SetActive(true);
        }
        else
        {
            LockCraft.SetActive(true);
            InventoryObject.SetActive(false);
        }
    }
    private static UiManager _instance = null;
    public static UiManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            else
            {
                return _instance;
            }
        }
    }
    public void SetStageName()
    {
        switch (GameManager.Instance.MapStageIndex)
        {
            case 1:
                StageNameText.text = "1. 슬라임의 숲";                
                break;
            case 2:
                StageNameText.text = "2. 달팽이 해변";                

                break;
            case 3:
                StageNameText.text = "3. 버려진 광산";                
                break;
            case 4:
                StageNameText.text = "4. 쥐수구";
                break;
            case 5:
                StageNameText.text = "5. 돼지 마을";                

                break;
            case 6:
                StageNameText.text = "업데이트 예정";                

                break;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            //Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void SetSubQuestText()
    {
        if(GameManager.Instance.subQuestLevel >=2)
        {
            return;
        }
        subQuestController.SetText();
    }

    public void SetTextPool_trans(GameObject pObject, InfiniCoin coin, UiTargetManager.TextType textType, bool isRaid, GameObject target)
    {
        textPollControllerTrans.SetTextTarget(target, coin, textType, target);
       
    }
    public void SetTextCritial_trans(GameObject pObject, InfiniCoin coin, bool isRaid, GameObject target)
    {        
        textPollControllerTrans.SetCriticalTextTarget(target, coin, target);
    }

    public void SetTextPool(GameObject pObject,InfiniCoin coin, UiTargetManager.TextType textType,bool isRaid,GameObject target)
    {
        if(target == null)
        {
            if (isRaid == false)
            {
                textPoolController.SetText(pObject, coin, textType);
            }
            else
            {
                raidTextPoolController.SetText(pObject, coin, textType);
            }
        }
        else
        {
            textPoolController.SetTextTarget(target, coin, textType,target);
        }
            
    }
    public void SetTextCritial(GameObject pObject, InfiniCoin coin, bool isRaid,GameObject target)
    {
        if(target ==null)
        {
            if (isRaid == false)
            {
                textPoolController.SetCriticalText(pObject, coin);
            }
            else
            {
                raidTextPoolController.SetCriticalText(pObject, coin);
            }
        }
        else
        {
            textPoolController.SetCriticalTextTarget(target, coin,target);
        }
      
    }
    private GameObject nowPanel;
    public void SetBottomPanel(GameObject _panel, BottomButtonController.ButtonType _paneltype)
    {
        if (nowPanel != null)
        {
            nowPanel.GetComponent<BottomButtonController>().SetDisable();
        }        
        nowPanel = _panel;
    }

    public void SetChanceText()
    {
        float chance = 0;
        //MaxLevel_rank == le
        if (GameManager.Instance.MaxLevel_rank == GameManager.Instance.Level)
        {
            //1% 확률
            chance = 2f;
        }
        else
        {
            chance = 2f - ((GameManager.Instance.MaxLevel_rank - GameManager.Instance.Level) * 0.1f);
            if (chance <= 0)
            {
                chance = 0.1f;
            }
        }        
        if (GameManager.Instance.bStartBoss ==true)
        {            
            float scrollChance = chance * 0.7f;
            ScrollChanceText.text = "도안 획득 " + scrollChance.ToString("N2") + " %";
            MatChanceText.text = "재료 획득 0 %";
        }
        else
        {
            ScrollChanceText.text = "도안 획득 0 %";
            MatChanceText.text = "재료 획득 " + chance.ToString("N1") + " %";
        }
        
    }
    void Start()
    {
        SetdataSavePanel(true);
        gameManager = GameManager.Instance;
        gameManager.StartEndEventHandler += GameManager_StartEndEventHandler;
        MailBox.SetActive(false);
        SetGoldText();
        SetGemText();
        SetMonsterCount();
        SetLevelText();
        CheckLevelButton();
        SetSoulText();
        SetBossTimer();
        SetChanceText();
        toggleAutoPlay.isOn = gameManager.IsAutoPlay;
        AutoPlayImage.SetActive(toggleAutoPlay.isOn);
        if(toggleAutoPlay.isOn ==true)
        {
            ToogleLable.text = "진행모드";
        }
        else
        {
            ToogleLable.text = "멈춤";
        }
        // k m b t        
        if(GameManager.Instance.TutorialList[6] >0)
            CheckGift();
        CheckTurorialLock();
        RestoreObject.SetActive(false);
#if UNITY_IOS
        RestoreObject.SetActive(true);
#endif
        //테스트 hp

        //if (File.Exists("Test.txt"))
        //{
        //    return;
        //}
        //var sr = File.CreateText("Test.txt");

        //for (int i = 0; i < 500; i++)
        //{
        //    sr.WriteLine(SystemManager.GetGold(i));
        //    //Debug.Log(SystemManager.GetEnmeyHp(i));
        //}

        //sr.Close();
        SetStageName();

    }

    private void GameManager_StartEndEventHandler()
    {
        SetdataSavePanel(false);
    }

    public void CheckGift()
    {
        PlayNanooManager.Instance.CheckGift(MailBox);               
    }

    public void SetAutoPlay_Trans()
    {
        gameManager.IsAutoPlay = true;
        GameManager.Instance.Save(GameManager.saveType.IsAutoPlay);
        toggleAutoPlay.isOn = gameManager.IsAutoPlay;
        AutoPlayImage.SetActive(toggleAutoPlay.isOn);
        if (toggleAutoPlay.isOn == true)
        {
            ToogleLable.text = "진행모드";
        }
        else
        {
            ToogleLable.text = "멈춤";
        }
    }
    public void SetGoldText()
    {
        GoldText.text = SetCost(gameManager.TotalGold);
    }
    public void SetGemText()
    {
        GemText.text = SetCost(gameManager.TotalGem);
    }
   
    public float defaultBossTime = 30f;
    public float fBossTime = 30f;
    public void CheckHP(InfiniCoin hp)
    {
        if (hp <= 0.99f)
        {                
            //CheckLevel();            
            
        }
        if (hp < 1000)
        {
            string[] temp = hp.ToString().Split('.');
            HpText.text = temp[0].ToString();
        }
        else
        {
            HpText.text = hp.ToString();
        }
        CheckHpFill(hp);
    }
    
    public void CheckHpFill(InfiniCoin hp)
    {
        //100 -10
        if (gameManager == null)
            return;

        InfiniCoin defalutHp = SystemManager.GetEnmeyHp(gameManager.Level);
        double deltaHp = 1;
        if (defalutHp.kPower > hp.kPower)
        {
            long count = defalutHp.kPower - hp.kPower;
            double fPower = Mathf.Pow(1000, count);
            deltaHp = ((hp.baseValue/ fPower) / defalutHp.baseValue);
        }
        else
        {
            deltaHp = (hp.baseValue / defalutHp.baseValue);
        }
         
        if (deltaHp > 1)
        {
            deltaHp = deltaHp / 1000;            
        }     
        
        HpFillImage.fillAmount = float.Parse(deltaHp.ToString());
        //Debug.Log(deltaHp);
    }
    public void SetSoulText()
    {
        SoulText.text = SetCost(gameManager.TotalSoul);
    }
    private void Update()
    {        
        if(GameManager.Instance.timerCotroller.bStart_attackBuff ==true)
        {
            if(BuffImage.activeSelf ==true)
            {
                BuffImage.SetActive(false);
            }
            if(EnableBuffImage.activeSelf ==false)
            {
                EnableBuffImage.SetActive(true);
            }
            if(GameManager.Instance.Pack1 ==false)
            {
                EnableBuffText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.AttackBuffTime);
            }
            else
            {
                EnableBuffText.text = "∞";
            }
            
        }        
        else
        {
            if (BuffImage.activeSelf == false)
            {
                BuffImage.SetActive(true);
                EnableBuffText.text = "버프";
            }
            if (EnableBuffImage.activeSelf == true)
            {
                EnableBuffImage.SetActive(false);
            }
        }
        if(gameManager.bMoveNew ==true && gameManager.MaxLevel == gameManager.Level)
        {
            TimerText.gameObject.SetActive(false);
            return;
        }
        if(gameManager.bStartBoss ==true)
        {
            fBossTime -= Time.deltaTime;
            TimerText.gameObject.SetActive(true);
            TimerText.text = fBossTime.ToString("N1");            
        }
        else
        {
            SetBossTimer();
            TimerText.gameObject.SetActive(false);
        }
        if (fBossTime <= 0 && TimerText.gameObject.activeSelf == true)
        {
            SetBossTimer();
            TimerText.gameObject.SetActive(false);
            gameManager.bStartBoss = false;
            gameManager.IsAutoPlay = false;
            toggleAutoPlay.isOn = false;          
            ToogleLable.text = "멈춤";
            GameManager.Instance.Save(GameManager.saveType.IsAutoPlay);
            BossStageStatusPanel.SetActive(true);
            BossStageStatusPanel.GetComponent<BossStageStatusPanelSrc>().SetData(false);
                        
            PrevLevel();
        }
        //CheckLevelText();
    }
    void SetBossTimer()
    {
        fBossTime = 30 + (gameManager.buffData.BossTImeBuff) + float.Parse((gameManager.GetArtifactValue(GameManager.AtrifactType.BossTime).ToString()));
    }
    public void BossKill()
    {
        SetBossTimer();
        
    }
    public void SetMonsterCount()
    {
        MonsterCountText.text = gameManager.GetMonsterCount() + " / " + gameManager.GetMaxMonsterCount();
    }
    public void SetLevelText()
    {
        int level = gameManager.Level+1;
        LevelText.text = "Stage. " + level.ToString();
    }
    public void CheckLevelButton()
    {
        if(gameManager.MaxLevel> gameManager.Level)
        {
            NextObject.SetActive(true);
        }
        else
        {
            NextObject.SetActive(false);
        }
        if(gameManager.Level ==0)
        {
            PrevObject.SetActive(false);
        }
        else
        {
            PrevObject.SetActive(true);
        }
    }
    public void NextLevel()
    {
        int maxStageIndex = (gameManager.MapStageIndex * 200)-1;
        if (gameManager.Level >= maxStageIndex)
        {
            SetNotification(NotificationType.Map);
            return;
        }

        gameManager.Level++;
        
        if (gameManager.MaxLevel < gameManager.Level)
        {
            gameManager.Level = gameManager.MaxLevel;
        }
        if(gameManager.bMoveNew == true)
        {
            gameManager.MoveStart();
        }
        GameManager.Instance.Save(GameManager.saveType.Level);
        GameManager.Instance.CheckMap();
        
        
        clearConroller.SetStageView();
        CheckLevelButton();
        SetLevelText();
        SetMonsterCount();
        gameManager.MakeNewMonster();
        SetChanceText();
        SetMapLevel();
    }
    public void Test_Level()
    {
        if (gameManager.MaxLevel < gameManager.Level)
        {
            gameManager.Level = gameManager.MaxLevel;
        }
        if (gameManager.bMoveNew == true)
        {
            gameManager.MoveStart();
        }

        GameManager.Instance.CheckMap();


        clearConroller.SetStageView();
        CheckLevelButton();
        SetLevelText();
        SetMonsterCount();
        gameManager.MakeNewMonster();
        SetChanceText();
    }
    public void PrevLevel()
    {
        int prevLevel = gameManager.Level -1; 
        int maxStageIndex = ((gameManager.MapStageIndex-1) * 200) - 1;
        if (prevLevel <= maxStageIndex)
        {
            SetNotification(NotificationType.MapMove);
            return;
        }

        toggleAutoPlay.isOn = false;
        SetAutoPlay();
        
        gameManager.Level--;
        
        if (gameManager.Level <=0 )
        {
            gameManager.Level = 0;
        }
        GameManager.Instance.Save(GameManager.saveType.Level);
        GameManager.Instance.CheckMap();
        clearConroller.SetStageView();
        CheckLevelButton();
        SetLevelText();
        SetMonsterCount();
        gameManager.MakeNewMonster();
        SetChanceText();
        SetMapLevel();
    }
    public void SetInitMap()
    {
        GameManager.Instance.CheckMap();
        clearConroller.SetStageView();
        CheckLevelButton();
        SetLevelText();
        SetMonsterCount();
        gameManager.MakeNewMonster();
        SetChanceText();
    }
    public void SetMapLevel()
    {
        switch(gameManager.MapStageIndex)
        {
            case 1:
                gameManager.level1MapStage = gameManager.Level;
                gameManager.Save(GameManager.saveType.level1MapStage);
                break;
            case 2:
                gameManager.level2MapStage = gameManager.Level;
                gameManager.Save(GameManager.saveType.level2MapStage);
                break;
            case 3:
                gameManager.level3MapStage = gameManager.Level;
                gameManager.Save(GameManager.saveType.level3MapStage);
                break;
            case 4:
                gameManager.level4MapStage = gameManager.Level;
                gameManager.Save(GameManager.saveType.level4MapStage);
                break;
            case 5:
                gameManager.level5MapStage = gameManager.Level;
                gameManager.Save(GameManager.saveType.level5MapStage);
                break;
        }
    }
    public void CheckHeroStatus(Text text,int heroindex)
    {
        text.text = "";
        for (int i=0; i< gameManager.heroPos.Length; i++)
        {
            if(gameManager.heroPos[i] == heroindex)
            {
                text.text += "[필드] ";
            }
        }
        for (int i = 0; i < gameManager.heroExpeditionPos.Length; i++)
        {
            if (gameManager.heroExpeditionPos[i] == heroindex)
            {
                text.text += "[원정대] ";
            }
        }
        for (int i = 0; i < gameManager.heroraidPos.Length; i++)
        {
            if (gameManager.heroraidPos[i] == heroindex)
            {
                text.text += "[레이드] ";
            }
        }
    }
    public void SetHeroCount_fillUI(Text text,Image fillImage,int heroIndex)
    {
        text.text = gameManager.herosInfos[heroIndex].HeroCount + " / 2";
        fillImage.fillAmount = (float)(gameManager.herosInfos[heroIndex].HeroCount) / 2f;
    }
    public void SetName_UIText(Text text, int tierIndex)
    {
        switch (gameManager.herosInfos[tierIndex].Tier)
        {
            case "n":
                //text.text = "일반";                
                text.color = normalColor;
                break;
            case "a":
                //text.text = "고급";                
                text.color = advanceColor;
                break;
            case "r":
                //text.text = "희귀";
                text.color = rareColor;
                break;
            case "h":
                //text.text = "영웅";
                text.color = heroColor;
                break;
            case "l":
                //text.text = "전설";
                text.color = lgendColor;
                break;
        }
        text.text = gameManager.herosInfos[tierIndex].Name;
    }
    public List<GameObject> OpenPanelList = new List<GameObject>();
    public void AddOpenPanel(GameObject p)
    {
        OpenPanelList.Add(p);
    }
    public void OpenPanelRemove()
    {
        if (OpenPanelList.Count > 0)
        {
            OpenPanelList.RemoveAt(OpenPanelList.Count - 1);
        }
    }
    public bool bOpenPanel = false;
    public void RemoveOpenPanel()
    {
        if(OpenPanelList.Count >0)
        {
            if(OpenPanelList[OpenPanelList.Count - 1].GetComponent<AndroidESCController>().panelType == AndroidESCController.PanelType.normal)
            {
                OpenPanelList[OpenPanelList.Count - 1].GetComponent<AndroidESCController>().Cancel();
            }
            else
            {
                CheckBottomUI();
                //OpenPanelList.RemoveAt(OpenPanelList.Count - 1);
            }            
            
        }
        else
        {
            if(bOpenPanel ==false)
            {
                AppQuitPanel.SetActive(true);                
            }
            
        }
    }
    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            RemoveOpenPanel();
        }
    }
    public void CheckBottomUI()
    {
        for(int i =0; i< BottomObjects.Count;i++)
        {
            if(BottomObjects[i].activeSelf ==true)
                BottomObjects[i].GetComponent<BottomButtonController>().SetDisable();
        }
    }
    public void SetTier_UIText(Text text, int tierIndex)
    {
        switch (gameManager.herosInfos[tierIndex].Tier)
        {
            case "n":
                text.text = "일반";                
                text.color = normalColor;
                break;
            case "a":
                text.text = "고급";                
                text.color = advanceColor;
                break;
            case "r":
                text.text = "희귀";
                text.color = rareColor;
                break;
            case "h":
                text.text = "영웅";
                text.color = heroColor;
                break;
            case "l":
                text.text = "전설";
                text.color = lgendColor;
                break;
        }
        
    }
    public void CheckHeroIconColor(Image heroIcon,int heroindex,bool isList=false)
    {
        if(isList == true)
        {
            heroIcon.color = EnableColor;
            return;
        }
        if(gameManager.herosInfos[heroindex].isGetHero ==true)
        {
            heroIcon.color = EnableColor;
        }
        else
        {
            heroIcon.color = DisableColor;
        }
    }
    public void SetHeroIcon_UI(Image heroIcon, int heroindex)
    {
        string strHeroIcon = "HeroIcon/" + heroindex;

        Sprite temp = Resources.Load<Sprite>(strHeroIcon);
        if (temp != null)
        {
            heroIcon.sprite = temp;
        }
    }
    public enum iconType
    {
        gold,
        gem,
        altar,
        attackbuff,
        ticket,
        nBox,
        sBox,
        menu_expedition,
        menu_raid,
        menu_transcendence,
        menu_altar,
        soul,
        ads,
        mat,
        armor,//1
        belt,//2
        boots,//3
        cape,//4
        gloves,//5
        helmet,//6
        necklace,//7
        ring,//8
        shield,//9
        pack1

    }
    public void SetIcon(Image Icon, iconType type)
    {
        string strHeroIcon;
        Sprite temp = null;
        Icon.gameObject.transform.localScale = new Vector3(1, 1, 1);
        switch (type)
        {
            case iconType.pack1:
                strHeroIcon = "Icon/adsbuff";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.gold:
                strHeroIcon = "Icon/Gold";
                temp = Resources.Load<Sprite>(strHeroIcon);               
                break;
            case iconType.gem:
                strHeroIcon = "Icon/Gem";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.attackbuff:
                strHeroIcon = "Icon/adsbuff";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.altar:
                strHeroIcon = "Icon/Altar";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.ticket:
                strHeroIcon = "Icon/ticket";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.nBox:
                strHeroIcon = "Icon/nbox";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.sBox:
                strHeroIcon = "Icon/sbox";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.menu_altar:
                strHeroIcon = "Icon/altar_menu";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.menu_expedition:
                strHeroIcon = "Icon/expedition";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.menu_raid:
                strHeroIcon = "Icon/raid";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.menu_transcendence:
                strHeroIcon = "Icon/transcendence";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.soul:
                strHeroIcon = "Icon/soul";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.ads:
                strHeroIcon = "Icon/ads";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.mat:
                Icon.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                strHeroIcon = "Icon/mat";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.armor:
                strHeroIcon = "Icon/armor";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.belt:
                strHeroIcon = "Icon/belt";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.boots:
                strHeroIcon = "Icon/boots";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.cape:
                strHeroIcon = "Icon/cape";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.gloves:
                strHeroIcon = "Icon/gloves";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.helmet:
                strHeroIcon = "Icon/helmet";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.necklace:
                strHeroIcon = "Icon/necklace";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.ring:
                strHeroIcon = "Icon/ring";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;
            case iconType.shield:
                strHeroIcon = "Icon/shield";
                temp = Resources.Load<Sprite>(strHeroIcon);
                break;

        }
        if (temp != null)
        {
            Icon.sprite = temp;
        }
    }
    public void ShowHeroInfoPanel(int heroindex)
    {
        heroInfoPanelSrc.gameObject.SetActive(true);
        heroInfoPanelSrc.SetData(heroindex);
    }
    public void SetHeroInfoPanel(int heroindex)
    {
        if(heroInfoPanelSrc.gameObject.activeSelf ==true)
        {
            heroInfoPanelSrc.SetData(heroindex);
        }                
    }

    public void SetHeroName_UI(Text text,int heroIndex)
    {
        text.text = gameManager.herosInfos[heroIndex].Name;
    }
    public void SetHeroLevel_UI(Text text, int heroindex)
    {
        text.text = "Lv. " + gameManager.herosInfos[heroindex].level;
    }
    public void SetHeroDPS_UI(Text text,int heroindex)
    {
        text.text = "ATK: " + SetCost(gameManager.herosInfos[heroindex].DPS);
    }
    public void Set_SpeacialAblity_InfoText(Text text, int heroindex)
    {
        text.text = "특수 능력 <color=yellow>(명성 Lv. " + gameManager.herosInfos[heroindex].FameLevel+ ")</color>";
    }
    public void SetLimitText_UI(Text text, int heroIndex)
    {        
        text.text = "한계돌파 Lv." + gameManager.herosInfos[heroIndex].LimitLevelUpCount + " (Max Level "+ gameManager.GetLimitMaxLevel(heroIndex) + ")";
    }
    public void SetSpacialAblityText_UI(Text ablity1,Text ablit2,Text ablity3, int heroIndex)
    {
        ablity1.text = GetSpeacialAblityName(gameManager.herosInfos[heroIndex].specialAblity1);
        ablit2.text = GetSpeacialAblityName(gameManager.herosInfos[heroIndex].specialAblity2);
        ablity3.text = GetSpeacialAblityName(gameManager.herosInfos[heroIndex].specialAblity3);
    }
    string GetSpeacialAblityName(string abstr)
    {
        string[] splitStr = abstr.Split('_');
        string ablityName = "";
        switch (splitStr[0])
        {
            case "dps":
                ablityName = "공격력 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "gold":
                ablityName = "골드 획득 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "alldps":
                ablityName = "모든 영웅 공격력 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "bosstime":
                ablityName = "보스전 시간증가 + " + splitStr[1] + " 초";
                return ablityName;
            case "click":
                ablityName = "클릭 데미지 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "kill":
                ablityName = splitStr[1] + "% 확률로 몬스터 즉사";
                return ablityName;
            case "expedition":
                ablityName = "원정대 보상 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "raid":
                ablityName = "레이드 보상 + " + splitStr[1] + " % 증가";
                return ablityName;
            case "x":
                ablityName = "없음";
                return ablityName;
        }
        return ablityName;
    }

    public void SetSelectHeroPanel()
    {
        SelectHero.CheckHero();
    }
    public void SetHeroColider(bool flag)
    {
        for (int i = 0; i < GameManager.Instance.HeroList.Count; i++)
        {
            GameManager.Instance.HeroList[i].GetComponent<BoxCollider2D>().enabled = flag;
        }
    }
    public void SetAutoPlay()
    {
        gameManager.IsAutoPlay = toggleAutoPlay.isOn;
        AutoPlayImage.SetActive(toggleAutoPlay.isOn);
        if (toggleAutoPlay.isOn == true)
        {
            ToogleLable.text = "진행모드";
        }
        else
        {
            ToogleLable.text = "멈춤";
        }
    }
    public string SetCost(InfiniCoin cost,bool isNormal =false)
    {
        if (cost == null)
            return "똥";
        InfiniCoin coin = cost;
        if(isNormal==true)
        {
            return cost.ToString();
        }
        if(cost.kPower >0)
        {
            return coin.ToString();
        }
        else
        {
            string[] temp = cost.ToString().Split('.');
            if(int.Parse(temp[1]) != 0)
            {
                string strTemp = temp[0]+"."+temp[1];
                return strTemp;
            }
            return temp[0];
            
        }
        
    }
    public void CheckUpgradePanel(int heroPos)
    {
        if(UpgradePanel.gameObject.activeSelf==true)
        {
            UpgradePanel.SetUpgrade();
        }
        HeroListPanel.LevelUpSetData(heroPos);
    }
    public void UpgradeHeroList()
    {
        for(int i =0; i< gameManager.herosInfos.Count;i++)
        {
            HeroListPanel.LevelUpSetData(i);
        }
    }
    bool bNotification = false;
    //
    public enum NotificationType
    {
        gem,
        gold,
        altar,
        card,
        soul,
        ads,
        maxitem,
        craft,
        inventoryFull,
        ItemUpgrade,
        Update,
        NeedHero,
        Map,
        MapLimit,
        MapMove,
        RankSucess,
        RankFail,
        MakeNickname,
        nicknameerror1,
        nicknameerror2,
        needRank5,
        save
    }
    public void SetNotification(NotificationType type)
    {
        if (bNotification == false)
        {
            Notification.SetActive(true);
            switch (type)
            {
                case NotificationType.save:
                    NotificationText.text = "저장 완료.";
                    UiManager.Instance.SetdataSavePanel(false);
                    break;
                case NotificationType.needRank5:
                    NotificationText.text = "5 Stage 클리어 필요.";
                    break;
                case NotificationType.nicknameerror2:
                    NotificationText.text = "닉네임이 너무 깁니다.";
                    break;
                case NotificationType.nicknameerror1:
                    NotificationText.text = "공백, 특수문자는 허용되지 않습니다.";
                    break;
                case NotificationType.MakeNickname:
                    NotificationText.text = "닉네임 설정 필요!\nMenu창에서 설정가능";
                    break;
                case NotificationType.RankSucess:
                    NotificationText.text = "랭크 갱신 성공";
                    break;
                case NotificationType.RankFail:
                    NotificationText.text = "갱신 실패";
                    break;
                case NotificationType.gem:
                    NotificationText.text = "재화가 부족 합니다.";
                    break;
                case NotificationType.gold:
                    NotificationText.text = "골드가 부족 합니다.";
                    break;
                case NotificationType.altar:
                    NotificationText.text = "영혼의 조각이 부족 합니다.";
                    break;
                case NotificationType.soul:
                    NotificationText.text = "번개가 부족 합니다.";
                    break;
                case NotificationType.card:
                    NotificationText.text = "영웅 갯수가 부족 합니다.";
                    break;
                case NotificationType.ads:
                    NotificationText.text = "광고가 부족 합니다.";
                    break;
                case NotificationType.maxitem:
                    NotificationText.text = "인벤토리가 가득 찼습니다.";
                    break;
                case NotificationType.craft:
                    NotificationText.text = "재료가 부족 합니다.";
                    break;
                case NotificationType.inventoryFull:
                    NotificationText.text = "인벤토리가 가득 찼습니다.";
                    break;
                case NotificationType.ItemUpgrade:
                    break;
                case NotificationType.Update:                
                    NotificationText.text = "업데이트 예정입니다..";                    
                    break;
                case NotificationType.NeedHero:
                    NotificationText.text = "영웅을 선택 하세요.";
                    break;
                case NotificationType.Map:
                    NotificationText.text = "지역을 모두 점령하였습니다.";
                    break;
                case NotificationType.MapLimit:
                    NotificationText.text = "아직 접근할 수 없습니다.";
                    break;
                case NotificationType.MapMove:
                    NotificationText.text = "지역 이동이 필요 합니다.";
                    break;
            }
           
            
            bNotification = true;
            StartCoroutine(EndNotification());
        }
    }
    public void SetNotification(string strText)
    {
        if(bNotification ==false)
        {
            Notification.SetActive(true);
            NotificationText.text = strText;
            bNotification = true;
            StartCoroutine(EndNotification());
        }
    }
    IEnumerator EndNotification()
    {
        yield return new WaitForSeconds(1f);
        Notification.SetActive(false);
        bNotification = false;
    }
    public void SetBuyComplete(InfiniCoin cost, BuyCompletePanel.buyType type,int ivalue =0)
    {
        BuyCompletePanel.SetActive(true);
        BuyCompletePanel.GetComponent<BuyCompletePanel>().type = type;
        BuyCompletePanel.GetComponent<BuyCompletePanel>().SetData(cost,ivalue);
    }
    public void ShowBreakLimitPanelObject(int heroindex)
    {
        BreakLimitPanelObject.SetActive(true);
        BreakLimitPanelObject.GetComponent<BreaklimitsPanelSrc>().SetData(heroindex);
        
    }
    public void SetCollectionUI(string title)
    {
        CollectionObject.SetActive(true);
        CollectionText.text = title;
        StartCoroutine(EndNotificationCollection());
    }
    IEnumerator EndNotificationCollection()
    {
        yield return new WaitForSeconds(2f);
        CollectionObject.SetActive(false);        
    }
}



