using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class TransDungeonGameSrc : MonoBehaviour
{
    public TranscendenceOpenPanelSrc TranscendenceOpenPanel;
    public Text MainText;
    public GameObject i1;
    public GameObject i2;
    public GameObject RewardEffect;
    public Text RewardCountText;
    public Text TimerText;
    public GameObject RewardPanel;
    public GameObject HpObject;
    public Text HpText;
    public Image HpFillImage;

    public GameObject HeroPos;
    public GameObject Hero;
    public GameObject TimeObject;
    bool bEnableTrue;
    List<GameObject> TimeObjectList = new List<GameObject>();
    int timeObjectIndex;
    private void Start()
    {
        for(int i=0; i< 20; i++)
        {
            GameObject temp = Instantiate(TimeObject);
            temp.transform.SetParent(TimeObject.transform.parent);
            temp.transform.localScale= new Vector3(1, 1, 1);
            TimeObjectList.Add(temp);
        }
    }
    public void StartGame()
    {
        MainText.text = "- 시험의 방  <color=red>레벨 1</color> -";
        bEnableTrue = false;
        if (GameManager.Instance.HeroList[GameManager.Instance.heroTransPos].activeSelf ==true)
        {
            bEnableTrue = true;
            GameManager.Instance.HeroList[GameManager.Instance.heroTransPos].SetActive(false);
        }
        Hero = Instantiate(GameManager.Instance.HeroList[GameManager.Instance.heroTransPos]);
        Hero.SetActive(false);
        Hero.name = GameManager.Instance.heroTransPos.ToString();
        //Hero.transform.SetParent(HeroPos.transform);
        Hero.GetComponent<Hero>().heroType = global::Hero.HeroType.transDungeon;
        Hero.GetComponent<Hero>().oriPos = new Vector3(-50, -3, 0);
        Hero.GetComponent<Hero>().enabled = false;
        Hero.GetComponent<SensorToolkit.RangeSensor2D>().enabled = false;
        Hero.transform.SetParent(HeroPos.transform);        
        StartCoroutine(enableRoutien());        
    }
    bool bStartGame = false;
    IEnumerator enableRoutien()
    {
        yield return new WaitForSeconds(0.2f);
        Hero.transform.position = new Vector3(0, 0, 0);        
        if(bEnableTrue ==true)
        {
            GameManager.Instance.HeroList[GameManager.Instance.heroTransPos].SetActive(true);
            bEnableTrue = false;
        }
        yield return new WaitForSeconds(0.1f);
        Hero.GetComponent<Hero>().enabled = true;
        Hero.GetComponent<SensorToolkit.RangeSensor2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        Hero.SetActive(true);
        TimerText.gameObject.SetActive(true);
        time = 15;
        killcount = 0;
        timeObjectIndex = 0;
        bStartGame = true;
    }
    float time;
    int killcount;
    public void EndGame()
    {
        if (Hero != null)
            Destroy(Hero);
        TimerText.gameObject.SetActive(false);
        bStartGame = false;
        time = 0;
        RewardEffect.SetActive(false);
        SetEndTier();
    }
    private void Update()
    {
        if(bStartGame ==true)
        {
            if(time <0 && bStartGame ==true)
            {
                time = 0;
                bStartGame = false;
                RewardPanel.SetActive(true);
                TimerText.gameObject.SetActive(false);
                GameManager.Instance.transCost = GameManager.Instance.Rebirth(killcount * 10);
                RewardCountText.text = UiManager.Instance.SetCost(GameManager.Instance.Rebirth(killcount * 10));
                RewardEffect.SetActive(true);
                //GameOver
            }
            time -= Time.deltaTime;
            TimerText.text = time.ToString("N1");
        }
    }
    void SetEndTier()
    {
        TranscendenceOpenPanel.SetTierEndGame();
    }
    public void RewardComplete()
    {
        i1.SetActive(true);
        i1.transform.Find("Image").gameObject.SetActive(true);
        i2.SetActive(true);
        i2.transform.Find("Image").gameObject.SetActive(true);
        RewardPanel.SetActive(false);
        SetEndTier();
    }
    public void ShowAds()
    {
        GameManager.Instance.adsType = GameManager.AdsType.Transcendence;        
        AdManager.Instance.ShowRewardedAds();
    }
    public void KillMonster()
    {
        killcount++;
        GameManager.Instance.TransTierMonsterCount++;
        GameManager.Instance.Save(GameManager.saveType.TransTierMonsterCount);
        int killIndex = killcount + 1;
        MainText.text = "- 시험의 방  <color=red>레벨 "+ killIndex + "</color> -";
        if (timeObjectIndex < TimeObjectList.Count)
        {
            TimeObjectList[timeObjectIndex].SetActive(true);
            time += 0.5f;
        }
        timeObjectIndex++;
    }
    public void CheckHP(InfiniCoin hp,int level)
    {
        if (hp <= 0.99f)
        {
            //CheckLevel();            

        }
        if (hp < 1000)
        {
            string[] temp = hp.ToString().Split('.');
            HpText.text = temp[0].ToString();
        }
        else
        {
            HpText.text = hp.ToString();
        }
        CheckHpFill(hp, level);
    }

    public void CheckHpFill(InfiniCoin hp,int level)
    {
        //100 -10
        if (GameManager.Instance == null)
            return;

        InfiniCoin defalutHp = SystemManager.GetEnmeyHp(level);
        double deltaHp = 1;
        if (defalutHp.kPower > hp.kPower)
        {
            long count = defalutHp.kPower - hp.kPower;
            double fPower = Mathf.Pow(1000, count);
            deltaHp = ((hp.baseValue / fPower) / defalutHp.baseValue);
        }
        else
        {
            deltaHp = (hp.baseValue / defalutHp.baseValue);
        }

        if (deltaHp > 1)
        {
            deltaHp = deltaHp / 1000;
        }

        HpFillImage.fillAmount = float.Parse(deltaHp.ToString());
        //Debug.Log(deltaHp);
    }

}
