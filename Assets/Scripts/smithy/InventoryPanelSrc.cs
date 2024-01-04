using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPanelSrc : MonoBehaviour
{
    public enum InventoryPanelType
    {
        normal,
        craft,
        Item,
        info
    }
    public InventoryPanelType inventoryType = InventoryPanelType.normal;


    public ScrollRect scrollRect;
    public ScrollRect scrollRect_2;

    public delegate void OnSelectCraftItemEvent(int pos);
    public event OnSelectCraftItemEvent OnSelectCraftItemEventHandler;

    public delegate void OnSelectCraftItem_UpgradeEvent(int pos);
    public event OnSelectCraftItem_UpgradeEvent OnSelectCraftItem_upgradeEventHandler;

    public delegate void OnSelectItem_HeroInfoEvent(int pos);
    public event OnSelectItem_HeroInfoEvent OnSelectItem_HeroInfoEventHandler;

    public GameObject topObject;
    public GameObject CraftObject;
    public Button CraftButton;
    public Text CraftText;

    public GameObject ItemViewObject;
    public Button ItemButton;
    public Text ItemText;


    public GameObject InventoryScroll_temp;
    public List<GameObject> InventoryScroll_lsit;
    public GameObject CraftItem;

    public GameObject InfoPanel;



    public GameObject InventoryItem_temp;
    public List<GameObject> InventoryItem_lsit;
    
    public void SetButton(int index)
    {
        if(index ==0)
        {
            CraftObject.SetActive(true);
            CraftButton.image.color = UiManager.Instance.enableButtonColor;
            CraftText.color = UiManager.Instance.enableButtonColor;

            ItemViewObject.SetActive(false);
            ItemButton.image.color = UiManager.Instance.disableButtonColor;
            ItemText.color = UiManager.Instance.disableButtonColor;
        }
        else if(index ==1)
        {
            CraftObject.SetActive(false);
            CraftButton.image.color = UiManager.Instance.disableButtonColor;
            CraftText.color = UiManager.Instance.disableButtonColor;

            ItemViewObject.SetActive(true);
            ItemButton.image.color = UiManager.Instance.enableButtonColor;
            ItemText.color = UiManager.Instance.enableButtonColor;
        }
    }
    void Start()
    {
        initdata();
        if(inventoryType == InventoryPanelType.normal)
        {
            SetButton(0);
        }
        if(inventoryType == InventoryPanelType.craft)
        {
            SetButton(0);
            //CraftObject.SetActive(true);
            //ItemViewObject.SetActive(false);
            //topObject.SetActive(false);
        }
        if(inventoryType == InventoryPanelType.info)
        {
            SetButton(1);
            CraftObject.SetActive(false);
            ItemViewObject.SetActive(true);
            topObject.SetActive(false);
        }

    }
    public void initdata()
    {
        if (InventoryScroll_lsit.Count == 0)
        {
            InventoryScroll_temp.SetActive(false);
            for (int i = 0; i < 19; i++)
            {
                GameObject temp = Instantiate(InventoryScroll_temp);
                temp.transform.SetParent(InventoryScroll_temp.transform.parent);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.transform.localScale = new Vector3(1, 1, 1);
                if (inventoryType == InventoryPanelType.normal)
                {
                    temp.GetComponent<InventoryItem>().OnInfoPanelEventHandler += InventoryPanelSrc_OnInfoPanelEventHandler;
                }
                if (inventoryType == InventoryPanelType.craft)
                {
                    temp.GetComponent<InventoryItem>().CraftSelectItemEventHandler += InventoryPanelSrc_CraftSelectItemEventHandler;
                }

                temp.SetActive(true);
                InventoryScroll_lsit.Add(temp);
            }
            CraftItem.GetComponent<InventoryItem>().OnInfoPanelEventHandler += InventoryPanelSrc_OnInfoPanelEventHandler;
        }
        for (int i = 0; i < InventoryScroll_lsit.Count; i++)
        {
            //if(GameManager.Instance.items[i].isGet ==true)
            {
                InventoryScroll_lsit[i].GetComponent<InventoryItem>().setData(i);
            }
        }
        CraftItem.GetComponent<InventoryItem>().inventoryItemType = InventoryItem.InventoryItemType.normal;
        CraftItem.GetComponent<InventoryItem>().setData();



        if(inventoryType == InventoryPanelType.normal || inventoryType == InventoryPanelType.craft || inventoryType == InventoryPanelType.info)
        {
            if (InventoryItem_lsit.Count == 0)
            {
                InventoryItem_temp.SetActive(false);
                for (int i = 0; i < 20; i++)
                {
                    GameObject temp = Instantiate(InventoryItem_temp);
                    temp.transform.SetParent(InventoryItem_temp.transform.parent);
                    temp.transform.localPosition = new Vector3(0, 0, 0);
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    temp.SetActive(true);
                    temp.GetComponent<InventoryItem>().inventoryItemType = InventoryItem.InventoryItemType.item;
                    temp.GetComponent<InventoryItem>().OnInfoPanelEventHandler += InventoryPanelSrc_OnInfoPanelEventHandler1;
                    InventoryItem_lsit.Add(temp);
                    if (inventoryType == InventoryPanelType.craft)
                    {                        
                        temp.GetComponent<InventoryItem>().CraftSelectItemEventHandler += InventoryPanelSrc_CraftSelectItemEventHandler1;
                    }
                    if(inventoryType == InventoryPanelType.info)
                    {
                        temp.GetComponent<InventoryItem>().inventoryItemType = InventoryItem.InventoryItemType.info;
                        temp.GetComponent<InventoryItem>().ItemSelectHeroEventHandler += InventoryPanelSrc_ItemSelectHeroEventHandler;
                    }

                }                
            }
            for (int i = 0; i < InventoryItem_lsit.Count; i++)
            {
                //if(GameManager.Instance.items[i].isGet ==true)
                {
                    InventoryItem_lsit[i].GetComponent<InventoryItem>().setData(i);
                }
            }
        }    

    }
    int m_SelectInventoryByHeroInfo = -1;
    private void InventoryPanelSrc_ItemSelectHeroEventHandler(int pos)
    {
        if(inventoryType == InventoryPanelType.info)
        {
            if (GameManager.Instance.items[pos].isGet == false)
                return;
            m_SelectInventoryByHeroInfo = pos;
            InfoPanel.SetActive(true);
            InfoPanel.GetComponent<SmithyInfoPanelSrc>().panelType = SmithyInfoPanelSrc.InfoPanelType.item;
            if (pos > -1)
            {
                InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos);
            }
            else
            {
                InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos, false);
            }
            //OnSelectItem_HeroInfoEventHandler?.Invoke(pos);
        }        
    }
    public void HeroInfoPanel_ItemSelect()
    {
        if(m_SelectInventoryByHeroInfo >-1)
        {
            OnSelectItem_HeroInfoEventHandler?.Invoke(m_SelectInventoryByHeroInfo);
            m_SelectInventoryByHeroInfo = -1;
        }
        
    }
    private void InventoryPanelSrc_CraftSelectItemEventHandler1(int pos)
    {
        //여기가 아이템 강화버튼누름
        if (inventoryType == InventoryPanelType.craft)
        {
            OnSelectCraftItem_upgradeEventHandler?.Invoke(pos);
            gameObject.SetActive(false);
            scrollRect.enabled = false;
            scrollRect_2.enabled = false;
        }
    }

    //아이템

    private void InventoryPanelSrc_OnInfoPanelEventHandler1(int pos)
    {
        InfoPanel.SetActive(true);
        InfoPanel.GetComponent<SmithyInfoPanelSrc>().panelType = SmithyInfoPanelSrc.InfoPanelType.item;
        if (pos > -1)
        {
            InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos);
        }
        else
        {
            InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos, false);
        }

    }

    private void InventoryPanelSrc_CraftSelectItemEventHandler(int pos)
    {
        if(inventoryType == InventoryPanelType.craft)
        {
            OnSelectCraftItemEventHandler?.Invoke(pos);
            gameObject.SetActive(false);
            scrollRect.enabled = false;
            scrollRect_2.enabled = false;
        }
    }

    private void InventoryPanelSrc_OnInfoPanelEventHandler(int pos)
    {
        InfoPanel.GetComponent<SmithyInfoPanelSrc>().panelType = SmithyInfoPanelSrc.InfoPanelType.normal;
        InfoPanel.SetActive(true);
        if (pos>-1)
        {            
            InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos);
        }
        else
        {
            InfoPanel.GetComponent<SmithyInfoPanelSrc>().SetData(pos,false);
        }
        
    }

    private void OnEnable()
    {
        initdata();
        GameManager.Instance.isNewItem = false;
        GameManager.Instance.Save(GameManager.saveType.isNewItem);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
