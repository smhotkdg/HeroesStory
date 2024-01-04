using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR_InfiniCoin;
public class EnemyManager : MonoBehaviour
{
    public List<GameObject> AttackPointsList;
    public GameObject Mimic;
    public InfiniCoin Hp;
    public InfiniCoin MaxHp;
    public GameObject ExpolsionEffect;
    public GameObject Enemy;
    public List<GameObject> EnemyList;
    public List<GameObject> BossList;
    List<GameObject> ExplosionList = new List<GameObject>();

    public List<GameObject> Enmey_Lv1;
    public List<GameObject> Enmey_Lv2;
    public List<GameObject> Enmey_Lv3;
    public List<GameObject> Enmey_Lv4;
    public List<GameObject> Enmey_Lv5;
    bool bGoldMonster = false;
    public bool bBoss = false;
    IEnumerator AttackAnimRoutine()
    {
        while(true)
        {
            if (Enemy.activeSelf == true)
            {
                int rand = Random.Range(0, 10);
                if(rand < 5)
                {
                    Enemy.GetComponent<Animator>().Play("attack");
                }
                else
                {
                    Enemy.GetComponent<Animator>().Play("idle");
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
    public void SetAttackPointsList(bool flag)
    {
        //for(int i =0; i<AttackPointsList.Count; i++)
        //{
        //    AttackPointsList[i].SetActive(flag);
        //}
    }
    public void SetEnmeySprite(bool checkBoss)
    {
        int level = GameManager.Instance.Level;
        if (level < 1000)
        {
            if(checkBoss ==false)
            {
                if (level < 20)
                {
                    Enemy = Enmey_Lv1[0];
                }
                else if (level < 40)
                {
                    Enemy = Enmey_Lv1[1];
                }
                else if (level < 60)
                {
                    Enemy = Enmey_Lv1[2];
                }
                else if (level < 80)
                {
                    Enemy = Enmey_Lv1[3];
                }
                else if (level < 100)
                {
                    Enemy = Enmey_Lv1[4];
                }
                else if (level < 120)
                {
                    Enemy = Enmey_Lv1[5];
                }
                else if (level < 140)
                {
                    Enemy = Enmey_Lv1[6];
                }
                else if (level < 160)
                {
                    Enemy = Enmey_Lv1[7];
                }
                else if (level < 180)
                {
                    Enemy = Enmey_Lv1[8];
                }
                else if (level < 200)
                {
                    Enemy = Enmey_Lv1[9];
                }


                else if(level < 220)
                {
                    Enemy = Enmey_Lv2[0];
                }
                else if (level < 240)
                {
                    Enemy = Enmey_Lv2[1];
                }
                else if (level < 260)
                {
                    Enemy = Enmey_Lv2[2];
                }
                else if (level < 280)
                {
                    Enemy = Enmey_Lv2[3];
                }
                else if (level < 300)
                {
                    Enemy = Enmey_Lv2[4];
                }
                else if (level < 320)
                {
                    Enemy = Enmey_Lv2[5];
                }
                else if (level < 340)
                {
                    Enemy = Enmey_Lv2[6];
                }
                else if (level < 360)
                {
                    Enemy = Enmey_Lv2[7];
                }
                else if (level < 380)
                {
                    Enemy = Enmey_Lv2[8];
                }
                else if (level < 400)
                {
                    Enemy = Enmey_Lv2[9];
                }


                else if (level < 420)
                {
                    Enemy = Enmey_Lv3[0];
                }
                else if (level < 440)
                {
                    Enemy = Enmey_Lv3[1];
                }
                else if (level < 460)
                {
                    Enemy = Enmey_Lv3[2];
                }
                else if (level < 480)
                {
                    Enemy = Enmey_Lv3[3];
                }
                else if (level < 500)
                {
                    Enemy = Enmey_Lv3[4];
                }
                else if (level < 520)
                {
                    Enemy = Enmey_Lv3[5];
                }
                else if (level < 540)
                {
                    Enemy = Enmey_Lv3[6];
                }
                else if (level < 560)
                {
                    Enemy = Enmey_Lv3[7];
                }
                else if (level < 580)
                {
                    Enemy = Enmey_Lv3[8];
                }
                else if (level < 600)
                {
                    Enemy = Enmey_Lv3[9];
                }




                else if (level < 620)
                {
                    Enemy = Enmey_Lv4[0];
                }
                else if (level < 640)
                {
                    Enemy = Enmey_Lv4[1];
                }
                else if (level < 660)
                {
                    Enemy = Enmey_Lv4[2];
                }
                else if (level < 680)
                {
                    Enemy = Enmey_Lv4[3];
                }
                else if (level < 700)
                {
                    Enemy = Enmey_Lv4[4];
                }
                else if (level < 720)
                {
                    Enemy = Enmey_Lv4[5];
                }
                else if (level < 740)
                {
                    Enemy = Enmey_Lv4[6];
                }
                else if (level < 760)
                {
                    Enemy = Enmey_Lv4[7];
                }
                else if (level < 780)
                {
                    Enemy = Enmey_Lv4[8];
                }
                else if (level < 800)
                {
                    Enemy = Enmey_Lv4[9];
                }



                else if(level < 820)
                {
                    Enemy = Enmey_Lv5[0];
                }
                else if (level < 840)
                {
                    Enemy = Enmey_Lv5[1];
                }
                else if (level < 860)
                {
                    Enemy = Enmey_Lv5[2];
                }
                else if (level < 880)
                {
                    Enemy = Enmey_Lv5[3];
                }
                else if (level < 900)
                {
                    Enemy = Enmey_Lv5[4];
                }
                else if (level < 920)
                {
                    Enemy = Enmey_Lv5[5];
                }
                else if (level < 940)
                {
                    Enemy = Enmey_Lv5[6];
                }
                else if (level < 960)
                {
                    Enemy = Enmey_Lv5[7];
                }
                else if (level < 980)
                {
                    Enemy = Enmey_Lv5[8];
                }
                else if (level < 1000)
                {
                    Enemy = Enmey_Lv5[9];
                }
            }
            else
            {
                if (level < 20)
                {
                    Enemy = Enmey_Lv1[4];
                }
                else if (level < 40)
                {
                    Enemy = Enmey_Lv1[5];
                }
                else if (level < 60)
                {
                    Enemy = Enmey_Lv1[6];
                }
                else if (level < 80)
                {
                    Enemy = Enmey_Lv1[7];
                }
                else if (level < 100)
                {
                    Enemy = Enmey_Lv1[8];
                }
                else if (level < 120)
                {
                    Enemy = Enmey_Lv1[9];
                }
                else if (level < 140)
                {
                    Enemy = Enmey_Lv1[10];
                }
                else if (level < 160)
                {
                    Enemy = Enmey_Lv1[10];
                }
                else if (level <180)
                {
                    Enemy = Enmey_Lv1[10];
                }
                else if (level < 200)
                {
                    Enemy = Enmey_Lv1[10];
                }


                else if(level < 220)
                {
                    Enemy = Enmey_Lv2[2];
                }
                else if (level < 240)
                {
                    Enemy = Enmey_Lv2[3];
                }
                else if (level <260)
                {
                    Enemy = Enmey_Lv2[4];
                }
                else if (level < 280)
                {
                    Enemy = Enmey_Lv2[5];
                }
                else if (level < 300)
                {
                    Enemy = Enmey_Lv2[6];
                }
                else if (level < 320)
                {
                    Enemy = Enmey_Lv2[7];
                }
                else if (level < 340)
                {
                    Enemy = Enmey_Lv2[10];
                }
                else if (level < 360)
                {
                    Enemy = Enmey_Lv2[10];
                }
                else if (level < 380)
                {
                    Enemy = Enmey_Lv2[10];
                }
                else if (level < 400)
                {
                    Enemy = Enmey_Lv2[10];
                }


                else if (level < 420)
                {
                    Enemy = Enmey_Lv3[4];
                }
                else if (level < 440)
                {
                    Enemy = Enmey_Lv3[5];
                }
                else if (level < 460)
                {
                    Enemy = Enmey_Lv3[6];
                }
                else if (level < 480)
                {
                    Enemy = Enmey_Lv3[7];
                }
                else if (level < 500)
                {
                    Enemy = Enmey_Lv3[8];
                }
                else if (level < 520)
                {
                    Enemy = Enmey_Lv3[9];
                }
                else if (level < 540)
                {
                    Enemy = Enmey_Lv3[10];
                }
                else if (level < 560)
                {
                    Enemy = Enmey_Lv3[10];
                }
                else if (level < 580)
                {
                    Enemy = Enmey_Lv3[10];
                }
                else if (level < 600)
                {
                    Enemy = Enmey_Lv3[10];
                }




                else if(level < 620)
                {
                    Enemy = Enmey_Lv4[4];
                }
                else if (level < 640)
                {
                    Enemy = Enmey_Lv4[5];
                }
                else if (level < 660)
                {
                    Enemy = Enmey_Lv4[6];
                }
                else if (level < 680)
                {
                    Enemy = Enmey_Lv4[7];
                }
                else if (level < 700)
                {
                    Enemy = Enmey_Lv4[8];
                }
                else if (level < 720)
                {
                    Enemy = Enmey_Lv4[9];
                }
                else if (level < 740)
                {
                    Enemy = Enmey_Lv4[10];
                }
                else if (level < 760)
                {
                    Enemy = Enmey_Lv4[10];
                }
                else if (level < 780)
                {
                    Enemy = Enmey_Lv4[10];
                }
                else if (level < 800)
                {
                    Enemy = Enmey_Lv4[10];
                }



                else if(level < 820)
                {
                    Enemy = Enmey_Lv5[4];
                }
                else if (level < 840)
                {
                    Enemy = Enmey_Lv5[5];
                }
                else if (level < 860)
                {
                    Enemy = Enmey_Lv5[6];
                }
                else if (level < 880)
                {
                    Enemy = Enmey_Lv5[7];
                }
                else if (level <900)
                {
                    Enemy = Enmey_Lv5[8];
                }
                else if (level < 920)
                {
                    Enemy = Enmey_Lv5[9];
                }
                else if (level < 940)
                {
                    Enemy = Enmey_Lv5[10];
                }
                else if (level < 960)
                {
                    Enemy = Enmey_Lv5[10];
                }
                else if (level < 980)
                {
                    Enemy = Enmey_Lv5[10];
                }
                else if (level < 1000)
                {
                    Enemy = Enmey_Lv5[10];
                }
            }
        }
        else
        {
            if (checkBoss == false)
            {
                int randPos = Random.Range(0, 10);
                Enemy = Enmey_Lv5[randPos];
            }
            else
            {
                Enemy = Enmey_Lv5[10];
            }
                
        }

    }
    public void SetEnemy(bool Move =false)
    {
        Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        bBoss = false;
        Enemy.SetActive(false);
        if (Move ==true)
        {
            Enemy.SetActive(false);            
        }
            

        //여기서 미믹 체크
        if (GetSpeacialMonster())
        {
            Enemy = Mimic;
            bGoldMonster = true;
        }
        else
        {
            //int splitValue = 5;
            bGoldMonster = false;
            if ((GameManager.Instance.Level + 1) % 5 == 0)
            {
                //if ((GameManager.Instance.Level / 5) >= BossList.Count)
                //{
                //    int rand = Random.Range(0, BossList.Count);
                //    Enemy = BossList[rand];
                //}
                //else
                //{
                //    Enemy = BossList[((GameManager.Instance.Level + 1) / 5) - 1];
                //}
                SetEnmeySprite(true);
                bBoss = true;
            }
            else
            {
                //if ((GameManager.Instance.Level/ splitValue) >= EnemyList.Count)
                //{
                //    int rand = Random.Range(0, EnemyList.Count);
                //    Enemy = EnemyList[rand];
                //}
                //else
                //{
                //    Enemy = EnemyList[GameManager.Instance.Level/ splitValue];
                //}
                SetEnmeySprite(false);

            }
        }
       
        if (Move == true)
        {
            Enemy.SetActive(true);            
        }
            
        if (GameManager.Instance.bMoveNew == true)
        {
            Enemy.SetActive(false);
            UiManager.Instance.HpObject.SetActive(false);
        }
        if (Move == false)
        {
            Enemy.SetActive(true);
        }
        
    }
    private void Start()
    {
        for(int i =0; i< 20; i++)
        {
            GameObject temp = Instantiate(ExpolsionEffect);
            temp.transform.SetParent(ExpolsionEffect.transform.parent);
            temp.transform.position = ExpolsionEffect.transform.position;
            temp.transform.localScale = ExpolsionEffect.transform.localScale;
            ExplosionList.Add(temp);
        }
        iExIndex = 0;
        SetHp();
        SetEnmey();
        StartCoroutine(AttackAnimRoutine());
    }
    public void ChagneMakeMonster()
    {
        SetHp();
        SetEnemy(true);
        UiManager.Instance.HpObject.SetActive(true);
    }
    public void SetEnmey()
    {
        //ExpolsionEffect.SetActive(true);
        StartCoroutine(SetEnmeyRoutine());
        //SetMakeEnemy();
    }
    int iExIndex;    
    public bool GetSpeacialMonster()
    {
        InfiniCoin value= GameManager.Instance.GetArtifactValue(GameManager.AtrifactType.GoldMonster);
        float rand = Random.Range(0, 100);
        if(rand <= value)
        {
            Debug.Log("골드 몬스터");
            return true;
        }
        return false;
    }
    public void Death()
    {
        GameManager.Instance.bStartEnemy = false;
        if (Enemy == null)
            return;
        Enemy.SetActive(false);
        iExIndex++;
        if (iExIndex >= ExplosionList.Count)
            iExIndex = 0;
        if(bBoss ==true)
        {
            if(GameManager.Instance.Level== GameManager.Instance.MaxLevel)
            {
                int maxStageIndex = (GameManager.Instance.MapStageIndex * 200) - 1;
                if (GameManager.Instance.Level >= maxStageIndex)
                {
                    
                }
                else
                {
                    UiManager.Instance.BossStageStatusPanel.SetActive(true);
                    UiManager.Instance.BossStageStatusPanel.GetComponent<BossStageStatusPanelSrc>().SetData(true);
                }           
            }
        }
      
        ExplosionList[iExIndex].SetActive(true);
        GameManager.Instance.CheckKillMonster();
        UiManager.Instance.SetMonsterCount();
        //UiManager.Instance.HpText.gameObject.SetActive(false);
        UiManager.Instance.HpObject.SetActive(false);
        if(GameManager.Instance.Level != GameManager.Instance.MaxLevel)
        {
            if (GameManager.Instance.bMoveNew == false)
            {
                StartCoroutine(SetEnmeyRoutine());
                //SetMakeEnemy();
            }
            else if (GameManager.Instance.IsAutoPlay == false)
            {
                StartCoroutine(SetEnmeyRoutine());
                //SetMakeEnemy();
            }
        }    

    }
    bool bDeath = false;
    public void AttackKill()
    {
        UiManager.Instance.SetTextPool(Enemy.gameObject, Hp, UiTargetManager.TextType.attack,false,null);
        Hp = 0;
        if (bDeath == false)
        {            
            Death();
            bDeath = true;
            CheckItem(GameManager.Instance.bStartBoss);
            if (GameManager.Instance.bStartBoss == true)
            {                
                UiManager.Instance.BossKill();                
            }
            if (GameManager.Instance.MaxLevel > GameManager.Instance.Level)
            {
                GameManager.Instance.AutoNext();
            }

            if (bGoldMonster == false)
            {                
                CheckGold10(1);                
            }
            else
            {                
                CheckGold10(25);
            }
            
        }
    }
    public void CheckItem(bool checkScroll = false)
    {
        GameManager.Instance.GetItemChange(checkScroll);
    }
    void CheckGold10(int count)
    {
        InfiniCoin value = GameManager.Instance.GetArtifactValue(GameManager.AtrifactType.GetGoldx10);
        float rand = Random.Range(0, 100);
        int iCountX = count;
        if (rand <= value)
        {
            iCountX = count * 10;
            //Debug.Log("골드 10배");            
        }
        if(iCountX > 100)
        {
            iCountX = 40;
        }
        GameManager.Instance.SetCoin(iCountX);

        

    }
    public void PercentAttack(float percent)
    {
        //Hp -= (MaxHp * percent);
        Hp -= (Hp * percent);
        if (Hp < 1)
        {
            Hp = 0;
            if (bDeath == false)
            {
                Death();
                bDeath = true;
                CheckItem(GameManager.Instance.bStartBoss);
                if (GameManager.Instance.bStartBoss == true)
                {
                    UiManager.Instance.BossKill();
                }
                if (GameManager.Instance.MaxLevel > GameManager.Instance.Level)
                {
                    GameManager.Instance.AutoNext();
                }
                if (bGoldMonster == false)
                {
                    CheckGold10(1);
                }
                else
                {
                    CheckGold10(25);
                }

            }

        }
        UiManager.Instance.HpText.text = UiManager.Instance.SetCost(Hp);
        UiManager.Instance.CheckHpFill(Hp);
    }
    public void Attack(InfiniCoin attackPower,bool bCritial,GameObject target)
    {
        if(bCritial ==false)
        {
            UiManager.Instance.SetTextPool(Enemy.gameObject, attackPower, UiTargetManager.TextType.attack,false, target);
        }
        else
        {
            UiManager.Instance.SetTextCritial(Enemy.gameObject, attackPower,false, target);
        }
        
        Hp -= attackPower;
        if(bColorAttack ==false)
        {
            bColorAttack = true;
            StartCoroutine(SetAttackRoutine());
        }
        
        
        if (Hp <1)
        {
            Hp = 0;  
            if(bDeath==false)
            {
                Death();
                bDeath = true;
                CheckItem(GameManager.Instance.bStartBoss);
                if (GameManager.Instance.bStartBoss ==true)
                {
                    UiManager.Instance.BossKill();                    
                }
                if(GameManager.Instance.MaxLevel > GameManager.Instance.Level)
                {
                    GameManager.Instance.AutoNext();
                }
                if(bGoldMonster == false)
                {                    
                    CheckGold10(1);
                }
                else
                {                    
                    CheckGold10(25);
                }
                    
            }
            
        }
        UiManager.Instance.HpText.text = UiManager.Instance.SetCost(Hp);
        UiManager.Instance.CheckHpFill(Hp);
    }
    bool bColorAttack =false;
    IEnumerator SetAttackRoutine()
    {
        Enemy.GetComponent<SpriteRenderer>().color = UiManager.Instance.AttackColor;
        yield return new WaitForSeconds(0.1f);
        Enemy.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        bColorAttack = false;
    }
    void SetHp()
    {
        Hp = SystemManager.GetEnmeyHp(GameManager.Instance.Level);
        MaxHp = Hp;
        UiManager.Instance.HpText.text = UiManager.Instance.SetCost(Hp);
        UiManager.Instance.CheckHpFill(Hp);
        CheckBoss();

    }
    void CheckBoss()
    {
        if (GameManager.Instance.GetMaxMonsterCount() == 1)
        {
            SoundManager.Instance.ChangeBoss(true);
            GameManager.Instance.bStartBoss = true;
        }
        else
        {
            SoundManager.Instance.ChangeBoss(false);
            GameManager.Instance.bStartBoss = false;
        }

    }
    public void SetMakeEnemy()
    {
        if (Enemy != null)
        {
            if (GameManager.Instance.bMoveNew == false)
            {
                SetEnemy(true);
                Enemy.SetActive(true);

                SetHp();
                //UiManager.Instance.HpText.gameObject.SetActive(true);
                UiManager.Instance.HpObject.SetActive(true);
                CheckBoss();

                GameManager.Instance.bStartEnemy = true;
                bDeath = false;
            }
            else
            {
                Enemy.SetActive(false);
            }

        }
    }
    IEnumerator SetEnmeyRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        if (Enemy != null )
        {
            if(GameManager.Instance.bMoveNew ==false)
            {
                SetEnemy(true);
                Enemy.SetActive(true);

                SetHp();
                //UiManager.Instance.HpText.gameObject.SetActive(true);
                UiManager.Instance.HpObject.SetActive(true);
                CheckBoss();

                GameManager.Instance.bStartEnemy = true;
                bDeath = false;
            }
            else
            {
                Enemy.SetActive(false);
            }
       
        }
    }
    public void setDisableEnemy()
    {
        Enemy.SetActive(false);
        //UiManager.Instance.HpText.gameObject.SetActive(false);
        UiManager.Instance.HpObject.SetActive(false);
    }
    
    
}
