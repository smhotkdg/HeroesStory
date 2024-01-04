using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
public class UpgradeHeroItem : MonoBehaviour
{
    public Image FillBoostImage;
    public Text BoostText;
    public List<GameObject> XObjectList;
    public GameObject Open;
    public GameObject Lock;
    int heroIndex = -1;

    public Text DPSText;
    public Text NameText;
    public Text LevelUPCostText;
    public Button LevelUPButton;

    public Image HeroIcon;
    public Text LevelText;

    InfiniCoin UpgradeCost;
    int xUpgradeCount = 1;
    public void SetData(int index)
    {
        if(index ==-1)
        {
            Open.SetActive(false);
            Lock.SetActive(true);
        }
        else
        {
            Open.SetActive(true);
            Lock.SetActive(false);
            heroIndex = index;
            SetCost();
            SetHero();
        }
        SetxUpgrade();
        SetFillUpgrade();
    }

    void SetFillUpgrade()
    {
        if (heroIndex == -1)
            return;
        double heroLevel = GameManager.Instance.herosInfos[heroIndex].level;
        int heroCount = (int)Mathf.Floor((float)(heroLevel) / 25f);
        double tempherolevel = 0;
        if (heroCount < 0)
        {
            tempherolevel = 25;
        }
        else
        {
            tempherolevel = (25 * heroCount) + 25;
        }
        if (tempherolevel == 25)
        {
            FillBoostImage.fillAmount = (float)(heroLevel / 25);
        }
        else if (heroLevel < tempherolevel)
        {
            float temp = (float)tempherolevel - (float)heroLevel;
            FillBoostImage.fillAmount = 1 - (float)(temp / 25);
        }
        else
        {
            FillBoostImage.fillAmount = 1;
        }

        BoostText.text = "레벨 " + tempherolevel.ToString("N0") + " 에서 공격력 부스트";
    }

    void SetxUpgrade()
    {

        if (xUpgradeCount == 1)
        {
            XObjectList[0].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[1].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[2].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
        else if (xUpgradeCount == 10)
        {
            XObjectList[1].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[0].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[2].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
        else if (xUpgradeCount == 100)
        {
            XObjectList[2].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[1].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[0].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }

    }
    public void SetX(int index)
    {
        xUpgradeCount = index;
        SetxUpgrade();
        //SetCost_Dps_Text();
        SetCost();
    }

    private void Update()
    {
        if (heroIndex == -1)
            return;

        if(GameManager.Instance.TotalGold>= UpgradeCost)
        {
            LevelUPCostText.color = UiManager.Instance.enableButtonColor;
            LevelUPButton.image.color = UiManager.Instance.enableButtonColor; 
        }
        else
        {
            LevelUPCostText.color = UiManager.Instance.disableButtonColor;
            LevelUPButton.image.color = UiManager.Instance.disableButtonColor;
        }
    }
    
    public void SetCost()
    {
        UpgradeCost = GameManager.Instance.GetHeroCost(heroIndex, xUpgradeCount);
        LevelUPCostText.text = UiManager.Instance.SetCost(UpgradeCost);
        UiManager.Instance.SetHeroLevel_UI(LevelText, heroIndex);
        UiManager.Instance.SetHeroDPS_UI(DPSText, heroIndex);
        UiManager.Instance.SetHeroName_UI(NameText, heroIndex);
        UiManager.Instance.SetName_UIText(NameText, heroIndex);
        SetFillUpgrade();
    }
    void SetHero()
    {
        UiManager.Instance.SetHeroIcon_UI(HeroIcon, heroIndex);
    }
    public void LevelUP()
    {
        if (GameManager.Instance.TotalGold >= UpgradeCost)
        {
            if (GameManager.Instance.CheckLevelLevel(heroIndex,xUpgradeCount) == false)
            {
                UiManager.Instance.ShowBreakLimitPanelObject(heroIndex);
                return;
            }
            Start_LevelUPEffect();
            GameManager.Instance.TotalGold -= UpgradeCost;
            GameManager.Instance.HeroLevelUP(heroIndex,xUpgradeCount);
            SetCost();
            UiManager.Instance.SetGoldText();
            GameManager.Instance.Save(GameManager.saveType.TotalGold);
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gold);
        }
        SetFillUpgrade();
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
    IEnumerator EffectAnim()
    {
        LevelText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        DPSText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(LevelUpAnimComplete);
    }
    void LevelUpAnimComplete()
    {
        bLevelupEffect = false;
    }
    public void ShowInfo()
    {
        if (heroIndex == -1)
            return;
        UiManager.Instance.ShowHeroInfoPanel(heroIndex);
    }
}
