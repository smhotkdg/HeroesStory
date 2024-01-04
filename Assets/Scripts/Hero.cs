using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using GR_InfiniCoin;
using SensorToolkit;
public class Hero : MonoBehaviour
{
    public SoundManager.SoundFXType fXType;
    public enum HeroType
    {
        normal,
        raid,
        transDungeon,
        non,
        
    }
    public RangeSensor2D rangeSensor;
    public RangeSensor2D AttackRangeSensor;
    public HeroType heroType;
    [SerializeField]
    public InfiniCoin Power;
    [SerializeField]
    public InfiniCoin InitDPS;
    GameManager gameManager;
    Animator animator;
    [SerializeField]
    float AttackDelayTime;
    public int HerosPos;

    IEnumerator coroutine;
    IEnumerator coroutineLerp;
    float lerpDuration = 1;


    public Vector3 oriPos;
    float x1Min = 1f;
    float x1Max = 2.1f;
    float y1Min = 1.78f;
    float y1Max = 2.17f;

    float x2Min = 0.9f;
    float x2Max = 1.4f;
    float y2Min = 2f;
    float y2Max = 2.64f;

    public bool bStartGame = false;

    private void Start()
    {
        if (heroType != HeroType.raid)
        {
            gameManager = GameManager.Instance;
            animator = GetComponent<Animator>();
            AttackDelayTime = SystemManager.Attack_time[int.Parse(this.name)];
            bAttack = true;

            SetOriPos();
            lerpDuration = AttackDelayTime * 1.5f;
            LevelUP();
            UpgradeMovespeed = 0;
            CheckDisableItem();
        }
        if(rangeSensor!=null)
        {
            
        }
        
    }
    

    public void SetOriPos()
    {
        if (heroType != HeroType.raid)
        {
            if(heroType == HeroType.normal)
            {
                oriPos = transform.position;
                if (gameManager == null)
                    gameManager = GameManager.Instance;
            }
            else
            {
                oriPos = new Vector3(-50, -3, 0);
            }
            Power = gameManager.herosInfos[int.Parse(this.name)].DPS;
        }
    }
    public void SetInitPosition()
    {
        if(heroType == HeroType.normal)
        {
            //bFindObject = false;
            //TestMove = false;        
            bFindObject = false;
            TestMove = false;
            animator.Play("move");
            Vector3 DefaultPos = oriPos;
            DefaultPos.y = oriPos.y - 5;
            transform.position = DefaultPos;
            StartCoroutine(InitMoveRoutine_pos());
        }
      
    }
    IEnumerator InitMoveRoutine_pos()
    {
        yield return new WaitForSeconds(0.3f);
        bFindObject = false;
        TestMove = false;
    }
    private void OnEnable()
    {
        if (heroType != HeroType.raid)
        {
            bAttack = true;

            if (heroType == HeroType.normal)
                transform.position = oriPos;
            else
                transform.position = new Vector3(-50,-3, 0);
            SetStageMove();
            //SetMove();
            if (heroType == HeroType.normal)
            {
                for (int i = 0; i < GameManager.Instance.heroPos.Length; i++)
                {
                    if (GameManager.Instance.heroPos[i] == int.Parse(this.name))
                    {
                        HerosPos = i;
                    }
                }
            }
         

            CheckDisableItem();
        }
      
    }
    private void OnDisable()
    {
        if (heroType != HeroType.raid)
        {
            coroutine = EndAttackRoutine();
            StopCoroutine(coroutine);
            if(bStartGetGold ==true)
            {
                bStartGetGold = false;
                GameManager.Instance.SetbItemGetGold(false);
                if (GoldItemRoutine != null)
                    StopCoroutine(GoldItemRoutine);
            }
        }
        
    }
    private void OnMouseDown()
    {
        if (heroType != HeroType.raid)
        {
            if (IsPointerOverUIObject(Input.mousePosition))
            {
                return;
            }

            //Debug.Log("Hero" + this.name);
            //이거할까말까?
            //UiManager.Instance.ShowHeroInfoPanel(int.Parse(this.name));
        }
    } 
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
    bool bAttack = false;
    public float speed = 1.0f;
    private float moveSpeed;
    private float startTime;
    private float journeyLength = 10;
    public bool TestMove =false;
    public bool bFindObject = false;
    Vector3 dst = new Vector3(0, 0, 0);

    public void SetStageMove()
    {
        TestMove = false;
        bFindObject = false;
    }

    private void FixedUpdate()   
    {

        if(bMoveTop ==true)
        {
            Vector3 initMove = new Vector3(transform.position.x, 20, 0);
            float movespeed = speed + (speed * UpgradeMovespeed);
            float distCovered = (Time.deltaTime) * movespeed * 1.5f;
            float fractionOfJourney = distCovered / 10;

            transform.position = Vector3.Lerp(transform.position, initMove, fractionOfJourney);

            Quaternion target = Quaternion.Euler(0, 0, 0);
            if (transform.position.x > 0)
            {
                target = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                target = Quaternion.Euler(0, 180, 0);
            }
            transform.rotation = target;
            animator.Play("move");
        }
        
        if (heroType != HeroType.raid)
        {
            if (bMove == true)
                return;
            if (gameManager != null)
            {
                //if (gameManager.bStartEnemy == true && bAttack == true)
                //{
                //    bAttack = false;

                //    animator.Play("attack");
                //    coroutine = EndAttackRoutine();
                //    StartCoroutine(coroutine);
                //    //Lerp();

                //}
            }
        }
     
        if (rangeSensor !=null)
        {
            List<GameObject> temp = rangeSensor.GetDetected();
            GameObject tempSensor = rangeSensor.GetNearest();
            if (temp.Count==5)
            {
                
                if (bFindObject ==false)
                {
                    int rand = Random.Range(0, temp.Count);
                    dst = temp[HerosPos/5].transform.position;
                    bFindObject = true;
                }
              
               
                if(AttackRangeSensor.GetNearest() !=null)
                {
                    //멈추기
                    if (heroType == HeroType.normal)
                    {
                        if (gameManager.bStartEnemy == true && bAttack == true)
                        {
                            Quaternion target = transform.rotation;
                            if (tempSensor.name == "Left")
                            {
                                target = Quaternion.Euler(0, 180, 0);
                            }
                            else if (tempSensor.name == "Right")
                            {
                                target = Quaternion.Euler(0, 0, 0);
                            }

                            transform.rotation = target;
                            bAttack = false;
                            animator.Play("attack");
                            coroutine = EndAttackRoutine();
                            StartCoroutine(coroutine);
                            TestMove = true;
                        }
                    }
                    if(heroType == HeroType.transDungeon)
                    {
                        if (gameManager.bStartEnemy_trans == true && bAttack == true)
                        {
                            Quaternion target = transform.rotation;
                            if (tempSensor.name == "Left")
                            {
                                target = Quaternion.Euler(0, 180, 0);
                            }
                            else if (tempSensor.name == "Right")
                            {
                                target = Quaternion.Euler(0, 0, 0);
                            }

                            transform.rotation = target;
                            bAttack = false;
                            animator.Play("attack");
                            coroutine = EndAttackRoutine();
                            StartCoroutine(coroutine);
                            TestMove = true;
                        }
                    }
                                                    
                }
                else if(TestMove ==false)
                {
                    float movespeed = speed + (speed * UpgradeMovespeed);
                    float distCovered = (Time.deltaTime) * movespeed;
                    float fractionOfJourney = distCovered / 10;
                    Vector3 prevPos = transform.position;                    
                    transform.position = Vector3.Lerp(transform.position, dst, fractionOfJourney);
                    Vector3 nowPos = transform.position;
                    Quaternion target = Quaternion.Euler(0, 0, 0);
                    if (prevPos.x > nowPos.x)
                    {
                        target = Quaternion.Euler(0, 0, 0);                        
                    }
                    else
                    {
                        target = Quaternion.Euler(0, 180, 0);
                    }
                    transform.rotation = target;
                    animator.Play("move");
                }
                
            }
            else if(TestMove ==false)
            {
                if(heroType == HeroType.normal)
                {
                    Vector3 initMove = new Vector3(transform.position.x, transform.position.y + 3, 0);
                    float deltaSpeed = speed;
                    if (initMove.y < GameManager.Instance.EnemyPos.position.y - 3.5f)
                    {
                        deltaSpeed = deltaSpeed * 10;
                    }
                    else
                    {
                        float movespeed = speed + (speed * UpgradeMovespeed);
                        deltaSpeed = movespeed;
                    }
                    float distCovered = (Time.deltaTime) * deltaSpeed;
                    float fractionOfJourney = distCovered / 10;

                    transform.position = Vector3.Lerp(transform.position, initMove, fractionOfJourney);

                    Quaternion target = Quaternion.Euler(0, 0, 0);
                    if (transform.position.x > 0)
                    {
                        target = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        target = Quaternion.Euler(0, 180, 0);
                    }
                    transform.rotation = target;
                    animator.Play("move");
                }
                if (heroType == HeroType.transDungeon)
                {
                    Vector3 initMove = new Vector3(transform.position.x, transform.position.y + 3, 0);
                    float deltaSpeed = speed;
                    if (initMove.y < GameManager.Instance.EnmeyPos_trans.position.y - 3.5f)
                    {
                        deltaSpeed = deltaSpeed * 10;
                    }
                    else
                    {
                        float movespeed = speed + (speed * UpgradeMovespeed);
                        deltaSpeed = movespeed;
                    }
                    float distCovered = (Time.deltaTime) * deltaSpeed;
                    float fractionOfJourney = distCovered / 10;

                    transform.position = Vector3.Lerp(transform.position, initMove, fractionOfJourney);

                    Quaternion target = Quaternion.Euler(0, 0, 0);
                    if (transform.position.x > 0)
                    {
                        target = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        target = Quaternion.Euler(0, 180, 0);
                    }
                    transform.rotation = target;
                    animator.Play("move");
                }

            }
            
        }
    }
    [SerializeField]
    InfiniCoin LevelUpCost;
    
    public void LevelUP()
    {
        LevelUpCost = SystemManager.InitCost[int.Parse(this.name)] * SystemManager.mathPow(1.07f, GameManager.Instance.herosInfos[int.Parse(this.name)].level);
    }
    public void SetPower(InfiniCoin _power)
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;
        Power = _power;
        GameManager.HerosInfo heros = gameManager.herosInfos[int.Parse(this.name)];
        heros.DPS = _power;
        gameManager.herosInfos[int.Parse(this.name)] = heros;
        InitDPS = gameManager.herosInfos[int.Parse(this.name)].InitDPS;
        //GameManager.Instance.Save(GameManager.saveType.herosInfos);
    }
    bool bFirstAttack = false;
    bool MagicAttackCheck(float chance,float atk)
    {
        int rand = Random.Range(0, 100);
        if(rand <= chance)
        {
            gameManager.Attack(Power+(Power + (Power*atk*0.01f)),false,this.gameObject);
            gameManager.SetItemEffect(ItemEffectController.EffectType.magicAttack, "마법 공격!", 1.25f, gameObject);
            Debug.Log("마법공격!!");
            return true;
        }
        return false;
    }
    bool BossTimeCheck(float chance,float time)
    {
        int rand = Random.Range(0, 100);
        if (rand <= chance)
        {
            if(gameManager.bStartBoss ==true)
            {
                UiManager.Instance.fBossTime += time;
                gameManager.SetItemEffect(ItemEffectController.EffectType.BossTime, "시간 증가!", 1.25f, gameObject);
                return true;
            }
        }
        return false;
    }
    bool bStartGetGold = false;
    IEnumerator GoldItemRoutine;
    bool GoldGetCheck(float chance,float sec)
    {
        int rand = Random.Range(0, 100);
        if(rand <= chance)
        {
            if(bStartGetGold ==false)
            {
                Debug.Log("골드 획득 버프!!"+sec +" 초 동안 지속");
                GoldItemRoutine = StartGetGoldRoutine(sec);
                StartCoroutine(GoldItemRoutine);
                bStartGetGold = true;
                GameManager.Instance.SetbItemGetGold(true);
                gameManager.SetItemEffect(ItemEffectController.EffectType.Gold, "골드 획득버프!", sec, gameObject);
                return true;
            }
        }
        return false;            
    }
    IEnumerator StartGetGoldRoutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        bStartGetGold = false;
        GameManager.Instance.SetbItemGetGold(false);
        //transform.position = oriPos;
    }
    bool MonsterHpCheck(float chance,float hp)
    {
        int rand = Random.Range(0, 100);
        if (rand <= chance)
        {
            if (gameManager.bStartBoss == true)
            {
                Debug.Log("최대 체력 공격!!");
                gameManager.AttackHp(hp * 0.01f);
                gameManager.SetItemEffect(ItemEffectController.EffectType.MonterHp, "현재 체력공격!", 1.25f, gameObject);
                return true;
            }
        }
        return false;
    }
    public float UpgradeMovespeed;
    void MoveSpeedCheck(float percent)
    {
        UpgradeMovespeed += percent*0.01f;        
    }
    void CriticalChanceCheck(float criChance)
    {
        m_ItemCriticalChance += criChance;
    }
    public float m_ItemAttackBuff;
    void ItemAttackCheck(float buff)
    {
        m_ItemAttackBuff += buff;
    }
    public float m_ItemCriticalAttackBuff;
    void ItemCriticalATK(float buff)
    {
        m_ItemCriticalAttackBuff += buff;
    }
  
    public float reduceExpedtionTime = 0;
    void ExpedtionTimeCheck(float percent)
    {
        reduceExpedtionTime += percent * 0.01f;
    }
    bool bStartAttackBuff = false;
    void AttackBuffCheck(float chance,float sec)
    {
        int rand = Random.Range(0, 100);
        if (rand <= chance)
        {
            if (bStartAttackBuff == false)
            {
                Debug.Log("공격력 버프!!" + sec + " 초 동안 지속");
                StartCoroutine(StartGetAttackBuffRoutine(sec));
                bStartAttackBuff = true;                
            }
        }
    }
    IEnumerator StartGetAttackBuffRoutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        bStartAttackBuff = false;
        
        //transform.position = oriPos;
    }
    void InitItemCheck(int ablityIndex, GameManager.Item itemPos)
    {
      
        switch (ablityIndex)
        {
            case 4:
                //ablityName = "<color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color>이동속도 증가";
                //MoveSpeedCheck(SystemManager.ItemAblityStr[itemPos]*10);
                MoveSpeedCheck(itemPos.ablityPower * 10);
                break;
            case 5:
                //ablityName = "치명타 확률 <color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color>증가";
                CriticalChanceCheck(itemPos.ablityPower);
                break;
            case 6:
                //공격력 증가
                ItemAttackCheck(itemPos.ablityPower);
                break;
            case 7:
                //ablityName = "원정대 시간<color=red> " + SystemManager.ItemAblityStr[itemPos] + "% </color>감소";                
                ExpedtionTimeCheck(itemPos.ablityPower);
                break;
            case 8:
                //치명타 데미지 증가
                ItemCriticalATK(itemPos.ablityPower);
                break;
                

        }
    }
    public void CheckDisableItem()
    {
        reduceExpedtionTime = 0;
        UpgradeMovespeed = 0;
        m_ItemCriticalChance = 0;
        m_ItemAttackBuff = 0;
        m_ItemCriticalAttackBuff = 0;
        if (GameManager.Instance.herosInfos[int.Parse(this.name)].item_1.isGet == true)
        {
            InitItemCheck(GameManager.Instance.herosInfos[int.Parse(this.name)].item_1.ablityType, GameManager.Instance.herosInfos[int.Parse(this.name)].item_1);
        }
        if (GameManager.Instance.herosInfos[int.Parse(this.name)].item_2.isGet == true)
        {
            InitItemCheck(GameManager.Instance.herosInfos[int.Parse(this.name)].item_2.ablityType, GameManager.Instance.herosInfos[int.Parse(this.name)].item_2);
        }
    }
    bool ItemAblityCheck(int ablityIndex,GameManager.Item itemPos)
    {
        bool bAblity = false;
        //string ablityName = "";
        switch (ablityIndex)
        {
            case 0:
                //ablityName = "<color=red>"+SystemManager.ItemChance[itemPos] +" % </color>확률로 공격력의 <color=red>" + SystemManager.ItemChance[itemPos]*20 + "% </color>마법공격";
                //
                //MagicAttackCheck(SystemManager.ItemChance[itemPos], SystemManager.ItemChance[itemPos] * 20);
                bAblity = MagicAttackCheck(itemPos.ablityPower, itemPos.ablityPower * 20);                
                break;
            case 1:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 보스전 시간<color=red>" + SystemManager.ItemAblityStr[itemPos] + " 초</color> 증가";
                //BossTimeCheck(SystemManager.ItemChance[itemPos], SystemManager.ItemAblityStr[itemPos]);
                bAblity  = BossTimeCheck(itemPos.ablityPower, itemPos.PChance);                
                break;
            case 2:
                //ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 골드 획득<color=red>" + SystemManager.ItemTime[itemPos] + "초 동안</color> 증가";
                bAblity = GoldGetCheck(itemPos.ablityPower, itemPos.time);
                
                break;
            case 3:
                // ablityName = "<color=red>" + SystemManager.ItemChance[itemPos] + "% </color>확률로 몬스터 최대체력의 <color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color> 공격";
                bAblity = MonsterHpCheck(itemPos.ablityPower, itemPos.PChance);
                
                break;
            case 4:
                //ablityName = "<color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color>이동속도 증가";
                //MoveSpeedCheck();
                break;
            case 5:
                //ablityName = "치명타 확률 <color=red>" + SystemManager.ItemAblityStr[itemPos] + "% </color>증가";
                //CriticalChanceCheck();
                break;
            case 6:
                //공격력
                //DoubleAttackCheck(SystemManager.ItemChance[itemPos]);
                break;
            case 7:
               // ablityName = "원정대 시간<color=red> " + SystemManager.ItemAblityStr[itemPos] + "% </color>감소";
                //RaidTimeCheck();
                break;
            case 8:
                //치명타 공격력
                //AttackBuffCheck(SystemManager.ItemChance[itemPos], SystemManager.ItemTime[itemPos]);
                break;

        }
        return bAblity;
    }
    float m_ItemCriticalChance = 0;
    public void Attack()
    {
        if (TestMove == false)
        {
            bFirstAttack = false;            
            return;
        }
            
        if (heroType != HeroType.raid)
        {
            bool bAblity = false;
            if(gameManager.herosInfos[int.Parse(this.name)].item_1.isGet ==true)
            {
                bAblity = ItemAblityCheck(gameManager.herosInfos[int.Parse(this.name)].item_1.ablityType,gameManager.herosInfos[int.Parse(this.name)].item_1);
            }
            if (gameManager.herosInfos[int.Parse(this.name)].item_2.isGet == true)
            {
                bAblity = ItemAblityCheck(gameManager.herosInfos[int.Parse(this.name)].item_2.ablityType, gameManager.herosInfos[int.Parse(this.name)].item_2);
            }
            if (bAblity == true)
                return;
            SoundManager.Instance.PlayFX(fXType);
            //여기 첫 공경만 되게 해야함 즉사는
            if(bFirstAttack == false)
            {
                if (gameManager.herosInfos[int.Parse(this.name)].killPercent > 0)
                {
                    int rand = Random.Range(0, 100);
                    if (rand <= gameManager.herosInfos[int.Parse(this.name)].killPercent)
                    {
                        if(heroType == HeroType.normal)
                        {
                            gameManager.AttackKill();
                            //Debug.Log("즉사");
                        }
                        else if (heroType == HeroType.transDungeon)
                        {
                            gameManager.AttackKillTrans();                            
                        }
                        
                    }
                }
            }        
            //int test = 0;
            if (gameManager.SoulAblityList[(int)GameManager.AtrifactType.criticalChance] + m_ItemCriticalChance > 0)
            //if(test ==0)
            {
                int iCrticalChance = Random.Range(0, 100);
                if (iCrticalChance <= gameManager.SoulAblityList[(int)GameManager.AtrifactType.criticalChance] + m_ItemCriticalChance)
                {
                    InfiniCoin criticalPower = Power * 2;
                    if (gameManager.SoulAblityList[(int)GameManager.AtrifactType.criticalDps] > 0)
                    {
                        InfiniCoin ciriticalBuff = gameManager.SoulAblityList[(int)GameManager.AtrifactType.criticalDps] * 15;
                        ciriticalBuff = ciriticalBuff * 0.01;
                        criticalPower = criticalPower + (criticalPower * ciriticalBuff);
                    }
                    if(m_ItemCriticalAttackBuff >0)
                    {
                        criticalPower = criticalPower + (criticalPower * (m_ItemCriticalAttackBuff * 0.01f));
                    }
                    if (heroType == HeroType.normal)
                    {
                        gameManager.Attack(criticalPower, true, this.gameObject);
                        //Debug.Log("크리티컬 공격  " + criticalPower);
                    }
                    else if(heroType == HeroType.transDungeon)
                    {
                        gameManager.AttackTrans(criticalPower, true, this.gameObject);
                    }
                    
                }
                else
                {
                    //일반 공격
                    NormalAttack();
                }
            }
            else
            {
                //일반 공격
                NormalAttack();
            }
            bFirstAttack = true;
        }
        else
        {
            
        }
    }
    void NormalAttack()
    {
        InfiniCoin attackPower = Power;
        if(m_ItemAttackBuff>0)
        {
            attackPower = attackPower + (attackPower * (m_ItemAttackBuff * 0.01f));
        }
        if(bStartAttackBuff == true)
        {
            if (heroType == HeroType.normal)
            {
                gameManager.Attack(attackPower * 1.5f, false, this.gameObject);
            }
            else
            {
                gameManager.AttackTrans(attackPower, false, this.gameObject);
            }
                
        }
        else
        {
            if (heroType == HeroType.normal)
            {
                gameManager.Attack(attackPower, false, this.gameObject);
            }
            else if(heroType == HeroType.transDungeon)
            {
                gameManager.AttackTrans(attackPower, false, this.gameObject);
            }
                
        }
        
    }
    public void SetMove(bool flag)
    {

    }
    bool bMove = false;

    bool bMoveTop = false;
    public void MoveTop()
    {
        bMoveTop = true;
    }
    public void SetInle()
    {
        animator.Play("idle");
    }
    public void SetMove()
    {
        if(animator ==null)
        {
            animator = GetComponent<Animator>();
        }
        bMove = true;
        animator.Play("move");
    }
    public void EndMove()
    {
        
        SetInitPosition();
        
        
        bMove = false;
        //animator.Play("idle");        
        bMoveTop = false;
    }
    void OnEndMove()
    {
        //transform.position = new Vector3(xPos, yPos, 0);
        animator.Play("attack");
        coroutine = EndAttackRoutine();
        StartCoroutine(coroutine);
    }
    //이동테스트
    public void Lerp()
    {
        
        int rand = Random.Range(0, 2);
        float xPos = 0;
        float yPos = 0;
        if (rand == 0)
        {
            xPos = Random.Range(x1Min, x1Max);
            yPos = Random.Range(y1Min, y1Max);
        }
        else
        {
            xPos = Random.Range(x2Min, x2Max);
            yPos = Random.Range(y2Min, y2Max);
        }

        transform.DOMove(new Vector3(xPos, yPos, 0), lerpDuration).OnComplete(OnEndMove);
        
        
     
    }
    IEnumerator EndAttackRoutine()
    {
        yield return new WaitForSeconds(AttackDelayTime);        
        bAttack = true;
        //transform.position = oriPos;
    }
}

