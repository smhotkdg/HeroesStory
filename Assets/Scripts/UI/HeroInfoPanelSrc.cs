using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
public class HeroInfoPanelSrc : MonoBehaviour
{
    public Text LimitLevelUpCountText;
    public GameObject InventoryInfo;
    public InventoryPanelSrc inventoryPanelSrc;
    public InventorySlot slot1;
    public InventorySlot slot2;
    public Text SoulCountText;
    public Text BoostText;
    public Image FillBoostImage;
    public List<GameObject> XObjectList;
 
    public GameObject AwakingEffect;
    public Image HeroIcon;
    public Text TierText;
    public Text HeroCardText;
    public Image HeroCardFillImage;
    public Text HeroNameText;
    public Text AwaekingText;

    public Text LevelText;
    public Text DPSText;
    public Text SpeacialAblityText;
    public Text SpecialAblity1Text;
    public Text SpecialAblity2Text;
    public Text SpecialAblity3Text;

    public Text SpecialAblity1Text_info;
    public Text SpecialAblity2Text_info;
    public Text SpecialAblity3Text_info;

    public Text LevelUpCostText;
    public Text FameCostText;

    public Button LevelUpButton;
    public Button FameButton;
    public Button AwakeingButton;
    public Image FillFameImage;

    UiManager uiManager;

    InfiniCoin UpgradeCost;

    int heroPositon;
    int xUpgradeIndex = 1;

    public GameObject BuySlotComfrimPanel;
    private void Start()
    {
        inventoryPanelSrc.OnSelectItem_HeroInfoEventHandler += InventoryPanelSrc_OnSelectItem_HeroInfoEventHandler;
    }
    public void ShowInvnetoryPanel(int index)
    {
        switch(index)
        {
            case 0:
                if (GameManager.Instance.herosInfos[heroPositon].equipItem_1 >-1)
                {
                    InventoryInfo.SetActive(true);
                    InventoryInfo.GetComponent<SmithyInfoPanelSrc>().SetInfoPanel(GameManager.Instance.herosInfos[heroPositon].equipItem_1,
                        SystemManager.ItemAblityStr[GameManager.Instance.herosInfos[heroPositon].equipItem_1],GameManager.Instance.herosInfos[heroPositon].item_1);
                }
                else
                {
                    inventoryPanelSrc.gameObject.SetActive(true);
                }
                break;
            case 1:
                if (GameManager.Instance.herosInfos[heroPositon].equipItem_2 > -1)
                {
                    InventoryInfo.SetActive(true);
                    InventoryInfo.GetComponent<SmithyInfoPanelSrc>().SetInfoPanel(GameManager.Instance.herosInfos[heroPositon].equipItem_2,
                        SystemManager.ItemAblityStr[GameManager.Instance.herosInfos[heroPositon].equipItem_2], GameManager.Instance.herosInfos[heroPositon].item_2);
                }
                else
                {
                    inventoryPanelSrc.gameObject.SetActive(true);
                }
                break;
        }
        
        
    }
    private void InventoryPanelSrc_OnSelectItem_HeroInfoEventHandler(int pos)
    {
        inventoryPanelSrc.gameObject.SetActive(false);

        GameManager.Item newItem = new GameManager.Item();
        newItem = GameManager.Instance.items[pos];
        //newItem.isGet = false;        
        newItem.count--;
        newItem.position = heroPositon;
        GameManager.Instance.items[pos] = newItem;
        GameManager.HerosInfo heros = new GameManager.HerosInfo();
        
        heros = GameManager.Instance.herosInfos[heroPositon];

        switch (slotIndex)
        {
            case 0:
                if(heros.equipItem_1 > -1)
                {
                    RemoveBeforeItem(0);
                }                
                heros.equipItem_1 = newItem.Item_Index;
                heros.item_1 = newItem;
                break;
            case 1:
                if (heros.equipItem_1 > -1)
                {
                    RemoveBeforeItem(1);
                }
                heros.equipItem_2 = newItem.Item_Index;
                heros.item_2 = newItem;
                break;
        }
        GameManager.Instance.herosInfos[heroPositon] = heros;
        GameManager.Instance.HeroList[heroPositon].GetComponent<Hero>().CheckDisableItem();
        // slotIndex
        SetSlot();
        if(newItem.count==0)
            GameManager.Instance.SetListItem(pos);

        GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
    }
    void RemoveBeforeItem(int position)
    {
       
        switch (position)
        {
            case 0:
                if (GameManager.Instance.herosInfos[heroPositon].equipItem_1 > -1)
                {
                    GameManager.HerosInfo heros = new GameManager.HerosInfo();
                    heros = GameManager.Instance.herosInfos[heroPositon];
                    
                    GameManager.Instance.MakeItem_(heros.equipItem_1, heros.item_1);

                    heros.equipItem_1 = -1;
                    GameManager.Item temp = new GameManager.Item();
                    
                    temp.isGet = false;
                    heros.item_1 = temp;
                    
                    GameManager.Instance.herosInfos[heroPositon] = heros;

                    GameManager.Instance.HeroList[heroPositon].GetComponent<Hero>().CheckDisableItem();
                }
                break;
            case 1:
                if (GameManager.Instance.herosInfos[heroPositon].equipItem_2 > -1)
                {               
                    GameManager.HerosInfo heros = new GameManager.HerosInfo();
                    heros = GameManager.Instance.herosInfos[heroPositon];

                    GameManager.Instance.MakeItem_(heros.equipItem_2, heros.item_2);

                    heros.equipItem_2 = -1;
                    GameManager.Item temp = new GameManager.Item();
                    
                    temp.isGet = false;
                    heros.item_2 = temp;
                    
                    GameManager.Instance.herosInfos[heroPositon] = heros;

                    GameManager.Instance.HeroList[heroPositon].GetComponent<Hero>().CheckDisableItem();
                }
                break;
        }
        GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
    }
    public void SetData(int heroPos)
    {
        AwakingEffect.SetActive(false);
        heroPositon = heroPos;
        uiManager = UiManager.Instance;

        uiManager.SetHeroIcon_UI(HeroIcon, heroPos);
        uiManager.CheckHeroIconColor(HeroIcon, heroPos);

        uiManager.SetTier_UIText(TierText, heroPos);
        
        uiManager.SetHeroName_UI(HeroNameText, heroPos);
        uiManager.Set_SpeacialAblity_InfoText(SpeacialAblityText, heroPos);
        uiManager.SetSpacialAblityText_UI(SpecialAblity1Text, SpecialAblity2Text, SpecialAblity3Text, heroPos);
        uiManager.SetLimitText_UI(LimitLevelUpCountText, heroPos);
        CheckFameCount();
        //UpgradeCost = GameManager.Instance.herosInfos[heroPos].Cost;
        SetCost_Dps_Text();
        SetHeroCardCount();
        SetSpecialAblityText();
        SetxUpgrade();
        SetFillUpgrade();

        SetSlot();
        
    }
    public void SetSlot()
    {
        slot1.SetItem(250, GameManager.Instance.herosInfos[heroPositon].level, heroPositon, 0);
        slot2.SetItem(500, GameManager.Instance.herosInfos[heroPositon].level, heroPositon, 1);
    }
    int slotIndex = -1;
    public void SetlSlotPos(int _slotPos)
    {
        slotIndex = _slotPos;
    }
    public void BuySlotShow(int _slotIndex)
    {
        slotIndex = _slotIndex;

        BuySlotComfrimPanel.SetActive(true);
    }
    public void BuySlot()
    {
        if (GameManager.Instance.TotalAltarCoin >= 50)
        {            
            GameManager.Instance.TotalAltarCoin -= 50;
            GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
            switch (slotIndex)
            {
                case 0:
                    GameManager.HerosInfo temp = GameManager.Instance.herosInfos[heroPositon];
                    temp.equipItem_1 = -1;
                    GameManager.Instance.herosInfos[heroPositon] = temp;                    
                    break;
                case 1:
                    GameManager.HerosInfo temp2 = GameManager.Instance.herosInfos[heroPositon];
                    temp2.equipItem_2 = -1;
                    GameManager.Instance.herosInfos[heroPositon] = temp2;
                    break;
            }
            GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
            BuySlotComfrimPanel.SetActive(false);
            SetSlot();
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.altar);
        }
    }
     
    void SetFillUpgrade()
    {
        double heroLevel = GameManager.Instance.herosInfos[heroPositon].level;
        int heroCount = (int)Mathf.Floor((float)(heroLevel) / 25f);
        double tempherolevel =0;
        if (heroCount <0)
        {
            tempherolevel = 25;
        }
        else
        {
            tempherolevel = (25 * heroCount)+25;
        }
        if(tempherolevel ==25)
        {            
            FillBoostImage.fillAmount = (float)(heroLevel / 25);
        }
        else if(heroLevel < tempherolevel)
        {
            float temp = (float)tempherolevel - (float)heroLevel;
            FillBoostImage.fillAmount = 1- (float)(temp / 25);
        }
        else
        {
            FillBoostImage.fillAmount = 1;
        }

        BoostText.text = "레벨 " + tempherolevel.ToString("N0") + " 에서 공격력 부스트";
    }
    void SetxUpgrade()
    {
       
        if(xUpgradeIndex == 1)
        {
            XObjectList[0].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[1].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[2].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
        else if(xUpgradeIndex == 10)
        {
            XObjectList[1].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[0].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[2].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
        else if (xUpgradeIndex == 100)
        {
            XObjectList[2].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[1].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[0].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }

    }
    public void SetX(int index)
    {
        xUpgradeIndex = index;
        SetxUpgrade();
        SetCost_Dps_Text();
    }
    void SetSpecialAblityText()
    {
        if (GameManager.Instance.herosInfos[heroPositon].specialAblity1 == "x")
        {
            SpecialAblity1Text_info.gameObject.SetActive(false);
        }
        else
        {
            if(GameManager.Instance.herosInfos[heroPositon].isAblity_1 == true)
            {                
                SpecialAblity1Text_info.text = "<color=red>획득</color>";
            }
            else
            {
                SpecialAblity1Text_info.text = "명성 Lv. 1 획득";
            }
            SpecialAblity1Text_info.gameObject.SetActive(true);
        }
        if (GameManager.Instance.herosInfos[heroPositon].specialAblity2 == "x")
        {
            SpecialAblity2Text_info.gameObject.SetActive(false);
        }
        else
        {
            if (GameManager.Instance.herosInfos[heroPositon].isAblity_2 == true)
            {
                SpecialAblity2Text_info.text = "<color=red>획득</color>";
            }
            else
            {                
                SpecialAblity2Text_info.text = "명성 Lv. 2 획득";
            }
            SpecialAblity2Text_info.gameObject.SetActive(true);
        }
        if (GameManager.Instance.herosInfos[heroPositon].specialAblity3 == "x")
        {
            SpecialAblity3Text_info.gameObject.SetActive(false);
        }
        else
        {
            if (GameManager.Instance.herosInfos[heroPositon].isAblity_3 == true)
            {
                SpecialAblity3Text_info.text = "<color=red>획득</color>";
            }
            else
            {                
                SpecialAblity3Text_info.text = "명성 Lv. 3 획득";
            }
            SpecialAblity3Text_info.gameObject.SetActive(true);
        }
    }
    void SetCost_Dps_Text_All()
    {
        uiManager.UpgradeHeroList();
    }
    void SetCost_Dps_Text()
    {
        UpgradeCost = GameManager.Instance.GetHeroCost(heroPositon,xUpgradeIndex);
        LevelUpCostText.text = UiManager.Instance.SetCost(UpgradeCost);
        uiManager.SetHeroDPS_UI(DPSText, heroPositon);
        uiManager.SetHeroLevel_UI(LevelText, heroPositon);
    }
    void SetHeroCardCount()
    {
        uiManager.SetHeroCount_fillUI(HeroCardText, HeroCardFillImage, heroPositon);

        if (GameManager.Instance.herosInfos[heroPositon].HeroCount >= 2)
        {
            AwakeingButton.image.color = uiManager.enableButtonColor;
            AwaekingText.color = uiManager.enableButtonColor;
        }
        else
        {
            AwakeingButton.image.color = uiManager.disableButtonColor;
            AwaekingText.color = uiManager.disableButtonColor;
        }
        AwaekingText.text = "각성Lv." + GameManager.Instance.herosInfos[heroPositon].AwakeningCount;
    }
    float m_fameCost;
    void CheckFameCount()
    {
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 0)
        {
            m_fameCost = 100;
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 1)
        {
            m_fameCost = 500;
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 2)
        {
            m_fameCost = 1000;
        }

        FameCostText.text = m_fameCost.ToString();
        if (GameManager.Instance.TotalSoul >= m_fameCost && GameManager.Instance.herosInfos[heroPositon].isGetHero == true)
        {
            FameButton.image.color = uiManager.enableButtonColor;
            FameCostText.color = uiManager.enableButtonColor;
        }
        else
        {
            FameButton.image.color = uiManager.disableButtonColor;
            FameCostText.color = uiManager.disableButtonColor;
        }
     
        FillFameImage.fillAmount = (float)(GameManager.Instance.herosInfos[heroPositon].FameCount) / m_fameCost;
        SoulCountText.text = GameManager.Instance.herosInfos[heroPositon].FameCount + " / " + m_fameCost.ToString("N0");
    }
    private void LateUpdate()
    {
        if(GameManager.Instance.TotalGold >= UpgradeCost && GameManager.Instance.herosInfos[heroPositon].isGetHero ==true)
        {
            LevelUpButton.image.color = uiManager.enableButtonColor;
            LevelUpCostText.color = uiManager.enableButtonColor;
        }
        else
        {
            LevelUpButton.image.color = uiManager.disableButtonColor;
            LevelUpCostText.color = uiManager.disableButtonColor;
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 0)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity1 == "x")
            {
                FameCostText.text = "--";
                FameButton.interactable = false;
            }
            else
            {
                FameButton.interactable = true;
            }
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 1)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity2 == "x")
            {
                FameCostText.text = "--";
                FameButton.interactable = false;
            }
            else
            {
                FameButton.interactable = true;
            }
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 2)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity3 == "x")
            {
                FameCostText.text = "--";
                FameButton.interactable = false;
            }
            else
            {
                FameButton.interactable = true;
            }
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel >= 3)
        {
            FameCostText.text = "--";
            FameButton.interactable = false;          
           
        }
    }
    public void LimitPanel()
    {
        uiManager.ShowBreakLimitPanelObject(heroPositon);
    }
    public void Levelup()
    {
        if (GameManager.Instance.TotalGold >= UpgradeCost && GameManager.Instance.herosInfos[heroPositon].isGetHero == true)
        {
            if (GameManager.Instance.CheckLevelLevel(heroPositon,xUpgradeIndex) == false)
            {
                uiManager.ShowBreakLimitPanelObject(heroPositon);
                return;
            }                
            
            Start_LevelUPEffect();
            GameManager.Instance.TotalGold -= UpgradeCost;
            GameManager.Instance.HeroLevelUP(heroPositon,xUpgradeIndex);
            SetCost_Dps_Text();
            uiManager.SetGoldText();
            uiManager.CheckUpgradePanel(heroPositon);
            SetFillUpgrade();
            SetSlot();
            GameManager.Instance.Save(GameManager.saveType.TotalGold);
        }
        else
        {
            if(GameManager.Instance.herosInfos[heroPositon].isGetHero ==false)
            {

            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gold);
            }
                
        }
    }
    public void Aweaking()
    {
        if (GameManager.Instance.herosInfos[heroPositon].HeroCount >= 2 && GameManager.Instance.herosInfos[heroPositon].isGetHero == true)
        {
            GameManager.Instance.achivementData.AchivementCount[5]++;
            GameManager.Instance.Save(GameManager.saveType.achivementData);
            AwaekingAnimComplete();
            GameManager.Instance.SetHeroAwaeking(heroPositon);
            SetHeroCardCount();
            SetCost_Dps_Text();
            uiManager.CheckUpgradePanel(heroPositon);
            if (GameManager.Instance.subquestType == GameManager.SubQuestType.aweaking)
            {
                GameManager.Instance.subQuestNow++;
                GameManager.Instance.Save(GameManager.saveType.subQuestNow);
                UiManager.Instance.SetSubQuestText();
            }
            SoundManager.Instance.PlayFX(SoundManager.SoundFXType.HeroinfoUpgrade);
        }
        else
        {
            if (GameManager.Instance.herosInfos[heroPositon].isGetHero == false)
            {

            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.card);
            }
            
        }
    }
    bool bLevelupEffect = false;
    public void Start_LevelUPEffect()
    {
        if (bLevelupEffect == false)
        {
            bLevelupEffect = true;
            StartCoroutine(EffectAnim());
        }
    }
    void LevelUpAnimComplete()
    {
        bLevelupEffect = false;
    }
    bool bAwakingEffect = false;
    void AwaekingAnimComplete()
    {
        if (bAwakingEffect == false)
        {
            bAwakingEffect = true;
            StartCoroutine(AweakingAnim());
        }
    }
    IEnumerator FameEffect(int index)
    {
        switch (index)
        {
            case 0:
                SpecialAblity1Text.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);                
                yield return new WaitForSeconds(0.1f);
                SpecialAblity1Text_info.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
                break;
            case 1:
                SpecialAblity2Text.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);                
                yield return new WaitForSeconds(0.1f);
                SpecialAblity2Text_info.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
                break;
            case 2:                
                SpecialAblity3Text.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
                yield return new WaitForSeconds(0.1f);
                SpecialAblity3Text_info.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);                
                break;
        }
        
        yield return new WaitForSeconds(0.1f);
        
    }
    IEnumerator AweakingAnim()
    {
        AwaekingText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        DPSText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        AwakingEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        AwakingEffect.SetActive(false);
        bAwakingEffect = false;
    }
    IEnumerator EffectAnim()
    {     
        LevelText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f),0.3f).SetEase(Ease.OutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        DPSText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(LevelUpAnimComplete);           
    }

    public void FameLevelUP()
    {
        if (m_fameCost <= 0)
            return;
        if(GameManager.Instance.herosInfos[heroPositon].FameLevel ==0)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity1 == "x")
                return;
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 1)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity2 == "x")
                return;
        }
        if (GameManager.Instance.herosInfos[heroPositon].FameLevel == 2)
        {
            if (GameManager.Instance.herosInfos[heroPositon].specialAblity3 == "x")
                return;
        }

        if (GameManager.Instance.TotalSoul >= m_fameCost && GameManager.Instance.herosInfos[heroPositon].isGetHero == true)
        {
            GameManager.Instance.TotalSoul -= m_fameCost;
            GameManager.Instance.Save(GameManager.saveType.TotalSoul);
            GameManager.HerosInfo heros = GameManager.Instance.herosInfos[heroPositon];
            heros.FameCount += m_fameCost;
            
            uiManager.SetSoulText();
            if(heros.FameCount >= m_fameCost)
            {
                heros.FameLevel++;
                heros.FameCount = 0;                
            }
            GameManager.Instance.herosInfos[heroPositon] = heros;

            FameUpgradeCheck();
            SoundManager.Instance.PlayFX(SoundManager.SoundFXType.HeroinfoUpgrade);
            GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
        }
        else
        {
            if (GameManager.Instance.herosInfos[heroPositon].isGetHero == false)
            {
                
            }
            else
            {
                uiManager.SetNotification(UiManager.NotificationType.soul);
            }
            
        }
        CheckFameCount();
    }
    void FameUpgradeCheck()
    {
        uiManager.Set_SpeacialAblity_InfoText(SpeacialAblityText, heroPositon);

        if(GameManager.Instance.herosInfos[heroPositon].isAblity_1 ==false && GameManager.Instance.herosInfos[heroPositon].FameLevel >=1)
        {
            //1번 업그레이드
            UpgradeFame(GameManager.Instance.herosInfos[heroPositon].specialAblity1);
            GameManager.HerosInfo heros = GameManager.Instance.herosInfos[heroPositon];
            heros.isAblity_1 = true;
            GameManager.Instance.herosInfos[heroPositon] = heros;
            StartCoroutine(FameEffect(0));
            GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
        }
        if (GameManager.Instance.herosInfos[heroPositon].isAblity_2 == false && GameManager.Instance.herosInfos[heroPositon].FameLevel >= 2)
        {
            //2번 업그레이드
            UpgradeFame(GameManager.Instance.herosInfos[heroPositon].specialAblity2);
            GameManager.HerosInfo heros = GameManager.Instance.herosInfos[heroPositon];
            heros.isAblity_2 = true;
            GameManager.Instance.herosInfos[heroPositon] = heros;
            StartCoroutine(FameEffect(1));
            GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
        }
        if (GameManager.Instance.herosInfos[heroPositon].isAblity_3 == false && GameManager.Instance.herosInfos[heroPositon].FameLevel >= 3)
        {
            //3번 업그레이드
            UpgradeFame(GameManager.Instance.herosInfos[heroPositon].specialAblity3);
            GameManager.HerosInfo heros = GameManager.Instance.herosInfos[heroPositon];
            heros.isAblity_3 = true;
            GameManager.Instance.herosInfos[heroPositon] = heros;
            StartCoroutine(FameEffect(2));
            GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
        }
        SetSpecialAblityText();
    }
    void UpgradeFame(string ablity)
    {
        string[] splitStr = ablity.Split('_');
        string ablityName = "";
        switch (splitStr[0])
        {
            case "dps":
                //ablityName = "공격력 + " + splitStr[1] + " % 증가";
                GameManager.Instance.DpsUpgrade(float.Parse(splitStr[1]) * 0.01f, heroPositon);
                SetCost_Dps_Text_All();
                SetCost_Dps_Text();

                break;
            case "gold":
                //ablityName = "골드 획득 + " + splitStr[1] + " % 증가";

                GameManager.HerosInfo heros = GameManager.Instance.herosInfos[heroPositon];
                heros.goldBuff += float.Parse(splitStr[1]) * 0.01f;
                GameManager.Instance.herosInfos[heroPositon] = heros;
                //현재 배치가 되어있는지 확인
                GameManager.Instance.CheckPostionBuff(heroPositon);
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "alldps":
                //ablityName = "모든 영웅 공격력 + " + splitStr[1] + " % 증가";
                GameManager.Instance.AllDpsUpgrade(float.Parse(splitStr[1]) * 0.01f);
                SetCost_Dps_Text_All();
                SetCost_Dps_Text();
                break;
            case "bosstime":                
                GameManager.HerosInfo herosBoss = GameManager.Instance.herosInfos[heroPositon];
                herosBoss.bossTimeBuff += float.Parse(splitStr[1]);
                GameManager.Instance.herosInfos[heroPositon] = herosBoss;

                //현재 배치가 되어있는지 확인
                GameManager.Instance.CheckPostionBuff(heroPositon);
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "click":
                GameManager.HerosInfo ClickBuffHero = GameManager.Instance.herosInfos[heroPositon];
                ClickBuffHero.ClickBuff += float.Parse(splitStr[1]) * 0.01f;
                GameManager.Instance.herosInfos[heroPositon] = ClickBuffHero;

                //현재 배치가 되어있는지 확인
                GameManager.Instance.CheckPostionBuff(heroPositon);
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "kill":
                ablityName = splitStr[1] + "% 확률로 몬스터 즉사";
                GameManager.HerosInfo AutoKill= GameManager.Instance.herosInfos[heroPositon];
                AutoKill.killPercent += float.Parse(splitStr[1]);
                GameManager.Instance.herosInfos[heroPositon] = AutoKill;
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "expedition":
                //ablityName = "원정대 보상 + " + splitStr[1] + " % 증가";
                GameManager.HerosInfo goAway = GameManager.Instance.herosInfos[heroPositon];
                goAway.goawayBuff += float.Parse(splitStr[1]) * 0.01f;
                GameManager.Instance.herosInfos[heroPositon] = goAway;

                //현재 배치가 되어있는지 확인
                GameManager.Instance.CheckPostionBuff(heroPositon);
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "raid":
                //ablityName = "레이드 보상 + " + splitStr[1] + " % 증가";
                GameManager.HerosInfo raidhero = GameManager.Instance.herosInfos[heroPositon];
                raidhero.raidBuff += float.Parse(splitStr[1]) * 0.01f;
                GameManager.Instance.herosInfos[heroPositon] = raidhero;
                //현재 배치가 되어있는지 확인
                GameManager.Instance.CheckPostionBuff(heroPositon);
                GameManager.Instance.Save(GameManager.saveType.herosInfos, heroPositon);
                break;
            case "x":
                ablityName = "없음";
                break;
        }
        
    }
   
}

