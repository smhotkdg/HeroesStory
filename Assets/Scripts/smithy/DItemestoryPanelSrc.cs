using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DItemestoryPanelSrc : MonoBehaviour
{
    public InventoryPanelSrc inventoryPanel;
    public Text NameText;
    public Image Icon;
    public Text CountText;
    public GameObject Effect;

    public Button ButtonExit;
    public Button DestoryButton;

    public Text MaCount;
    int MatTotalCount = 0;
    int m_index;
    GameObject m_p;
    SmithyInfoPanelSrc.InfoPanelType infoPanelType;
    private void OnDisable()
    {
        if(Effect !=null)
            Effect.SetActive(false);
    }
    public void setData(int index,GameObject p, SmithyInfoPanelSrc.InfoPanelType infoPanel)
    {
        infoPanelType = infoPanel;
        m_p = p;
        ButtonExit.interactable = true;
        DestoryButton.interactable = true;
        m_index = index;

        if (infoPanel == SmithyInfoPanelSrc.InfoPanelType.normal)
        {
            Icon.sprite = GameManager.Instance.ScrollSprite[GameManager.Instance.Scrolls[index].Item_Index];
            //NameText.text = SystemManager.Itemname[GameManager.Instance.Scrolls[index].Item_Index];
            NameText.text = "도안";
            CountText.text = 1.ToString();

            //여기서 가중치 주자
            MatTotalCount = (5);
            MaCount.text = MatTotalCount.ToString();
        }
        else if(infoPanel == SmithyInfoPanelSrc.InfoPanelType.item)
        {
            Icon.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.items[index].Item_Index];
            NameText.text = SystemManager.Itemname[GameManager.Instance.items[index].Item_Index];
            CountText.text =1.ToString();

            //여기서 가중치 주자
            MatTotalCount = (5);
            MaCount.text = MatTotalCount.ToString();
        }
    }
    public void DestoryItem()
    {

        ButtonExit.interactable = false;
        DestoryButton.interactable = false;

        GameManager.Instance.materialCount += MatTotalCount;
        GameManager.Instance.Save(GameManager.saveType.materialCount);
        if (infoPanelType == SmithyInfoPanelSrc.InfoPanelType.normal)
        {
            GameManager.Instance.SetListScroll(m_index);
        }
        else if(infoPanelType == SmithyInfoPanelSrc.InfoPanelType.item)
        {
            GameManager.Instance.SetListItem(m_index);
        }
        


        inventoryPanel.initdata();
        StartCoroutine(EffectRoutine());
    }
    IEnumerator EffectRoutine()
    {
        if (Effect != null)
            Effect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        m_p.SetActive(false);
    }
}
