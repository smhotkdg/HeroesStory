using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GR_InfiniCoin;
public class SystemManager
{
    static double defalutEnmeyHp = 10;
    public static  List<Dictionary<string, object>> data;
    public static List<string> Names = new List<string>();
    public static List<string> Tiers = new List<string>();
    public static List<double> attackPower = new List<double>();
    public static List<int> InitCost = new List<int>();
    public static List<string> specialAblity_1 = new List<string>();
    public static List<string> specialAblity_2 = new List<string>();
    public static List<string> specialAblity_3 = new List<string>();
    public static List<int> fame_cost = new List<int>();
    public static List<float> Attack_time = new List<float>();
    public static float LevelUp_threshold = 1f;

    public static List<Dictionary<string, object>> soulAblitydata;
    public static List<string> soulAblityTitle = new List<string>();

    public static List<string> Itemname = new List<string>();
    public static List<int> ItemType = new List<int>();
    public static List<int> ItemAblityStr = new List<int>();
    public static List<int> ItemChance = new List<int>();
    public static List<float> ItemPlus = new List<float>();
    public static List<int> ItemTime = new List<int>();


    public static List<Dictionary<string, object>> CollectionData;
    public static List<string> Collectiontitle = new List<string>();
    public static List<string> CollectionName = new List<string>();

    public static List<int> Collectionpower = new List<int>();
    public static List<int> CollectionHero1 = new List<int>();
    public static List<int> CollectionHero2 = new List<int>();
    public static List<int> CollectionHero3 = new List<int>();
    public static List<int> CollectionHero4 = new List<int>();



    public static List<Dictionary<string, object>> dataItem;

    public static List<Dictionary<string, object>> NicknameData;
    public static string GetNickname(int pos)
    {        
        return NicknameData[pos]["name"].ToString();
    }
    public static void SetData()
    {
        data = CSVReader.Read("herodata");
        //NicknameData = CSVReader.Read("nicknamedata");
        for (int i =0;i< data.Count; i++)
        {
            Names.Add(data[i]["name"].ToString());
            Tiers.Add(data[i]["tier"].ToString());
            attackPower.Add(double.Parse(data[i]["attack"].ToString()));
            InitCost.Add(int.Parse(data[i]["initcost"].ToString()));
            specialAblity_1.Add(data[i]["special_ablity_1"].ToString());
            specialAblity_2.Add(data[i]["special_ablity_2"].ToString());
            specialAblity_3.Add(data[i]["special_ablity_3"].ToString());
            fame_cost.Add(int.Parse(data[i]["fame_cost"].ToString()));
            Attack_time.Add(float.Parse(data[i]["attack_time"].ToString()));
        }
        soulAblitydata = CSVReader.Read("soulAblity");
        for (int i = 0; i < soulAblitydata.Count; i++)
        {
            soulAblityTitle.Add(soulAblitydata[i]["title"].ToString());
      
        }

        dataItem = CSVReader.Read("item");
        for (int i = 0; i < dataItem.Count; i++)
        {
            Itemname.Add(dataItem[i]["name"].ToString());
            ItemType.Add(int.Parse(dataItem[i]["itemtype"].ToString()));
            ItemAblityStr.Add(int.Parse(dataItem[i]["ablity"].ToString()));
            ItemChance.Add(int.Parse(dataItem[i]["chance"].ToString()));
            ItemPlus.Add(float.Parse(dataItem[i]["plus"].ToString()));
            ItemTime.Add(int.Parse(dataItem[i]["time"].ToString()));
        }


        CollectionData = CSVReader.Read("collection");
        for (int i = 0; i < CollectionData.Count; i++)
        {
            Collectiontitle.Add(CollectionData[i]["title"].ToString());
            CollectionName.Add(CollectionData[i]["name"].ToString());
            Collectionpower.Add(int.Parse(CollectionData[i]["power"].ToString()));

            CollectionHero1.Add(int.Parse(CollectionData[i]["hero1"].ToString()));
            CollectionHero2.Add(int.Parse(CollectionData[i]["hero2"].ToString()));
            CollectionHero3.Add(int.Parse(CollectionData[i]["hero3"].ToString()));
            CollectionHero4.Add(int.Parse(CollectionData[i]["hero4"].ToString()));
        }
    }
    public static double getAttackPower(int pos)
    {
        double power = GameManager.Instance.herosInfos[pos].AwakeningCount * LevelUp_threshold;
        //double attackbuff = GameManager.Instance.herosInfos[pos].AttackBuff;
        {
            return (attackPower[pos] + (attackPower[pos] * power));
        }        
    }
    public static double GetInitAttackPower(int pos)
    {
        return attackPower[pos];
    }
    public static InfiniCoin mathPow(InfiniCoin value, double n)
    {
        InfiniCoin val = 1;
        int count=0;
        
        for (double i = 1; i <= n; i++)
        {
            val = value * val;
            count++;
        }
        return val;
    }

    public static InfiniCoin GetEnmeyHp(int level,bool bRaid =false)
    {
        InfiniCoin hp = defalutEnmeyHp;
        
        if (level == 0)
            return hp;
        float levelupThreshold = 1.52f;
        int defaultLevel = 90;
        if(level < defaultLevel)
        {
            //levelupThreshold = 1.55f;
            if(level <= 28)
            {
                hp = 497 * Mathf.Exp(0.2791f * (float)level);
                if (hp < 0)
                    hp = 50;
            }
            else
            {
                levelupThreshold = 1.52f;
                hp = mathPow(levelupThreshold, level) * defalutEnmeyHp;
            }
            

        }
        else if(level < 500)
        {
            hp = mathPow(levelupThreshold, defaultLevel) * defalutEnmeyHp;            
            levelupThreshold = 1.195f;            
            hp = mathPow(levelupThreshold, level - defaultLevel) * hp;
        }
        else
        {
            hp = mathPow(levelupThreshold, defaultLevel) * defalutEnmeyHp;
            levelupThreshold = 1.195f;
            
            hp = mathPow(levelupThreshold, 500 - defaultLevel) * hp;

            levelupThreshold = 1.05f;
            hp = mathPow(levelupThreshold, level - 500) * hp;

            //float uplevelupThreshold = 1 + ((level - 500) * 0.005f);
            //hp = (hp * uplevelupThreshold);

        }
        //if(level >= 200000)
        //{
        //    hp = mathPow(1.52f, defaultLevel) * defalutEnmeyHp;
        //    levelupThreshold = 1.195f;
        //    //levelupThreshold = 1.145f;
        //    hp = mathPow(levelupThreshold, 500 - defaultLevel) * hp;

        //    float uplevelupThreshold = 1 + ((200000 - 500) * 0.005f);
        //    hp = (hp * uplevelupThreshold);

        //    levelupThreshold = 1.545f;
        //    hp = mathPow(levelupThreshold, level - (200000-1)) * hp;
        //}
        //hp = mathPow(levelupThreshold, level) * hp;
        if (bRaid ==false)
        {
            if ((level + 1) % 5 == 0)
            {
                InfiniCoin value = GameManager.Instance.GetArtifactValue(GameManager.AtrifactType.BossHp);
                hp = hp * 7;

                hp = hp - (hp * value);
            }
        }
       
      
        return hp;
    }


    public static string ChangeFormat(double target)
    {
        string haveGold = target.ToString("0");
        if (double.IsInfinity(target) == true)
        {
            return "infinity";
        }

        string[] unit = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L","M","N","O","P","Q","R","S","T","U",
        "V","W","X","Y","Z","Aa","Ab","Ac","Ad","Ae","Af","Ag","Ah","Ai","Aj","Ak","Al","Am","An","Ao","Ap","Aq","Ar","As","At","Au","Av","Aw","Ax","Ay","Az",
        "Ba","Bb","Bc","Bd","Be","Bf","Bg","Bh","Bi","Bj","Bk","Bl","Bm","Bn","Bo","Bp","Bq","Br","Bs","Bt","Bu","Bv","Bw","Bx","By","Bz",
        "Ca","Cb","Cc","Cd","Ce","Cf","Cg","Ch","Ci","Cj","Ck","Cl","Cm","Cn","Co","Cp","Cq","Cr","Cs","Ct","Cu","Cv","Cw","Cx","Cy","Cz",
        "Da","Db","Dc","Dd","De","Df","Dg","Dh","Di","Dj","Dk","Dl","Dm","Dn","Do","Dp","Dq","Dr","Ds","Dt","Du","Dv","Dw","Dx","Dy","Dz",
        "Ea","Eb","Ec","Ed","Ee","Ef","Eg","Eh","Ei","Ej","Ek","El","Em","En","Eo","Ep","Eq","Er","Es","Et","Eu","Ev","Ew","Ex","Ey","Ez"};


        int[] cVal = new int[unit.Length];
        int index = 0;
        while (true)
        {
            string last4 = "";
            if (haveGold.Length >= 4)
            {
                last4 = haveGold.Substring(haveGold.Length - 4);
                int intLast4 = int.Parse(last4);

                cVal[index] = intLast4 % 1000;

                haveGold = haveGold.Remove(haveGold.Length - 3);
            }
            else
            {
                cVal[index] = int.Parse(haveGold);
                break;
            }

            index++;
        }

        if (index > 0)
        {
            int r = cVal[index] * 1000 + cVal[index - 1];
            string temp = (r / 1000f).ToString("N2");

            //return string.Format("{0:#.###} {1}", (float)r / 1000f, unit[index]);                        
            return string.Format("{0} {1}", temp, unit[index]);
        }

        return haveGold;
    }

    public static InfiniCoin GetGold(int level, bool isShop = false)
    {
        InfiniCoin gold = 1;
        if (level == 0)
        {
            if (GameManager.Instance.buffData.GoldBuff > 0)
            {
                gold = gold + (gold * GameManager.Instance.buffData.GoldBuff);                
            }
            else
            {
                return 1;
            }            
        }
  
        int minLevel = 60;
        int maxLevel = 500;
        if (level < minLevel)
        {
            gold = (mathPow(1.25, level) + 10) * 2;          
        }
        else if (level < maxLevel)        
        {
            //gold = GetEnmeyHp(level);
            //gold = gold / 2000;
            gold = (mathPow(1.25, minLevel) + 10) * 2;

            float levelupThreshold = 1.15f;
            gold = mathPow(levelupThreshold, level - minLevel) * gold;
        }
        else
        {
            gold = (mathPow(1.4, minLevel) + 10) * 2;
            float levelupThreshold = 1.15f;
            gold = mathPow(levelupThreshold, maxLevel) * gold;

            levelupThreshold = 1.05f;
            gold = mathPow(levelupThreshold, level - maxLevel) * gold;
        }


        if ((level + 1) % 5 == 0 && level >=0)
        {
            gold = gold * 10;
        }        
        if (GameManager.Instance.bItemGetGold == true)
        {
            gold = gold * 1.5f;
        }
        if(GameManager.Instance.timerCotroller.bStart_attackBuff == true && isShop ==false)
        {
            gold = gold * 2;
        }
        if (GameManager.Instance.buffData.GoldBuff > 0)
        {
            gold = gold + (gold * GameManager.Instance.buffData.GoldBuff);
        }
        if (level <0)
        {
            gold = 1;
        }
        InfiniCoin CollectionPower = 1;
        if(GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold50] ==1)
        {
            CollectionPower += 0.5;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold100] == 1)
        {
            CollectionPower += 1;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold200] == 1)
        {
            CollectionPower += 2;
        }
        gold = gold * CollectionPower;
        return gold;
    }
    public static InfiniCoin GetGold_Normal(int level, bool isShop = false)
    {
        InfiniCoin gold = 1;
        if (level == 0)
        {
            if (GameManager.Instance.buffData.GoldBuff > 0)
            {
                gold = gold + (gold * GameManager.Instance.buffData.GoldBuff);
            }
            else
            {
                return 1;
            }
        }
        
        int minLevel = 60;
        int maxLevel = 500;
        if (level < minLevel)
        {
            gold = (mathPow(1.25, level) + 10) * 2;
        }
        else if (level < maxLevel)
        {
            //gold = GetEnmeyHp(level);
            //gold = gold / 2000;
            gold = (mathPow(1.25, minLevel) + 10) * 2;

            float levelupThreshold = 1.15f;
            gold = mathPow(levelupThreshold, level - minLevel) * gold;
        }
        else
        {
            gold = (mathPow(1.4, minLevel) + 10) * 2;
            float levelupThreshold = 1.15f;
            gold = mathPow(levelupThreshold, maxLevel) * gold;

            levelupThreshold = 1.05f;
            gold = mathPow(levelupThreshold, level - maxLevel) * gold;
        }

        if (GameManager.Instance.bItemGetGold == true)
        {
            gold = gold * 1.5f;
        }
        if (GameManager.Instance.timerCotroller.bStart_attackBuff == true && isShop == false)
        {
            gold = gold * 2;
        }
        if (GameManager.Instance.buffData.GoldBuff > 0)
        {
            gold = gold + (gold * GameManager.Instance.buffData.GoldBuff);
        }
        if (level < 0)
        {
            gold = 1;
        }
        InfiniCoin CollectionPower = 1;
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold50] == 1)
        {
            CollectionPower += 0.5;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold100] == 1)
        {
            CollectionPower += 1;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.gold200] == 1)
        {
            CollectionPower += 2;
        }
        gold = gold * CollectionPower;
        return gold;
    }
}
