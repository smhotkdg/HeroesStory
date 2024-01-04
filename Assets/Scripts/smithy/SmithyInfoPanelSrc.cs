using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SmithyInfoPanelSrc : MonoBehaviour
{
    public enum InfoPanelType
    {
        normal,
        item
    }
    public GameObject RemoveIcon;
    public GameObject InventoryPanel;
    public InfoPanelType panelType = InfoPanelType.normal;
    public GameObject DestoryPanel;
    public Text NameText;
    public Image Icon;
    public Text CountText;
    public Text AblityText;
    int m_index =-1;
    public void SetData(int index, bool isOn = true)
    {
        CountText.gameObject.SetActive(true);
        if (panelType == InfoPanelType.normal)
        {
            if (isOn == true)
            {
                RemoveIcon.SetActive(true);
                m_index = index;
                Icon.sprite = GameManager.Instance.ScrollSprite[GameManager.Instance.Scrolls[index].Item_Index];
                //NameText.text = SystemManager.Itemname[GameManager.Instance.Scrolls[index].Item_Index];
                switch(GameManager.Instance.Scrolls[index].ablityType)
                {
                    case 0:
                        NameText.text = "반지";
                        break;
                    case 1:
                        NameText.text = "목걸이";
                        break;
                    case 2:
                        NameText.text = "투구";
                        break;
                    case 3:
                        NameText.text = "갑옷";
                        break;
                    case 4:
                        NameText.text = "부츠";
                        break;
                    case 5:
                        NameText.text = "장갑";
                        break;
                    case 6:
                        NameText.text = "벨트";
                        break;
                    case 7:
                        NameText.text = "방패";
                        break;
                    case 8:
                        NameText.text = "망토";
                        break;
                }
                CountText.text = GameManager.Instance.Scrolls[index].count.ToString();
                //AblityText.text = GameManager.Instance.GetItemAblityName(GameManager.Instance.Scrolls[index].ablityType, GameManager.Instance.Scrolls[index].Item_Index);
                AblityText.text = GameManager.Instance.GetItemAblityName_Scroll(GameManager.Instance.Scrolls[index].ablityType, GameManager.Instance.Scrolls[index].name);
            }
            else
            {
                //여기 아이템으로
                //Icon.sprite =
                RemoveIcon.SetActive(false);
                UiManager.Instance.SetIcon(Icon,UiManager.iconType.mat);
                NameText.text = "제작 재료";
                CountText.text = GameManager.Instance.materialCount.ToString();
                AblityText.text = "아이템을 제작하는데 사용합니다.";
            }
        }
        else if(panelType == InfoPanelType.item)
        {
            RemoveIcon.SetActive(true);
            m_index = index;
            Icon.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.items[index].Item_Index];
            NameText.text = SystemManager.Itemname[GameManager.Instance.items[index].Item_Index];
            CountText.text = GameManager.Instance.items[index].count.ToString();
            AblityText.text = GameManager.Instance.GetItemAblityName(GameManager.Instance.items[index].ablityType, index);
        }        
    }
    public void SetInfoPanel(int ItemPos,int ablitType,GameManager.Item myItme)
    {
        if (panelType == InfoPanelType.item)
        {
            Icon.sprite = GameManager.Instance.ItemsSprite[ItemPos];
            NameText.text = SystemManager.Itemname[ItemPos];
            CountText.gameObject.SetActive(false);
            AblityText.text = GameManager.Instance.GetItemAblityName_Item(myItme);
        }
    }
    public void SelectItem()
    {
        if(InventoryPanel !=null)
        {
            InventoryPanel.GetComponent<InventoryPanelSrc>().HeroInfoPanel_ItemSelect();
            gameObject.SetActive(false);
        }
    }
    public void ShowDestoryPanel()
    {
       
        if (m_index == -1)
            return;
        DestoryPanel.SetActive(true);
        DestoryPanel.GetComponent<DItemestoryPanelSrc>().setData(m_index, gameObject,panelType);    
    }
}
