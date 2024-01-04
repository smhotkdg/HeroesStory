using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GR_InfiniCoin;
public class RaidPanelSrc : MonoBehaviour
{
    public GameObject AutoPlayPanel;
    public Toggle AutoPlayToggle;
    public GameObject RaidTutorial;
    public GameObject RaidTutorial2;
    public Text TicketCountText;
    public Text TicketTimeText;
    public GameObject TouchText;
    public List<GameObject> removeList;
    public List<Text> LevelTextList;
    public List<Text> DpsTextList;
    public GameObject EndRaidPanel;
    public GameObject SkillEffect;
    public GameObject LevelUPText;
    public GameObject BossDeathEffect;
    public List<Sprite> EffectSpriteList;
    public GameObject DiceEffect;
    public GameObject DiceGetEffect;
    public Image GetDiceImage;
    public Text DiceGetText;
    public List<Sprite> ButtonSpriteList;
    public Sprite NormalDice;
    public GameObject RollingDiceObject;
    public Text Raid_GateLevelText;
    public GameObject LevelPrevButton;
    public GameObject LevelNextButton;
    public GameObject healEffect;
    public Button RollingButton;
    public GameObject SelectEffect;
    public Text RaidLevelText;
    public Text DiceCount;
    public Image HpFillImage;
    public Text HpFillText;
    public ScrollRect listRect;
    public SelectHeroPanelSrc raidHeroList;
    public Button RaidButton;

    public List<GameObject> RaidHeroList;
    public List<InfiniCoin> PowerList;

    public List<GameObject> Raid_Game_PosList;
    public List<Vector3> InitHeroPos = new List<Vector3>();

    public GameObject EnmeyObject;
    public GameObject AttackPos;
    public Image Effect1_1;
    public Image Effect1_2;
    public Image Effect1_3;


    public Image Effect2_1;
    public Image Effect2_2;
    public Image Effect2_3;
    public void CheckAutoPlay()
    {
        GameManager.Instance.isRaidAutoPlay = AutoPlayToggle.isOn;
        GameManager.Instance.Save(GameManager.saveType.isRaidAutoPlay);
        RollingButton.enabled = !GameManager.Instance.isRaidAutoPlay;
        AutoPlayPanel.SetActive(GameManager.Instance.isRaidAutoPlay);
        if(GameManager.Instance.isRaidAutoPlay==true)
        {
            //자동모드
            RollingButton.image.color = UiManager.Instance.disableButtonColor;
        }
        else
        {
            RollingButton.image.color = UiManager.Instance.enableButtonColor;
        }
        delayTime = 1.5f;
        startDelay = 1.5f;
    }
    public void CheckAutoPlayInit()
    {
        AutoPlayToggle.isOn = GameManager.Instance.isRaidAutoPlay;
        RollingButton.enabled = !GameManager.Instance.isRaidAutoPlay;
        AutoPlayPanel.SetActive(GameManager.Instance.isRaidAutoPlay);
        if (GameManager.Instance.isRaidAutoPlay == true)
        {
            //자동모드
            RollingButton.image.color = UiManager.Instance.disableButtonColor;
        }
        else
        {
            RollingButton.image.color = UiManager.Instance.enableButtonColor;
        }
    }
    private void Start()
    {
        if(GameManager.Instance.TutorialList[8]==1)
        {
            GameManager.Instance.TutorialList[8] = 2;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            RaidTutorial.SetActive(true);
        }
        raidHeroList.raidClickEvent += RaidHeroList_raidClickEvent;
        for(int i =0; i< RaidHeroList.Count; i++)
        {
            PowerList.Add(0);
            AllAttackPostion.Add(Raid_Game_PosList[i].transform.position);
            InitHeroPos.Add(Raid_Game_PosList[i].transform.position);
        }
    }
    public bool isAUto = false;
    float delayTime = 1.5f;
    float startDelay = 1.5f;
    private void FixedUpdate()
    {
        if(GameManager.Instance.raidTicketCount >=3)
        {
            TicketTimeText.gameObject.SetActive(false);
        }
        else
        {
            TicketTimeText.gameObject.SetActive(true);
            TicketTimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.TicketTime);
        }
        TicketCountText.text = "x " + GameManager.Instance.raidTicketCount;
        if(GameManager.Instance.isRaidAutoPlay ==true && isStartRaid ==true)
        {
            if(startDelay <0)
            {
                if (GameDIceCount > 0 && isAUto == false)
                {
                    isAUto = true;
                    delayTime = 1.5f;
                    RollingDice();
                }
                else if (isAUto == true && RollingButton.interactable == true)
                {
                    delayTime -= Time.deltaTime;
                    if (delayTime <= 0)
                    {
                        isAUto = false;
                    }
                }
            }
            else
            {
                startDelay -= Time.deltaTime;
            }
           

        }
    }
    void CheckRaidLevel()
    {
        if(GameManager.Instance.RaidNowLevel ==1)
        {
            LevelPrevButton.SetActive(false);
        }
        else
        {
            LevelPrevButton.SetActive(true);
        }
        if(GameManager.Instance.RaidNowLevel == GameManager.Instance.RaidMaxLevel)
        {
            LevelNextButton.SetActive(false);
        }
        else 
        {
            LevelNextButton.SetActive(true);
        }
        Raid_GateLevelText.text = "Lv. " + GameManager.Instance.RaidNowLevel;
    }
    public void NextLevel()
    {
        GameManager.Instance.RaidNowLevel++;
        if (GameManager.Instance.RaidNowLevel > GameManager.Instance.RaidMaxLevel)
        {
            GameManager.Instance.RaidNowLevel = GameManager.Instance.RaidMaxLevel;
        }
        GameManager.Instance.Save(GameManager.saveType.RaidNowLevel);
        CheckRaidLevel();
    }
    public void PrevLevel()
    {
        GameManager.Instance.RaidNowLevel--;
        if (GameManager.Instance.RaidNowLevel <1)
        {
            GameManager.Instance.RaidNowLevel = 1;
        }
        GameManager.Instance.Save(GameManager.saveType.RaidNowLevel);
        CheckRaidLevel();
    }

    private void RaidHeroList_raidClickEvent(int heroindex)
    {       
        
        if (raidHeroPos < 0)
            return;
        listRect.enabled = false;
        GameManager.Instance.heroraidPos[raidHeroPos] = heroindex;
        GameManager.Instance.Save(GameManager.saveType.heroraidPos);
        //Debug.Log(heroindex);
        CheckData();
        CheckRemovePoint(heroindex);
        raidHeroList.gameObject.SetActive(false);
        
    }
    void CheckRemovePoint(int heroIndex =-1)
    {
        for(int i=0; i< GameManager.Instance.heroraidPos.Length; i++)
        {
            if(GameManager.Instance.heroraidPos[i] >-1)
            {
                removeList[i].SetActive(true);
                LevelTextList[i].gameObject.SetActive(true);
                DpsTextList[i].gameObject.SetActive(true);               
               
                UiManager.Instance.SetHeroDPS_UI(DpsTextList[i], GameManager.Instance.heroraidPos[i]);
                UiManager.Instance.SetHeroLevel_UI(LevelTextList[i], GameManager.Instance.heroraidPos[i]);
                               
            }
            else
            {
                removeList[i].SetActive(false);
                LevelTextList[i].gameObject.SetActive(false);
                DpsTextList[i].gameObject.SetActive(false);
            }
        }
    }
    IEnumerator AutoPlayFalseRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        isAUto = false;
    }
    private void OnEnable()
    {
        CheckAutoPlayInit();
        stageCount = 0;
        CheckData();
        CheckRaidLevel();
        RollingDiceObject.SetActive(false);
        RollingButton.image.sprite = NormalDice;
        DiceEffect.SetActive(false);
        RollingButton.interactable = true;
        isAUto = false;
        CheckRemovePoint();
        EndRaidPanel.SetActive(false);

        //for(int i =0; i< 20; i++)
        //{
        //  Debug.Log(UiManager.Instance.SetCost(SystemManager.GetEnmeyHp(i * 3, true) * 12.5f));
        //}
    }
  

    public void RemoveHero(int i)
    {
        GameManager.Instance.heroraidPos[i] = -1;
        CheckRemovePoint();
        CheckData();
        GameManager.Instance.Save(GameManager.saveType.heroraidPos);
    }

    void CheckData()
    {
        for(int i=0; i< GameManager.Instance.heroraidPos.Length; i++)
        {
            if(GameManager.Instance.heroraidPos[i] >-1)
            {
                RaidHeroList[i].SetActive(true);
                UiManager.Instance.SetHeroIcon_UI(RaidHeroList[i].GetComponent<Image>(), GameManager.Instance.heroraidPos[i]);
            }
            else
            {
                RaidHeroList[i].SetActive(false);
            }
        }
        defaultDiceCount = 7;
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.dice] ==1)
        {
            defaultDiceCount +=1;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.dice_1] == 1)
        {
            defaultDiceCount += 1;
        }
        GameDIceCount = defaultDiceCount;
    }

    public void GoRaid()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        bool bAll = true;
        for(int i=0; i< RaidHeroList.Count; i++)
        {
            if(RaidHeroList[i].activeSelf ==true)
            {
                
            }
            else
            {
                bAll = false;
            }
        }
        if(bAll ==false)
        {
            UiManager.Instance.SetNotification("영웅을 모두 등록해야 합니다.");
        }
        else if(GameManager.Instance.raidTicketCount >0)
        {
            //레이드 시작
            startDelay = 1.5f;
            isStartRaid = true;
            GameManager.Instance.raidTicketCount--;
            GameManager.Instance.Save(GameManager.saveType.raidTicketCount);
            StartRaid();
            RollingDiceObject.SetActive(false);
            RollingButton.image.sprite = NormalDice;
            DiceEffect.SetActive(false);
            RollingButton.interactable = true;
            isAUto = false;
            TouchText.SetActive(true);
            stageCount = 0;
            TotalGold = 0;
            
            isMax = false;
            SoundManager.Instance.ChangeDungeon(true);
            EndRaidPanel.SetActive(false);
        }
        else
        {
            //광고보고 티켓 획득
            UiManager.Instance.TicketAdsPanel.SetActive(true);
        }
    }
    bool isStartRaid = false;
    int raidHeroPos = -1;
    public void SelectRaidHero(int selectindex)
    {
        raidHeroPos = selectindex;
        raidHeroList.gameObject.SetActive(true);
    }    
    public void StartRaid()
    {
        //RaidEffect.SetActive(true);
        StartRaidEffect();        

    }
    void StartRaidEffect()
    {
        Effect1_1.gameObject.SetActive(true);
        Effect1_2.gameObject.SetActive(true);
        Effect1_3.gameObject.SetActive(true);
        Effect1_1.DOColor(new Color(1, 1, 1, 1), 1).OnComplete(CompleteStartRaidEffect);
        Effect1_2.DOColor(new Color(1, 1, 1, 1), 1);
        Effect1_3.DOColor(new Color(1, 1, 1, 1), 1);
    }
    void CompleteStartRaidEffect()
    {
        Effect2_1.gameObject.SetActive(true);
        Effect2_2.gameObject.SetActive(true);
        Effect2_3.gameObject.SetActive(true);
        
        UiManager.Instance.MainCanvasGroup.alpha = 0;
        UiManager.Instance.RaidCanvas.SetActive(true);
        GameManager.Instance.SetRaidCamera();
        //setHeros
        SetRaidHeros();
        Effect2_1.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(CompleteStartRaidEffect_2);
        Effect2_2.DOColor(new Color(1, 1, 1, 0), 1);
        Effect2_3.DOColor(new Color(1, 1, 1, 0), 1);
    }
    void CompleteStartRaidEffect_2()
    {
        Effect2_1.gameObject.SetActive(false);
        Effect2_2.gameObject.SetActive(false);
        Effect2_3.gameObject.SetActive(false);

        if (GameManager.Instance.TutorialList[8] == 2)
        {
            GameManager.Instance.TutorialList[8] = 3;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            RaidTutorial2.SetActive(true);
        }
    }
    void CompleteStartRaidEffect_From()
    {
        Effect2_1.gameObject.SetActive(false);
        Effect2_2.gameObject.SetActive(false);
        Effect2_3.gameObject.SetActive(false);
    }
    void CompleteStartRaidEffect_Exit()
    {
        Effect2_1.gameObject.SetActive(false);
        Effect2_2.gameObject.SetActive(false);
        Effect2_3.gameObject.SetActive(false);
        Effect1_1.gameObject.SetActive(true);
        Effect1_2.gameObject.SetActive(true);
        Effect1_3.gameObject.SetActive(true);

        
        UiManager.Instance.MainCanvasGroup.alpha = 1;
        UiManager.Instance.RaidCanvas.SetActive(false);
        GameManager.Instance.ImidiMoveCamera();

        Effect1_1.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(EndCompleteStartRaidEffect_Exit);
        Effect1_2.DOColor(new Color(1, 1, 1, 0), 1);
        Effect1_3.DOColor(new Color(1, 1, 1, 0), 1);

        //여기서 레이드 화면 초기화
        CheckData();
        CheckRaidLevel();

        if(GameManager.Instance.TutorialList[9] ==0)
        {
            GameManager.Instance.TutorialList[9] = 1;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            UiManager.Instance.CheckTurorialLock();
            UiManager.Instance.NewContentEventPanel.SetActive(true);
            UiManager.Instance.NewContentEventPanel.GetComponent<NewContentEventSrc>().SetData("altar");
        }
    }
    void EndCompleteStartRaidEffect_Exit()
    {
        Effect1_1.gameObject.SetActive(false);
        Effect1_2.gameObject.SetActive(false);
        Effect1_3.gameObject.SetActive(false);
    }
    public void ExitRaid()
    {
        isStartRaid = false;
        bMoveHero = false;
        startDelay = 1.5f;
        Effect2_1.gameObject.SetActive(true);
        Effect2_2.gameObject.SetActive(true);
        Effect2_3.gameObject.SetActive(true);
        Effect2_1.DOColor(new Color(1, 1, 1, 1), 1).OnComplete(CompleteStartRaidEffect_Exit);
        Effect2_2.DOColor(new Color(1, 1, 1, 1), 1);
        Effect2_3.DOColor(new Color(1, 1, 1, 1), 1);
        SoundManager.Instance.ChangeDungeon(false);
    }
    bool bMoveHero = false;
    void SetRaidHeros()
    {
        for(int i=0; i< GameManager.Instance.heroraidPos.Length;i++)
        {
            if(GameManager.Instance.heroraidPos[i] >-1)
            {
                Raid_Game_PosList[i].SetActive(true);
                Raid_Game_PosList[i].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.HeroList[GameManager.Instance.heroraidPos[i]].GetComponent<SpriteRenderer>().sprite;
                if (Raid_Game_PosList[i].GetComponent<Animator>() == null)
                {
                    Raid_Game_PosList[i].gameObject.AddComponent<Animator>();
                }
                
                Animator temp = GameManager.Instance.HeroList[GameManager.Instance.heroraidPos[i]].GetComponent<Animator>();
                Raid_Game_PosList[i].gameObject.GetComponent<Animator>().runtimeAnimatorController = temp.runtimeAnimatorController;
                Raid_Game_PosList[i].GetComponent<Animator>().Play("idle");
                if(GameManager.Instance.timerCotroller.bStart_attackBuff ==true)
                {
                    PowerList[i] = GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].DPS*2;
                }
                else
                {
                    PowerList[i] = GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].DPS;
                }
                Raid_Game_PosList[i].transform.position = InitHeroPos[i];
                
            }
            else
            {
                Raid_Game_PosList[i].SetActive(false);
            }
        }        
        SetEnemy();
        bMoveHero = true;
    }
    
    public void SetEnemy()
    {
        GameObject temp;
        if (GameManager.Instance.RaidNowLevel >= GameManager.Instance.enemyManager.BossList.Count)
        {
            int rand = Random.Range(0, GameManager.Instance.enemyManager.BossList.Count);
            temp = GameManager.Instance.enemyManager.BossList[rand];
        }
        else
        {
            temp = GameManager.Instance.enemyManager.BossList[GameManager.Instance.RaidNowLevel];
        }

        EnmeyObject.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
        if (EnmeyObject.GetComponent<Animator>() == null)
        {
            EnmeyObject.gameObject.AddComponent<Animator>();
        }
        Animator tempAnim = temp.GetComponent<Animator>();
        EnmeyObject.gameObject.GetComponent<Animator>().runtimeAnimatorController = tempAnim.runtimeAnimatorController;
        
        EnmeyObject.GetComponent<Animator>().Play("idle");
        
        monsterHp = SystemManager.GetEnmeyHp(GameManager.Instance.RaidNowLevel*3 ,true) * 12.5f;
        defaultHp = monsterHp;
        CheckHpFill(monsterHp);
        SetText();

    }
    InfiniCoin defaultHp = new InfiniCoin();
    InfiniCoin monsterHp = new InfiniCoin();
    int stageCount;
    InfiniCoin TotalGold = new InfiniCoin();
    public void CheckHpFill(InfiniCoin hp)
    {
        //100 -10
        if(bColorAttack ==false)
        {
            bColorAttack = true;
            if(gameObject.activeSelf ==true)
            {
                StartCoroutine(SetAttackRoutine());
            }
            
        }

        
        double deltaHp = (hp.baseValue / defaultHp.baseValue);
        if (deltaHp > 1)
        {
            deltaHp = deltaHp / 1000;
        }

        //Debug.Log(deltaHp);
        HpFillImage.fillAmount = float.Parse(deltaHp.ToString());
        HpFillText.text = UiManager.Instance.SetCost(monsterHp);
        CheckNext();
    }
    void CheckNext()
    {
        if (monsterHp < 1)
        {
            //Debug.Log("보스 잡음");
            TotalGold += SystemManager.GetGold(GameManager.Instance.RaidNowLevel + 10);
            BossDeathEffect.SetActive(true);
            LevelUPText.SetActive(true);
            //if (GameDIceCount > 0)
            {
                GameManager.Instance.RaidNowLevel++;
                GameManager.Instance.Save(GameManager.saveType.RaidNowLevel);
                if (GameManager.Instance.RaidMaxLevel < GameManager.Instance.RaidNowLevel)
                {
                    GameManager.Instance.RaidMaxLevel = GameManager.Instance.RaidNowLevel;
                    GameManager.Instance.Save(GameManager.saveType.RaidMaxLevel);
                    GameManager.Instance.achivementData.AchivementCount[4]++;
                    GameManager.Instance.Save(GameManager.saveType.achivementData);
                    isMax = true;
                }
                SetEnemy();
                stageCount++;
            }

            if (GameManager.Instance.subquestType == GameManager.SubQuestType.raidBossKill)
            {
                GameManager.Instance.subQuestNow++;
                GameManager.Instance.Save(GameManager.saveType.subQuestNow);
                UiManager.Instance.SetSubQuestText();
            }
        }
    }
    bool isMax = false;
    float getBuff(string ablity)
    {
        string[] splitStr = ablity.Split('_');        
        float buff = 0;
        if(splitStr[0] == "raid")
        {
            buff = float.Parse(splitStr[1]) * 0.01f;
        }

        return buff;
    }
    void CheckEnd()
    {
        float buff = 0;
        if (GameDIceCount <= 0)
        {
            for(int i =0; i< GameManager.Instance.heroraidPos.Length;i++)
            {
                if(GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].isAblity_1 == true)
                {
                    buff += getBuff(GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].specialAblity1);
                }
                if (GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].isAblity_2==true)
                {
                    buff += getBuff(GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].specialAblity2);
                }
                if (GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].isAblity_3 == true)
                {
                    buff += getBuff(GameManager.Instance.herosInfos[GameManager.Instance.heroraidPos[i]].specialAblity3);
                }
            }
            CheckNext();
            if(stageCount >0)
            {
                EndRaidPanel.SetActive(true);
                EndRaidPanel.GetComponent<RaidEndPanelSrc>().ShowReward(stageCount, buff, isMax);
                isStartRaid = false;
            }
            else
            {
                ExitRaid();
            }
            
        }
    }
    int defaultDiceCount = 7;
    int GameDIceCount;
    void SetText()
    {
        int level = GameManager.Instance.RaidNowLevel;
        RaidLevelText.text = "- 레이드 <color=red>레벨 " + level + "</color> -";
        DiceCount.text = "남은 횟수 : " + GameDIceCount.ToString();
    }
    int SternHero = -1;
    private void OnDisable()
    {
        if (EndRaidPanel != null)
            EndRaidPanel.SetActive(false);      
    }
    public void RollingDice()
    {
        if(GameDIceCount >0)
        {
            RollingButton.interactable = false;
            //여기서 선택하고
            TouchText.SetActive(false);
            //            
            isDoubleAttack = 1;
            SetText();
            int DiceRand = Random.Range(0, 6);

            if(gameObject.activeSelf == true)
            {
                StartCoroutine(rollingDiceRoutine(DiceRand));
            }
            
        }
        else
        {
            RollingButton.interactable = false;
        }

        
        //영웅선택
    }
    IEnumerator rollingDiceRoutine(int DiceRand)
    {
        RollingDiceObject.SetActive(true);
        RollingButton.gameObject.SetActive(false);
        RollingDiceObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.7f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.7f);        
        RollingDiceObject.transform.DOLocalMove(new Vector3(0, -450, 0), 0.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.5f);
        RollingDiceObject.SetActive(false);
        RollingButton.gameObject.SetActive(true);
        RollingButton.image.sprite = ButtonSpriteList[DiceRand];
        GetDiceImage.sprite = EffectSpriteList[DiceRand];
        DiceEffect.GetComponent<UiTargetManager>().y_Margin = 50;
        switch (DiceRand)
        {
            case 0:
                //공격
                if (gameObject.activeSelf == true)
                    StartCoroutine(rollingHero(1));
                GameDIceCount--;
                DiceGetText.text = "공격 !";                
                break;
            case 1:
                //더블 공격
                if (gameObject.activeSelf == true)
                    StartCoroutine(rollingHero(2));
                GameDIceCount--;
                DiceGetText.text = "더블 어택 !";
                
                break;
            case 2:
                if (gameObject.activeSelf == true)
                    StartCoroutine(AllAttackRoutine());
                GameDIceCount--;
                DiceGetText.text = "전체 공격 !";
                
                DiceEffect.SetActive(true);
                DiceGetEffect.SetActive(true);
                DiceEffect.GetComponent<UiTargetManager>().y_Margin = 150;
                DiceEffect.GetComponent<UiTargetManager>().SetWorldObject(RaidButton.gameObject, UiTargetManager.TextType.attack);
                //전체공격
                break;
            case 3:
                //다이스 횟수 추가
                GameDIceCount++;
                
                RollingButton.interactable = true;
                DiceGetText.text = "횟수 증가 !";
                DiceEffect.SetActive(true);
                DiceGetEffect.SetActive(true);
                
                DiceEffect.GetComponent<UiTargetManager>().y_Margin = 150;
                DiceEffect.GetComponent<UiTargetManager>().SetWorldObject(RaidButton.gameObject, UiTargetManager.TextType.attack);
                break;
            case 4:
                //적 회복
                monsterHp += defaultHp * 0.2;
                if (monsterHp > defaultHp)
                {
                    defaultHp = monsterHp;
                }
                GameDIceCount--;
                CheckHpFill(monsterHp);
                healEffect.SetActive(true);
                RollingButton.interactable = true;
                
                DiceEffect.SetActive(true);
                DiceGetEffect.SetActive(true);                
                DiceEffect.GetComponent<UiTargetManager>().SetWorldObject(EnmeyObject, UiTargetManager.TextType.attack);
                DiceGetText.text = "체력 회복 !";
                CheckEnd();
                break;
            case 5:
                //공격
                if (gameObject.activeSelf == true)
                    StartCoroutine(rollingHero(3));
                GameDIceCount--;
                DiceGetText.text = "마법 공격 !";
                
                
                break;
        }
        SetText();
        if (DiceRand == 2 || DiceRand ==3 || DiceRand ==4)
        {
            yield return new WaitForSeconds(1f);
            DiceEffect.SetActive(false);
            DiceGetEffect.SetActive(false);
        }        
    }
    bool bColorAttack = false;
    IEnumerator SetAttackRoutine()
    {
        if(bMoveHero ==true)
        {
            EnmeyObject.GetComponent<SpriteRenderer>().color = UiManager.Instance.AttackColor;
            yield return new WaitForSeconds(0.1f);
            EnmeyObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            bColorAttack = false;
        }        
    }

    List<Vector3> AllAttackPostion = new List<Vector3>();
    IEnumerator AllAttackRoutine()
    {
        RollingButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        for(int i =0; i< Raid_Game_PosList.Count; i++)
        {
            if(i != SternHero && bMoveHero ==true)
            {
                AllAttackPostion[i] = Raid_Game_PosList[i].transform.position;
                Raid_Game_PosList[i].GetComponent<Animator>().Play("move");
                Raid_Game_PosList[i].transform.DOMove(AttackPos.transform.position, 1);
                if (gameObject.activeSelf == true)
                    StartCoroutine(AllAttack_1(i));
                yield return new WaitForSeconds(1f);
            }         
        }

        if(SternHero !=-1)
        {
            SternHero = -1;
        }
        
    }   
    public void setAttack(InfiniCoin Power,bool bCritical)
    {
        if(bCritical ==false)
        {
            UiManager.Instance.SetTextPool(EnmeyObject, Power, UiTargetManager.TextType.attack, true,null);
        }
        else
        {
            UiManager.Instance.SetTextCritial(EnmeyObject, Power,true,null);
        }
        
    }
    
    bool checkCritical()
    {
        int iCritical = Random.Range(0, 100);
        if(iCritical <= 5)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    IEnumerator AllAttack_1(int pos)
    {
        yield return new WaitForSeconds(1f);
        Raid_Game_PosList[pos].GetComponent<Animator>().Play("attack");
        yield return new WaitForSeconds(0.4f);
        if(checkCritical() == false)
        {
            monsterHp -= PowerList[pos];
            setAttack(PowerList[pos],false);
        }
        else
        {
            monsterHp -= (PowerList[pos]*2);
            setAttack(PowerList[pos]*2, true);
        }
        
        
        CheckHpFill(monsterHp);
        yield return new WaitForSeconds(0.5f);
        if(bMoveHero == true)
        {
            Raid_Game_PosList[pos].GetComponent<Animator>().Play("move");
            Raid_Game_PosList[pos].transform.DOMove(AllAttackPostion[pos], 1);
        }        
        yield return new WaitForSeconds(1f);
        Raid_Game_PosList[pos].GetComponent<Animator>().Play("idle");
        if(SternHero ==-1)
        {
            if (pos == 4)
            {
                RollingButton.interactable = true;
                CheckEnd();
                
            }
        }
        else
        {
            if (pos == 3)
            {
                RollingButton.interactable = true;
                CheckEnd();
                
            }
        }
        


    }
    IEnumerator rollingHero(int index)
    {
        int rand = Random.Range(10, 20);
        int heroPosIndex =-1;
        bool bStren = false;
        for(int i=0; i <rand; i++)
        {
            if(SternHero != i)
            {                
                int randHero = Random.Range(0, Raid_Game_PosList.Count);
                SelectEffect.transform.SetParent(Raid_Game_PosList[randHero].transform);
                SelectEffect.transform.localPosition = new Vector3(0, -0.15f, 0);
                SelectEffect.SetActive(true);
                heroPosIndex = randHero;
                yield return new WaitForSeconds(0.15f);
            }
            else
            {
                bStren = true;
            }
            
        }
        DiceEffect.GetComponent<UiTargetManager>().SetWorldObject(Raid_Game_PosList[heroPosIndex], UiTargetManager.TextType.attack);
        DiceEffect.SetActive(true);
        DiceGetEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (bStren == false)
        {
            SelectEffect.SetActive(false);
            StartAttact(heroPosIndex, index);            
            

        }
        else
        {
            RollingButton.interactable = true;
            
        }

        if (SternHero != -1)
        {
            SternHero = -1;
        }
        yield return new WaitForSeconds(0.5f);
        DiceGetEffect.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        DiceEffect.SetActive(false);
    }
    int attackHeroPosition = -1;
    int isDoubleAttack = 1;
    Vector3 initPos = new Vector3(100,100,100);
    void StartAttact(int heroPos,int attackType)
    {
        if (heroPos == -1)
            return;
        isDoubleAttack = attackType;
        attackHeroPosition = heroPos;
        initPos = Raid_Game_PosList[heroPos].transform.position;
        Raid_Game_PosList[attackHeroPosition].GetComponent<Animator>().Play("move");
        Raid_Game_PosList[heroPos].transform.DOMove(AttackPos.transform.position, 1).OnComplete(AttackEnd);        
        
    }
    IEnumerator AttackRoutine(int isDouble)
    {
        if(isDouble ==2)
        {
            yield return new WaitForSeconds(0.5f);
            if(checkCritical()==false)
            {
                monsterHp -= PowerList[attackHeroPosition];
                setAttack(PowerList[attackHeroPosition],false);
            }
            else
            {
                monsterHp -= PowerList[attackHeroPosition]*2;
                setAttack(PowerList[attackHeroPosition]*2,true);
            }
            
            CheckHpFill(monsterHp);
            Raid_Game_PosList[attackHeroPosition].GetComponent<Animator>().Play("attack");
            yield return new WaitForSeconds(0.5f);
            if(checkCritical() == false)
            {
                monsterHp -= PowerList[attackHeroPosition];
                setAttack(PowerList[attackHeroPosition],false);
            }
            else
            {
                monsterHp -= PowerList[attackHeroPosition]*2;
                setAttack(PowerList[attackHeroPosition]*2,true);
            }
            
            CheckHpFill(monsterHp);
        }
        else if(isDouble == 3)
        {                     
            yield return new WaitForSeconds(0.5f);
            SkillEffect.SetActive(true);
            if (checkCritical() == false)
            {
                monsterHp -= (PowerList[attackHeroPosition] * 3);
                setAttack(PowerList[attackHeroPosition] * 3,false);
            }
            else
            {
                monsterHp -= (PowerList[attackHeroPosition] * 5);
                setAttack(PowerList[attackHeroPosition] * 5, true);
            }
            
            CheckHpFill(monsterHp);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            if(checkCritical() == false)
            {
                monsterHp -= PowerList[attackHeroPosition];
                setAttack(PowerList[attackHeroPosition],false);
            }
            else
            {
                monsterHp -= PowerList[attackHeroPosition]*2;
                setAttack(PowerList[attackHeroPosition]*2,true);
            }
            
            CheckHpFill(monsterHp);
        }
        
        
        yield return new WaitForSeconds(1f);
        Raid_Game_PosList[attackHeroPosition].GetComponent<Animator>().Play("move");
        Raid_Game_PosList[attackHeroPosition].transform.DOMove(initPos, 1).OnComplete(EndAttack);
    }
    void EndAttack()
    {
        Raid_Game_PosList[attackHeroPosition].GetComponent<Animator>().Play("idle");
        RollingButton.interactable = true;
        CheckEnd();
        
    }
    IEnumerator m_AttackRoutine;
    void AttackEnd()
    {
        if(gameObject.activeSelf ==true)
        {
            StartCoroutine(AttackRoutine(isDoubleAttack));

            Raid_Game_PosList[attackHeroPosition].GetComponent<Animator>().Play("attack");
        }
        
    }
}


