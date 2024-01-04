using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR_InfiniCoin;
using DG.Tweening;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Save Data
    /// </summary>
    /// 
    public enum TutorialType
    {
        click,
        upgrade,
        buff,
        shop,
        selecthero,
        herolist,
        automove,
        expedition,
        raid,
        altar,
        transcendence
    }
    public enum SubQuestType
    {
        heroLevelup,
        clickLevelup,
        expedtionGo,
        click,
        raidBossKill,
        altarUpgrade,
        stage,
        gatchaHero,
        aweaking,
        heroGet,
        non
    }
    public enum AtrifactType
    {
        alldps,
        criticalDps,
        criticalChance,
        costDiscount,
        ClickPower,
        GoldMonster,
        GetGoldx10,
        KillGetGold,
        BossHp,
        BossTime,
        Non
    };

    public struct Clicker
    {
        public InfiniCoin TotalClickPower;
        public InfiniCoin UpgradeClickpower;
        public double level;
    }
    public struct RankerData
    {
        public int[] HeroPos;
        public string nickname;
        public string Stage;
        public string rank;
        public string UserID;
    }
    public enum EquipmentItemType
    {
        magicAttack,
        Bosstime,
        Gold,
        MonsterHp,
        Movement,
        CriticalChance,
        doubleAttack,
        expeditionTime,
        Attackbuff
    }
    public struct Item
    {
        public bool isGet;
        public float ablityPower;
        public int itemLevel;
        public int itemTier;
        public int ablityType;
        public int position;
        public int count;
        public string name;
        public int Item_Index;
        //
        public float time;
        //
        public float PChance;
    }
    public struct scroll
    {
        public bool isGet;
        public float ablityPower;
        public int itemLevel;
        public int itemTier;
        public int ablityType;
        public int position;
        public int count;
        public string name;
        public int Item_Index;

        //
        public float time;
        //
        public float PChance;
    }
    public struct TimerCotroller
    {
        public float NormalBoxTime;
        public float SpeacialBoxTime;
        public bool bStart_normal;
        public bool bStart_Speacial;
        public List<float> ExpedtionTime;
        public List<bool> bExpedtionTime;

        public float AttackBuffTime;
        public bool bStart_attackBuff;
        public float TicketTime;


        public float AdsTicketTime;
        public bool bAdsTicketTime;

        public float AdsGoldTime;
        public bool bAdsGoldTime;


        public float TransTicketTime;
        public bool bTrasnTicketTime;
    }
    public struct HerosInfo
    {
        //획득 여부
        public bool isGetHero;
        //히어로 갯수
        public int HeroCount;
        //히어로 등급
        public string Tier;
        //초기 dps
        public InfiniCoin InitDPS;
        public InfiniCoin ResetDps;
        //업그레이드 후 dps
        public InfiniCoin DPS;
        //이름
        public string Name;
        //업그레이드 비용
        public InfiniCoin Cost;
        public string specialAblity1;
        public string specialAblity2;
        public string specialAblity3;

        public int LimitLevelUpCount;

        public bool isAblity_1;
        public bool isAblity_2;
        public bool isAblity_3;

        public float fameDps;

        public double level;
        //각성 카운트
        public double AwakeningCount;
        //명성 카운트
        public double FameCount;
        public int FameLevel;
        public double AttackBuff;
        //레벨업 버프 파워
        public double LevelUpBuff;
        public int equipItem_1;
        public int equipItem_2;
        public int equipItem_3;
        public int equipItem_4;
        public Item item_1;
        public Item item_2;
        public Item item_3;
        public Item item_4;
        //
        public float goldBuff;
        public float ClickBuff;
        public float raidBuff;
        public float goawayBuff;
        public float bossTimeBuff;
        public float killPercent;
        public void SetDps(InfiniCoin value)
        {
            DPS = value;
        }
        public void AddDps(InfiniCoin value)
        {
            DPS += value;
        }
        public void AddLevel(double value)
        {
            level += value;
        }
        public void AddAwakeningCount(double val)
        {
            AwakeningCount += val;
        }
        public void AddFameCount(double count)
        {
            FameCount += count;
        }


    };

    public enum AdsType
    {
        normalBox,
        gold,
        powerBuff,
        Transcendence,
        offlinereward,
        altar,
        ticket,
        non
    }
    public struct BuffData
    {
        public float GoldBuff;
        public float BossTImeBuff;
        public float ClickBuff;
        public float RaidBuff;
        public float GoawayBuff;
    };
    public struct AchivementData
    {
        public List<double> AchivementMax;
        public List<double> AchivementLevel;
        public List<double> AchivementCount;
    };


    [Space]
    [Header("========== Save Data ==========")]
    [Space]
    public InfiniCoin tickGoldChecker = new InfiniCoin();
    public bool isRaidAutoPlay;
    public List<RankerData> rankerDatas = new List<RankerData>();
    public float rankRenewTime;
    public bool bMakeNickName;
    public string strNickName;
    public bool IsNewCollection;
    public List<int> CollectionList = new List<int>();
    public bool Pack1;
    public int MapStageIndex;
    public int level1MapStage;
    public int level2MapStage;
    public int level3MapStage;
    public int level4MapStage;
    public int level5MapStage;

    public int TransTierMonsterCount;
    public int TransTier;
    public bool transTutorial;
    public int GatchaCount;
    public float achivement_time;
    public int materialCount;
    public List<scroll> Scrolls = new List<scroll>();
    public int TotalreBrithCount;
    public int subQuestIndex;
    public int subQuestLevel;
    public int subQuestNow;
    public List<int> TutorialList = new List<int>();
    public string UserID;
    public int TutorialIndex;
    public InfiniCoin TotalGold;
    public InfiniCoin TotalSoul;
    public InfiniCoin TotalGem;
    public InfiniCoin TotalAltarCoin;

    public int MaxLevel_rank_Nanoo;
    public int Stage_Nanoo;
    public int AltarLevel;
    public float AltarPercent;
    public int Level;
    public int MaxLevel;
    public int MaxLevel_rank;
    public int RaidMaxLevel;
    public int RaidNowLevel;
    public bool bSave = false;
    public Clicker clicker = new Clicker();
    public List<HerosInfo> herosInfos = new List<HerosInfo>();    
    public int[] heroPos = new int[10] { -1, -1, -2, -2, -2, -2, -2, -2, -2, -2 };
    public int[] heroExpeditionPos = new int[8] { -2, -2, -2, -2, -2, -2, -2, -2 };
    public int[] heroExpeditionComplete = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
    public int[] heroraidPos = new int[5] { -1, -1, -1, -1, -1 };
    public int heroTransPos;
    public int MaxMonsterCount;
    public int MonsterCount;
    public bool IsAutoPlay;
    public TimerCotroller timerCotroller = new TimerCotroller();
    public AchivementData achivementData = new AchivementData();
    public BuffData buffData = new BuffData();
    public List<double> SoulAblityList = new List<double>();
    public List<Item> items = new List<Item>();
    public bool isBGM;
    public bool isFx;
    public int iLangueageType;
    public bool isNewHero;
    public int raidTicketCount;
    public bool isNewItem;
    public int IsNewSmith;
    ///
    ///    
    [Space]
    [Header("========== Normal Data ==========")]
    [Space]
    public MapStageClearSrc mapStageClearSrc;
    public TransDungeonGameSrc transDungeonGameSrc;
    public List<GameObject> MapList;
    public ItemEffectController itemEffectController;
    public bool bItemGetGold;
    public double MaxLevel_Achivement;
    public SubQuestType subquestType = SubQuestType.non;
    public bool bChect;
    public float defaultExpeditionTime;
    public Camera mainCamera;
    public int NormalBoxCost;
    public int SpeacialBoxCost;
    public AdsType adsType = AdsType.non;
    public TimeManager timeManager;
    public float defaultNormalBoxTime;
    public float defaultSpeacialBoxTime;
    public bool bMoveNew;
    public Transform EnemyPos;
    public Transform EnmeyPos_trans;
    public bool bStartEnemy_trans;
    public GirdMoveManager myGridMoveManager;
    public List<GameObject> HeroList;
    public List<Transform> HeroPosTransfrom;
    public List<Transform> HeroPos_Src;
    private HeroStatus heroStatus;
    public EnemyManager enemyManager;
    public TransEnemyManager transenemyManager;
    public CollectingEffectController CoinCollectionEffect;
    public CollectingEffectController GemCollectionEffect;

    public CollectingEffectController materialCollectionEffect;
    public CollectingEffectController ScrollCollectionEffect;
    public CollectingEffectController materialCollection_achivementEffect;
    public InfiniCoin hp = new InfiniCoin();
    public bool bStartBoss;
    public bool bStartEnemy;
    //뽑기 전용
    public List<int> Lv1HeroIndex = new List<int>();
    public List<int> Lv2HeroIndex = new List<int>();
    public List<int> Lv3HeroIndex = new List<int>();
    public List<int> Lv4HeroIndex = new List<int>();
    public List<int> Lv5HeroIndex = new List<int>();

    public List<Sprite> ScrollSprite;
    public List<Sprite> ItemsSprite;

    float RaidDefaultTIme;

    public delegate void StartEndEvent();
    public event StartEndEvent StartEndEventHandler;

    private static GameManager _instance = null;
    public static GameManager Instance
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
    //public string generateUniqueNickName()
    //{
    //    int rand = Random.Range(0,SystemManager.NicknameData.Count);
    //    return SystemManager.GetNickname(rand);
    //}
    public string generateUniqueID(int _characterLength = 20)
    {
        StringBuilder _builder = new StringBuilder();
        Enumerable
            .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => System.Guid.NewGuid())
            .Take(_characterLength)
            .ToList().ForEach(e => _builder.Append(e));
        return _builder.ToString();
    }

    private void Start()
    {
        mainCamera.transform.position = new Vector3(0, 0, -10);
        MakeNamingSystem();
        //try
        //{
        //    if (ES3.KeyExists("bSave"))
        //    {
        //        bSave = ES3.Load<bool>("bSave");
        //    }
        //    else
        //    {
        //        //mainCamera.transform.position = new Vector3(1.5f, 1.4f, -10);
        //    }
        //}
        //catch
        //{
        //    if (ES3.RestoreBackup("hoitsave_craft.es3"))
        //    {
        //        if (ES3.KeyExists("bSave"))
        //        {
        //            bSave = ES3.Load<bool>("bSave");
        //        }
        //    }
        //}
        SystemManager.SetData();
        bStartEnemy = false;
        bStartEnemy_trans = false;

        initData();

        LoadData();
        CheckMaxAhcivement();
        //아이디 생성
#if UNITY_EDITOR
        if (UserID == string.Empty)
            UserID = generateUniqueID();
#endif  
        if (UserID == string.Empty)
        {
            if (GameServiceManager.Instance == null)
            {
                UserID = generateUniqueID();
            }
            else
            {
                if (GameServiceManager.Instance.UserID == string.Empty || GameServiceManager.Instance.UserID == "Unavailable Player Identification")
                {
                    UserID = generateUniqueID();
                    Save(saveType.UserID);
                }
                else
                {
                    UserID = GameServiceManager.Instance.UserID;

                    Save(saveType.UserID);
                }
            }
        }
        //if (strNickName == "Empty")
        //{
        //    if (GameServiceManager.Instance.UserNickName == string.Empty)
        //    {                
        //        strNickName = generateUniqueID();
        //        Save(saveType.NickName);
        //    }
        //    else
        //    {                
        //        strNickName = GameServiceManager.Instance.UserNickName;
        //        Save(saveType.NickName);
        //    }
        //}
        MakeHeroCardRand();
        MakeItemRand();
        MakeItemRaidRand();
        MakeRandScroll();
        MakeTierItem();
        myGridMoveManager.onMoveEndEvent += MyGridMoveManager_onMoveEndEvent;
        myGridMoveManager.onMoveHalfEndEvent += MyGridMoveManager_onMoveHalfEndEvent;
        CheckTimer();
        CheckAchivement();
        //Debug.Log("히어로 데이터 = " + herosInfos.Count);
        timeManager.getTImeEventHandler += TimeManager_getTImeEventHandler;
        PlayNanooManager.Instance.CheckCheatEventHandler += Instance_CheckCheatEventHandler;
        SetXMoveCamera(true);
        CheckMap();

        StartEndEventHandler?.Invoke();
    }

    void CheckMaxAhcivement()
    {
        MaxLevel_Achivement = 0;
        for (int i = 0; i < herosInfos.Count; i++)
        {
            if (herosInfos[i].level > MaxLevel_Achivement)
            {
                MaxLevel_Achivement = herosInfos[i].level;
            }
        }
        achivementData.AchivementCount[0] = MaxLevel_Achivement;

    }
    public void CheckLearderBoard()
    {
        //if (GameServiceManager.Instance != null)
        //{
        //    GameServiceManager.Instance.ShowLearderboardUI();
        //}
    }
    void SetHeroMove()
    {
        for (int i = 0; i < heroPos.Length; i++)
        {
            if (heroPos[i] > -1)
            {
                HeroList[heroPos[i]].GetComponent<Hero>().SetStageMove();
            }
        }
        enemyManager.SetAttackPointsList(false);
    }
    private void MyGridMoveManager_onMoveHalfEndEvent()
    {
        //여기서 이미지쇽쇽        
        UiManager.Instance.CompleteStagePanel.SetActive(true);
        enemyManager.SetAttackPointsList(false);
    }
    public void InitStageView()
    {
        StartCoroutine(endStageViewRoutine());
    }
    IEnumerator endStageViewRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        UiManager.Instance.SetInit_StageMove();
        bStartMove = false;
        for (int i = 0; i < heroPos.Length; i++)
        {
            if (heroPos[i] > -1)
            {
                HeroList[heroPos[i]].GetComponent<Hero>().EndMove();
            }
        }
    }
    private void MyGridMoveManager_onMoveEndEvent()
    {
        bMoveNew = false;
        enemyManager.SetEnmey();
        enemyManager.SetAttackPointsList(true);
        if ((Level + 1) % 5 == 0)
        {
            if (MaxLevel == Level)
                UiManager.Instance.bossEffectAnim.SetActive(true);
        }
        //for (int i = 0; i < heroPos.Length; i++)
        //{
        //    if (heroPos[i] > -1)
        //    {
        //        HeroList[heroPos[i]].GetComponent<Hero>().EndMove();
        //    }
        //}
    }

    private void Instance_CheckCheatEventHandler(bool bChect)
    {
        if (bChect == true)
        {
            UiManager.Instance.ConnectFail.SetActive(true);
        }
    }

    private void TimeManager_getTImeEventHandler(float _time)
    {
        //시간 로드함~~ 메롱메롱
        
        float time = _time;
        //time = 3600;
        if (time > 500)
        {
            float rewardTime = time;
            if (TutorialList[6] > 0)
            {
                UiManager.Instance.OfflineRewardPanel.SetActive(true);
                if (rewardTime > 3600 * 4)
                {
                    rewardTime = 3600 * 4;
                }
                rewardTime = rewardTime / 2;
                UiManager.Instance.OfflineRewardPanel.GetComponent<OfflineRewardController>().Set(GetGoldTick() * rewardTime);
            }            
        }
        else
        {
            CheckCollection();
        }
        CheckRaidTicket(time);
        rankRenewTime -= time;
        if(rankRenewTime <0)
        {
            rankRenewTime = 0;
        }


        if (timerCotroller.bAdsTicketTime == true)
        {
            timerCotroller.AdsTicketTime -= time;
        }

        if (timerCotroller.bAdsGoldTime == true)
        {
            timerCotroller.AdsGoldTime -= time;
        }


        if (timerCotroller.bStart_normal == true)
        {
            timerCotroller.NormalBoxTime -= time;
        }
        if (timerCotroller.bStart_Speacial == true)
        {
            timerCotroller.SpeacialBoxTime -= time;
        }
        if (timerCotroller.bTrasnTicketTime == true)
        {
            timerCotroller.TransTicketTime -= time;
        }
        for (int i = 0; i < timerCotroller.bExpedtionTime.Count; i++)
        {
            if (timerCotroller.bExpedtionTime[i] == true)
            {
                timerCotroller.ExpedtionTime[i] -= time;
            }
        }
        if (timerCotroller.bStart_attackBuff == true)
        {
            timerCotroller.AttackBuffTime -= time;
        }

        UiManager.Instance.SetdataSavePanel(false);
    }
    private void FixedUpdate()
    {
        CheckRaidTicket();

        if (rankRenewTime <= 0)
        {
            rankRenewTime = 0;
        }
        else
        {
            rankRenewTime -= Time.deltaTime;
        }


        achivementData.AchivementCount[7] += Time.deltaTime;
        if (timerCotroller.bAdsTicketTime == true && timerCotroller.AdsTicketTime > 0)
        {
            timerCotroller.AdsTicketTime -= Time.deltaTime;
            if (timerCotroller.AdsTicketTime < 0)
            {
                timerCotroller.AdsTicketTime = 0;
                timerCotroller.bAdsTicketTime = false;
            }
        }
        else
        {
            if (timerCotroller.AdsTicketTime < 0)
            {
                timerCotroller.AdsTicketTime = 0;
                timerCotroller.bAdsTicketTime = false;
            }
        }

        if (timerCotroller.bAdsGoldTime == true && timerCotroller.AdsGoldTime > 0)
        {
            timerCotroller.AdsGoldTime -= Time.deltaTime;
            if (timerCotroller.AdsGoldTime < 0)
            {
                timerCotroller.AdsGoldTime = 0;
                timerCotroller.bAdsGoldTime = false;
            }
        }
        else
        {
            if (timerCotroller.AdsGoldTime < 0)
            {
                timerCotroller.AdsGoldTime = 0;
                timerCotroller.bAdsGoldTime = false;
            }
        }

        if (Pack1 == false)
        {
            if (timerCotroller.bStart_attackBuff == true && timerCotroller.AttackBuffTime > 0)
            {
                timerCotroller.AttackBuffTime -= Time.deltaTime;
                if (timerCotroller.AttackBuffTime < 0)
                {
                    timerCotroller.AttackBuffTime = 0;
                    timerCotroller.bStart_attackBuff = false;
                }
            }
            else
            {
                if (timerCotroller.AttackBuffTime < 0)
                {
                    timerCotroller.AttackBuffTime = 0;
                    timerCotroller.bStart_attackBuff = false;
                }
            }
        }


        if (timerCotroller.bTrasnTicketTime == true && timerCotroller.TransTicketTime > 0)
        {
            timerCotroller.TransTicketTime -= Time.deltaTime;
            if (timerCotroller.TransTicketTime < 0)
            {
                timerCotroller.TransTicketTime = 0;
                timerCotroller.bTrasnTicketTime = false;
            }
        }
        else
        {
            if (timerCotroller.TransTicketTime < 0)
            {
                timerCotroller.TransTicketTime = 0;
                timerCotroller.bTrasnTicketTime = false;
            }
        }


        if (timerCotroller.bStart_normal == true && timerCotroller.NormalBoxTime > 0)
        {
            timerCotroller.NormalBoxTime -= Time.deltaTime;
            if (timerCotroller.NormalBoxTime < 0)
            {
                timerCotroller.NormalBoxTime = 0;
                timerCotroller.bStart_normal = false;
            }
        }
        else
        {
            if (timerCotroller.NormalBoxTime < 0)
            {
                timerCotroller.NormalBoxTime = 0;
                timerCotroller.bStart_normal = false;
            }
        }
        if (timerCotroller.bStart_Speacial == true && timerCotroller.SpeacialBoxTime > 0)
        {
            timerCotroller.SpeacialBoxTime -= Time.deltaTime;
            if (timerCotroller.SpeacialBoxTime < 0)
            {
                timerCotroller.SpeacialBoxTime = 0;
                timerCotroller.bStart_Speacial = false;
            }
        }
        else
        {
            if (timerCotroller.SpeacialBoxTime < 0)
            {
                timerCotroller.SpeacialBoxTime = 0;
                timerCotroller.bStart_Speacial = false;
            }
        }
        for (int i = 0; i < timerCotroller.bExpedtionTime.Count; i++)
        {
            if (timerCotroller.bExpedtionTime[i] == true && timerCotroller.ExpedtionTime[i] > 0)
            {
                timerCotroller.ExpedtionTime[i] -= Time.deltaTime;
                if (timerCotroller.ExpedtionTime[i] < 0)
                {
                    timerCotroller.ExpedtionTime[i] = 0;
                    timerCotroller.bExpedtionTime[i] = false;
                }
            }
            else
            {
                if (timerCotroller.ExpedtionTime[i] < 0)
                {
                    timerCotroller.ExpedtionTime[i] = 0;
                    timerCotroller.bExpedtionTime[i] = false;
                }
            }
        }
    }
    void CheckTimer()
    {

    }
    void CheckRaidTicket(float time = 0)
    {
        if (time == 0)
        {
            if (raidTicketCount < 3)
            {
                timerCotroller.TicketTime -= Time.deltaTime;
                if (timerCotroller.TicketTime < 0)
                {
                    raidTicketCount++;
                    //Save(saveType.raidTicketCount);
                    if (raidTicketCount < 3)
                    {
                        timerCotroller.TicketTime = RaidDefaultTIme;
                        //Save(saveType.timerCotroller);
                    }
                    else
                    {
                        timerCotroller.TicketTime = 0;
                    }
                }
            }
            else
            {
                timerCotroller.TicketTime = RaidDefaultTIme;
                //Save(saveType.timerCotroller);
            }
        }
        else
        {
            //여기 시간확인
            if (raidTicketCount < 3)
            {
                float fDeltaTime = time;
                if (time - RaidDefaultTIme >= 0)
                {
                    raidTicketCount++;
                    Save(saveType.raidTicketCount);
                    timerCotroller.TicketTime = RaidDefaultTIme;
                    fDeltaTime = time - timerCotroller.TicketTime;
                }
                float index = fDeltaTime / RaidDefaultTIme;
                timerCotroller.TicketTime -= fDeltaTime;
                if (index >= 1)
                {
                    raidTicketCount += (int)index;
                    if (raidTicketCount > 3)
                    {
                        raidTicketCount = 3;
                        timerCotroller.TicketTime = RaidDefaultTIme;
                        Save(saveType.timerCotroller);
                    }
                    Save(saveType.raidTicketCount);
                }
                if (timerCotroller.TicketTime > RaidDefaultTIme)
                {
                    timerCotroller.TicketTime = RaidDefaultTIme;
                    Save(saveType.timerCotroller);
                }
            }
            else
            {
                timerCotroller.TicketTime = RaidDefaultTIme;
                Save(saveType.timerCotroller);
            }
        }

    }
    public List<HerosInfo> UpdateCheckHeros = new List<HerosInfo>();

    public string getTime(float inputTime)
    {
        string timestr = string.Empty;
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(inputTime);
        int timecount = 0;
        if (timeSpan.Days > 0)
        {
            timestr += timeSpan.Days + " 일 ";
            timecount++;
        }
        if (timeSpan.Hours > 0)
        {
            timestr += timeSpan.Hours + " 시간 ";
            timecount++;
        }
        if (timeSpan.Minutes > 0 && timecount != 2)
        {
            timestr += timeSpan.Minutes + " 분 ";
            timecount++;
        }
        if (timeSpan.Seconds > 0 && timecount != 2)
        {
            timestr += timeSpan.Seconds + " 초 ";
            timecount++;
        }
        return timestr;
    }
    public void setSpecialBoxTime()
    {
        timerCotroller.bStart_Speacial = true;
        timerCotroller.SpeacialBoxTime = defaultSpeacialBoxTime;
        Save(saveType.timerCotroller);
    }
    public void SetbItemGetGold(bool bFlag)
    {
        bItemGetGold = bFlag;
    }
    void initData()
    {
        tickGoldChecker = 0;
        isRaidAutoPlay = false;
        MaxLevel_rank_Nanoo = 0;
        rankRenewTime = 0;
        bMakeNickName = false;
        strNickName = "Empty";
        IsNewCollection = false;
        Pack1 = false;
        MapStageIndex = 1;
        level1MapStage = 0;
        level2MapStage = 200;
        level3MapStage = 400;
        level4MapStage = 600;
        level5MapStage = 800;
        TransTierMonsterCount = 0;
        TransTier = 0;
        transTutorial = false;
        GatchaCount = 0;
        heroTransPos = -1;
        IsNewSmith = 0;
        bItemGetGold = false;
        materialCount = 0;
        TotalreBrithCount = 0;
        subQuestIndex = 0;
        TutorialIndex = 0;
        isBGM = true;
        isFx = true;
        UserID = string.Empty;
        isNewHero = false;
        isNewItem = false;
        raidTicketCount = 3;
        iLangueageType = 1;
        defaultExpeditionTime = 7200;
        defaultNormalBoxTime = 3600;
        defaultSpeacialBoxTime = 172800;
        NormalBoxCost = 50;
        SpeacialBoxCost = 150;
        RaidDefaultTIme = 3600 * 3;
        //RaidDefaultTIme = 3600;
        //RaidDefaultTIme = 30;
        timerCotroller.TicketTime = RaidDefaultTIme;
        timerCotroller.bStart_normal = false;
        timerCotroller.bStart_Speacial = true;
        timerCotroller.AttackBuffTime = 601;
        timerCotroller.bStart_attackBuff = false;
        timerCotroller.NormalBoxTime = 0;
        timerCotroller.SpeacialBoxTime = defaultSpeacialBoxTime;
        timerCotroller.ExpedtionTime = new List<float>();
        timerCotroller.bExpedtionTime = new List<bool>();

        timerCotroller.bAdsGoldTime = false;
        timerCotroller.bAdsTicketTime = false;
        timerCotroller.AdsGoldTime = 1800;
        timerCotroller.AdsTicketTime = 600;

        timerCotroller.bTrasnTicketTime = false;
        timerCotroller.TransTicketTime = 0;
        for(int i =0; i< 30; i++)
        {
            RankerData tempRank = new RankerData();
            tempRank.HeroPos = new int[10] { -1, -1, -1, -1, -1, -1, -1,-1,-1,-1};
            tempRank.nickname = "갱신 필요";
            tempRank.rank = "0";
            tempRank.Stage = "0";
            tempRank.UserID = "";
            rankerDatas.Add(tempRank);
        }
        for (int i = 0; i < 11; i++)
        {
            TutorialList.Add(0);
        }
        for (int i = 0; i < 40; i++)
        {
            Item a = new Item();
            a.isGet = false;
            items.Add(a);


            scroll temp = new scroll();
            temp.isGet = false;
            Scrolls.Add(temp);

            CollectionList.Add(0);
        }

        for (int i = 0; i < 8; i++)
        {
            timerCotroller.ExpedtionTime.Add(defaultExpeditionTime);
            timerCotroller.bExpedtionTime.Add(false);
        }
        achivementData.AchivementCount = new List<double>();
        achivementData.AchivementLevel = new List<double>();
        achivementData.AchivementMax = new List<double>();
        for (int i = 0; i < 15; i++)
        {
            achivementData.AchivementCount.Add(0);
            achivementData.AchivementLevel.Add(0);
            achivementData.AchivementMax.Add(0);
        }
        buffData.BossTImeBuff = 0;
        buffData.ClickBuff = 0;
        buffData.GoawayBuff = 0;
        buffData.GoldBuff = 0;
        buffData.RaidBuff = 0;
        bMoveNew = false;
        IsAutoPlay = true;
        MaxMonsterCount = 10;
        MonsterCount = 0;
        heroPos = new int[10] { -1, -1, -2, -2, -2, -2, -2, -2, -2, -2 };
        heroExpeditionPos = new int[8] { -2, -2, -2, -2, -2, -2, -2, -2 };
        heroExpeditionComplete = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
        heroraidPos = new int[5] { -1, -1, -1, -1, -1 };
        for (int i = 0; i < 10; i++)
        {
            SoulAblityList.Add(0);
        }
        clicker.level = 0;
        clicker.TotalClickPower = 1;
        clicker.UpgradeClickpower = 1f;
        Level = 0;
        MaxLevel = Level;
        RaidMaxLevel = 1;
        RaidNowLevel = 1;
        TotalGold = 0;
        TotalSoul = 0;
        TotalGem = 0;
        TotalAltarCoin = 0;
        AltarPercent = 0;
        AltarLevel = 0;

        //디버그용
        for (int i = 0; i < TutorialList.Count; i++)
        {
            TutorialList[i] = 0;
        }
        //TutorialList[2] = 1;
        TotalGold = 0;
        TotalGem = 40;
        for (int i = 0; i < SystemManager.data.Count; i++)
        {
            HerosInfo heros = new HerosInfo();
            //10레벨 테스트        

            heros.isGetHero = false;
            heros.HeroCount = 0;


            //heros.HeroCount = 10;
            heros.LimitLevelUpCount = 0;
            heros.Tier = SystemManager.Tiers[i];
            heros.Name = SystemManager.Names[i];
            heros.Cost = SystemManager.InitCost[i];
            heros.DPS = SystemManager.attackPower[i];
            heros.InitDPS = SystemManager.attackPower[i];
            heros.ResetDps = heros.InitDPS;
            heros.specialAblity1 = SystemManager.specialAblity_1[i];
            heros.specialAblity2 = SystemManager.specialAblity_2[i];
            heros.specialAblity3 = SystemManager.specialAblity_3[i];
            heros.isAblity_1 = false;
            heros.isAblity_2 = false;
            heros.isAblity_3 = false;
            heros.level = 0;
            heros.AwakeningCount = 0;
            heros.FameCount = 0;
            heros.FameLevel = 0;
            heros.AttackBuff = 0;
            heros.equipItem_1 = -2;
            heros.equipItem_2 = -2;
            heros.equipItem_3 = -2;
            heros.equipItem_4 = -2;
            heros.killPercent = 0;

            Item newItem = new Item();
            newItem.ablityPower = -1;
            newItem.ablityType = -1;
            newItem.itemLevel = -1;
            newItem.itemTier = -1;
            newItem.isGet = false;
            newItem.position = -1;
            newItem.time = 0;
            newItem.PChance = 0;

            heros.item_1 = newItem;
            heros.item_2 = newItem;
            heros.item_3 = newItem;
            heros.item_4 = newItem;

            heros.goldBuff = 0;
            heros.ClickBuff = 0;
            heros.goawayBuff = 0;
            heros.raidBuff = 0;
            heros.bossTimeBuff = 0;
            UpdateCheckHeros.Add(heros);
        }

      
    }
    public HerosInfo MakeHero(int i)
    {
        HerosInfo heros = new HerosInfo();
        heros.isGetHero = false;
        heros.HeroCount = 0;
        heros.LimitLevelUpCount = 0;
        heros.Tier = SystemManager.Tiers[i];
        heros.Name = SystemManager.Names[i];
        heros.Cost = SystemManager.InitCost[i];
        heros.DPS = SystemManager.attackPower[i];
        heros.InitDPS = SystemManager.attackPower[i];
        heros.ResetDps = heros.InitDPS;
        heros.specialAblity1 = SystemManager.specialAblity_1[i];
        heros.specialAblity2 = SystemManager.specialAblity_2[i];
        heros.specialAblity3 = SystemManager.specialAblity_3[i];
        heros.isAblity_1 = false;
        heros.isAblity_2 = false;
        heros.isAblity_3 = false;
        heros.level = 0;
        heros.AwakeningCount = 0;
        heros.FameCount = 0;
        heros.FameLevel = 0;
        heros.AttackBuff = 0;
        heros.equipItem_1 = -2;
        heros.equipItem_2 = -2;
        heros.equipItem_3 = -2;
        heros.equipItem_4 = -2;
        heros.killPercent = 0;
        heros.goldBuff = 0;
        heros.ClickBuff = 0;
        heros.goawayBuff = 0;
        heros.raidBuff = 0;
        heros.bossTimeBuff = 0;
        heros.fameDps = 0;
        Item newItem = new Item();
        newItem.ablityPower = -1;
        newItem.ablityType = -1;
        newItem.itemLevel = -1;
        newItem.itemTier = -1;
        newItem.isGet = false;
        newItem.position = -1;
        newItem.time = 0;
        newItem.PChance = 0;

        heros.item_1 = newItem;
        heros.item_2 = newItem;
        heros.item_3 = newItem;
        heros.item_4 = newItem;
        heros.LevelUpBuff = 0;
        //AltarDpsUpgrade(i);
        heros = AlldpsUpgradeTranscedence((int)AtrifactType.alldps, heros);
        heros = DpsUpgradeInit(heros, i);
        return heros;
    }
    private void OnApplicationQuit()
    {
        if (bSaveType == true)
        {
            while (true)
            {
                if (bSaveType == false)
                {
                    break;
                }
            }
        }
        Save(saveType.timerCotroller);
        Save(saveType.RankTime);
        Save(saveType.raidTicketCount);
   

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            if (bSaveType == true)
            {
                while (true)
                {
                    if (bSaveType == false)
                    {
                        break;
                    }
                }
            }
            Save(saveType.timerCotroller);
            Save(saveType.RankTime);
            Save(saveType.raidTicketCount);
        }
        else
        {
            //TimerManager.Instance.CheckTime();
            PlayNanooManager.Instance.GetTime();
            if(TutorialList.Count >0)
            {
                if (TutorialList[6] > 0)
                    UiManager.Instance.CheckGift();
            }
        }
    }
    public enum CollectionType
    {
        dice_1,
        dice,
        atk50,
        atk100,
        atk200,
        gold50,
        gold100,
        gold200,
        mat100,
        mat200,
        altar100,
        altar200,
        soul50,
        soul100,
        soul200,
        trasn25,
        trasn25_2,
        hero


    }
    public enum saveType
    {
        level1MapStage,
        level2MapStage,
        level3MapStage,
        level4MapStage,
        level5MapStage,
        MapStageIndex,
        TransTierMonsterCount,
        TransTier,
        transTutorial,
        GatchaCount,
        heroTransPos,
        IsNewSmith,
        materialCount,
        Scrolls,
        items,
        MaxLevel_rank,
        TotalreBrithCount,
        subQuestIndex,
        subQuestNow,
        subQuestLevel,
        TutorialList,
        isNewHero,
        isNewItem,
        TutorialIndex,
        UserID,
        raidTicketCount,
        isFx,
        isBGM,
        iLangueageType,
        achivementData,
        herosInfos,
        heroPos,
        heroExpeditionPos,
        heroExpeditionComplete,
        heroraidPos,
        IsAutoPlay,
        SoulAblityList,
        buffData,
        MaxMonsterCount,
        MonsterCount,
        Level,
        MaxLevel,
        RaidMaxLevel,
        RaidNowLevel,
        TotalGold,
        AltarLevel,
        AltarPercent,
        TotalGem,
        TotalAltarCoin,
        TotalSoul,
        clicker,
        timerCotroller,
        Pack1,
        collection,
        isNewCollection,
        NickName,
        RankTime,
        RankData,
        MaxrankNanoo,
        isRaidAutoPlay,
        tickGoldChecker
    }
    bool bAllSaveHeros = false;

    bool bSaveType = false;
    public void Save(saveType type, int heroPosition = -1)
    {
        if(bTotalSave ==true)
        {
            return;
        }
        if (bSaveType == true)
        {
            return;
        }                    
        switch (type)
        {
            case saveType.tickGoldChecker:
                bSaveType = true;
                ES3.Save("tickGoldChecker", tickGoldChecker);
                break;
            case saveType.isRaidAutoPlay:
                bSaveType = true;
                ES3.Save("isRaidAutoPlay", isRaidAutoPlay);
                break;
            case saveType.MaxrankNanoo:
                bSaveType = true;
                ES3.Save("MaxLevel_rank_Nanoo", MaxLevel_rank_Nanoo);
                ES3.Save("Stage_Nanoo", Stage_Nanoo);
                break;
            case saveType.RankData:
                bSaveType = true;
                ES3.Save("rankerDatas", rankerDatas);
                break;
            case saveType.RankTime:
                bSaveType = true;
                ES3.Save("rankRenewTime", rankRenewTime);
                break;
            case saveType.NickName:
                bSaveType = true;
                ES3.Save("bMakeNickName", bMakeNickName);
                ES3.Save("strNickName", strNickName);
                break;
            case saveType.isNewCollection:
                bSaveType = true;
                ES3.Save("IsNewCollection", IsNewCollection);
                break;
            case saveType.collection:
                bSaveType = true;
                ES3.Save("CollectionList", CollectionList);
                break;
            case saveType.Pack1:
                bSaveType = true;
                ES3.Save("Pack1", Pack1);
                break;
            case saveType.level1MapStage:
                bSaveType = true;
                ES3.Save("level1MapStage", level1MapStage);
                break;
            case saveType.level2MapStage:
                bSaveType = true;
                ES3.Save("level2MapStage", level2MapStage);
                break;
            case saveType.level3MapStage:
                bSaveType = true;
                ES3.Save("level3MapStage", level3MapStage);
                break;
            case saveType.level4MapStage:
                bSaveType = true;
                ES3.Save("level4MapStage", level4MapStage);
                break;
            case saveType.level5MapStage:
                bSaveType = true;
                ES3.Save("level5MapStage", level5MapStage);
                break;                
            case saveType.MapStageIndex:
                bSaveType = true;
                ES3.Save("MapStageIndex", MapStageIndex);
                break;
            case saveType.TransTierMonsterCount:
                bSaveType = true;
                ES3.Save("TransTierMonsterCount", TransTierMonsterCount);
                break;
            case saveType.TransTier:
                bSaveType = true;
                ES3.Save("TransTier", TransTier);
                break;
            case saveType.transTutorial:
                bSaveType = true;
                ES3.Save("transTutorial", transTutorial);
                break;
            case saveType.GatchaCount:
                bSaveType = true;
                ES3.Save("GatchaCount", GatchaCount);
                break;
            case saveType.heroTransPos:
                bSaveType = true;
                ES3.Save("heroTransPos", heroTransPos);
                break;
            case saveType.IsNewSmith:
                bSaveType = true;
                ES3.Save("IsNewSmith", IsNewSmith);
                break;
            case saveType.materialCount:
                bSaveType = true;
                ES3.Save("materialCount", materialCount);
                break;
            case saveType.Scrolls:
                bSaveType = true;
                ES3.Save("Scrolls", Scrolls);
                break;
            case saveType.items:
                bSaveType = true;
                ES3.Save("items", items);
                break;
            case saveType.MaxLevel_rank:
                bSaveType = true;
                ES3.Save("MaxLevel_rank", MaxLevel_rank);
                break;
            case saveType.TotalreBrithCount:
                bSaveType = true;
                ES3.Save("TotalreBrithCount", TotalreBrithCount);
                break;
            case saveType.subQuestIndex:
                bSaveType = true;
                ES3.Save("subQuestIndex", subQuestIndex);
                break;
            case saveType.subQuestNow:
                bSaveType = true;
                ES3.Save("subQuestNow", subQuestNow);
                break;
            case saveType.subQuestLevel:
                bSaveType = true;
                ES3.Save("subQuestLevel", subQuestLevel);
                break;
            case saveType.TutorialList:
                bSaveType = true;
                ES3.Save("TutorialList", TutorialList);
                break;
            case saveType.isNewHero:
                bSaveType = true;
                ES3.Save("isNewHero", isNewHero);
                break;
            case saveType.isNewItem:
                bSaveType = true;
                ES3.Save("isNewItem", isNewItem);
                break;
            case saveType.TutorialIndex:
                bSaveType = true;
                ES3.Save("TutorialIndex", TutorialIndex);
                break;
            case saveType.UserID:
                bSaveType = true;
                ES3.Save("UserID", UserID);
                break;
            case saveType.raidTicketCount:
                bSaveType = true;
                ES3.Save("raidTicketCount", raidTicketCount);
                break;
            case saveType.isFx:
                bSaveType = true;
                ES3.Save("isFx", isFx);
                break;
            case saveType.isBGM:
                bSaveType = true;
                ES3.Save("isBGM", isBGM);
                break;
            case saveType.iLangueageType:
                bSaveType = true;
                ES3.Save("iLangueageType", iLangueageType);
                break;
            case saveType.achivementData:
                bSaveType = true;
                ES3.Save("achivementData", achivementData);
                break;
            case saveType.herosInfos:
                SaveHerosInfo(heroPosition);
                break;
            case saveType.heroPos:
                bSaveType = true;
                string heroPosStr = "heroPos";
                ES3.Save(heroPosStr, heroPos);
                break;
            case saveType.heroExpeditionPos:
                bSaveType = true;
                ES3.Save("heroExpeditionPos", heroExpeditionPos);
                break;
            case saveType.heroExpeditionComplete:
                bSaveType = true;
                ES3.Save("heroExpeditionComplete", heroExpeditionComplete);
                break;
            case saveType.heroraidPos:
                bSaveType = true;
                ES3.Save("heroraidPos", heroraidPos);
                break;
            case saveType.IsAutoPlay:
                bSaveType = true;
                ES3.Save("IsAutoPlay", IsAutoPlay);
                break;
            case saveType.SoulAblityList:
                bSaveType = true;
                ES3.Save("SoulAblityList", SoulAblityList);
                break;
            case saveType.buffData:
                bSaveType = true;
                ES3.Save("buffData", buffData);
                break;
            case saveType.MaxMonsterCount:
                bSaveType = true;
                ES3.Save("MaxMonsterCount", MaxMonsterCount);
                break;
            case saveType.MonsterCount:
                bSaveType = true;
                ES3.Save("MonsterCount", MonsterCount);
                break;
            case saveType.Level:
                bSaveType = true;
                ES3.Save("Level", Level);
                break;
            case saveType.MaxLevel:
                bSaveType = true;
                ES3.Save("MaxLevel", MaxLevel);
                break;
            case saveType.RaidMaxLevel:
                bSaveType = true;
                ES3.Save("RaidMaxLevel", RaidMaxLevel);
                break;
            case saveType.RaidNowLevel:
                bSaveType = true;
                ES3.Save("RaidNowLevel", RaidNowLevel);
                break;
            case saveType.TotalGold:
                bSaveType = true;
                ES3.Save("TotalGold", TotalGold);
                break;
            case saveType.AltarLevel:
                bSaveType = true;
                ES3.Save("AltarLevel", AltarLevel);
                break;
            case saveType.AltarPercent:
                bSaveType = true;
                ES3.Save("AltarPercent", AltarPercent);
                break;
            case saveType.TotalGem:
                bSaveType = true;
                ES3.Save("TotalGem", TotalGem);
                break;
            case saveType.TotalAltarCoin:
                bSaveType = true;
                ES3.Save("TotalAltarCoin", TotalAltarCoin);
                break;
            case saveType.TotalSoul:
                bSaveType = true;
                ES3.Save("TotalSoul", TotalSoul);
                break;
            case saveType.clicker:
                bSaveType = true;
                ES3.Save("clicker", clicker);
                break;
            case saveType.timerCotroller:
                bSaveType = true;
                ES3.Save("timerCotroller", timerCotroller);
                break;
        }
        bSaveType = false;
    }  
    bool bSaveRoutine = false;
    bool bTotalSave = false;
    IEnumerator SaveStartRoutine()
    {
        UiManager.Instance.DataSavePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (bSaveType == true)
        {
            while (true)
            {
                if (bSaveType == false)
                {
                    break;
                }
            }
        }
        bTotalSave = true;
        ES3.CreateBackup("hoitsave_craft.es3");
        SaveData();
        UiManager.Instance.SetNotification(UiManager.NotificationType.save);
        bTotalSave = false;
    }
    public void AsyncSaveStart()
    {
        StartCoroutine(SaveStartRoutine());
    }
 

    public void SaveHerosInfo(int heroPosition)
    {
        if (bSaveRoutine == false && bSaveType ==false)
        {
            bSaveType = true;
            if (heroPosition <= -1)
                return;
            string title = "herosInfos";
            title = title + heroPosition;
            ES3.Save(title, herosInfos[heroPosition]);
            bSaveRoutine = true;
            StartCoroutine(herosInfoSaveRoutine());
        }
    }
    IEnumerator herosInfoSaveRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        bSaveRoutine = false;
    }

    public void AppQuit()
    {
        AsyncSaveStart();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else 
        UnityEngine.Application.Quit();
#endif

        
    }
    public void SaveData()
    {
        ES3.Save("isRaidAutoPlay", isRaidAutoPlay);
        ES3.Save("Stage_Nanoo", Stage_Nanoo);
        ES3.Save("MaxLevel_rank_Nanoo", MaxLevel_rank_Nanoo);
        ES3.Save("rankerDatas", rankerDatas);
        ES3.Save("rankRenewTime", rankRenewTime);
        ES3.Save("IsNewCollection", IsNewCollection);
        ES3.Save("CollectionList", CollectionList);
        ES3.Save("Pack1", Pack1);
        ES3.Save("level1MapStage", level1MapStage);
        ES3.Save("level2MapStage", level2MapStage);
        ES3.Save("level3MapStage", level3MapStage);
        ES3.Save("level4MapStage", level4MapStage);
        ES3.Save("level5MapStage", level5MapStage);

        ES3.Save("MapStageIndex", MapStageIndex);
        ES3.Save("TransTierMonsterCount", TransTierMonsterCount);
        ES3.Save("TransTier", TransTier);
        ES3.Save("transTutorial", transTutorial);
        ES3.Save("GatchaCount", GatchaCount);
        ES3.Save("heroTransPos", heroTransPos);
        ES3.Save("IsNewSmith", IsNewSmith);
        ES3.Save("materialCount", materialCount);
        ES3.Save("Scrolls", Scrolls);
        ES3.Save("items", items);
        ES3.Save("MaxLevel_rank", MaxLevel_rank);
        ES3.Save("TotalreBrithCount", TotalreBrithCount);
        ES3.Save("subQuestIndex", subQuestIndex);
        ES3.Save("subQuestNow", subQuestNow);
        ES3.Save("subQuestLevel", subQuestLevel);
        ES3.Save("TutorialList", TutorialList);
        ES3.Save("isNewHero", isNewHero);
        ES3.Save("isNewItem", isNewItem);
        ES3.Save("TutorialIndex", TutorialIndex);
        ES3.Save("UserID", UserID);
        ES3.Save("raidTicketCount", raidTicketCount);
        ES3.Save("isFx", isFx);
        ES3.Save("isBGM", isBGM);
        ES3.Save("iLangueageType", iLangueageType);
        
        ES3.Save("achivementData", achivementData);
        string title = "herosInfos" ;
        ES3.Save(title, herosInfos);
        string heroPosStr = "heroPos";
        ES3.Save(heroPosStr, heroPos);

        ES3.Save("heroExpeditionPos", heroExpeditionPos);
        ES3.Save("heroExpeditionComplete", heroExpeditionComplete);
        ES3.Save("heroraidPos", heroraidPos);

        ES3.Save("IsAutoPlay", IsAutoPlay);
        ES3.Save("SoulAblityList", SoulAblityList);
        ES3.Save("buffData", buffData);

        ES3.Save("MaxMonsterCount", MaxMonsterCount);
        ES3.Save("MonsterCount", MonsterCount);
        ES3.Save("Level", Level);
        ES3.Save("MaxLevel", MaxLevel);

        ES3.Save("RaidMaxLevel", RaidMaxLevel);
        ES3.Save("RaidNowLevel", RaidNowLevel);
        ES3.Save("TotalGold", TotalGold);
        ES3.Save("AltarLevel", AltarLevel);
        ES3.Save("AltarPercent", AltarPercent);
        ES3.Save("TotalGem", TotalGem);
        ES3.Save("TotalAltarCoin", TotalAltarCoin);
        ES3.Save("TotalSoul", TotalSoul);
        ES3.Save("clicker", clicker);
        ES3.Save("timerCotroller", timerCotroller);

        bSave = true;
        ES3.Save("bSave", bSave);
        
        //Debug.Log("저장 완료");
    }
    
    void LoadData()
    {
        
        string title = "herosInfos";
        try
        {
            if (ES3.KeyExists(title))
            {
                herosInfos = (ES3.Load<List<HerosInfo>>(title));
                int count = herosInfos.Count;
                //중간에 추가 확인
                if (UpdateCheckHeros.Count > herosInfos.Count)
                {

                    for (int i = 0; i < herosInfos.Count; i++)
                    {
                        if (herosInfos[i].Name != UpdateCheckHeros[i].Name)
                        {
                            herosInfos.Insert(i, UpdateCheckHeros[i]);

                        }
                    }
                }
                //끝부분 추가확인
                if (UpdateCheckHeros.Count > herosInfos.Count)
                {
                    for (int i = 0; i < UpdateCheckHeros.Count - count; i++)
                    {
                        herosInfos.Add(MakeHero(count + i));
                    }
                }

            }
            else
            {
                herosInfos = UpdateCheckHeros;
            }
            if (ES3.KeyExists("MaxLevel_rank_Nanoo"))
            {
                MaxLevel_rank_Nanoo = ES3.Load<int>("MaxLevel_rank_Nanoo");
            }
            if (ES3.KeyExists("Stage_Nanoo"))
            {
                Stage_Nanoo = ES3.Load<int>("Stage_Nanoo");
            }

            if (ES3.KeyExists("rankRenewTime"))
            {
                rankRenewTime = ES3.Load<float>("rankRenewTime");
            }
            if (ES3.KeyExists("IsNewCollection"))
            {
                IsNewCollection = ES3.Load<bool>("IsNewCollection");
            }
            if (ES3.KeyExists("CollectionList"))
            {
                CollectionList = ES3.Load<List<int>>("CollectionList");
            }
            if (ES3.KeyExists("level1MapStage"))
            {
                level1MapStage = ES3.Load<int>("level1MapStage");
            }
            if (ES3.KeyExists("rankerDatas"))
            {
                rankerDatas = ES3.Load<List<RankerData>>("rankerDatas");
            }
            if (ES3.KeyExists("level2MapStage"))
            {
                level2MapStage = ES3.Load<int>("level2MapStage");
            }
            if (ES3.KeyExists("level3MapStage"))
            {
                level3MapStage = ES3.Load<int>("level3MapStage");
            }
            if (ES3.KeyExists("level4MapStage"))
            {
                level4MapStage = ES3.Load<int>("level4MapStage");
            }
            if (ES3.KeyExists("level5MapStage"))
            {
                level5MapStage = ES3.Load<int>("level5MapStage");
            }

            if (ES3.KeyExists("bMakeNickName"))
            {
                bMakeNickName = ES3.Load<bool>("bMakeNickName");
            }
            if (ES3.KeyExists("strNickName"))
            {
                strNickName = ES3.Load<string>("strNickName");
            }

            if (ES3.KeyExists("MapStageIndex"))
            {
                MapStageIndex = ES3.Load<int>("MapStageIndex");
            }
            if (ES3.KeyExists("Pack1"))
            {
                Pack1 = ES3.Load<bool>("Pack1");
            }
            if (ES3.KeyExists("TransTierMonsterCount"))
            {
                TransTierMonsterCount = ES3.Load<int>("TransTierMonsterCount");
            }
            if (ES3.KeyExists("transTutorial"))
            {
                transTutorial = ES3.Load<bool>("transTutorial");
            }
            if (ES3.KeyExists("heroTransPos"))
            {
                heroTransPos = ES3.Load<int>("heroTransPos");
            }
            if (ES3.KeyExists("IsNewSmith"))
            {
                IsNewSmith = ES3.Load<int>("IsNewSmith");
            }
            if (ES3.KeyExists("GatchaCount"))
            {
                GatchaCount = ES3.Load<int>("GatchaCount");
            }
            if (ES3.KeyExists("materialCount"))
            {
                materialCount = ES3.Load<int>("materialCount");
            }
            if (ES3.KeyExists("Scrolls"))
            {
                Scrolls = ES3.Load<List<scroll>>("Scrolls");
            }
            if (ES3.KeyExists("items"))
            {
                items = ES3.Load<List<Item>>("items");
            }
            if (ES3.KeyExists("TransTier"))
            {
                TransTier = ES3.Load<int>("TransTier");
            }
            if (ES3.KeyExists("TutorialList"))
            {
                TutorialList = ES3.Load<List<int>>("TutorialList");
            }

            if (ES3.KeyExists("SoulAblityList"))
            {
                SoulAblityList = ES3.Load<List<double>>("SoulAblityList");
            }
            if (ES3.KeyExists("achivementData"))
            {
                achivementData = ES3.Load<AchivementData>("achivementData");
            }
            if (ES3.KeyExists("MaxLevel_rank"))
            {
                MaxLevel_rank = ES3.Load<int>("MaxLevel_rank");
            }
            if (ES3.KeyExists("subQuestIndex"))
            {
                subQuestIndex = ES3.Load<int>("subQuestIndex");
            }
            if (ES3.KeyExists("TotalreBrithCount"))
            {
                TotalreBrithCount = ES3.Load<int>("TotalreBrithCount");
            }
            if (ES3.KeyExists("subQuestLevel"))
            {
                subQuestLevel = ES3.Load<int>("subQuestLevel");
            }
            if (ES3.KeyExists("subQuestNow"))
            {
                subQuestNow = ES3.Load<int>("subQuestNow");
            }
            if (ES3.KeyExists("TutorialIndex"))
            {
                TutorialIndex = ES3.Load<int>("TutorialIndex");
            }

            if (ES3.KeyExists("UserID"))
            {
                UserID = ES3.Load<string>("UserID");
            }

            if (ES3.KeyExists("buffData"))
            {
                buffData = ES3.Load<BuffData>("buffData");
            }

            if (ES3.KeyExists("MaxMonsterCount"))
            {
                MaxMonsterCount = ES3.Load<int>("MaxMonsterCount");
            }
            if (ES3.KeyExists("MonsterCount"))
            {
                MonsterCount = ES3.Load<int>("MonsterCount");
            }
            if (ES3.KeyExists("Level"))
            {
                Level = ES3.Load<int>("Level");
            }
            if (ES3.KeyExists("raidTicketCount"))
            {
                raidTicketCount = ES3.Load<int>("raidTicketCount");
            }
            if (ES3.KeyExists("MaxLevel"))
            {
                MaxLevel = ES3.Load<int>("MaxLevel");
            }
            if (ES3.KeyExists("RaidMaxLevel"))
            {
                RaidMaxLevel = ES3.Load<int>("RaidMaxLevel");
            }
            if (ES3.KeyExists("RaidNowLevel"))
            {
                RaidNowLevel = ES3.Load<int>("RaidNowLevel");
            }
            if (ES3.KeyExists("TotalGold"))
            {
                TotalGold = ES3.Load<InfiniCoin>("TotalGold");
            }
            if (ES3.KeyExists("TotalGem"))
            {
                TotalGem = ES3.Load<InfiniCoin>("TotalGem");
            }
            if (ES3.KeyExists("TotalAltarCoin"))
            {
                TotalAltarCoin = ES3.Load<InfiniCoin>("TotalAltarCoin");
            }
            if(ES3.KeyExists("tickGoldChecker"))
            {
                tickGoldChecker = ES3.Load<InfiniCoin>("tickGoldChecker");
            }
            if (ES3.KeyExists("AltarLevel"))
            {
                AltarLevel = ES3.Load<int>("AltarLevel");
            }
            if (ES3.KeyExists("AltarPercent"))
            {
                AltarPercent = ES3.Load<float>("AltarPercent");
            }
            if (ES3.KeyExists("TotalSoul"))
            {
                TotalSoul = ES3.Load<InfiniCoin>("TotalSoul");
            }
            if (ES3.KeyExists("IsAutoPlay"))
            {
                IsAutoPlay = ES3.Load<bool>("IsAutoPlay");
            }
            if (ES3.KeyExists("clicker"))
            {
                clicker = ES3.Load<Clicker>("clicker");
            }
            if (ES3.KeyExists("timerCotroller"))
            {
                timerCotroller = ES3.Load<TimerCotroller>("timerCotroller");
            }
            if (ES3.KeyExists("heroExpeditionPos"))
            {
                heroExpeditionPos = ES3.Load<int[]>("heroExpeditionPos");
            }
            if (ES3.KeyExists("heroExpeditionComplete"))
            {
                heroExpeditionComplete = ES3.Load<int[]>("heroExpeditionComplete");
            }
            if (ES3.KeyExists("heroraidPos"))
            {
                heroraidPos = ES3.Load<int[]>("heroraidPos");
            }

            if (ES3.KeyExists("isFx"))
            {
                isFx = ES3.Load<bool>("isFx");
            }
            if (ES3.KeyExists("isBGM"))
            {
                isBGM = ES3.Load<bool>("isBGM");
            }
            if (ES3.KeyExists("iLangueageType"))
            {
                iLangueageType = ES3.Load<int>("iLangueageType");
            }

            if (ES3.KeyExists("isNewHero"))
            {
                isNewHero = ES3.Load<bool>("isNewHero");
            }

            if (ES3.KeyExists("isNewItem"))
            {
                isNewItem = ES3.Load<bool>("isNewItem");
            }
            string heroPosStr = "heroPos";
            if (ES3.KeyExists(heroPosStr))
            {
                heroPos = ES3.Load<int[]>(heroPosStr);
                for (int i = 0; i < heroPos.Length; i++)
                {
                    if (heroPos[i] > -1)
                    {
                        InitSetHeroPos(heroPos[i], i);
                    }

                }
            }
            
            if (ES3.KeyExists("isRaidAutoPlay"))
            {
                isRaidAutoPlay = ES3.Load<bool>("isRaidAutoPlay");
            }
            if(herosInfos.Count >0)
            {
                for(int i=0;i< herosInfos.Count;i++)
                {
                    string newSaveHeros = "herosInfos";
                    newSaveHeros = newSaveHeros + i.ToString();
                    if (ES3.KeyExists(newSaveHeros))
                    {
                        herosInfos[i] = ES3.Load<HerosInfo>(newSaveHeros);
                    }
                    
                }
            }
            ES3.CreateBackup("hoitsave_craft.es3");
        }
        catch
        {
            if (ES3.RestoreBackup("hoitsave_craft.es3"))
            {
                BackupLoad();
            }
        }
     
        
    }
    void BackupLoad()
    {
        Debug.Log("<color=red>==============backup load =============</color>");
        string title = "herosInfos";
        if (ES3.KeyExists(title))
        {
            herosInfos = (ES3.Load<List<HerosInfo>>(title));
            int count = herosInfos.Count;
            //중간에 추가 확인
            if (UpdateCheckHeros.Count > herosInfos.Count)
            {

                for (int i = 0; i < herosInfos.Count; i++)
                {
                    if (herosInfos[i].Name != UpdateCheckHeros[i].Name)
                    {
                        herosInfos.Insert(i, UpdateCheckHeros[i]);

                    }
                }
            }
            //끝부분 추가확인
            if (UpdateCheckHeros.Count > herosInfos.Count)
            {
                for (int i = 0; i < UpdateCheckHeros.Count - count; i++)
                {
                    herosInfos.Add(MakeHero(count + i));
                }
            }

        }
        else
        {
            herosInfos = UpdateCheckHeros;
        }
        if (ES3.KeyExists("MaxLevel_rank_Nanoo"))
        {
            MaxLevel_rank_Nanoo = ES3.Load<int>("MaxLevel_rank_Nanoo");
        }
        if (ES3.KeyExists("Stage_Nanoo"))
        {
            Stage_Nanoo = ES3.Load<int>("Stage_Nanoo");
        }

        if (ES3.KeyExists("rankRenewTime"))
        {
            rankRenewTime = ES3.Load<float>("rankRenewTime");
        }
        if (ES3.KeyExists("IsNewCollection"))
        {
            IsNewCollection = ES3.Load<bool>("IsNewCollection");
        }
        if (ES3.KeyExists("CollectionList"))
        {
            CollectionList = ES3.Load<List<int>>("CollectionList");
        }
        if (ES3.KeyExists("level1MapStage"))
        {
            level1MapStage = ES3.Load<int>("level1MapStage");
        }
        if (ES3.KeyExists("rankerDatas"))
        {
            rankerDatas = ES3.Load<List<RankerData>>("rankerDatas");
        }
        if (ES3.KeyExists("level2MapStage"))
        {
            level2MapStage = ES3.Load<int>("level2MapStage");
        }
        if (ES3.KeyExists("level3MapStage"))
        {
            level3MapStage = ES3.Load<int>("level3MapStage");
        }
        if (ES3.KeyExists("level4MapStage"))
        {
            level4MapStage = ES3.Load<int>("level4MapStage");
        }
        if (ES3.KeyExists("level5MapStage"))
        {
            level5MapStage = ES3.Load<int>("level5MapStage");
        }

        if (ES3.KeyExists("bMakeNickName"))
        {
            bMakeNickName = ES3.Load<bool>("bMakeNickName");
        }
        if (ES3.KeyExists("strNickName"))
        {
            strNickName = ES3.Load<string>("strNickName");
        }

        if (ES3.KeyExists("MapStageIndex"))
        {
            MapStageIndex = ES3.Load<int>("MapStageIndex");
        }
        if (ES3.KeyExists("Pack1"))
        {
            Pack1 = ES3.Load<bool>("Pack1");
        }
        if (ES3.KeyExists("TransTierMonsterCount"))
        {
            TransTierMonsterCount = ES3.Load<int>("TransTierMonsterCount");
        }
        if (ES3.KeyExists("transTutorial"))
        {
            transTutorial = ES3.Load<bool>("transTutorial");
        }
        if (ES3.KeyExists("heroTransPos"))
        {
            heroTransPos = ES3.Load<int>("heroTransPos");
        }
        if (ES3.KeyExists("IsNewSmith"))
        {
            IsNewSmith = ES3.Load<int>("IsNewSmith");
        }
        if (ES3.KeyExists("GatchaCount"))
        {
            GatchaCount = ES3.Load<int>("GatchaCount");
        }
        if (ES3.KeyExists("materialCount"))
        {
            materialCount = ES3.Load<int>("materialCount");
        }
        if (ES3.KeyExists("Scrolls"))
        {
            Scrolls = ES3.Load<List<scroll>>("Scrolls");
        }
        if (ES3.KeyExists("items"))
        {
            items = ES3.Load<List<Item>>("items");
        }
        if (ES3.KeyExists("TransTier"))
        {
            TransTier = ES3.Load<int>("TransTier");
        }
        if (ES3.KeyExists("TutorialList"))
        {
            TutorialList = ES3.Load<List<int>>("TutorialList");
        }

        if (ES3.KeyExists("SoulAblityList"))
        {
            SoulAblityList = ES3.Load<List<double>>("SoulAblityList");
        }
        if (ES3.KeyExists("achivementData"))
        {
            achivementData = ES3.Load<AchivementData>("achivementData");
        }
        if (ES3.KeyExists("MaxLevel_rank"))
        {
            MaxLevel_rank = ES3.Load<int>("MaxLevel_rank");
        }
        if (ES3.KeyExists("subQuestIndex"))
        {
            subQuestIndex = ES3.Load<int>("subQuestIndex");
        }
        if (ES3.KeyExists("TotalreBrithCount"))
        {
            TotalreBrithCount = ES3.Load<int>("TotalreBrithCount");
        }
        if (ES3.KeyExists("subQuestLevel"))
        {
            subQuestLevel = ES3.Load<int>("subQuestLevel");
        }
        if (ES3.KeyExists("subQuestNow"))
        {
            subQuestNow = ES3.Load<int>("subQuestNow");
        }
        if (ES3.KeyExists("TutorialIndex"))
        {
            TutorialIndex = ES3.Load<int>("TutorialIndex");
        }

        if (ES3.KeyExists("UserID"))
        {
            UserID = ES3.Load<string>("UserID");
        }

        if (ES3.KeyExists("buffData"))
        {
            buffData = ES3.Load<BuffData>("buffData");
        }

        if (ES3.KeyExists("MaxMonsterCount"))
        {
            MaxMonsterCount = ES3.Load<int>("MaxMonsterCount");
        }
        if (ES3.KeyExists("MonsterCount"))
        {
            MonsterCount = ES3.Load<int>("MonsterCount");
        }
        if (ES3.KeyExists("Level"))
        {
            Level = ES3.Load<int>("Level");
        }
        if (ES3.KeyExists("raidTicketCount"))
        {
            raidTicketCount = ES3.Load<int>("raidTicketCount");
        }
        if (ES3.KeyExists("MaxLevel"))
        {
            MaxLevel = ES3.Load<int>("MaxLevel");
        }
        if (ES3.KeyExists("RaidMaxLevel"))
        {
            RaidMaxLevel = ES3.Load<int>("RaidMaxLevel");
        }
        if (ES3.KeyExists("RaidNowLevel"))
        {
            RaidNowLevel = ES3.Load<int>("RaidNowLevel");
        }
        if (ES3.KeyExists("TotalGold"))
        {
            TotalGold = ES3.Load<InfiniCoin>("TotalGold");
        }
        if (ES3.KeyExists("TotalGem"))
        {
            TotalGem = ES3.Load<InfiniCoin>("TotalGem");
        }
        if (ES3.KeyExists("TotalAltarCoin"))
        {
            TotalAltarCoin = ES3.Load<InfiniCoin>("TotalAltarCoin");
        }
        if (ES3.KeyExists("AltarLevel"))
        {
            AltarLevel = ES3.Load<int>("AltarLevel");
        }
        if (ES3.KeyExists("AltarPercent"))
        {
            AltarPercent = ES3.Load<float>("AltarPercent");
        }
        if (ES3.KeyExists("TotalSoul"))
        {
            TotalSoul = ES3.Load<InfiniCoin>("TotalSoul");
        }
        if (ES3.KeyExists("IsAutoPlay"))
        {
            IsAutoPlay = ES3.Load<bool>("IsAutoPlay");
        }
        if (ES3.KeyExists("clicker"))
        {
            clicker = ES3.Load<Clicker>("clicker");
        }
        if (ES3.KeyExists("timerCotroller"))
        {
            timerCotroller = ES3.Load<TimerCotroller>("timerCotroller");
        }
        if (ES3.KeyExists("heroExpeditionPos"))
        {
            heroExpeditionPos = ES3.Load<int[]>("heroExpeditionPos");
        }
        if (ES3.KeyExists("heroExpeditionComplete"))
        {
            heroExpeditionComplete = ES3.Load<int[]>("heroExpeditionComplete");
        }
        if (ES3.KeyExists("heroraidPos"))
        {
            heroraidPos = ES3.Load<int[]>("heroraidPos");
        }

        if (ES3.KeyExists("isFx"))
        {
            isFx = ES3.Load<bool>("isFx");
        }
        if (ES3.KeyExists("isBGM"))
        {
            isBGM = ES3.Load<bool>("isBGM");
        }
        if (ES3.KeyExists("iLangueageType"))
        {
            iLangueageType = ES3.Load<int>("iLangueageType");
        }

        if (ES3.KeyExists("isNewHero"))
        {
            isNewHero = ES3.Load<bool>("isNewHero");
        }

        if (ES3.KeyExists("isNewItem"))
        {
            isNewItem = ES3.Load<bool>("isNewItem");
        }
        if (ES3.KeyExists("isRaidAutoPlay"))
        {
            isRaidAutoPlay = ES3.Load<bool>("isRaidAutoPlay");
        }

        string heroPosStr = "heroPos";
        if (ES3.KeyExists(heroPosStr))
        {
            heroPos = ES3.Load<int[]>(heroPosStr);
            for (int i = 0; i < heroPos.Length; i++)
            {
                if (heroPos[i] > -1)
                {
                    InitSetHeroPos(heroPos[i], i);
                }

            }
        }
        if (herosInfos.Count > 0)
        {
            for (int i = 0; i < herosInfos.Count; i++)
            {
                string newSaveHeros = "herosInfos";
                newSaveHeros = newSaveHeros + i.ToString();
                if (ES3.KeyExists(newSaveHeros))
                {
                    herosInfos[i] = ES3.Load<HerosInfo>(newSaveHeros);
                }

            }
        }

        ES3.CreateBackup("hoitsave_craft.es3");
    }
    private void Update()
    {      
    }
    public int SelectHeroPos = -1;
    void InitSetHeroPos(int heropos,int index)
    {        
        Vector3 heroposition = HeroPosTransfrom[index].position;
        heroposition.y += 0.45f;
        HeroList[heropos].transform.position = heroposition;
        HeroList[heropos].GetComponent<Hero>().SetOriPos();
        HeroList[heropos].SetActive(true);
    }
    bool isPosHero()
    {
        for(int i=0; i < heroPos.Length;i++)
        {
            if(heroPos[i] >-1)
            {
                return true;
            }
        }
        return false;
    }
    public void SetRaidCamera()
    {
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.y =-19f;
        
        cameraPos.x = 20;      
      
        mainCamera.transform.position = cameraPos;
        mainCamera.orthographicSize = 6.5f;
    }
    public void ImidiMoveCamera()
    {
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.y = 0f;
        cameraPos.x = 0;
        if (isPosHero() == true|| bInitCamera==false)
        {
            cameraPos.x = 0;
        }
        else
        {
            //cameraPos.x = 1.5f;
        }
        mainCamera.orthographicSize = 5f;
        mainCamera.transform.position = cameraPos;
    }
    public void MoveCamera(bool isDefault)
    {
        Vector3 cameraPos = mainCamera.transform.position;
        if (isPosHero() == true || bInitCamera ==false)
        {
            cameraPos.x = 0;
        }
        else
        {
            //cameraPos.x = 1.5f;
        }
        if (isDefault ==true)
        {            
            // y -0.9;
            cameraPos.y = 0f;         
            mainCamera.transform.DOMove(cameraPos, 0.5f).SetEase(Ease.OutFlash);
            //mainCamera.transform.position = cameraPos;
        }
        else
        {
            //y = 1.4f
            cameraPos.y = 0f;          
            mainCamera.transform.DOMove(cameraPos, 0.5f).SetEase(Ease.OutFlash);
            //mainCamera.transform.position = cameraPos;
        }
        mainCamera.orthographicSize =5f;
    }
    bool bInitCamera = true;
    public void SetXMoveCamera(bool flag)
    {
        bInitCamera = flag;
        Vector3 cameraPos = mainCamera.transform.position;
        if (isPosHero() == true)
        {
            cameraPos.x = 0;
        }
        else
        {
            if(flag == true)
            {
                //cameraPos.x = 1.5f;
            }
            else
            {
                cameraPos.x = 0f;
            }
            
        }
        mainCamera.orthographicSize = 5f;
        mainCamera.transform.DOMove(cameraPos, 0.5f).SetEase(Ease.OutFlash);            
        
    }
    public void UnsetHero(int heroIndex,int iheroPos)
    {
        HeroList[heroIndex].transform.position = new Vector3(-20, -5, 0);
        UnsetBuff(heroPos[iheroPos]);
        heroPos[iheroPos] = -1;
        HeroList[heroIndex].SetActive(false);
        Save(saveType.heroPos);
    }
    public void UnsetHeroRaid(int heroPos)
    {
        heroraidPos[heroPos] = -1;
        Save(saveType.heroraidPos);
    }
    public void UnsetExpedition(int heroPos)
    {
        heroExpeditionPos[heroPos] = -1;
        heroExpeditionComplete[heroPos] = -1;

        timerCotroller.bExpedtionTime[heroPos] = false;
        timerCotroller.ExpedtionTime[heroPos] = defaultExpeditionTime;
        Save(saveType.timerCotroller);
        Save(saveType.heroExpeditionPos);
        Save(saveType.heroExpeditionComplete);
    }
    public void SetHeroPos(int heroIndex)
    {
        if (SelectHeroPos == -1)
        {
            UiManager.Instance.SetNotification("위치를 선택하세요");
            return;            
        }
            
        if(heroPos[SelectHeroPos] >-1)
        {
            HeroList[heroPos[SelectHeroPos]].transform.position = new Vector3(-20, -5, 0);            
            HeroList[heroPos[SelectHeroPos]].SetActive(false);
        }
        if (heroIndex == -1)
        {            
            HeroList[heroPos[SelectHeroPos]].transform.position = new Vector3(-20, -5, 0);
            UnsetBuff(heroPos[SelectHeroPos]);
            heroPos[SelectHeroPos] = -1;
            UiManager.Instance.SetSelectHeroPanel();            
            HeroPosTransfrom[SelectHeroPos].GetComponent<SelectPosSrc>().CheckButton();
            Save(saveType.heroPos);
            return;
        }
            
        for(int i=0; i< heroPos.Length; i++)
        {
            if(heroPos[i] == heroIndex)
            {
                heroPos[i] = -1;
            }
        }
        heroPos[SelectHeroPos] = heroIndex;
        Save(saveType.heroPos);
        Vector3 heroposition = HeroPosTransfrom[SelectHeroPos].position;
        heroposition.y += 0.45f;
        HeroList[heroPos[SelectHeroPos]].transform.position = heroposition;
        HeroList[heroPos[SelectHeroPos]].GetComponent<Hero>().SetOriPos();
        HeroList[heroPos[SelectHeroPos]].SetActive(true);
        HeroPosTransfrom[SelectHeroPos].GetComponent<SelectPosSrc>().CheckButton();
        SetBuff(heroPos[SelectHeroPos]);

        SelectHeroPos = -1;


    }
    public void CheckPostionBuff(int heroIndex)
    {
        for(int i =0; i< heroPos.Length;i++)
        {
            if(heroPos[i] == heroIndex)
            {
                SetBuff(heroIndex);
            }
        }
    } 
    public void ClickBuff(float power)
    {

    }
    void SetBuff(int index)
    {
        buffData.GoldBuff += herosInfos[index].goldBuff;
        buffData.BossTImeBuff += herosInfos[index].bossTimeBuff;
        buffData.ClickBuff += herosInfos[index].ClickBuff;
        buffData.RaidBuff += herosInfos[index].raidBuff;
        buffData.GoawayBuff += herosInfos[index].goawayBuff;
        if(buffData.ClickBuff >0)
            clicker.TotalClickPower = clicker.TotalClickPower + (clicker.TotalClickPower * buffData.ClickBuff);

        //Debug.Log("골드 버프 = " + buffData.GoldBuff);
        //Debug.Log("보스 타임 버프 = " + buffData.BossTImeBuff);
        //Debug.Log("클릭 버프 = " + buffData.ClickBuff);
        //Debug.Log("레이드 버프 = " + buffData.RaidBuff);
        //Debug.Log("원정대 버프 = " + buffData.GoawayBuff);
    }
    void UnsetBuff(int index)
    {
        buffData.GoldBuff -= herosInfos[index].goldBuff;
        buffData.BossTImeBuff -= herosInfos[index].bossTimeBuff;
        buffData.ClickBuff -= herosInfos[index].ClickBuff;
        buffData.RaidBuff -= herosInfos[index].raidBuff;
        buffData.GoawayBuff -= herosInfos[index].goawayBuff;
        if (buffData.ClickBuff > 0)
            clicker.TotalClickPower = clicker.TotalClickPower - (clicker.TotalClickPower * buffData.ClickBuff);

        //Debug.Log("골드 버프 = " + buffData.GoldBuff);
        //Debug.Log("보스 타임 버프 = " + buffData.BossTImeBuff);
        //Debug.Log("클릭 버프 = " + buffData.ClickBuff);
        //Debug.Log("레이드 버프 = " + buffData.RaidBuff);
        //Debug.Log("원정대 버프 = " + buffData.GoawayBuff);
    }

    public enum HeroStatus
    {
        idle,
        move,
        attack
    }

    public enum HeroCard { Normal, Advanced, Rare, Hero, Legend }
    Dictionary<HeroCard, int> m_Cards = new Dictionary<HeroCard, int>();
    public HeroCard m_Card = HeroCard.Normal;
    public void MakeHeroCardRand()
    {       
        m_Cards.Add(HeroCard.Normal, 140); // 70        
        m_Cards.Add(HeroCard.Advanced, 32); //16        
        m_Cards.Add(HeroCard.Rare, 24); //12        
        m_Cards.Add(HeroCard.Hero, 3); //1.5        
        m_Cards.Add(HeroCard.Legend,1);  //0.5

        for(int i =0; i< herosInfos.Count; i++)
        {
            if(herosInfos[i].Tier == "n")
            {
                Lv1HeroIndex.Add(i);
            }
            if (herosInfos[i].Tier == "a")
            {
                Lv2HeroIndex.Add(i);
            }
            if (herosInfos[i].Tier == "r")
            {
                Lv3HeroIndex.Add(i);
            }
            if (herosInfos[i].Tier == "h")
            {
                Lv4HeroIndex.Add(i);
            }
            if (herosInfos[i].Tier == "l")
            {
                Lv5HeroIndex.Add(i);
            }

        }
    }
    void MakeNamingSystem()
    {
        ICConfig.RemoveLeadingNames();
        Dictionary<long, string> customNames = new Dictionary<long, string>();
        string[] alphabets = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        int iNameCount = 1;
        string strname = "";
        string strKName = "";
        customNames.Add(0, "");
        for (int i = 0; i < 26; i++)
        {
            for (int k = 0; k < 26; k++)
            {
                strKName = strname + alphabets[k];
                customNames.Add(iNameCount, strKName);
                iNameCount++;
            }
            strname = alphabets[i];
        }
        ICConfig.OverrideNames(customNames);

    }
    public HeroCard GetRandomHero()
    {
        m_Card = WeightedRandomizer.From(m_Cards).TakeOne();
        return m_Card;
    }
    public void AttackHp(float hpPercent)
    {
        enemyManager.PercentAttack(hpPercent);
    }
    public void Attack(InfiniCoin dps,bool bCritlal, GameObject target)
    {
        if(timerCotroller.bStart_attackBuff ==true)
        {
            enemyManager.Attack(dps*2, bCritlal, target);
        }
        else
        {
            enemyManager.Attack(dps, bCritlal, target);
        }
        
    }
    public void AttackTrans(InfiniCoin dps, bool bCritlal, GameObject target)
    {
        if (timerCotroller.bStart_attackBuff == true)
        {
            transenemyManager.Attack(dps * 2, bCritlal, target);
        }
        else
        {
            transenemyManager.Attack(dps, bCritlal, target);
        }

    }
    public void AttackKill()
    {
        enemyManager.AttackKill();
    }
    public void AttackKillTrans()
    {
        transenemyManager.AttackKill();
    }
    int CheckMaxMonsterCount()
    {
        //if()
        return 10;
    }
    public int GetMonsterCount()
    {
        if (MaxLevel == Level)
        {
            MaxMonsterCount = CheckMaxMonsterCount();
            return MonsterCount;
        }
        else
        {
            if ((Level+1) % 5 == 0)
            {
                MaxMonsterCount = 1;                
                return 1;
            }
            else
            {
                return CheckMaxMonsterCount();
            }
        }        
    }
    public int GetMaxMonsterCount()
    {
        if ((Level + 1) % 5 == 0)
        {
            MaxMonsterCount = 1;
            //if (MaxLevel == Level)
            //    UiManager.Instance.bossEffectAnim.SetActive(true);
            return 1;
        }
        else
        {            
            return CheckMaxMonsterCount();
        }
    }
    public bool bStartMove = false;
    public int GetSubQuestMaxStage()
    {
        int subStage = (1 + subQuestLevel) * 10;
        return subStage;
    }
    public bool bEndStage = false;
    public void CheckKillMonster()
    {
        bEndStage = false;
        if (MaxLevel == Level)
        {
            UiManager.Instance.clearConroller.LevelUPSet(MonsterCount);
            MonsterCount++;
            Save(saveType.MonsterCount);
            bStartMove = true;
        }
        if(MonsterCount >= MaxMonsterCount && MaxLevel == Level)
        {
            MonsterCount = 0;            
            MaxLevel++;
            Save(saveType.MonsterCount);
            Save(saveType.MaxLevel);
            int maxStageIndex = (MapStageIndex * 200)-1;
            if(Level>= maxStageIndex)
            {
                Level = maxStageIndex;
                Save(saveType.Level);
                //여기서 최초 보상
                mapStageClearSrc.gameObject.SetActive(true);
                mapStageClearSrc.SetData(MapStageIndex);
                //UiManager.Instance.SetNotification(UiManager.NotificationType.Map);
                bEndStage = true;
            }
            if(MaxLevel_rank < MaxLevel)
            {
                MaxLevel_rank = MaxLevel;
                achivementData.AchivementCount[8] = MaxLevel_rank;
                Save(saveType.MaxLevel_rank);
                Save(saveType.achivementData);
            }
            if(UiManager.Instance.RaidCanvas.activeSelf ==false && UiManager.Instance.TransDungeonCanvas.activeSelf ==false && mapStageClearSrc.gameObject.activeSelf==false)
            {                
                //if(bEndStage ==false)
                {
                    UiManager.Instance.StageClearPanel.SetActive(true);
                }
                
            }                
            if (MaxLevel >=70 && TutorialList[10] ==0)
            {
                TutorialList[10] = 1;
                UiManager.Instance.CheckTurorialLock();
                UiManager.Instance.NewContentEventPanel.SetActive(true);
                UiManager.Instance.NewContentEventPanel.GetComponent<NewContentEventSrc>().SetData("trans");
            }            
            AutoNext();
            UiManager.Instance.SetLevelText();
            UiManager.Instance.CheckLevelButton();
            bMoveNew = true;MoveStart();
            bStartMove = false;

        }
        if(bStartMove ==true)
        {
            if (MaxLevel == Level)
            {
                UiManager.Instance.Move_Stage();
                SetHeroMove();
            }            
        }
        
    }
    public void MakeNewMonster()
    {
        enemyManager.ChagneMakeMonster();
    }

    public void SetCoin(int count)
    {
        if(startTrans ==true)
        {
            startTrans = false;
        }
        CoinCollectionEffect.CollectItem(count, SystemManager.GetGold(Level));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Coin);
    }
    public void SetGemEffect(int count,Transform pPos)
    {
        GemCollectionEffect.CollectItem(count, 0);
        GemCollectionEffect._popPosition.transform.position = pPos.position;
    }
    public void SetMatEffect(int count, Transform pPos)
    {
        materialCollection_achivementEffect.CollectItem(count, 0);
        materialCollection_achivementEffect._popPosition.transform.position = pPos.position;
    }
    public void AutoNext()
    {
        if(IsAutoPlay ==true)
        {
            int maxStageIndex = (MapStageIndex * 200)-1;
            if (Level >= maxStageIndex)
            {   
                IsAutoPlay = false;
                Save(saveType.IsAutoPlay);
                UiManager.Instance.SetAutoPlay();
            }
            else
            {
                UiManager.Instance.NextLevel();
            }
            
        }
    }
    public void MoveStart()
    {

        enemyManager.Enemy.SetActive(false);
        for (int i = 0; i < heroPos.Length; i++)
        {
            if(heroPos[i] >-1)
            {
                HeroList[heroPos[i]].GetComponent<Hero>().SetInle();
            }            
        }
        
        myGridMoveManager.MoveStart();
    }

    


    public InfiniCoin GetHeroCost(int heroindex,int xCount)
    {
        InfiniCoin cost =  new InfiniCoin();
        cost = 0;
        for(int i =0; i< xCount; i++)
        {
            cost += SystemManager.InitCost[heroindex] * SystemManager.mathPow(1.07f, herosInfos[heroindex].level+i);
        }
        InfiniCoin value = GetArtifactValue(AtrifactType.costDiscount);

        cost = cost - (cost * value);

        return cost;
    }
    public void HeroLevelUP(int iHeroIndex,int xUpgrade,bool bAwaking =false)
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);

        HerosInfo herosInfo = herosInfos[iHeroIndex];
        double power = herosInfos[iHeroIndex].AwakeningCount * SystemManager.LevelUp_threshold;
        for (int i=0;i < xUpgrade; i++)
        {
            //레벨업 업적  
            if(subquestType == SubQuestType.heroLevelup)
            {
                subQuestNow++;                
                UiManager.Instance.SetSubQuestText();
            }            
            herosInfo.level++;
            if (herosInfo.level > MaxLevel_Achivement)
            {
                MaxLevel_Achivement = herosInfo.level;
                achivementData.AchivementCount[0] = MaxLevel_Achivement;
            }
            if (herosInfo.level % 100 == 0)
            {
                //herosInfo.DPS = herosInfo.DPS * 5 * (power + 1);
                herosInfo.LevelUpBuff += 5;
                herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * 5);
            }
            else if (herosInfo.level % 25 == 0)
            {
                //herosInfo.DPS = herosInfo.DPS * 2 * (power + 1);
                herosInfo.LevelUpBuff += 2;
                herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * 2);

            }               
                   
            
            InfiniCoin  initDps = herosInfo.InitDPS * (herosInfo.level + 1);
            herosInfo = SetDps(herosInfo, power, initDps);


            herosInfos[iHeroIndex] = herosInfo;
            HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);
        }
        Save(saveType.subQuestNow);
        Save(saveType.herosInfos, iHeroIndex);
    }


    /// <summary>
    /// DPS CHECK
    /// </summary>
    /// <param name="iHeroIndex"></param>
    /// 
    HerosInfo SetDps(HerosInfo hero, InfiniCoin power,InfiniCoin initDps)
    {
        HerosInfo herosInfo = hero;

        //levelup 버프

        if (herosInfo.LevelUpBuff > 0)
        {            
            initDps = initDps + (initDps * herosInfo.LevelUpBuff);
        }

        herosInfo.DPS = (((SoulAblityList[(int)AtrifactType.alldps] * 0.02) + 1) * initDps);       
     
        if (AltarLevel >0)
        {
            herosInfo.DPS = herosInfo.DPS + (herosInfo.DPS * (AltarLevel * 1f));
        }
        if (power > 0)
        {
            herosInfo.DPS = herosInfo.DPS + (herosInfo.DPS * power);
        }
        return herosInfo;
    }
    public void SetHeroAwaeking(int iHeroIndex)
    {
        HerosInfo herosInfo = herosInfos[iHeroIndex];
        herosInfo.HeroCount -= 2;
        if (herosInfo.HeroCount < 0)
        {
            herosInfo.HeroCount = 0;
        }            
        herosInfo.AwakeningCount++;    

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;
        InfiniCoin initDps = new InfiniCoin();

        herosInfos[iHeroIndex] = herosInfo;

        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);

        initDps = herosInfo.InitDPS * (herosInfo.level + 1);
    
        herosInfo = SetDps(herosInfo, power, initDps);
     
        //herosInfo.LevelUpBuff += 2;
        herosInfos[iHeroIndex] = herosInfo;
        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);


        herosInfos[iHeroIndex] = herosInfo;

        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);

        Save(saveType.herosInfos, iHeroIndex);
    }

    public void DpsUpgrade(float dpsPower,int iHeroIndex)
    {
        HerosInfo herosInfo = herosInfos[iHeroIndex]; 

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;
        herosInfo.fameDps = dpsPower;
        herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * herosInfo.fameDps);


        InfiniCoin initDps = new InfiniCoin();
        initDps = herosInfo.InitDPS * (herosInfo.level + 1);

        herosInfo = SetDps(herosInfo, power, initDps);

        herosInfos[iHeroIndex] = herosInfo;

        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);

        Save(saveType.herosInfos, iHeroIndex);
    }

    public HerosInfo DpsUpgradeInit(HerosInfo hero, int iHeroIndex)
    {
        HerosInfo herosInfo = hero;

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;
        if(herosInfos[iHeroIndex].AttackBuff >0)
        {
            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * herosInfos[iHeroIndex].AttackBuff);
        }        
        

        herosInfo.AttackBuff = herosInfos[iHeroIndex].AttackBuff;
        InfiniCoin initDps = new InfiniCoin();
        initDps = herosInfo.InitDPS * (herosInfo.level + 1);

        herosInfo = SetDps(herosInfo, power, initDps);

        herosInfos[iHeroIndex] = herosInfo;

        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);

        return herosInfo;
    }

    public void DpsDowngrade(float dpsPower,int iHeroIndex)
    {
        HerosInfo herosInfo = herosInfos[iHeroIndex];

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

        herosInfo.InitDPS = herosInfo.InitDPS/ (1+dpsPower);


        InfiniCoin initDps = new InfiniCoin();
        initDps = herosInfo.InitDPS * (herosInfo.level + 1);

        herosInfo = SetDps(herosInfo, power, initDps);

        herosInfos[iHeroIndex] = herosInfo;

        HeroList[iHeroIndex].GetComponent<Hero>().SetPower(herosInfo.DPS);
    }

    public void AltarDpsUpgrade()
    {
        for (int i = 0; i < herosInfos.Count; i++)
        {
            HerosInfo herosInfo = herosInfos[i];

            InfiniCoin power = new InfiniCoin();
            power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;
       

            InfiniCoin initDps = new InfiniCoin();
            initDps = herosInfo.InitDPS * (herosInfo.level + 1);

            herosInfo = SetDps(herosInfo, power, initDps);

            herosInfos[i] = herosInfo;

            HeroList[i].GetComponent<Hero>().SetPower(herosInfo.DPS);
            //Save(saveType.herosInfos, i);            
        }
        //AllSave(saveType.herosInfos);

    }
    public void AltarDpsUpgrade(int index)
    {
        HerosInfo herosInfo = herosInfos[index];

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

        if(AltarLevel >0)
        {
            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * (AltarLevel * 1f));            
        }
        else
        {
            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS);            
        }
        

        InfiniCoin initDps = new InfiniCoin();
       
        initDps = herosInfo.InitDPS * (herosInfo.level + 1);


        herosInfo = SetDps(herosInfo, power, initDps);

        herosInfos[index] = herosInfo;

        HeroList[index].GetComponent<Hero>().SetPower(herosInfo.DPS);

        //Save(saveType.herosInfos, index);
    }

    public void AllDpsUpgrade(float dpsPower)
    {
        for(int i =0; i< herosInfos.Count; i++)
        {
            HerosInfo herosInfo = herosInfos[i];
            herosInfo.AttackBuff += dpsPower;
            InfiniCoin power = new InfiniCoin();
            power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * dpsPower);

            InfiniCoin initDps = new InfiniCoin();
            initDps = herosInfo.InitDPS * (herosInfo.level + 1);

            herosInfo = SetDps(herosInfo, power, initDps);

            herosInfos[i] = herosInfo;

            HeroList[i].GetComponent<Hero>().SetPower(herosInfo.DPS);            
        }
        //AllSave(saveType.herosInfos);

    }


    public bool TrasnUpgradeAttack = false;
    public void AlldpsUpgradeTranscedence(int artifactIndex)
    {
        for (int i = 0; i < herosInfos.Count; i++)
        {
            HerosInfo herosInfo = herosInfos[i];

            InfiniCoin power = new InfiniCoin();
            power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

            //herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * dpsPower);

            InfiniCoin initDps = new InfiniCoin();
            initDps = herosInfo.InitDPS * (herosInfo.level + 1);

            herosInfo = SetDps(herosInfo, power, initDps);

            herosInfos[i] = herosInfo;

            HeroList[i].GetComponent<Hero>().SetPower(herosInfo.DPS);
            
        }
        //AllSave(saveType.herosInfos);
    }

    public HerosInfo AlldpsUpgradeTranscedence(int artifactIndex,HerosInfo heros)
    {
       
        HerosInfo herosInfo = heros;

        InfiniCoin power = new InfiniCoin();
        power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

        //herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * dpsPower);

        InfiniCoin initDps = new InfiniCoin();

        initDps = herosInfo.InitDPS * (herosInfo.level + 1);
        if(herosInfo.fameDps >0)
        {
            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * herosInfo.fameDps);
        }        

        herosInfo = SetDps(herosInfo, power, initDps);
        return herosInfo;
        //herosInfos[heroPos] = herosInfo;

        //HeroList[heroPos].GetComponent<Hero>().SetPower(herosInfo.DPS);

    }

    public void AllDpsDpsDowngrade(float dpsPower)
    {
        for (int i = 0; i < herosInfos.Count; i++)
        {
            HerosInfo herosInfo = herosInfos[i];
            herosInfo.AttackBuff -= dpsPower;
            if(herosInfo.AttackBuff <0)
            {
                herosInfo.AttackBuff = 0;
            }
            InfiniCoin power = new InfiniCoin();
            power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

            herosInfo.InitDPS = herosInfo.InitDPS / (1+dpsPower);

            InfiniCoin initDps = new InfiniCoin();
            initDps = herosInfo.InitDPS * (herosInfo.level + 1);

            herosInfo = SetDps(herosInfo, power, initDps);

            herosInfos[i] = herosInfo;

            HeroList[i].GetComponent<Hero>().SetPower(herosInfo.DPS);            
        }
        //AllSave(saveType.herosInfos);
    }
    //재단 유물 등등
    public void DpsUpgrade(float dpsPower)
    {
        for (int i = 0; i < herosInfos.Count; i++)
        {
            HerosInfo herosInfo = herosInfos[i];

            InfiniCoin power = new InfiniCoin();
            power = herosInfo.AwakeningCount * SystemManager.LevelUp_threshold;

            herosInfo.InitDPS = herosInfo.InitDPS + (herosInfo.InitDPS * dpsPower);

            InfiniCoin initDps = new InfiniCoin();
            initDps = herosInfo.InitDPS * (herosInfo.level + 1);

            herosInfo = SetDps(herosInfo, power, initDps);

            herosInfos[i] = herosInfo;

            HeroList[i].GetComponent<Hero>().SetPower(herosInfo.DPS);
        }
        //AllSave(saveType.herosInfos);
    }

    public double GetFameCount(int iHeroIndex)
    {
        double fameCount = (herosInfos[iHeroIndex].FameCount+1) * SystemManager.fame_cost[iHeroIndex];
        return fameCount;
    }
    public void LevelUpClickPower()
    {
        clicker.level++;
        clicker.TotalClickPower += clicker.UpgradeClickpower;
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (subquestType == SubQuestType.clickLevelup)
        {
            subQuestNow++;
            //Save(saveType.subQuestNow);
            UiManager.Instance.SetSubQuestText();
        }
        if (clicker.level % 200 == 0)
        {
            clicker.TotalClickPower = (clicker.TotalClickPower * 20);
            clicker.UpgradeClickpower = clicker.UpgradeClickpower * 20;
        }
        else if (clicker.level % 100 == 0)
        {
            clicker.TotalClickPower = (clicker.TotalClickPower * 10);
            clicker.UpgradeClickpower = clicker.UpgradeClickpower * 10;
        }       
        else if (clicker.level % 25 == 0)
        {
            clicker.TotalClickPower = (clicker.TotalClickPower * 2);
            clicker.UpgradeClickpower = clicker.UpgradeClickpower * 2;

        }

    }
    public void UpgradeClickPower()
    {
        Clicker temp = clicker;

        temp.UpgradeClickpower = temp.UpgradeClickpower + (temp.UpgradeClickpower);

        InfiniCoin initDps = new InfiniCoin();
        if(temp.level ==0)
        {
            initDps = (temp.UpgradeClickpower);
        }
        else
        {
            initDps = ((temp.level+1) * temp.UpgradeClickpower);
        }
        

        temp.TotalClickPower = initDps;
        clicker = temp;
    }
    IEnumerator TouchAttackRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        bAttack = false;
    }
    bool bAttack = false;
    public void TouchAttack()
    {
        if (bAttack == true)
            return;
        bAttack = true;
        StartCoroutine(TouchAttackRoutine());
        if(enemyManager.Enemy.activeSelf ==true)
        {
            if (subquestType == SubQuestType.click)
            {
                subQuestNow++;
                Save(saveType.subQuestNow);
                UiManager.Instance.SetSubQuestText();
            }
            SoundManager.Instance.PlayFX(SoundManager.SoundFXType.TouchAttack);

            int rand = Random.Range(0, 100);
            bool bCritical = false;
            if(rand <= 1)
            {
                bCritical = true;
            }
            if (timerCotroller.bStart_attackBuff == true)
            {
                if(bCritical == true)
                {
                    enemyManager.Attack(clicker.TotalClickPower *4,true,null);
                }
                else
                {
                    enemyManager.Attack(clicker.TotalClickPower*2,false,null);
                }
                
            }
            else
            {
                if(bCritical ==true)
                {
                    enemyManager.Attack(clicker.TotalClickPower*2,true,null);
                }
                else
                {
                    enemyManager.Attack(clicker.TotalClickPower,false,null);
                }
                
            }
        }    
    }
    public void setSelectPosNormal()
    {
        for(int i=0; i< HeroPos_Src.Count;i++)
        {
            HeroPos_Src[i].gameObject.GetComponent<SelectPosSrc>().setNormal();
        }
    }
    public InfiniCoin AdsGold = new InfiniCoin();
    public InfiniCoin transCost = new InfiniCoin();
    public GameObject PanelsTrans;
    public InfiniCoin m_OfflineRewardCost = new InfiniCoin();
    public InfiniCoin adAltarCount = new InfiniCoin();
    public void RewardAds()
    {
        switch (adsType)
        {
            case AdsType.normalBox:
                //여기서 노말 상자->
                timerCotroller.bStart_normal = true;
                timerCotroller.NormalBoxTime = defaultNormalBoxTime;
                Save(saveType.timerCotroller);
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
                break;
            case AdsType.gold:
                UiManager.Instance.SetBuyComplete(AdsGold, BuyCompletePanel.buyType.gold);
                AdsGold = 0;
                timerCotroller.bAdsGoldTime = true;
                timerCotroller.AdsGoldTime = 1800;
                Save(saveType.timerCotroller);
                break;
            case AdsType.Transcendence:
                TotalSoul += transCost * 2;
                Save(saveType.TotalSoul);
                transDungeonGameSrc.RewardComplete();
                UiManager.Instance.SetBuyComplete(transCost * 2, BuyCompletePanel.buyType.soul);
                transCost = 0;
                break;
            case AdsType.powerBuff:
                timerCotroller.bStart_attackBuff = true;
                timerCotroller.AttackBuffTime = 7201;
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.attackBuff);
                Save(saveType.timerCotroller);
                break;
            case AdsType.offlinereward:
                //TotalGold += m_OfflineRewardCost * 3;
                //UiManager.Instance.SetGoldText();
                UiManager.Instance.OfflineRewardPanel.SetActive(false);
                UiManager.Instance.SetBuyComplete(m_OfflineRewardCost * 3, BuyCompletePanel.buyType.gold);
                break;
            case AdsType.altar:
                TotalAltarCoin += adAltarCount;
                Save(saveType.TotalAltarCoin);
                UiManager.Instance.SetBuyComplete(adAltarCount, BuyCompletePanel.buyType.altar);
                break;
            case AdsType.ticket:
                raidTicketCount++;
                if(raidTicketCount >3)
                {
                    raidTicketCount = 3;
                }
                Save(saveType.raidTicketCount);
                UiManager.Instance.SetBuyComplete(1, BuyCompletePanel.buyType.ticket,1);
                timerCotroller.bAdsTicketTime = true;
                timerCotroller.AdsTicketTime = 600;
                Save(saveType.timerCotroller);
                break;
        }
        achivementData.AchivementCount[6]++;
        GameManager.Instance.Save(GameManager.saveType.achivementData);
        m_OfflineRewardCost = 0;
        adsType = AdsType.non;
    }
    public void SetBuffPackage()
    {
        timerCotroller.bStart_attackBuff = true;
        timerCotroller.AttackBuffTime = 5000000;
        Save(saveType.timerCotroller);
    }

    public double GetArtifactValue(AtrifactType artifactIndex)
    {
        double value = 1;

        switch (artifactIndex)
        {
            case AtrifactType.alldps:
                //데미지
                if (SoulAblityList[(int)AtrifactType.alldps] > 0)
                {
                    value = SoulAblityList[(int)AtrifactType.alldps] * 2;
                }
                else
                {
                    value = 0;
                }

                return value;
            case AtrifactType.criticalDps:
                //치명타
                if (SoulAblityList[(int)AtrifactType.criticalDps] > 0)
                {
                    value = SoulAblityList[(int)AtrifactType.criticalDps] * 15;
                }
                else
                {
                    value = 0;
                }

                return value;
            case AtrifactType.criticalChance:
                //치명타 확률
                if (SoulAblityList[(int)AtrifactType.criticalChance] > 0)
                {
                    value = SoulAblityList[(int)AtrifactType.criticalChance];
                }
                else
                {
                    value = 0;
                }
                break;
            case AtrifactType.costDiscount:
                //비용감소
                if (SoulAblityList[(int)AtrifactType.costDiscount] > 0)
                {
                    value = 99.999 * (1 - System.Math.Pow(2.178, -0.01 * SoulAblityList[(int)AtrifactType.costDiscount]));
                    value = value * 0.01;
                }
                else
                {
                    value = 0;
                }
                break;
            case AtrifactType.ClickPower:
                //클릭
                if (SoulAblityList[(int)AtrifactType.ClickPower] > 0)
                {
                    value = SoulAblityList[(int)AtrifactType.ClickPower] * 20;
                }
                else
                {
                    value = 0;
                }

                return value;
            case AtrifactType.GoldMonster:
                //보물고블린
                if (SoulAblityList[(int)AtrifactType.GoldMonster] > 0)
                {
                    value = 9900 * (1 - System.Math.Pow(2.178, -0.002 * SoulAblityList[(int)AtrifactType.GoldMonster])) + 1;
                    value = 1 + (1 * (value*0.01));
                }
                else
                {
                    value = 1;
                }


                return value;
            case AtrifactType.GetGoldx10:
                //몬스터가 10배
                if (SoulAblityList[(int)AtrifactType.GetGoldx10] > 0)
                {
                    value = 100 * (1 - System.Math.Pow(2.178, -0.0025 * SoulAblityList[(int)AtrifactType.GetGoldx10]));                    
                }
                else
                {
                    value = 0;
                }

                return value;
            case AtrifactType.KillGetGold:
                //처지시 골드증가
                if (SoulAblityList[(int)AtrifactType.KillGetGold] > 0)
                {
                    value = SoulAblityList[(int)AtrifactType.KillGetGold] * 5;
                    value = value * 0.01;
                }
                else
                {
                    value = 0;
                }
                return value;
            case AtrifactType.BossHp:
                //보스 체력삼소
                if (SoulAblityList[(int)AtrifactType.BossHp] > 0)
                {
                    value = 50 * (1 - System.Math.Pow(2.178, -0.002 * SoulAblityList[(int)AtrifactType.BossHp]));
                    value = value * 0.01;                
                }
                else
                {
                    value = 0;
                }
                return value;
            case AtrifactType.BossTime:
                //보스 시간증가
                if (SoulAblityList[(int)AtrifactType.BossTime] > 0)
                {

                    value = 30 * (1 - System.Math.Pow(2.178, -0.034 * SoulAblityList[(int)AtrifactType.BossTime]));
                }
                else
                {
                    value = 0;
                }

                return value;
        }
        return value;
    }
    int getCalculatAltarHero(int heroindex,int cost)
    {
        int calCount = 0;
        calCount += (herosInfos[heroindex].FameLevel* (cost*2));
        int awacount = (int)(herosInfos[heroindex].AwakeningCount * cost);
        calCount += awacount;
        int cardCount = ((herosInfos[heroindex].HeroCount / 2) * cost);
        calCount += cardCount;
        int maxLevel = ((int)(herosInfos[heroindex].level / 10))*cost;
        calCount += maxLevel;
        if (calCount == 0)
            calCount = cost;
        return calCount;
    }
    public int GetAlterCount(int heroIndex)
    {
        int getAltarCount = 0;
        switch (herosInfos[heroIndex].Tier)
        {
            case "n":
                //text.text = "일반";                
                getAltarCount = 1;                
                break;
            case "a":
                //text.text = "고급";                
                getAltarCount = 2;
                break;
            case "r":
                //text.text = "희귀";
                getAltarCount = 3;
                break;
            case "h":
                //text.text = "영웅";
                getAltarCount = 4;
                break;
            case "l":
                //text.text = "전설";
                getAltarCount = 5;
                break;
        }
        getAltarCount = getCalculatAltarHero(heroIndex, getAltarCount);

        int power = 0;
        if (CollectionList[(int)CollectionType.altar100] == 1)
        {
            power += 1;
        }
        if (CollectionList[(int)CollectionType.altar200] == 1)
        {
            power += 2;
        }
        if (power > 0)
        {
            getAltarCount = getAltarCount + (getAltarCount * power);
        }

        return getAltarCount;
    }
    public InfiniCoin GetSlotCost(int index)
    {
        InfiniCoin cost = 100;
        switch (index)
        {
            case 0:
                cost = 500;
                break;
            case 1:
                cost = 100;
                break;
            case 2:
                //10g
                cost = 100000000000000000 ;
                cost = cost * 100000;
                break;
            case 3:
                //100e
                cost = 100000000000000000;
                break;
            case 4:
                //10d
                cost = 10000000000000;
                break;
            case 5:
                //1c
                cost = 1000000000;
                break;
            case 6:
                //1b
                cost = 1000000;
                break;
            case 7:
                //1a
                cost = 1000;
                break;
        }
        return cost;
    }
    public InfiniCoin GetExpCost(int index)
    {
        InfiniCoin cost = 100;
        switch (index)
        {
            case 0:
                //1a
                cost = 1000;
                break;
            case 1:
                //1b
                cost = 1000000;
                break;
            case 2:
                //1c
                cost = 1000000000;
                break;
            case 3:
                //10d
                cost = 10000000000000;
                break;
            case 4:
                //100e
                cost = 100000000000000000;
                break;
            case 5:
                //10g
                cost = 100000000000000000;
                cost = cost * 100000;
                break;
            case 6:
                cost = 500;
                break;
            case 7:
                cost = 1000;
                break;
        }
        return cost;
    }

    public float GetExpeditionTime(int heroindex,int expPos)
    {
        float reduceTime = 0;
        float time = defaultExpeditionTime;
        switch(expPos)
        {
            case 0:
                break;
            case 1:
                //상자
                time = time * 3;
                break;
            case 2:
                //보석
                time = time * 4;
                break;
            case 3:
                //영혼의 조각
                time = time * 2;
                break;
            case 4:
                //재료
                time = time * 2;
                break;
            case 5:
                //번개
                time = time * 2;
                break;
            case 6:
                //보석
                time = time * 4;
                break;
            case 7:
                //골드
                break;
        }
        switch (herosInfos[heroindex].Tier)
        {
            case "n":
                //text.text = "일반";                
                reduceTime = 1;
                break;
            case "a":
                //text.text = "고급";         
                reduceTime = Random.Range(10, 20);                
                break;
            case "r":
                //text.text = "희귀";
                reduceTime = Random.Range(30, 35);
                break;
            case "h":
                //text.text = "영웅";
                reduceTime = Random.Range(40, 45);
                break;
            case "l":
                //text.text = "전설";
                reduceTime = Random.Range(50, 60);
                break;
        }
        float resultItemTime = 0;
        if (herosInfos[heroindex].item_1.isGet == true)
        {
            if(herosInfos[heroindex].item_1.ablityType == 7)
            {
                //resultItemTime = SystemManager.ItemAblityStr[herosInfos[heroindex].item_1.Item_Index];
                resultItemTime = herosInfos[heroindex].item_1.ablityPower;
            }
        }
        if (herosInfos[heroindex].item_2.isGet == true)
        {
            if (herosInfos[heroindex].item_2.ablityType == 7)
            {
                resultItemTime = herosInfos[heroindex].item_2.ablityPower;
            }
        }

        reduceTime = time - (time * (reduceTime * 0.01f));
        reduceTime = reduceTime - (reduceTime * (resultItemTime*0.01f));
        return reduceTime;
    }
    public int GetExpedtionItemCount(int heroindex)
    {
        int ItemCount= 1;
        switch (herosInfos[heroindex].Tier)
        {
            case "n":
                //text.text = "일반";                
                ItemCount = 1;
                break;
            case "a":
                //text.text = "고급";         
                ItemCount = Random.Range(1, 2);
                break;
            case "r":
                //text.text = "희귀";
                ItemCount = Random.Range(1, 3);
                break;
            case "h":
                //text.text = "영웅";
                ItemCount = Random.Range(2, 4);
                break;
            case "l":
                //text.text = "전설";
                ItemCount = Random.Range(3, 5);
                break;
        }
        
        return ItemCount;
    }
    public enum GetItemType { Gold, Gem, Box, Soul, Altar}
    public Dictionary<GetItemType, int> m_Item = new Dictionary<GetItemType, int>();
    public  GetItemType m_itemType = GetItemType.Gold;
    void MakeItemRand()
    {
        m_Item.Add(GetItemType.Gold, 35);
        m_Item.Add(GetItemType.Gem, 32);
        m_Item.Add(GetItemType.Box, 20);
        m_Item.Add(GetItemType.Soul, 13);        
    }

    public GetItemType GetRandomItem()
    {
        m_itemType = WeightedRandomizer.From(m_Item).TakeOne();     

        return m_itemType;
    }
    InfiniCoin m_tickCoin = new InfiniCoin();
    InfiniCoin CheckTickCost(bool isShop =false)
    {       
        m_tickCoin = 1;
        InfiniCoin gold = new InfiniCoin();
        InfiniCoin Hp = SystemManager.GetEnmeyHp(MaxLevel_rank,true);
        InfiniCoin TotalDps = new InfiniCoin();
        double totalLevel =0;
        for(int i =0; i< heroPos.Length; i++)
        {
            if(heroPos[i] >-1)
            {
                if (herosInfos[heroPos[i]].isGetHero == true)
                {
                    TotalDps += herosInfos[heroPos[i]].DPS;
                    totalLevel+= herosInfos[heroPos[i]].level;
                }
            }            
        }   
        int attackCount = 0;
        if (TotalDps >0)
        {
            InfiniCoin maxHp = SystemManager.GetEnmeyHp(MaxLevel_rank);            
            while (true)
            {
                if(maxHp <=0)
                {
                    break;
                }
                else
                {
                    attackCount++;
                    if (attackCount > 5000)
                        break;
                    maxHp -= TotalDps;
                }
                
            }
        }
        InfiniCoin GetGold = SystemManager.GetGold_Normal(MaxLevel_rank);
        gold = 1;
        if (attackCount >0)
        {
            gold = GetGold / attackCount;
        }


        if (m_tickCoin < 1)
        {
            m_tickCoin = 1;
        }
        else
        {
            if (MaxLevel_rank < 50)
            {

                m_tickCoin = gold;
            }
            else
            {
                int temp = MaxLevel_rank- 50;
                if (temp <= 0)
                {
                    temp = 1;
                }
                if(temp >20)
                {
                    temp = 20;
                }
                m_tickCoin = gold * temp;
            }

            
        }

        if(m_tickCoin > tickGoldChecker)
        {
            tickGoldChecker = m_tickCoin;
            Save(saveType.tickGoldChecker);
        }
        return tickGoldChecker;
    }
    public InfiniCoin GetGoldTick()
    {        
        return CheckTickCost();
    }
    public InfiniCoin GetShopCoin()
    {
        return CheckTickCost(true);
    }
   
    public enum GetRaidItemType { Box1, Box2, Altar }
    Dictionary<GetRaidItemType, int> m_RaidItem = new Dictionary<GetRaidItemType, int>();
    public GetRaidItemType m_RaiditemType = GetRaidItemType.Box1;
    void MakeItemRaidRand()
    {
        m_RaidItem.Add(GetRaidItemType.Box1, 600);
        m_RaidItem.Add(GetRaidItemType.Box2, 1);
        m_RaidItem.Add(GetRaidItemType.Altar, 400);
    }

    public GetRaidItemType GetrandomRaidItem()
    {
        m_RaiditemType = WeightedRandomizer.From(m_RaidItem).TakeOne();

        return m_RaiditemType;
    }
    public bool startTrans = false;
    public void SetTranscendence(InfiniCoin initCost,int index,GameObject panel)
    {
        startTrans = true;
        TotalreBrithCount++;
        Save(saveType.TotalreBrithCount);
        UiManager.Instance.TransEffectObjectr.SetActive(true);
        panel.SetActive(false);
        for (int i =0; i< herosInfos.Count; i++)
        {
            HerosInfo heros = herosInfos[i];
            heros.level = 0;
            heros.LevelUpBuff = 0;
            heros.Cost = SystemManager.InitCost[i];
            heros.DPS = SystemManager.attackPower[i];
            heros.InitDPS = SystemManager.attackPower[i];
            heros.ResetDps = heros.InitDPS;
            
            heros = AlldpsUpgradeTranscedence((int)AtrifactType.alldps, heros);
            heros = DpsUpgradeInit(heros, i);
            

            herosInfos[i] = heros;
        }
        if(index ==0)
        {
            TotalSoul += initCost;
        }
        else if(index ==1)
        {
            TotalSoul += (initCost * 2);
        }
        else if (index == 2)
        {
            TotalSoul += (initCost * 10);
        }
        Save(saveType.TotalSoul);
        MaxLevel = 0;
        Level = 0;
        Save(saveType.MaxLevel);
        Save(saveType.Level);
        enemyManager.ChagneMakeMonster();
        MonsterCount = 0;
        Save(saveType.MonsterCount);
        clicker.level = 0;
        clicker.TotalClickPower = 1;
        clicker.UpgradeClickpower = 1;
        UiManager.Instance.SetLevelText();
        UiManager.Instance.SetSoulText();
        UiManager.Instance.CheckLevelButton();
        UiManager.Instance.clearConroller.SetStageView();
        UiManager.Instance.SetMonsterCount();
        UiManager.Instance.SetGoldText();
        UiManager.Instance.CheckBottomUI();


        UiManager.Instance.SetAutoPlay_Trans();
        TotalGold = 0;
    }
    public void CheckAchivement()
    {
        //레벨업
        achivementData.AchivementMax[0] = (achivementData.AchivementLevel[0]+1)*25;
        //뽑기
        achivementData.AchivementMax[1] = (achivementData.AchivementLevel[1] + 1) * 10;
        //원정대
        achivementData.AchivementMax[2] = (achivementData.AchivementLevel[2] + 1) * 15;
        //재물등록
        achivementData.AchivementMax[3] = (achivementData.AchivementLevel[3] + 1) * 10;
        //레이드 보스 사냥
        achivementData.AchivementMax[4] = (achivementData.AchivementLevel[4] + 1) * 5;
        //각성
        achivementData.AchivementMax[5] = (achivementData.AchivementLevel[5] + 1) * 5;
        //광고
        achivementData.AchivementMax[6] = (achivementData.AchivementLevel[6] + 1) * 6;
        //시간
        achivementData.AchivementMax[7] = (achivementData.AchivementLevel[7] +1)* 3600;
        //스테이지
        achivementData.AchivementMax[8] = (achivementData.AchivementLevel[8] + 1) * 5;
        //아이템제작
        achivementData.AchivementMax[9] = (achivementData.AchivementLevel[9] + 1);
        //제단 레벨업
        achivementData.AchivementMax[10] = (achivementData.AchivementLevel[10] + 1) *2;

        achivementData.AchivementMax[11] = 1000;
        achivementData.AchivementMax[12] = 1000;
        achivementData.AchivementMax[13] = 1000;
        achivementData.AchivementMax[14] = 1000;
        GameManager.Instance.Save(GameManager.saveType.achivementData);
    }
    public bool isAchivement()
    {
        for (int i = 0; i < achivementData.AchivementMax.Count; i++)
        {
            if (achivementData.AchivementCount[i] >= achivementData.AchivementMax[i])
            {
                return true;
            }
        }
        return false;
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("Intro");
    }
    public void BuyTicketGem()
    {
        if(TotalGem >= 200)
        {
            TotalGem -= 200;
            Save(saveType.TotalGem);
            UiManager.Instance.SetGemText();
            raidTicketCount += 3;
            if (raidTicketCount > 3)
            {
                raidTicketCount = 3;
            }
            Save(saveType.raidTicketCount);
            UiManager.Instance.SetBuyComplete(3, BuyCompletePanel.buyType.ticket,3);
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
        }
        
    }
    public void CheckTutorial()
    {
        int count = 0;
        
        for (int i = 0; i < herosInfos.Count; i++)
        {
            if (herosInfos[i].isGetHero == true)
            {
                count++;
            }
        }        
       

        if (subquestType == SubQuestType.heroGet)
        {
            subQuestNow = count;
            Save(saveType.subQuestNow);
            UiManager.Instance.SetSubQuestText();
        }
    }


    public enum ItemTierType { Tier_Lv1, Tier_Lv2, Tier_Lv3, Tier_Lv4, Tier_Lv5}
    Dictionary<ItemTierType, int> m_TierItem= new Dictionary<ItemTierType, int>();
    public ItemTierType m_tierType = ItemTierType.Tier_Lv1;
    void MakeTierItem()
    {
        m_TierItem.Add(ItemTierType.Tier_Lv1, 40);
        m_TierItem.Add(ItemTierType.Tier_Lv2, 30);
        m_TierItem.Add(ItemTierType.Tier_Lv3, 22);
        m_TierItem.Add(ItemTierType.Tier_Lv4, 13);
        m_TierItem.Add(ItemTierType.Tier_Lv5, 5);
    }

    public ItemTierType GetTierItem()
    {
        m_tierType = WeightedRandomizer.From(m_TierItem).TakeOne();

        return m_tierType;
    }


    public enum ScrollItemType { ring, necklace, helmet, armor, boots , gloves , belt , shield , cape }
    Dictionary<ScrollItemType, int> m_scrollItem = new Dictionary<ScrollItemType, int>();
    public ScrollItemType m_scrollItemType = ScrollItemType.ring;
    void MakeRandScroll()
    {
        //마법공격
        m_scrollItem.Add(ScrollItemType.ring, 10);
        //보스전 시간증가
        m_scrollItem.Add(ScrollItemType.necklace, 10);
        //골드 획득
        m_scrollItem.Add(ScrollItemType.helmet, 10);
        //최대 체력 공격
        m_scrollItem.Add(ScrollItemType.armor, 10);
        //이동속도
        m_scrollItem.Add(ScrollItemType.boots, 10);
        //치명타 확률
        m_scrollItem.Add(ScrollItemType.gloves, 10);
        //공격력
        m_scrollItem.Add(ScrollItemType.belt, 10);
        //원정대 시간
        m_scrollItem.Add(ScrollItemType.shield, 10);
        //치명타 데미지
        m_scrollItem.Add(ScrollItemType.cape, 10);
    }

    public ScrollItemType GetRandScroll()
    {
        m_scrollItemType = WeightedRandomizer.From(m_scrollItem).TakeOne();

        return m_scrollItemType;
    }

    public void GetItemChange(bool isBoss)
    {
        //스크롤 획득
        float chance = 0;        
        //MaxLevel_rank == le
        if(MaxLevel_rank == Level)
        {
            //2% 확률
            chance = 2f;
        }
        else
        {
            chance = 2f - ((MaxLevel_rank - Level)*0.1f);
            if(chance <=0)
            {
                chance = 0.01f;
            }
        }

        float getItemChance = Random.Range(0f, 100f);
        //Debug.Log(getItemChance);
        //테스트
        if(getItemChance <=chance)
        {            
            if (isBoss == true)
            {
                int bossRand = Random.Range(0, 15);
                if (bossRand < 12)
                    return;

                isNewItem = true;
                Save(saveType.isNewItem);
                int totalItemCount = 0;
                for (int i = 0; i < Scrolls.Count; i++)
                {
                    if (Scrolls[i].isGet == true)
                    {
                        totalItemCount++;
                    }
                }
                if (totalItemCount > 19)
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.maxitem);
                    return;
                }
                //int rand = Random.Range(0, 9);
                ScrollItemType itemType = GetRandScroll();
                //여기서 가중치
                //int scrollRand = Random.Range(0, 5);
                                
                //int randposition = (5 * rand) + scrollRand;
                //int randposition = rand;
                int randposition = (int)itemType;
                ScrollCollectionEffect._itemList[ScrollCollectionEffect.iIndex].GetComponent<Image>().sprite = ScrollSprite[randposition];
                ScrollCollectionEffect.CollectItem(1, 0);

                scroll tempItem = new scroll();
                tempItem.isGet = true;
                tempItem.itemLevel = 0;
                tempItem.itemTier = 0;
                tempItem.name = "호잇";
                tempItem.position = -1;
                tempItem.count = 1;
                tempItem.Item_Index = randposition;
                string AblityType = string.Empty;
                tempItem.ablityType = randposition;
                bool bSame = false;
                for (int i = 0; i < Scrolls.Count; i++)
                {
                    if (Scrolls[i].ablityType == tempItem.ablityType)
                    {
                        tempItem.count = Scrolls[i].count;
                        tempItem.count++;
                        Scrolls[i] = tempItem;
                        bSame = true;
                        break;
                    }
                }
                if (bSame == false)
                {
                    for (int i = 0; i < Scrolls.Count; i++)
                    {
                        if (Scrolls[i].isGet == false)
                        {
                            Scrolls[i] = tempItem;
                            break;
                        }
                    }
                }
                Save(saveType.Scrolls);

            }
            else
            {
                isNewItem = true;
                Save(saveType.isNewItem);
                int matCount = 1;
                if(CollectionList[(int)CollectionType.mat100] ==1)
                {
                    matCount += 1;
                }
                if (CollectionList[(int)CollectionType.mat200] == 1)
                {
                    matCount += 2;
                }
                materialCollectionEffect.CollectItem(matCount, 0);

                materialCount+= matCount;

                Save(saveType.materialCount);
                if (IsNewSmith ==0)
                {
                    IsNewSmith = 1;
                    Save(saveType.IsNewSmith);
                    UiManager.Instance.NewContentEventPanel.SetActive(true);
                    UiManager.Instance.NewContentEventPanel.GetComponent<NewContentEventSrc>().SetData("mat");
                    UiManager.Instance.CheckTurorialLock();
                }
            }
        }      
    }
    public void MakeScrollByGift(ScrollItemType itemType)
    {       
        int randposition = (int)itemType;
        ScrollCollectionEffect._itemList[ScrollCollectionEffect.iIndex].GetComponent<Image>().sprite = ScrollSprite[randposition];
        ScrollCollectionEffect.CollectItem(1, 0);

        scroll tempItem = new scroll();
        tempItem.isGet = true;
        tempItem.itemLevel = 0;
        tempItem.itemTier = 0;
        tempItem.name = "호잇";
        tempItem.position = -1;
        tempItem.count = 1;
        tempItem.Item_Index = randposition;
        string AblityType = string.Empty;
        tempItem.ablityType = randposition;
        bool bSame = false;
        for (int i = 0; i < Scrolls.Count; i++)
        {
            if (Scrolls[i].ablityType == tempItem.ablityType)
            {
                tempItem.count = Scrolls[i].count;
                tempItem.count++;
                Scrolls[i] = tempItem;
                bSame = true;
                break;
            }
        }
        if (bSame == false)
        {
            for (int i = 0; i < Scrolls.Count; i++)
            {
                if (Scrolls[i].isGet == false)
                {
                    Scrolls[i] = tempItem;
                    break;
                }
            }
        }
    }

    public string GetItemAblityName(int ablityIndex,int itemPos)
    {
        string ablityName = "";
        switch (ablityIndex)
        {
            case 0:
                //ablityName = "<color=red>"+SystemManager.ItemChance[itemPos] +" % </color>확률로 공격력의 <color=red>" + SystemManager.ItemChance[itemPos]*20 + "% </color>마법공격";
                ablityName = "<color=red>" + items[itemPos].ablityPower.ToString("N1") + " % </color> <size=15>[1% ~ 25%]</size> 확률로 공격력의\n<color=red>" + (items[itemPos].ablityPower * 20).ToString("N1") + "% </color> <size=15>[20% ~ 500%]</size> 마법공격";
                break;
            case 1:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 보스전 시간<color=red>" + SystemManager.ItemAblityStr[itemPos] + " 초</color> 증가";
                ablityName = "<color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 보스전 시간\n<color=red>" + items[itemPos].PChance.ToString("N1") + " 초</color> <size=15>[0.5 ~ 5.5]</size> 증가";
                break;
            case 2:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 골드 획득<color=red>" + SystemManager.ItemTime[itemPos] + " 초 동안</color> 증가";
                ablityName = "<color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 골드 획득\n<color=red>" + items[itemPos].time.ToString("N1") + " 초 동안</color> <size=15>[1 ~ 50]</size> 증가";
                break;
            case 3:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 몬스터 최대체력의 <color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color> 공격";
                ablityName = "<color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 보스 현재체력의\n<color=red>" + items[itemPos].PChance.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 공격";
                break;
            case 4:
                //ablityName = "<color=red>" + SystemManager.ItemAblityStr[itemPos]*10 + "% </color>이동속도 증가";
                ablityName = "<color=red>" + (items[itemPos].ablityPower * 10).ToString("N1") + "% </color> <size=15>[10% ~ 450%]</size> 이동속도 증가";
                break;
            case 5:
                //ablityName = "치명타 확률 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color>증가";
                ablityName = "치명타 확률 <color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 45%]</size> 증가";
                break;
            case 6:
                //공격력 
                //ablityName = "공격력 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color> 증가";
                ablityName = "공격력 <color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 250%]</size> 증가";
                break;
            case 7:
                //ablityName = "원정대 시간<color=red> " + SystemManager.ItemAblityStr[itemPos] + "% </color>감소";
                ablityName = "원정대 시간<color=red> " + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 25%]</size> 감소";
                break;
            case 8:
                //치명타 공격력
                //ablityName = "치명타 데미지 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color> 증가";
                ablityName = "치명타 데미지 <color=red>" + items[itemPos].ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 250%]</size> 증가";
                break;
        }
        return ablityName;
    }

    public string GetItemAblityName_Item(Item myItem)
    {
        string ablityName = "";
        switch (myItem.ablityType)
        {
            case 0:
                //ablityName = "<color=red>"+SystemManager.ItemChance[itemPos] +" % </color>확률로 공격력의 <color=red>" + SystemManager.ItemChance[itemPos]*20 + "% </color>마법공격";
                ablityName = "<color=red>" + myItem.ablityPower.ToString("N1") + " % </color> <size=15>[1% ~ 25%]</size> 확률로 공격력의\n<color=red>" + (myItem.ablityPower * 20).ToString("N1") + "% </color> <size=15>[20% ~ 500%]</size> 마법공격"; ;
                break;
            case 1:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 보스전 시간<color=red>" + SystemManager.ItemAblityStr[itemPos] + " 초</color> 증가";
                ablityName = "<color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 보스전 시간\n<color=red>" + myItem.PChance.ToString("N1") + " 초</color> <size=15>[0.5 ~ 5.5]</size> 증가"; ;
                break;
            case 2:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 골드 획득<color=red>" + SystemManager.ItemTime[itemPos] + " 초 동안</color> 증가";
                ablityName = "<color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 골드 획득\n<color=red>" + myItem.time.ToString("N1") + " 초 동안</color> <size=15>[1 ~ 50]</size> 증가"; ;
                break;
            case 3:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 몬스터 최대체력의 <color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color> 공격";
                ablityName = "<color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 확률로 보스 현재체력의\n<color=red>" + myItem.PChance.ToString("N1") + "% </color> <size=15>[0.1% ~ 5%]</size> 공격"; ;
                break;
            case 4:
                //ablityName = "<color=red>" + SystemManager.ItemAblityStr[itemPos]*10 + "% </color>이동속도 증가";
                ablityName = "<color=red>" + (myItem.ablityPower * 10).ToString("N1") + "% </color> <size=15>[10% ~ 450%]</size> 이동속도 증가";
                break;
            case 5:
                //ablityName = "치명타 확률 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color>증가";
                ablityName = "치명타 확률 <color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 45%]</size> 증가";                
                break;
            case 6:
                //공격력 
                //ablityName = "공격력 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color> 증가";
                ablityName = "공격력 <color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 250%]</size> 증가";
                break;
            case 7:
                //ablityName = "원정대 시간<color=red> " + SystemManager.ItemAblityStr[itemPos] + "% </color>감소";
                ablityName = "원정대 시간<color=red> " + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 25%]</size> 감소";
                break;
            case 8:
                //치명타 공격력
                //ablityName = "치명타 데미지 <color=red>" + SystemManager.ItemChance[itemPos] + "% </color> 증가";
                ablityName = "치명타 데미지 <color=red>" + myItem.ablityPower.ToString("N1") + "% </color> <size=15>[1% ~ 250%]</size> 증가";
                break;
        }
        return ablityName;
    }


    public string GetItemAblityName_Scroll(int ablityIndex,string name)
    {
        string ablityName = "";
        switch (ablityIndex)
        {
            case 0:
                ablityName = "<size=15>[1% ~ 25%] 확률로 공격력의\n[20% ~ 500%] 마법공격</size>";
                break;
            case 1:
                ablityName = "<size=15>[0.1% ~ 5%] 확률로 보스전 시간\n[0.5 ~ 5.5] 초 증가</size>";
                break;
            case 2:
                ablityName = "<size=15>[0.1% ~ 5%] 확률로 골드 획득\n[1 ~ 50] 초 동안 증가</size>";
                break;
            case 3:
                ablityName = "<size=15>[0.1% ~ 5%] 확률로 보스 현재체력의\n[0.1% ~ 5%] 공격</size>";
                break;
            case 4:
                ablityName = "<size=15>[10% ~ 450%] 이동속도 증가</size>";
                break;
            case 5:
                ablityName = "<size=15>[1% ~ 45%] 치명타 확률증가</size>";
                break;
            case 6:
                //공격력 
                ablityName = "<size=15>[1% ~ 250%] 공격력 증가</size>";
                break;
            case 7:
                ablityName = "<size=15>[1% ~ 25%] 원정대 시간감소</size>";
                break;
            case 8:
                //치명타 공격력
                ablityName = "<size=15>[1% ~ 250%] 치명타 데미지증가</size>";
                break;
        }
        return ablityName;
    }

    public void SetListScroll(int m_index)
    {
        scroll tempItem = new scroll();
        tempItem = Scrolls[m_index];
        tempItem.count--;
        if(tempItem.count ==0)
            tempItem.isGet = false;
        Scrolls[m_index] = tempItem;

        List<scroll> tempList = new List<scroll>();

        for (int i = 0; i < Scrolls.Count; i++)
        {
            if (Scrolls[i].isGet == true)
            {
                tempList.Add(Scrolls[i]);
            }
            scroll temp = new scroll();
            temp.isGet = false;
            Scrolls[i] = temp;
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            Scrolls[i] = tempList[i];
        }
        Save(saveType.Scrolls);
    }
    
    public void MakeNewItem(int m_index,int tier,string name,int ablityType,int item_index)
    {
        if (m_index == -1)
            return;
        Item tempItem = new Item();
        tempItem.isGet = true;
        tempItem.itemLevel = 0;
        tempItem.itemTier = tier;
        tempItem.name = name;
        tempItem.position = -1;
        tempItem.count = 1;
        tempItem.ablityType = ablityType;
        float ablityPower = 0;
        float time=0;
        float pChance = 0;
        float temp =0;
        switch(ablityType)
        {
            case 0:
                temp = tier * 5;
                ablityPower = Random.Range(temp + 1, temp + 5);
                break;
            case 1:
                ablityPower = Random.Range((float)tier+0.1f, (float)tier +1);
                pChance = Random.Range((float)tier+0.5f, (float)tier + 1.5f);
                break;
            case 2:
                temp = tier * 10;
                ablityPower = Random.Range((float)tier+0.1f, (float)tier + 1);
                time = Random.Range((float)temp + 1, (float)temp + 10);
                break;
            case 3:                
                ablityPower = Random.Range((float)tier+0.1f, (float)tier +1f);
                pChance = Random.Range(tier+0.1f, tier + 1);
                break;
            case 4:
                temp = tier * 10;
                ablityPower = Random.Range(temp + 1, temp + 5);
                break;
            case 5:
                temp = tier * 10;
                ablityPower = Random.Range(temp + 1, temp + 5);
                break;
            case 6:
                temp = tier * 50;
                ablityPower = Random.Range(temp + 1, temp + 50);
                break;
            case 7:
                temp = tier * 5;
                ablityPower = Random.Range(temp + 1, temp + 5);
                break;
            case 8:
                temp = tier * 50;
                ablityPower = Random.Range(temp + 1, temp + 50);
                break;
        }
        tempItem.ablityPower = ablityPower;
        tempItem.time = time;
        tempItem.PChance = pChance;
        tempItem.Item_Index = item_index;
        string AblityType = string.Empty;
        
        bool bSame = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == tempItem.name && items[i].ablityPower == tempItem.ablityPower && items[i].time == tempItem.time && items[i].PChance == tempItem.PChance)
            {
                tempItem.count = items[i].count;
                tempItem.count++;
                items[i] = tempItem;
                bSame = true;
                break;
            }
        }
        if (bSame == false)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isGet == false)
                {
                    items[i] = tempItem;
                    break;
                }
            }
        }
        Save(saveType.items);
    }
    public int GetItemPos()
    {
        for(int i =0;i< items.Count; i++)
        {
            if(items[i].isGet ==false)
            {
                return i;
            }
        }
        return -1;
    }
    public void MakeItem_(int itemIndex,Item prevItem)
    {
        int randposition = itemIndex;        

        Item tempItem = new Item();
        tempItem = prevItem;
        tempItem.isGet = true;
        tempItem.itemLevel = 0;
        //  int randposition = (5 * rand) + scrollRand;
        //int randTier = itemIndex % 5;        
        //tempItem.itemTier = randTier;
        //tempItem.name = SystemManager.Itemname[randposition];
        //tempItem.position = -1;
        tempItem.count = 1;
        //tempItem.Item_Index = randposition;
        //tempItem.m_time = 0;
        //tempItem.m_PChance = 0;
        string AblityType = string.Empty;
        
       //tempItem.ablityType = SystemManager.ItemAblityStr[randposition];
        bool bSame = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == tempItem.name && items[i].ablityPower == tempItem.ablityPower && items[i].time == tempItem.time && items[i].PChance == tempItem.PChance)
            {
                tempItem.count = items[i].count;
                tempItem.count++;
                items[i] = tempItem;
                bSame = true;
                break;
            }
        }
        if (bSame == false)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isGet == false)
                {
                    items[i] = tempItem;
                    break;
                }
            }
        }
        Save(saveType.items);
    }
    public void SetListItem(int m_index)
    {
        Item tempItem = new Item();
        tempItem.isGet = false;
        items[m_index] = tempItem;

        List<Item> tempList = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].isGet == true)
            {
                tempList.Add(items[i]);
            }
            Item temp = new Item();
            temp.isGet = false;
            items[i] = temp;
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            items[i] = tempList[i];
        }
        Save(saveType.items);
    }
    public bool CheckInventoryFull()
    {
        bool bFull = true;
        for (int i = 0; i < 20; i++)
        {
            if (items[i].isGet == false)
            {
                bFull = false;
            }
        }
        if (bFull == true)
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.inventoryFull);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetItemEffect(ItemEffectController.EffectType effectType,string title,float disableTime,GameObject target)
    {
        itemEffectController.SetEffect(effectType, title, disableTime, target);
    }
    public void CheckMap()
    {
        for (int i = 0; i < MapList.Count; i++)
        {
            if (MapList[i].activeSelf == true)
            {
                MapList[i].SetActive(false);
            }
        }
        
        if (Level < 40)
        {
            MapList[0].SetActive(true);
        }
        else if (Level < 80)
        {
            MapList[1].SetActive(true);
        }
        else if (Level < 120)
        {
            MapList[2].SetActive(true);
        }
        else if (Level < 160)
        {
            MapList[3].SetActive(true);
        }
        else if (Level < 200)
        {
            MapList[4].SetActive(true);
        }
        else if (Level < 240)
        {
            MapList[5].SetActive(true);
        }
        else if (Level < 280)
        {
            MapList[6].SetActive(true);

        }
        else if (Level < 320)
        {
            MapList[7].SetActive(true);
        }
        else if (Level < 360)
        {
            MapList[8].SetActive(true);
        }
        else if (Level < 400)
        {
            MapList[9].SetActive(true);
        }
        else if (Level < 440)
        {
            MapList[10].SetActive(true);
        }
        else if (Level < 480)
        {
            MapList[11].SetActive(true);
        }
        else if (Level < 520)
        {
            MapList[12].SetActive(true);
        }
        else if (Level < 560)
        {
            MapList[13].SetActive(true);
        }
        else if (Level < 600)
        {
            MapList[14].SetActive(true);
        }
        else if (Level < 640)
        {
            MapList[15].SetActive(true);
        }
        else if (Level < 680)
        {
            MapList[16].SetActive(true);
        }
        else if (Level < 720)
        {
            MapList[17].SetActive(true);
        }
        else if (Level < 760)
        {
            MapList[18].SetActive(true);
        }
        else if (Level < 800)
        {
            MapList[19].SetActive(true);
        }
        else if (Level < 840)
        {
            MapList[20].SetActive(true);
        }
        else if (Level < 880)
        {
            MapList[21].SetActive(true);
        }
        else if (Level < 920)
        {
            MapList[22].SetActive(true);
        }
        else if (Level < 960)
        {
            MapList[23].SetActive(true);
        }
        else if (Level < 1000)
        {
            MapList[24].SetActive(true);
        }
        else
        {
            MapList[24].SetActive(true);

        }

    }

    public int GetLimitMaxLevel(int heroPos)
    {
        return (herosInfos[heroPos].LimitLevelUpCount + 1) * 150;
    }
    public int GetLimitMaxLevelNext(int heroPos)
    {
        return ((herosInfos[heroPos].LimitLevelUpCount+1) + 1) * 150;
    }
    public int GetLimitCost(int heroPos)
    {
        return (herosInfos[heroPos].LimitLevelUpCount + 1) * 5;
    }
    public bool CheckLevelLevel(int heroPos,int xCount)
    {
        if(herosInfos[heroPos].level+xCount <= GetLimitMaxLevel(heroPos))
        {
            return true;
        }
        return false;
    }
    public bool bstartDungoen = true;
    public void SetTransDungeonCamera()
    {
        Vector3 cameraPos = mainCamera.transform.position;      

        cameraPos.x = -50;
        mainCamera.transform.position = cameraPos;
        //mainCamera.orthographicSize = 6.5f;
    }
    public void StartTransDungeon()
    {
        SetTransDungeonCamera();
        UiManager.Instance.MainCanvasGroup.alpha = 0;
        UiManager.Instance.TransDungeonCanvas.SetActive(true);
        transDungeonGameSrc.StartGame();
        transenemyManager.StartDungeon();
    }
    public void ResetTestTrans()
    {
        timerCotroller.bTrasnTicketTime = false;
        timerCotroller.TransTicketTime = 0;
    }
    public void EndTransDungeon()
    {   
        UiManager.Instance.MainCanvasGroup.alpha = 1;
        ImidiMoveCamera();
        transDungeonGameSrc.EndGame();
        transenemyManager.EndDungeon();
        UiManager.Instance.TransDungeonCanvas.SetActive(false);
        
    }

    public InfiniCoin Rebirth(int level_soul)
    {
        double dSoulCount = 0;
        double temp1 = 1, temp2 = 1, temp3 = 1, temp4 = 1;
        if (level_soul > 600)
        {
            temp4 = System.Math.Pow(1.003, level_soul - 601);
        }
        if (level_soul > 400)
        {
            if (level_soul - 401 > 200)
            {
                temp3 = System.Math.Pow(1.005, 200);
            }
            else
            {
                temp3 = System.Math.Pow(1.005, level_soul - 401);
            }
        }
        if (level_soul > 300)
        {
            if (GameManager.Instance.MaxLevel - 301 > 100)
            {
                temp2 = System.Math.Pow(1.01, 100);
            }
            else
            {
                temp2 = System.Math.Pow(1.01, level_soul - 301);
            }
        }
        if (level_soul >= 100)
        {
            if (level_soul - 100 > 201)
            {
                temp1 = System.Math.Pow(1.02, 201);
            }
            else
            {
                temp1 = System.Math.Pow(1.02, level_soul - 100);
            }
        }
        else
        {
            temp1 = level_soul * 0.9f;
        }
        if(level_soul >=100)
        {
            dSoulCount = temp1 * temp2 * temp3 * temp4;
            dSoulCount = System.Math.Floor(dSoulCount * 100);
        }
        else
        {
            dSoulCount = temp1;
        }
        

        InfiniCoin Count = dSoulCount;

        int tier = TransTier + 1;
        Count = Count * tier;

        return Count;
    }


  
    List<string> CollectionPos = new List<string>();
    public void CheckCollection()
    {
        CollectionPos.Clear();
        for (int i =0; i< SystemManager.CollectionName.Count;i++)
        {
            if(CollectionList[i] ==0)
            {
                if(herosInfos[SystemManager.CollectionHero1[i]].isGetHero ==true && herosInfos[SystemManager.CollectionHero2[i]].isGetHero == true&&
                    herosInfos[SystemManager.CollectionHero3[i]].isGetHero == true&& herosInfos[SystemManager.CollectionHero4[i]].isGetHero == true)
                {
                    //여기가 컬렉션 완성!!
                    IsNewCollection = true;
                    Save(saveType.isNewCollection);
                    CollectionList[i] = 1;
                    Save(saveType.collection);
                    string strPower = SystemManager.Collectionpower[i].ToString();
                    switch (SystemManager.Collectiontitle[i])
                    {
                        case "dice":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=#ECDE0A>레이드 횟수 증가 +" + strPower + "</color>)");
                            break;
                        case "atk":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=red>모든 공격력 x" + strPower + " % 증가</color>)");
                            AllDpsUpgrade(float.Parse(strPower) * 0.01f);
                            break;
                        case "gold":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=yellow>골드 획득 x" + strPower + " % 증가</color>)");
                            break;
                        case "mat":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=#97A895>재료 획득 x" + strPower + " % 증가</color>)");
                            break;
                        case "altar":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=blue>영혼의 조각 x" + strPower + " % 증가</color>)");
                            break;
                        case "soul":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=yellow>번개 x" + strPower + " % 증가</color>)");
                            break;
                        case "trans":
                            //
                            CollectionPos.Add(SystemManager.CollectionName[i] + "  (<color=#ECA40A>시험의 방 보스체력 감소 -" + strPower + " %</color>)");
                            break;
                        case "hero":
                            CollectionPos.Add("<color=#E51717>랜덤 전설 영웅!</color>");
                            break;
                    }
                }
            }
        }
        if(CollectionPos.Count >0)
        {
            StartCoroutine(CollectionRoutine());
        }
    }
    IEnumerator CollectionRoutine()
    {
        for(int i=0; i< CollectionPos.Count;i++)
        {
            UiManager.Instance.SetCollectionUI(CollectionPos[i]);
            yield return new WaitForSeconds(2.5f);
        }
        //Save(saveType.collection);
        //Save(saveType.isNewCollection);
        CollectionPos.Clear();
    }
}


