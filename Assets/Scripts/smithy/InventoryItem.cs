using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour
{
    public enum InventoryItemType
    {
        normal,
        craft,
        hero,
        item,
        info
    }
    
    public InventoryItemType inventoryItemType = InventoryItemType.normal;
    public Text CountText;
    public Image Icon;
    public Text UpgradeText;
    int m_position = -1;

    public delegate void OnInfoPanelEvent(int pos);
    public event OnInfoPanelEvent OnInfoPanelEventHandler;

    public delegate void CraftSelectItemEvent(int pos);
    public event CraftSelectItemEvent CraftSelectItemEventHandler;

    public delegate void ItemSelectHeroEvent(int pos);
    public event ItemSelectHeroEvent ItemSelectHeroEventHandler;


    
    private void Start()
    {
       
    }    
    public void setData(int position = -1)
    {
        m_position = position;
        if (inventoryItemType == InventoryItemType.normal || inventoryItemType == InventoryItemType.craft)
        {
            if (position > -1)
            {
                //여기가 스크롤
                if (GameManager.Instance.Scrolls[position].isGet == true)
                {
                    Icon.sprite = GameManager.Instance.ScrollSprite[GameManager.Instance.Scrolls[position].Item_Index];
                    CountText.text = GameManager.Instance.Scrolls[position].count.ToString();
                    Icon.gameObject.SetActive(true);
                }
                else
                {
                    Icon.gameObject.SetActive(false);
                    CountText.text = "";
                }
            }
            else
            {
                //여기가 재료
                if (GameManager.Instance.materialCount > 0)
                {
                    CountText.text = GameManager.Instance.materialCount.ToString();
                    Icon.gameObject.SetActive(true);
                }
                else
                {
                    CountText.text = "0";
                    //Icon.gameObject.SetActive(false);
                }

            }
        }
        else if (inventoryItemType == InventoryItemType.item || inventoryItemType == InventoryItemType.info)
        {
            if (GameManager.Instance.items[position].isGet == true && GameManager.Instance.items[position].count >0)
            {
                Icon.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.items[position].Item_Index];
                CountText.text = GameManager.Instance.items[position].count.ToString();
                Icon.gameObject.SetActive(true);
            }
            else
            {
                Icon.gameObject.SetActive(false);
                CountText.text = "";
            }
        }
       
    }
    public void SelectItem_Craft()
    {
        if (inventoryItemType == InventoryItemType.craft)
        {
            if (GameManager.Instance.Scrolls[m_position].isGet == true)
            {
                CraftSelectItemEventHandler?.Invoke(m_position);
            }            
        }
        if (inventoryItemType == InventoryItemType.item)
        {
            if (GameManager.Instance.items[m_position].isGet == true)
            {
                CraftSelectItemEventHandler?.Invoke(m_position);
            }
        }

    }
    public void SelectItem_Hero()
    {
        //여기서 아이템 선택해서 해당 히어로한테 등록
        if (inventoryItemType == InventoryItemType.info)
        {
            ItemSelectHeroEventHandler?.Invoke(m_position);
        }
    }
    public void SetInfoView()
    {
        if (m_position > -1)
        {
            //여기가 아이템 // 스크롤
            if(inventoryItemType == InventoryItemType.normal)
            {
                if (GameManager.Instance.Scrolls[m_position].isGet == true)
                {
                    OnInfoPanelEventHandler?.Invoke(m_position);
                }
            }
            if (inventoryItemType == InventoryItemType.item)
            {
                if (GameManager.Instance.items[m_position].isGet == true)
                {
                    OnInfoPanelEventHandler?.Invoke(m_position);
                }
            }
        }
        else
        {
            OnInfoPanelEventHandler?.Invoke(m_position);
        }        
    }
}
