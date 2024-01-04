using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class InventorySlot : MonoBehaviour
{
    public GameObject RemoveButton;
    public Text NeedLevelText;
    public GameObject Lock;
    public GameObject Open_Buy;
    public GameObject Opne;

    public GameObject PlusIcon;
    public Image Iocn;
    public Text TitleText;

    InfiniCoin m_needLevel;
    InfiniCoin m_nowLevel;
    int m_heroPos;
    int m_equipIndex;
    int m_equipPos;
    void CheckEquip(int equipIndex)
    {
        m_equipIndex = equipIndex;

        if (equipIndex > -1)
        {
            //장비를 착용함.
            RemoveButton.SetActive(true);
            Lock.SetActive(false);
            Open_Buy.SetActive(false);
            Opne.SetActive(true);
            PlusIcon.SetActive(false);
            Iocn.gameObject.SetActive(true);


            switch (m_equipPos)
            {
                case 0:
                    Iocn.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.herosInfos[m_heroPos].equipItem_1];
                    TitleText.text = SystemManager.Itemname[GameManager.Instance.herosInfos[m_heroPos].equipItem_1];
                    break;
                case 1:
                    Iocn.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.herosInfos[m_heroPos].equipItem_2];
                    TitleText.text = SystemManager.Itemname[GameManager.Instance.herosInfos[m_heroPos].equipItem_2];
                    break;
            }
        }
        else if (equipIndex == -1)
        {
            //슬롯만 구매함
            Lock.SetActive(false);
            Open_Buy.SetActive(false);
            Opne.SetActive(true);
            PlusIcon.SetActive(true);
            RemoveButton.SetActive(false);
            Iocn.gameObject.SetActive(false);
            TitleText.text = "착용 장비 없음";
        }
        else if (m_nowLevel < m_needLevel)
        {
            //구매할 레벨이 안됨
            Lock.SetActive(true);
            Open_Buy.SetActive(false);
            Opne.SetActive(false);
            NeedLevelText.text = "Lv . " + UiManager.Instance.SetCost(m_needLevel);
        }
        else if (equipIndex == -2)
        {
            //아직 슬롯을 구매하지 않음
            Lock.SetActive(false);
            Open_Buy.SetActive(true);
            Opne.SetActive(false);
            RemoveButton.SetActive(false);
        }    
        
    }
    public void RemoveIcon()
    {
        if (GameManager.Instance.CheckInventoryFull() == true)
            return;

        if (m_equipIndex >-1)
        {
            switch (m_equipPos)
            {
                case 0:
                    if(GameManager.Instance.herosInfos[m_heroPos].equipItem_1 >-1)
                    {                   
                        GameManager.HerosInfo heros = new GameManager.HerosInfo();
                        heros = GameManager.Instance.herosInfos[m_heroPos];

                        GameManager.Instance.MakeItem_(heros.equipItem_1,heros.item_1);
                        heros.equipItem_1 = -1;

                        
                        GameManager.Item temp = new GameManager.Item();
                        temp.isGet = false;
                        heros.item_1 = temp;

                        GameManager.Instance.herosInfos[m_heroPos] = heros;
                        GameManager.Instance.HeroList[m_heroPos].GetComponent<Hero>().CheckDisableItem();
                    }                    
                    break;
                case 1:
                    if (GameManager.Instance.herosInfos[m_heroPos].equipItem_2 > -1)
                    {
                        GameManager.HerosInfo heros = new GameManager.HerosInfo();
                        heros = GameManager.Instance.herosInfos[m_heroPos];

                        GameManager.Instance.MakeItem_(heros.equipItem_2, heros.item_2);


                        heros.equipItem_2 = -1;

                        GameManager.Item temp = new GameManager.Item();
                        
                        temp.isGet = false;
                        heros.item_2 = temp;

                        GameManager.Instance.herosInfos[m_heroPos] = heros;
                        GameManager.Instance.HeroList[m_heroPos].GetComponent<Hero>().CheckDisableItem();

                    }
                    break;
            }

            

         
        }

        GameManager.Instance.Save(GameManager.saveType.herosInfos, m_heroPos);
    }
    public void SetItem(InfiniCoin needLevel,InfiniCoin nowLevel,int heroPos,int equipPos)
    {
        m_needLevel = needLevel;
        m_nowLevel = nowLevel;
        m_heroPos = heroPos;
        m_equipPos = equipPos;
        switch (equipPos)
        {
            case 0:
                CheckEquip(GameManager.Instance.herosInfos[m_heroPos].equipItem_1);
                break;
            case 1:
                CheckEquip(GameManager.Instance.herosInfos[m_heroPos].equipItem_2);
                break;
        }
    }
}
