using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemEffectController : MonoBehaviour
{

    public List<Sprite> ItemSpriteList;
    public GameObject TempItem;
    public GameObject TempMagicAttack;
    public GameObject TempBossHP;
    public GameObject TempBossTime;

    public List<GameObject> ItemList = new List<GameObject>();

    public List<GameObject> MagicAttackList = new List<GameObject>();
    public List<GameObject> BossHPList = new List<GameObject>();
    public List<GameObject> BossTimeList = new List<GameObject>();

    int iItemCount;
    int MagicAttackCount;
    int BossHpCount;
    int BossTimeCount;
    void Start()
    {
        iItemCount = 0;

        if(ItemList.Count ==0)
        {
            for (int i = 0; i < 20;i++)
            {
                GameObject temp = Instantiate(TempItem);
                temp.transform.SetParent(TempItem.transform.parent);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                ItemList.Add(temp);


                GameObject temp_2 = Instantiate(TempMagicAttack);
                temp_2.transform.SetParent(TempMagicAttack.transform.parent);
                temp_2.transform.localScale = new Vector3(1, 1, 1);
                temp_2.transform.localPosition = new Vector3(0, 0, 0);
                MagicAttackList.Add(temp_2);

                GameObject temp_3 = Instantiate(TempBossHP);
                temp_3.transform.SetParent(TempBossHP.transform.parent);
                temp_3.transform.localScale = new Vector3(1, 1, 1);
                temp_3.transform.localPosition = new Vector3(0, 0, 0);
                BossHPList.Add(temp_3);

                GameObject temp_4 = Instantiate(TempBossTime);
                temp_4.transform.SetParent(TempBossTime.transform.parent);
                temp_4.transform.localScale = new Vector3(1, 1, 1);
                temp_4.transform.localPosition = new Vector3(0, 0, 0);
                BossTimeList.Add(temp_4);
            }
        }
    }
    public enum EffectType
    {
        magicAttack,
        BossTime,
        Gold,
        MonterHp
    }
    
    public void SetEffect(EffectType effectType,string title,float disableTime,GameObject target)
    {
        if(iItemCount >= ItemList.Count)
        {
            iItemCount = 0;
        }

        if (MagicAttackCount >= MagicAttackList.Count)
        {
            MagicAttackCount = 0;
        }

        if (BossHpCount >= BossHPList.Count)
        {
            BossHpCount = 0;
        }

        if (BossTimeCount >= BossTimeList.Count)
        {
            BossTimeCount = 0;
        }

        switch(effectType)
        {
            case EffectType.BossTime:
                BossTimeList[BossTimeCount].SetActive(true);
                BossTimeCount++;
                break;
            case EffectType.magicAttack:
                MagicAttackList[MagicAttackCount].SetActive(true);
                MagicAttackCount++;
                break;
            case EffectType.MonterHp:
                BossHPList[BossHpCount].SetActive(true);
                BossHpCount++;
                break;
        }
        ItemList[iItemCount].SetActive(true);
        
        ItemList[iItemCount].GetComponent<ItemEffectSrc>().SetData(ItemSpriteList[(int)effectType], title, disableTime);
        ItemList[iItemCount].GetComponent<UiTargetManager>().WorldObject = target;

        iItemCount++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
