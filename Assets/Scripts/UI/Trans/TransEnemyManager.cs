using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR_InfiniCoin;

public class TransEnemyManager : MonoBehaviour
{
    public TransDungeonGameSrc transDungeonGameSrc;
    public List<GameObject> EnmeyList;
    public GameObject Enemy;
    public GameObject ExpolsionEffect;
    List<GameObject> ExplosionList = new List<GameObject>();

    IEnumerator AttackAnimRoutine()
    {
        while (true)
        {
            if (Enemy.activeSelf == true)
            {
                int rand = Random.Range(0, 10);
                if (rand < 5)
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

    public void SetEnemy(bool Move = false)
    {
        Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        SetEnmeySprite();
        Enemy.SetActive(true);
    }
    public void SetEnmeySprite()
    {
        int pos = (Level - 1) / 5;
        if(pos >=9)
        {
            pos = 9;
        }
        Enemy = EnmeyList[pos];      
    }

    int iExIndex;
    int Level;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject temp = Instantiate(ExpolsionEffect);
            temp.transform.SetParent(ExpolsionEffect.transform.parent);
            temp.transform.position = ExpolsionEffect.transform.position;
            temp.transform.localScale = ExpolsionEffect.transform.localScale;
            ExplosionList.Add(temp);
        }
        iExIndex = 0;
        StartCoroutine(AttackAnimRoutine());
    }
    public void EndDungeon()
    {
        Enemy.SetActive(false);
    }
    public void StartDungeon()
    {
        Level = 1;
        SetHp();
        GameManager.Instance.bStartEnemy_trans = true;
        StartCoroutine(SetEnmeyRoutine());
    }
    public InfiniCoin Hp;
    void SetHp()
    {
        Hp = SystemManager.GetEnmeyHp(Level * 10);
        float discountPower=0;
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.trasn25] ==1)
        {
            discountPower += 0.25f;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.trasn25_2] == 1)
        {
            discountPower += 0.25f;
        }
        if(discountPower >0)
        {
            Hp = Hp - (Hp * discountPower);
        }
        transDungeonGameSrc.CheckHP(Hp, Level * 10);
        transDungeonGameSrc.CheckHpFill(Hp, Level * 10);        
    }

    public void AttackKill()
    {
        UiManager.Instance.SetTextPool_trans(Enemy.gameObject, Hp, UiTargetManager.TextType.attack, false, null);
        Hp = 0;     
        Death();        
    }

    public void PercentAttack(float percent)
    {
        //Hp -= (MaxHp * percent);
        Hp -= (Hp * percent);
        if (Hp < 1)
        {
            Hp = 0;            
            Death();   

        }
        transDungeonGameSrc.CheckHP(Hp, Level * 10);
        transDungeonGameSrc.CheckHpFill(Hp, Level * 10);
    }
    public void Attack(InfiniCoin attackPower, bool bCritial, GameObject target)
    {
        if (bCritial == false)
        {
            UiManager.Instance.SetTextPool_trans(Enemy.gameObject, attackPower, UiTargetManager.TextType.attack, false, target);
        }
        else
        {
            UiManager.Instance.SetTextCritial_trans(Enemy.gameObject, attackPower, false, target);
        }

        Hp -= attackPower;
        if (bColorAttack == false)
        {
            bColorAttack = true;
            StartCoroutine(SetAttackRoutine());
        }


        if (Hp < 1)
        {
            Hp = 0;           
            Death();               

        }
        transDungeonGameSrc.CheckHP(Hp, Level * 10);
        transDungeonGameSrc.CheckHpFill(Hp, Level * 10);
    }
    bool bColorAttack = false;
    IEnumerator SetAttackRoutine()
    {
        Enemy.GetComponent<SpriteRenderer>().color = UiManager.Instance.AttackColor;
        yield return new WaitForSeconds(0.1f);
        Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        bColorAttack = false;
    }
    public void Death()
    {
        GameManager.Instance.bStartEnemy_trans = false;
        Level++;
        //GameManager.Instance.bStartEnemy = false;
        if (Enemy == null)
            return;
        Enemy.SetActive(false);
        iExIndex++;
        if (iExIndex >= ExplosionList.Count)
            iExIndex = 0;
        
        ExplosionList[iExIndex].SetActive(true);
        //UiManager.Instance.HpText.gameObject.SetActive(false);
        transDungeonGameSrc.HpObject.SetActive(false);

        StartCoroutine(SetEnmeyRoutine());
        transDungeonGameSrc.KillMonster();
    }
    IEnumerator SetEnmeyRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        if (Enemy != null)
        {
            //if (GameManager.Instance.bMoveNew == false)
            {
                SetEnemy(true);
                Enemy.SetActive(true);

                SetHp();
                //UiManager.Instance.HpText.gameObject.SetActive(true);
                transDungeonGameSrc.HpObject.SetActive(true);
                GameManager.Instance.bStartEnemy_trans = true;
            }
            //else
            //{
                //Enemy.SetActive(false);
            //}

        }
    }
}
