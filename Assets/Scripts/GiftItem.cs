using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftItem : MonoBehaviour
{
    public Text TitleText;
    public Image Icon;
    public GR_InfiniCoin.InfiniCoin gemCount;
    public enum giftType{
        gem,
        gem2,
        nbox,
        sbox,
        gem100,
        mat50,
        mat100,

        armor,//1
        belt,//2
        boots,//3
        cape,//4
        gloves,//5
        helmet,//6
        necklace,//7
        ring,//8
        shield,
        altar100,
        altar50,
        buffPack
    }

    public giftType GiftType = giftType.gem;
    public string itemCode = "";
    public void SetInitData()
    {
        switch (GiftType)
        {
            case giftType.gem100:
                TitleText.text = "보석 +100";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gem);
                break;
            case giftType.mat100:
                TitleText.text = "재료 +100";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.mat);
                break;
            case giftType.mat50:
                TitleText.text = "재료 +50";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.mat);
                break;
            case giftType.armor:
                TitleText.text = "갑옷 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.armor);
                break;
            case giftType.belt:
                TitleText.text = "벨트 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.belt);
                break;
            case giftType.boots:
                TitleText.text = "신발 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.boots);
                break;
            case giftType.cape:
                TitleText.text = "망토 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.cape);
                break;
            case giftType.gloves:
                TitleText.text = "장갑 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gloves);
                break;
            case giftType.helmet:
                TitleText.text = "투구 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.helmet);
                break;
            case giftType.necklace:
                TitleText.text = "목걸이 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.necklace);
                break;
            case giftType.ring:
                TitleText.text = "반지 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.ring);
                break;
            case giftType.shield:
                TitleText.text = "방패 도안 +1";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.shield);
                break;

            case giftType.gem:
                TitleText.text = "보석 +500";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gem);
                break;
            case giftType.gem2:
                TitleText.text = "보석 +1000";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.gem);
                break;
            case giftType.nbox:
                TitleText.text = "일반 영웅상자";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.nBox);
                break;
            case giftType.sbox:
                TitleText.text = "고급 영웅상자";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.sBox);
                break;

            case giftType.altar50:
                TitleText.text = "영혼의 조각 +50";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.altar);
                break;
            case giftType.altar100:
                TitleText.text = "영혼의 조각 +100";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.altar);
                break;
            case giftType.buffPack:
                TitleText.text = "버프 패키지";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.pack1);
                break;

        }

        //PlayNanooManager.Instance.useItemEventHandler += Instance_useItemEventHandler;
    }
    public void GetReward()
    {
        switch (GiftType)
        {
            case giftType.gem100:
                UiManager.Instance.SetBuyComplete(100, BuyCompletePanel.buyType.gem);
                break;
            case giftType.mat50:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.mat,50);
                break;
            case giftType.mat100:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.mat,100);
                break;
            case giftType.gem:
                UiManager.Instance.SetBuyComplete(500, BuyCompletePanel.buyType.gem);
                break;
            case giftType.gem2:
                UiManager.Instance.SetBuyComplete(1000, BuyCompletePanel.buyType.gem);
                break;

            case giftType.armor:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.armor);
                break;
            case giftType.belt:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.belt);
                break;
            case giftType.boots:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.boots);
                break;
            case giftType.cape:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.cape);
                break;
            case giftType.gloves:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.gloves);
                break;
            case giftType.helmet:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.helmet);
                break;
            case giftType.necklace:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.necklace);
                break;
            case giftType.ring:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.ring);
                break;
            case giftType.shield:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.shield);
                break;


            case giftType.nbox:
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Normal;
                break;
            case giftType.sbox:
                UiManager.Instance.GatchaPanel.SetActive(true);
                UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.Speacial;
                break;

            case giftType.altar50:
                UiManager.Instance.SetBuyComplete(50, BuyCompletePanel.buyType.altar_gift);
                break;

            case giftType.altar100:
                UiManager.Instance.SetBuyComplete(100, BuyCompletePanel.buyType.altar_gift);
                break;
            case giftType.buffPack:
                UiManager.Instance.SetBuyComplete(0, BuyCompletePanel.buyType.pack1);
                break;
        }

        Destroy(this.gameObject);
    }
    private void Instance_useItemEventHandler()
    {
       
    }

    public void GetGift()
    {
        PlayNanooManager.Instance.UseItem(itemCode,this.gameObject);
       
    }
}
