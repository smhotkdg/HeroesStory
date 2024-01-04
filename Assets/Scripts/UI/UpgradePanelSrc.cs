using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GR_InfiniCoin;
public class UpgradePanelSrc : MonoBehaviour
{
    public Image FillBoostImage;
    public Text BoostText;
    public List<GameObject> XObjectList;
    public GameObject TempUpgrade;
    public Text ClickLevelUpCostText;
    public Text ClickPowerText;
    public Text LevelText;
    public GameObject ClickUpgrade;
    public RectTransform contentSize;
    public Button ClickButton;
    int initCost = 5;
    List<GameObject> UpgradeHero = new List<GameObject>();

    void SetFillUpgrade()
    {
        double heroLevel = GameManager.Instance.clicker.level;
        int heroCount = (int)Mathf.Floor((float)(heroLevel) / 25);
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

    private void Start()
    {
        initData();
    }
    int xUpgradeIndex = 1;
    void SetxUpgrade()
    {

        if (xUpgradeIndex == 1)
        {
            XObjectList[0].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            XObjectList[1].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
            XObjectList[2].GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
        else if (xUpgradeIndex == 10)
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


        costValue = 0;
        for (int i = 0; i < xUpgradeIndex; i++)
        {
            costValue += initCost * SystemManager.mathPow(1.07f, GameManager.Instance.clicker.level + i);
        }

        InfiniCoin value = GameManager.Instance.GetArtifactValue(GameManager.AtrifactType.costDiscount);

        costValue = costValue - (costValue * value);

    }
    GR_InfiniCoin.InfiniCoin costValue = new GR_InfiniCoin.InfiniCoin();
    public void SetX(int index)
    {
        xUpgradeIndex = index;
        SetxUpgrade();
        //SetCost_Dps_Text();
        SetClickText();
    }

    void initData()
    {
        if(UpgradeHero.Count ==0)
        {
            int totalCount = 11;
            Vector2 myvec = contentSize.sizeDelta;
            myvec.y = 160 * totalCount;
            contentSize.sizeDelta = myvec;


            RectTransform myrect_temp = ClickUpgrade.GetComponent<RectTransform>();
            myrect_temp.offsetMin = new Vector2(0, myrect_temp.offsetMin.y);
            myrect_temp.offsetMax = new Vector2(0, myrect_temp.offsetMax.y);

            

            Vector3 myVec_temp = ClickUpgrade.transform.localPosition;
            myVec_temp.z = 0;
            //myVec.x = 0;
            myVec_temp.y = -75;
            ClickUpgrade.transform.localPosition = myVec_temp;

            ClickUpgrade.SetActive(true);

            for (int i = 0; i < 10; i++)
            {
                
                GameObject temp = Instantiate(TempUpgrade);
                temp.transform.SetParent(TempUpgrade.transform.parent);
                temp.transform.localScale = new Vector3(1, 1, 1);
                //temp.transform.localPosition = new Vector3(0, 0, 0);
                
                temp.name = "-1";
                UpgradeHero.Add(temp);

                RectTransform myrect = temp.GetComponent<RectTransform>();
                myrect.offsetMin = new Vector2(0, myrect.offsetMin.y);
                myrect.offsetMax = new Vector2(0, myrect.offsetMax.y);

                UpgradeHero[i].GetComponent<UpgradeHeroItem>().SetData(-1);

                Vector3 myVec = temp.transform.localPosition;
                myVec.z = 0;
                //myVec.x = 0;
                myVec.y = -75 - (160 * (i+1));
                temp.transform.localPosition = myVec;

                temp.SetActive(true);
            }
        }
        for(int i =0; i< UpgradeHero.Count;i++)
        {
            UpgradeHero[i].name = "-1";
            UpgradeHero[i].GetComponent<UpgradeHeroItem>().SetData(-1);
        }
        int count = -1;
        for (int i = 0; i < GameManager.Instance.heroPos.Length; i++)
        {
            if (GameManager.Instance.heroPos[i] > -1)
            {
                count++;
                UpgradeHero[count].name = GameManager.Instance.heroPos[i].ToString();
                UpgradeHero[count].GetComponent<UpgradeHeroItem>().SetData(GameManager.Instance.heroPos[i]);
            }           
        }
    }
    private void OnEnable()
    {
        initData();
        SetClickText();
        SetxUpgrade();
        SetFillUpgrade();
    }
    void SetClickText()
    {
        costValue = 0;
        for (int i = 0; i < xUpgradeIndex; i++)
        {
            costValue += initCost * SystemManager.mathPow(1.07f, GameManager.Instance.clicker.level + i);
        }
        InfiniCoin value = GameManager.Instance.GetArtifactValue(GameManager.AtrifactType.costDiscount);

        costValue = costValue - (costValue * value);

        LevelText.text = "lv." + GameManager.Instance.clicker.level.ToString();
        ClickPowerText.text = "클릭 파워 : "+UiManager.Instance.SetCost(GameManager.Instance.clicker.TotalClickPower);



        ClickLevelUpCostText.text = costValue.ToString();
        //for (int i = 0; i < xUpgradeIndex; i++)
        //{
        //    ClickLevelUpCostText.text = UiManager.Instance.SetCost(initCost * SystemManager.mathPow(1.07f, GameManager.Instance.clicker.level + i));
        //}

    }
    private void FixedUpdate()
    {      
        
        if (GameManager.Instance.TotalGold >= costValue)
        {
            ClickButton.image.color = UiManager.Instance.enableButtonColor;
            ClickLevelUpCostText.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            ClickButton.image.color = UiManager.Instance.disableButtonColor;
            ClickLevelUpCostText.color = UiManager.Instance.disableButtonColor;
        }
    }

    public void UpgradeClick()
    {
        if(GameManager.Instance.TotalGold>= costValue)
        {
            for (int i = 0; i < xUpgradeIndex; i++)
            {
                GR_InfiniCoin.InfiniCoin cost = initCost * SystemManager.mathPow(1.07f, GameManager.Instance.clicker.level);
                if (GameManager.Instance.TotalGold >= cost)
                {
                    GameManager.Instance.TotalGold -= cost;                    
                    GameManager.Instance.LevelUpClickPower();
                    UiManager.Instance.SetGoldText();
                    if (i == 0)
                        Start_LevelUPEffect();
                }
            }
            GameManager.Instance.Save(GameManager.saveType.TotalGold);
            GameManager.Instance.Save(GameManager.saveType.subQuestNow);
            GameManager.Instance.Save(GameManager.saveType.clicker);
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.gold);
        }
      
        SetClickText();
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
        ClickPowerText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(LevelUpAnimComplete);
    }
    void LevelUpAnimComplete()
    {
        bLevelupEffect = false;
    }
    public void SetUpgrade()
    {
        for(int i =0; i< UpgradeHero.Count; i++)
        {
            if(UpgradeHero[i].name != "-1")
            {
                UpgradeHero[i].GetComponent<UpgradeHeroItem>().SetCost();
            }
        }
    }
}
