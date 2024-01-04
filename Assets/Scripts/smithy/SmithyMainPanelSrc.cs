 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SmithyMainPanelSrc : MonoBehaviour
{
    public GameObject tutorialObject;
    public Text Craft_titleText;
    public GameObject ExplosionEffect;
    public GameObject HammerObject;
    public GameObject SelectUIObject;
    public GameObject EffectObject;
    public Image Icon;

    public Button CraftButton;
    public Text CraftText;
    public Image ScrolIcon;
    public Text ItemName;

    public Text materialCount;
    public Text AltarCount;

    public GameObject PlusImage;

    public InventoryPanelSrc inventory;
   

    int needMaterialCount;
    int needAltarCount;

    int m_pos = -1;
    private void Start()
    {
        inventory.OnSelectCraftItemEventHandler += Inventory_OnSelectCraftItemEventHandler;
        inventory.OnSelectCraftItem_upgradeEventHandler += Inventory_OnSelectCraftItem_upgradeEventHandler;
    }

    private void Inventory_OnSelectCraftItem_upgradeEventHandler(int pos)
    {
        SetItem_upgrade(pos);
    }

    private void Inventory_OnSelectCraftItemEventHandler(int pos)
    {
        SetItem(pos);
    }
    public bool isCraft = false;
    public bool bEnableCraft = false;
    void SetItem(int pos)
    {
        Craft_titleText.text = "제작";
        m_pos = pos;
        PlusImage.SetActive(false);
        ScrolIcon.gameObject.SetActive(true);
        ItemName.gameObject.SetActive(true);

        ScrolIcon.sprite = GameManager.Instance.ScrollSprite[GameManager.Instance.Scrolls[pos].Item_Index];
        //ItemName.text = SystemManager.Itemname[GameManager.Instance.Scrolls[pos].Item_Index];
        SetName(pos);
        int tier = GameManager.Instance.Scrolls[pos].itemTier;
        int mtier = tier * 25;
        int aTier = tier * 10;

        needMaterialCount = 50 + mtier;
        needAltarCount = 25 + aTier;

        if(GameManager.Instance.materialCount >= needMaterialCount && GameManager.Instance.TotalAltarCoin >= needAltarCount)
        {
            CraftButton.image.color = UiManager.Instance.enableButtonColor;
            CraftText.color = UiManager.Instance.enableButtonColor;
            bEnableCraft = true;
        }
        else
        {
            CraftButton.image.color = UiManager.Instance.disableButtonColor;
            CraftText.color = UiManager.Instance.disableButtonColor;
        }
        materialCount.text = "x "+needMaterialCount+"\n(보유 : "+GameManager.Instance.materialCount+")";
        AltarCount.text = "x " + needAltarCount + "\n(보유 : " + UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin) + ")"; ;
    }
    bool isUpgradeItem = false;
    void SetName(int pos)
    {
        switch (GameManager.Instance.Scrolls[pos].ablityType)
        {
            case 0:
                ItemName.text = "반지";
                break;
            case 1:
                ItemName.text = "목걸이";
                break;
            case 2:
                ItemName.text = "투구";
                break;
            case 3:
                ItemName.text = "갑옷";
                break;
            case 4:
                ItemName.text = "부츠";
                break;
            case 5:
                ItemName.text = "장갑";
                break;
            case 6:
                ItemName.text = "벨트";
                break;
            case 7:
                ItemName.text = "방패";
                break;
            case 8:
                ItemName.text = "망토";
                break;
        }
    }
    void SetItem_upgrade(int pos)
    {
        Craft_titleText.text = "강화";
        m_pos = pos;
        PlusImage.SetActive(false);
        ScrolIcon.gameObject.SetActive(true);
        ItemName.gameObject.SetActive(true);

        ScrolIcon.sprite = GameManager.Instance.ItemsSprite[GameManager.Instance.items[pos].Item_Index];
        //ItemName.text = SystemManager.Itemname[GameManager.Instance.items[pos].Item_Index];
        SetName(pos);



        int tier = GameManager.Instance.items[pos].itemTier;
        int mtier = tier * 30;
        int aTier = tier * 20;

        needMaterialCount = 50 + mtier;
        needAltarCount = 30 + aTier;

        if (GameManager.Instance.materialCount >= needMaterialCount && GameManager.Instance.TotalAltarCoin >= needAltarCount)
        {
            CraftButton.image.color = UiManager.Instance.enableButtonColor;
            CraftText.color = UiManager.Instance.enableButtonColor;
            isUpgradeItem = true;
        }
        else
        {
            CraftButton.image.color = UiManager.Instance.disableButtonColor;
            CraftText.color = UiManager.Instance.disableButtonColor;
        }
        
        materialCount.text = "x " + needMaterialCount + "\n(보유 : " + GameManager.Instance.materialCount + ")";
        AltarCount.text = "x " + needAltarCount + "\n(보유 : " + UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin) + ")"; ;
    }

    private void OnEnable()
    {
        InitCraftData();
        if(GameManager.Instance.IsNewSmith ==1)
        {
            GameManager.Instance.IsNewSmith = 2;
            GameManager.Instance.Save(GameManager.saveType.IsNewSmith);
            tutorialObject.SetActive(true);
        }
    }
    void InitCraftData()
    {
        PlusImage.SetActive(true);
        ScrolIcon.gameObject.SetActive(false);
        ItemName.gameObject.SetActive(false);

        CraftButton.image.color = UiManager.Instance.disableButtonColor;
        CraftText.color = UiManager.Instance.disableButtonColor;

        materialCount.text = "선택필요" + "\n(보유 : " + GameManager.Instance.materialCount + ")"; ;
        AltarCount.text = "선택필요" + "\n(보유 : " + UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin) + ")";   
        Craft_titleText.text = "선택";
        bEnableCraft = false;
        isUpgradeItem = false;
    }
    public void SelectScroll()
    {
        inventory.gameObject.SetActive(true);
    }
    bool bStart = false;
    Vector3 initEffectPos = new Vector3();
    public void CraftItem()
    {
        if(bEnableCraft ==true)
        {

            if (GameManager.Instance.CheckInventoryFull() == true)
                return;


            GameManager.Instance.materialCount -= needMaterialCount;
            GameManager.Instance.Save(GameManager.saveType.materialCount);
            GameManager.Instance.TotalAltarCoin -= needAltarCount;
            GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
            //여기서 스크롤 없애고 아이템 만들어주면됨 호잇호잇            
            //int scrollRand = Random.Range(0, 5);

            int scrollRand = (int)GameManager.Instance.GetTierItem();
            //여기가 티어
            int randposition = (5 * GameManager.Instance.Scrolls[m_pos].Item_Index) + scrollRand;


            
            
            GameManager.Instance.MakeNewItem(m_pos, scrollRand, SystemManager.Itemname[randposition], GameManager.Instance.Scrolls[m_pos].ablityType,
                randposition);
            
            Icon.sprite = GameManager.Instance.ItemsSprite[randposition];

            GameManager.Instance.SetListScroll(m_pos);
            
            
            if (bStart == false)
            {
                bStart = true;
                InitCraftData();
                SelectUIObject.SetActive(false);
                HammerObject.SetActive(true);
            }

            GameManager.Instance.achivementData.AchivementCount[9]++;
            GameManager.Instance.Save(GameManager.saveType.achivementData);
        }
        else if(isUpgradeItem ==true)
        {
            //여기서 강화
            UiManager.Instance.SetNotification(UiManager.NotificationType.Update);
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.craft);
        }
      

    }
    public void SetGetItemEffect()
    {
        HammerObject.SetActive(false);
        ExplosionEffect.SetActive(true);        
    }
    public void GetEffect()
    {        
        initEffectPos = EffectObject.transform.localPosition;
        Vector3 movePos = initEffectPos;
        movePos.y = movePos.y + 300;
        EffectObject.SetActive(true);
        EffectObject.transform.Find("Effect").gameObject.GetComponent<FlatFX>().AddEffect(EffectObject.transform.position, 4);

        EffectObject.transform.DOLocalMove(movePos, 2.0f).OnComplete(EndEffect);
    }
    public void EndEffect()
    {
        EffectObject.transform.localPosition = initEffectPos;        
        EffectObject.SetActive(false);
        bStart = false;
        SelectUIObject.SetActive(true);
    }
}
