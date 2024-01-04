using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanelSrc : MonoBehaviour
{
    public GameObject GiftTemp;
    List<GameObject> tempList = new List<GameObject>();
    private void OnEnable()
    {
        GiftTemp.SetActive(false);
        PlayNanooManager.Instance.GetMailBox(this.gameObject);
    }
    public void MakeItem(string itemCode, int ItemCount,int exSec,string uid)
    {
        GameObject gift = Instantiate(GiftTemp);
        gift.transform.SetParent(GiftTemp.transform.parent);
        gift.transform.localPosition = new Vector3(0, 0, 0);
        gift.transform.localScale = new Vector3(1, 1, 1);
        switch (itemCode)
        {
            case "gem100":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.gem100;
                break;
            case "mat50":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.mat50;
                break;
            case "mat100":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.mat100;
                break;

            case "shield_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.shield;
                break;
            case "ring_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.ring;
                break;
            case "necklace_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.necklace;
                break;
            case "helmet_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.helmet;
                break;
            case "cape_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.cape;
                break;
            case "boots_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.boots;
                break;
            case "belt_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.belt;
                break;
            case "armor_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.armor;
                break;
            case "gloves_scroll":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.gloves;
                break;

            case "gem500":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.gem;                
                break;
            case "gem1000":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.gem2;
                break;
            case "speacialBox":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.sbox;
                break;
            case "normalBox":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.nbox;
                break;

            case "altar100":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.altar100;
                break;
            case "altar50":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.altar50;
                break;

            case "Buffpackage":
                gift.GetComponent<GiftItem>().GiftType = GiftItem.giftType.buffPack;
                break;

        }
        gift.SetActive(true);
        gift.GetComponent<GiftItem>().SetInitData();
        gift.GetComponent<GiftItem>().itemCode = uid;
        tempList.Add(gift);
    }
    private void OnDisable()
    {
        for (int i = 0; i < tempList.Count; i++)
        {
            Destroy(tempList[i]);
        }
        tempList.Clear();
        UiManager.Instance.CheckGift();
    }
}
