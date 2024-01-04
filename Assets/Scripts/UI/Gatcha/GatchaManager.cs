using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GatchaManager : MonoBehaviour
{
    public enum GatchaType { Normal,Speacial,tutorial,Hero,legend };
    public GameObject DefaultHero;
    public GameObject ExitButton;
    public GameObject ReStartButton;
    public GameObject GatchaBox;
    public GameObject newNotification;
    public Text COstText;
    public Text aweakingText;
    public GameObject NotifiCation;
    public GatchaType gatchaType = GatchaType.Normal;

    public List<GameObject> EffectList;
 
    public void ResetGatcha()
    {
        newNotification.SetActive(false);
        aweakingText.gameObject.SetActive(false);
        if (gatchaType == GatchaType.Normal)
        {            
            if(GameManager.Instance.TotalGem >= GameManager.Instance.NormalBoxCost)
            {
                GameManager.Instance.TotalGem -= GameManager.Instance.NormalBoxCost;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                UiManager.Instance.SetGemText();
                
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
                return;
            }
        }
        else
        {
            if (GameManager.Instance.TotalGem >= GameManager.Instance.SpeacialBoxCost)
            {
                GameManager.Instance.TotalGem -= GameManager.Instance.SpeacialBoxCost;
                GameManager.Instance.Save(GameManager.saveType.TotalGem);
                UiManager.Instance.SetGemText();
                
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
                return;
            }
        }
        GatchaBox.SetActive(true);
        ExitButton.SetActive(false);
        ReStartButton.SetActive(false);
        if (temp != null)
        {
            Destroy(temp);
        }
    }
    private void OnEnable()
    {
        UiManager.Instance.bOpenPanel = true;
    }
    private void OnDisable()
    {
        UiManager.Instance.bOpenPanel = false;
        GatchaBox.SetActive(false);
        ExitButton.SetActive(false);
        ReStartButton.SetActive(false);
        newNotification.SetActive(false);
        aweakingText.gameObject.SetActive(false);
        for (int i = 0; i < EffectList.Count; i++)
        {
            if (EffectList[i].activeSelf == true)
            {
                EffectList[i].SetActive(false);
            }
        }
        //GameManager.Instance.AllSave(GameManager.saveType.herosInfos);
        GameManager.Instance.CheckCollection();
        //GameManager.Instance.CheckTutorial();
        if (temp!=null)
        {
            Destroy(temp);
        }

    }
    public void StartGatcha()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.subquestType == GameManager.SubQuestType.gatchaHero)
        {
            GameManager.Instance.subQuestNow++;
            GameManager.Instance.Save(GameManager.saveType.subQuestNow);
            UiManager.Instance.SetSubQuestText();
        }     
        if (gatchaType == GatchaType.tutorial)
        {
            GatchaBox.SetActive(false);
            EffectList[0].SetActive(true);
            SetHeroTutorial();
            return;
        }
        else
        {
            while (true)
            {
                GatchaBox.SetActive(false);
                for (int i = 0; i < EffectList.Count; i++)
                {
                    if (EffectList[i].activeSelf == true)
                    {
                        EffectList[i].SetActive(false);
                    }
                }
                GameManager.HeroCard heroCard = GameManager.Instance.GetRandomHero();
                EffectList[(int)heroCard].SetActive(true);

                if (gatchaType == GatchaType.Normal)
                {
                    if (heroCard == GameManager.HeroCard.Normal || heroCard == GameManager.HeroCard.Advanced)
                    {
                        SetHero(heroCard);
                        break;
                    }
                }
                else if (gatchaType == GatchaType.Speacial)
                {
                    if (heroCard == GameManager.HeroCard.Advanced || heroCard == GameManager.HeroCard.Rare
                        || heroCard == GameManager.HeroCard.Hero || heroCard == GameManager.HeroCard.Legend)
                    {
                        SetHero(heroCard);
                        break;
                    }
                }
                else if(gatchaType == GatchaType.Hero)
                {
                    if ( heroCard == GameManager.HeroCard.Hero)
                    {
                        SetHero(heroCard);
                        break;
                    }
                }
                else if (gatchaType == GatchaType.legend)
                {
                    if (heroCard == GameManager.HeroCard.Legend)
                    {
                        SetHero(heroCard);
                        break;
                    }
                }
            }
        }
       
        GameManager.Instance.achivementData.AchivementCount[1]++;
        GameManager.Instance.Save(GameManager.saveType.achivementData);

        if (GameManager.Instance.subQuestLevel == 0 && GameManager.Instance.subquestType == GameManager.SubQuestType.heroGet)
        {
            GameManager.Instance.CheckTutorial();
        }
        GameManager.Instance.GatchaCount++;
        GameManager.Instance.Save(GameManager.saveType.GatchaCount);
        //GameManager.Instance.Save(GameManager.saveType.herosInfos);
    }
    
    void CheckColor()
    {      
        if (gatchaType == GatchaType.Normal)
        {
            if (GameManager.Instance.TotalGem >= GameManager.Instance.NormalBoxCost)
            {
                COstText.color = UiManager.Instance.enableButtonColor;
                ReStartButton.GetComponent<Button>().image.color = UiManager.Instance.enableButtonColor;
            }
            else
            {
                COstText.color = UiManager.Instance.disableButtonColor;
                ReStartButton.GetComponent<Button>().image.color = UiManager.Instance.disableButtonColor;

            }
        }
        else
        {
            if (GameManager.Instance.TotalGem >= GameManager.Instance.SpeacialBoxCost)
            {
                COstText.color = UiManager.Instance.enableButtonColor;
                ReStartButton.GetComponent<Button>().image.color = UiManager.Instance.enableButtonColor;
            }
            else
            {
                COstText.color = UiManager.Instance.disableButtonColor;
                ReStartButton.GetComponent<Button>().image.color = UiManager.Instance.disableButtonColor;
            }
        }
    }
    void SetHeroTutorial()
    {
        List<int> tempList = new List<int>();
        tempList.Add(0);
        MakeHeroCard(tempList);
    }
    void SetHero(GameManager.HeroCard heroCard)
    {
        switch (heroCard)
        {
            case GameManager.HeroCard.Normal:
                MakeHeroCard(GameManager.Instance.Lv1HeroIndex);
                break;
            case GameManager.HeroCard.Advanced:
                MakeHeroCard(GameManager.Instance.Lv2HeroIndex);
                break;
            case GameManager.HeroCard.Rare:
                MakeHeroCard(GameManager.Instance.Lv3HeroIndex);
                break;
            case GameManager.HeroCard.Hero:
                MakeHeroCard(GameManager.Instance.Lv4HeroIndex);
                break;
            case GameManager.HeroCard.Legend:
                MakeHeroCard(GameManager.Instance.Lv5HeroIndex);
                break;
        }
    }
    GameObject temp;
    void MakeHeroCard(List<int> heroList)
    {       
        int rand = Random.Range(0, heroList.Count);

        temp = Instantiate(DefaultHero);
        temp.transform.SetParent(DefaultHero.transform.parent);
        temp.transform.localPosition = DefaultHero.transform.localPosition;
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.name = heroList[rand].ToString();

        GameManager.HerosInfo herosInfo = GameManager.Instance.herosInfos[heroList[rand]];


        if (GameManager.Instance.GatchaCount < 6)
        {
            if(herosInfo.isGetHero ==true)
            {
                while(true)
                {
                    rand = Random.Range(0, heroList.Count);

                    temp = Instantiate(DefaultHero);
                    temp.transform.SetParent(DefaultHero.transform.parent);
                    temp.transform.localPosition = DefaultHero.transform.localPosition;
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    temp.name = heroList[rand].ToString();

                    herosInfo = GameManager.Instance.herosInfos[heroList[rand]];
                    if(herosInfo.isGetHero ==false)
                    {
                        break;
                    }
                }
            }
        }

        if (herosInfo.isGetHero ==false)
        {
            GameManager.Instance.isNewHero = true;
            GameManager.Instance.Save(GameManager.saveType.isNewHero);
            newNotification.SetActive(true);
            newNotification.transform.SetAsLastSibling();
        }
     
        herosInfo.isGetHero = true;
        herosInfo.HeroCount++;
        GameManager.Instance.herosInfos[heroList[rand]] = herosInfo;
        GameManager.Instance.Save(GameManager.saveType.herosInfos, heroList[rand]);
        temp.SetActive(true);
        if (herosInfo.HeroCount >= 2)
        {
            aweakingText.gameObject.SetActive(true);
        }

        if (GameManager.Instance.herosInfos[heroList[rand]].HeroCount >= 2)
        {
            NotifiCation.SetActive(true);
            GameManager.Instance.isNewHero = true;
            GameManager.Instance.Save(GameManager.saveType.isNewHero);
        }

        StartCoroutine(EndRoutine());
    }
    IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(0.4f);
        ExitButton.SetActive(true);
        ReStartButton.SetActive(true);
        if(gatchaType == GatchaType.Normal || gatchaType == GatchaType.tutorial)
        {
            COstText.text = GameManager.Instance.NormalBoxCost.ToString();
        }
        else
        {
            COstText.text = GameManager.Instance.SpeacialBoxCost.ToString();
        }
        CheckColor();
    }
}
