using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpeditionGetSometingPanel : MonoBehaviour
{
    public List<Text> TextList;
    public List<GameObject> GetList;
    public int GetheroIndex = -1;
    public bool bBox = false;


    public int ExpeditionPos = -1;
    float getBuff(string ablity)
    {
        string[] splitStr = ablity.Split('_');
        float buff = 0;
        if (splitStr[0] == "expedition")
        {
            buff = float.Parse(splitStr[1]) * 0.01f;
        }

        return buff;
    }
    private void OnEnable()
    {
        bBox = false;
        if (ExpeditionPos == -1)
            return;
        if (GetheroIndex == -1)
            return;
        int count = GameManager.Instance.GetExpedtionItemCount(GetheroIndex);
        float m_buff = 0;
        if(GameManager.Instance.herosInfos[GetheroIndex].isAblity_1 ==true)
        {
            m_buff += getBuff(GameManager.Instance.herosInfos[GetheroIndex].specialAblity1);
        }
        if (GameManager.Instance.herosInfos[GetheroIndex].isAblity_2 == true)
        {
            m_buff += getBuff(GameManager.Instance.herosInfos[GetheroIndex].specialAblity2);
        }
        if (GameManager.Instance.herosInfos[GetheroIndex].isAblity_3 == true)
        {
            m_buff += getBuff(GameManager.Instance.herosInfos[GetheroIndex].specialAblity3);
        }


        for (int i =0; i < GetList.Count; i++)
        {
            GetList[i].SetActive(false);
        }
        
        switch (ExpeditionPos)
        {
            case 0:                
                GR_InfiniCoin.InfiniCoin Coins = GameManager.Instance.GetGoldTick();
                Coins = Coins * 1000;
                if (m_buff > 0)
                {
                    Coins = Coins + (Coins * m_buff);
                }
                else
                {

                }
                GetList[0].SetActive(true);
                GetList[0].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(Coins);
                GameManager.Instance.TotalGold += Coins;
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
                UiManager.Instance.SetGoldText();
                break;
            case 1:
                bBox = true;
                GetList[2].SetActive(true);
                GetList[2].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(1);
                break;
            case 2:
                GR_InfiniCoin.InfiniCoin gem_temp = new GR_InfiniCoin.InfiniCoin();
                if (m_buff > 0)
                {
                    GameManager.Instance.TotalGem += 10 + (10 * m_buff);
                    TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
                    gem_temp = 10 + (10 * m_buff);
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                }
                else
                {
                    GameManager.Instance.TotalGem += 10;
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                    gem_temp = 10;
                    TextList[0].text = 10.ToString();
                }
                UiManager.Instance.SetGemText();

                GetList[1].SetActive(true);
                GetList[1].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(gem_temp);
                break;
            case 3:
                GR_InfiniCoin.InfiniCoin altar_temp = new GR_InfiniCoin.InfiniCoin();
                if (m_buff > 0)
                {
                    int power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        int AltarCount = 10 + (10 * power);

                        GameManager.Instance.TotalAltarCoin += AltarCount + (AltarCount * m_buff);
                        TextList[0].text = Mathf.Floor(AltarCount + (AltarCount * m_buff)).ToString();
                        altar_temp = AltarCount + (AltarCount * m_buff);
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                    }
                    else
                    {
                        GameManager.Instance.TotalAltarCoin += 10 + (10 * m_buff);
                        TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
                        altar_temp = 10 + (10 * m_buff);
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                    }
                  
                }
                else
                {

                    int power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.altar200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        int AltarCount = 10 + (10 * power);
                        GameManager.Instance.TotalAltarCoin += AltarCount;
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                        altar_temp = AltarCount;
                        TextList[0].text = AltarCount.ToString();
                    }
                    else
                    {
                        GameManager.Instance.TotalAltarCoin += 10;
                        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                        altar_temp = 10;
                        TextList[0].text = 10.ToString();
                    }
                 
                }

                GetList[4].SetActive(true);
                GetList[4].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(altar_temp);
                break;
            case 4:
                GR_InfiniCoin.InfiniCoin mat_temp = new GR_InfiniCoin.InfiniCoin();
                if (m_buff > 0)
                {
                    int power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.mat100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.mat200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        int matCount = 10 + (10 * power);

                        GameManager.Instance.materialCount += matCount + Mathf.RoundToInt((matCount * m_buff));
                        TextList[0].text = Mathf.Floor(matCount + (matCount * m_buff)).ToString();
                        mat_temp = matCount + Mathf.RoundToInt((matCount * m_buff));
                        GameManager.Instance.Save(GameManager.saveType.materialCount);
                    }
                    else
                    {
                        GameManager.Instance.materialCount += 10 + Mathf.RoundToInt((10 * m_buff));
                        TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
                        mat_temp = 10 + Mathf.RoundToInt((10 * m_buff));
                        GameManager.Instance.Save(GameManager.saveType.materialCount);
                    }
                 
                }
                else
                {
                    int power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.mat100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.mat200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        int matCount = 10 + (10 * power);                        
                        GameManager.Instance.materialCount += matCount;
                        GameManager.Instance.Save(GameManager.saveType.materialCount);
                        TextList[0].text = matCount.ToString();
                        mat_temp = matCount;
                    }
                    else
                    {
                        GameManager.Instance.materialCount += 10;
                        GameManager.Instance.Save(GameManager.saveType.materialCount);
                        TextList[0].text = 10.ToString();
                        mat_temp = 10;
                    }
                    
                }
                GetList[5].SetActive(true);
                GetList[5].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(mat_temp);
                break;
            case 5:
                GR_InfiniCoin.InfiniCoin Soul_temp = new GR_InfiniCoin.InfiniCoin();
                if (m_buff > 0)
                {
                    float power=0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul50] == 1)
                    {
                        power += 0.5f;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        float soulCount = 10 + (10 * power);

                        GameManager.Instance.TotalSoul += soulCount + (soulCount * m_buff);
                        TextList[0].text = Mathf.Floor(soulCount + (soulCount * m_buff)).ToString();
                        Soul_temp = soulCount + (soulCount * m_buff);
                        GameManager.Instance.Save(GameManager.saveType.TotalSoul);
                    }
                    else
                    {
                        GameManager.Instance.TotalSoul += 10 + (10 * m_buff);
                        TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
                        Soul_temp = 10 + (10 * m_buff);
                        GameManager.Instance.Save(GameManager.saveType.TotalSoul);

                    }
                    
                }
                else
                {
                    float power = 0;
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul50] == 1)
                    {
                        power += 0.5f;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul100] == 1)
                    {
                        power += 1;
                    }
                    if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul200] == 1)
                    {
                        power += 2;
                    }
                    if (power > 0)
                    {
                        float soulCount = 10 + (10 * power);
                        GameManager.Instance.TotalSoul += soulCount;
                        GameManager.Instance.Save(GameManager.saveType.TotalSoul);
                        TextList[0].text = soulCount.ToString();
                        Soul_temp = soulCount;
                    }
                    else
                    {
                        GameManager.Instance.TotalSoul += 10;
                        GameManager.Instance.Save(GameManager.saveType.TotalSoul);
                        TextList[0].text = 10.ToString();
                        Soul_temp = 10;
                    }
                   
                }
                UiManager.Instance.SetSoulText();

                GetList[3].SetActive(true);
                GetList[3].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(Soul_temp);
                break;
            case 6:
                GR_InfiniCoin.InfiniCoin gem_temp_2 = new GR_InfiniCoin.InfiniCoin();
                if (m_buff > 0)
                {
                    GameManager.Instance.TotalGem += 10 + (10 * m_buff);
                    TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
                    gem_temp_2 = 10 + (10 * m_buff);
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                }
                else
                {
                    GameManager.Instance.TotalGem += 10;
                    GameManager.Instance.Save(GameManager.saveType.TotalGem);
                    gem_temp_2 = 10;
                    TextList[0].text = 10.ToString();
                }
                UiManager.Instance.SetGemText();

                GetList[1].SetActive(true);
                GetList[1].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(gem_temp_2);
                break;
            case 7:                
                GR_InfiniCoin.InfiniCoin Coins_2 = GameManager.Instance.GetGoldTick();
                Coins_2 = Coins_2 * 2000;
                if (m_buff > 0)
                {
                    Coins_2 = Coins_2 + (Coins_2 * m_buff);
                }
                else
                {

                }
                GetList[0].SetActive(true);
                GetList[0].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(Coins_2);
                GameManager.Instance.TotalGold += Coins_2;
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
                UiManager.Instance.SetGoldText();
                break;
        }

        //for(int i =0; i< count; i++)
        //{
        //    GameManager.GetItemType getItemType =  GameManager.Instance.GetRandomItem();
        //    GetList[(int)getItemType].SetActive(true);
        //    switch (getItemType)
        //    {
        //        case GameManager.GetItemType.Gold:
        //            GR_InfiniCoin.InfiniCoin Coins = GameManager.Instance.GetGoldTick();
        //            Coins = Coins * 1000;
        //            if (m_buff >0)
        //            {
        //                Coins = Coins + (Coins * m_buff);
        //            }
        //            else
        //            {

        //            }
        //            GetList[(int)getItemType].transform.Find("infoText").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(Coins);
        //            GameManager.Instance.TotalGold += Coins;
        //            UiManager.Instance.SetGoldText();
        //            break;
        //        case GameManager.GetItemType.Gem:
        //            if (m_buff > 0)
        //            {
        //                GameManager.Instance.TotalGem = 10 + (10 * m_buff);
        //                TextList[0].text = Mathf.Floor(10 + (10 * m_buff)).ToString();
        //            }
        //            else
        //            {
        //                GameManager.Instance.TotalGem+=10;
        //                TextList[0].text = 10.ToString();
        //            }
                    
        //            UiManager.Instance.SetGemText();
        //            break;
        //        case GameManager.GetItemType.Box:
        //            bBox = true;
        //            break;
        //        case GameManager.GetItemType.Soul:
        //            if (m_buff > 0)
        //            {
        //                GameManager.Instance.TotalSoul = 5 + (5 * m_buff);
        //                TextList[1].text = Mathf.Floor(5 + (5 * m_buff)).ToString();
        //            }
        //            else
        //            {
        //                GameManager.Instance.TotalSoul+= 5;
        //                TextList[1].text = 5.ToString();
        //            }
                    
        //            break;
        //        case GameManager.GetItemType.Altar:
        //            if (m_buff > 0)
        //            {
        //                GameManager.Instance.TotalAltarCoin = 5 + (5 * m_buff);
        //                TextList[2].text = Mathf.Floor(5 + (5 * m_buff)).ToString();
        //            }
        //            else
        //            {
        //                GameManager.Instance.TotalAltarCoin++;
        //                TextList[2].text = 1.ToString();
        //            }
                    
        //            break;
        //    }

        //}
        
    }
    private void OnDisable()
    {
        UiManager.Instance.SetGemText();
        UiManager.Instance.SetGoldText();
        UiManager.Instance.SetSoulText();
        if(bBox ==true)
        {
            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
        }
    }
    public void SetGetSometing()
    {

    }
}
