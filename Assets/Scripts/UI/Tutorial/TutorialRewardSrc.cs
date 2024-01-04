using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialRewardSrc : MonoBehaviour
{
    public Image Icon;
    public Text ValueText;

    public delegate void OnComplete();
    public event OnComplete OnCompleteEventHandler;

    public enum ItemType
    {
        Gold,
        Gem,
        Box,
        sBox,
        altar,
        soul

    };
    private void OnEnable()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.GetBuff);
    }
    GR_InfiniCoin.InfiniCoin m_cost = new GR_InfiniCoin.InfiniCoin();
    ItemType m_itemType;
    public void SetValue(GR_InfiniCoin.InfiniCoin value,ItemType itemType)
    {
        m_cost = value;
        m_itemType = itemType;
        ValueText.text = "+ " + UiManager.Instance.SetCost(m_cost);
        switch (itemType)
        {
            case ItemType.Box:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.nBox);
                break;
            case ItemType.Gem:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gem);
                break;
            case ItemType.Gold:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gold);
                break;
            case ItemType.sBox:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.sBox);
                break;
            case ItemType.altar:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.altar);
                break;
            case ItemType.soul:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.soul);
                break;
        }
        
    }
    private void OnDisable()
    {
        switch (m_itemType)
        {
            case ItemType.Box:
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
                break;
            case ItemType.Gem:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gem);
                GameManager.Instance.TotalGem += m_cost;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                UiManager.Instance.SetGemText();
                break;
            case ItemType.Gold:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gold);
                GameManager.Instance.TotalGold += m_cost;
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
                UiManager.Instance.SetGoldText();
                break;
            case ItemType.sBox:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.sBox);
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
                break;
            case ItemType.altar:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.altar);
                GameManager.Instance.TotalAltarCoin += m_cost;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                break;
            case ItemType.soul:
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.soul);
                GameManager.Instance.TotalSoul += m_cost;
                GameManager.Instance.Save(GameManager.saveType.TotalSoul);
                UiManager.Instance.SetSoulText();
                break;
        }
        OnCompleteEventHandler?.Invoke();
    }
}
