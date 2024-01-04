using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyCompletePanel : MonoBehaviour
{
    public enum buyType
    {
        gem,
        gold,
        altar,
        attackBuff,
        ticket,
        mat,
        armor,//1
        belt,//2
        boots,//3
        cape,//4
        gloves,//5
        helmet,//6
        necklace,//7
        ring,//8
        shield,//9
        altar_gift,
        soul,
        pack1

    }
    public buyType type = buyType.gold;

    public Text BuyCountText;
    public Image BuyImage;

    private void OnEnable()
    {
        isPack1 = false;
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.GetBuff);
    }
    bool isPack1;
    private void OnDisable()
    {
        if(isPack1 ==true)
        {
            UiManager.Instance.GatchaPanel.SetActive(true);
            UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Hero;
        }
    }
    public void SetData(GR_InfiniCoin.InfiniCoin value,int iValue=0)
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.GetBuff);
        BuyCountText.text = UiManager.Instance.SetCost(value);
        switch (type)
        {
            case buyType.pack1:
                isPack1 = true;
                GameManager.Instance.Pack1 = true;
                GameManager.Instance.Save(GameManager.saveType.Pack1);
                GameManager.Instance.SetBuffPackage();
                BuyCountText.text = "패키지 구입완료";
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.pack1);
                break;
            case buyType.armor:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.armor);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.armor);
                break;
            case buyType.belt:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.belt);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.belt);
                break;
            case buyType.boots:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.boots);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.boots);
                break;
            case buyType.cape:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.cape);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.cape);
                break;
            case buyType.gloves:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.gloves);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.gloves);
                break;
            case buyType.helmet:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.helmet);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.helmet);
                break;
            case buyType.necklace:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.necklace);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.necklace);
                break;
            case buyType.ring:
                BuyCountText.text = 1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.ring);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.ring);
                break;
            case buyType.shield:
                BuyCountText.text =1.ToString();
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.shield);
                GameManager.Instance.MakeScrollByGift(GameManager.ScrollItemType.shield);
                break;
            


            case buyType.mat:
                GameManager.Instance.materialCount += iValue;
                BuyImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.mat);
                BuyCountText.text = iValue.ToString();
                GameManager.Instance.Save(GameManager.saveType.materialCount);
                break;
            case buyType.gem:
                GameManager.Instance.TotalGem += value;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.gem);
                UiManager.Instance.SetGemText();
                break;

            case buyType.soul:
                //GameManager.Instance.TotalGem += value;
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.soul);
                UiManager.Instance.SetSoulText();
                break;

            case buyType.gold:
                GameManager.Instance.TotalGold += value;
                GameManager.Instance.Save(GameManager.saveType.TotalGold);
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.gold);
                UiManager.Instance.SetGoldText();
                break;
            case buyType.attackBuff:
                BuyCountText.text = "2 시간";
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.attackbuff);                
                break;
            case buyType.altar:
                BuyCountText.text = UiManager.Instance.SetCost(value*2);
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.altar);
                break;
            case buyType.altar_gift:
                BuyCountText.text = UiManager.Instance.SetCost(value);
                GameManager.Instance.TotalAltarCoin += value;
                GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
                BuyImage.transform.localScale = new Vector3(1, 1, 1);
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.altar);
                break;
            case buyType.ticket:
                BuyCountText.text = "+ " + iValue.ToString();
                UiManager.Instance.TicketAdsPanel.SetActive(false);                
                UiManager.Instance.SetIcon(BuyImage, UiManager.iconType.ticket);
                BuyImage.transform.localScale = new Vector3(2, 1, 1);
                break;
        }
    }
}
