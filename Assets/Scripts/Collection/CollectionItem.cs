using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectionItem : MonoBehaviour
{
    public GameObject pPanel;
    public GameObject EnableObject;
    public Text TitleText;
    public Text EnableText;
    public List<Image> HeroList;
    public GameObject GetButton;
    public List<Text> GradeText;
    public List<Text> NameText;
    public void SetData()
    {
        GetButton.SetActive(false);
        string strPower = SystemManager.Collectionpower[int.Parse(this.name)].ToString();
        switch(SystemManager.Collectiontitle[int.Parse(this.name)])
        {
            case "dice":
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=#ECDE0A>레이드 횟수 증가 +" + strPower+"</color>)";
                break;
            case "atk":
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=red>모든 공격력 x" + strPower+" % 증가</color>)";
                break;
            case "gold":
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=yellow>골드 획득 x" + strPower + " % 증가</color>)";
                break;
            case "mat":
                
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=#97A895>재료 획득 x" + strPower + " % 증가</color>)";
                break;
            case "altar":                
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=blue>영혼의 조각 x" + strPower + " % 증가</color>)";
                break;
            case "soul":
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=yellow>번개 x" + strPower + " % 증가</color>)";
                break;
            case "trans":
                
                TitleText.text = SystemManager.CollectionName[int.Parse(this.name)] + "  (<color=#ECA40A>시험의 방 보스체력 감소 -" + strPower + " %</color>)";
                break;
            case "hero":
                
                TitleText.text = "<color=#E51717>랜덤 전설 영웅!</color>";
                if(GameManager.Instance.CollectionList[(int)GameManager.CollectionType.hero] ==1)
                {
                    GetButton.SetActive(true);
                }
                break;
        }        
        if(GameManager.Instance.CollectionList[int.Parse(this.name)]>=1)
        {
            EnableObject.SetActive(true);
            EnableText.text = "활성화";
        }
        else
        {
            EnableObject.SetActive(false);
            EnableText.text = "비활성화";
        }
        UiManager.Instance.SetHeroIcon_UI(HeroList[0], SystemManager.CollectionHero1[int.Parse(this.name)]);
        UiManager.Instance.SetHeroIcon_UI(HeroList[1], SystemManager.CollectionHero2[int.Parse(this.name)]);
        UiManager.Instance.SetHeroIcon_UI(HeroList[2], SystemManager.CollectionHero3[int.Parse(this.name)]);
        UiManager.Instance.SetHeroIcon_UI(HeroList[3], SystemManager.CollectionHero4[int.Parse(this.name)]);

        UiManager.Instance.SetTier_UIText(GradeText[0], SystemManager.CollectionHero1[int.Parse(this.name)]);
        UiManager.Instance.SetTier_UIText(GradeText[1], SystemManager.CollectionHero2[int.Parse(this.name)]);
        UiManager.Instance.SetTier_UIText(GradeText[2], SystemManager.CollectionHero3[int.Parse(this.name)]);
        UiManager.Instance.SetTier_UIText(GradeText[3], SystemManager.CollectionHero4[int.Parse(this.name)]);

        UiManager.Instance.SetName_UIText(NameText[0], SystemManager.CollectionHero1[int.Parse(this.name)]);
        UiManager.Instance.SetName_UIText(NameText[1], SystemManager.CollectionHero2[int.Parse(this.name)]);
        UiManager.Instance.SetName_UIText(NameText[2], SystemManager.CollectionHero3[int.Parse(this.name)]);
        UiManager.Instance.SetName_UIText(NameText[3], SystemManager.CollectionHero4[int.Parse(this.name)]);

        if(GameManager.Instance.CollectionList[int.Parse(this.name)] !=0)
        {
            HeroList[0].color = UiManager.Instance.EnableColor;
            HeroList[1].color = UiManager.Instance.EnableColor;
            HeroList[2].color = UiManager.Instance.EnableColor;
            HeroList[3].color = UiManager.Instance.EnableColor;
        }
        else
        {
            if (GameManager.Instance.herosInfos[SystemManager.CollectionHero1[int.Parse(this.name)]].isGetHero == false)
            {
                HeroList[0].color = UiManager.Instance.DisableColor;
            }
            else
            {
                HeroList[0].color = UiManager.Instance.EnableColor;
            }
            if (GameManager.Instance.herosInfos[SystemManager.CollectionHero2[int.Parse(this.name)]].isGetHero == false)
            {
                HeroList[1].color = UiManager.Instance.DisableColor;
            }
            else
            {
                HeroList[1].color = UiManager.Instance.EnableColor;
            }
            if (GameManager.Instance.herosInfos[SystemManager.CollectionHero3[int.Parse(this.name)]].isGetHero == false)
            {
                HeroList[2].color = UiManager.Instance.DisableColor;
            }
            else
            {
                HeroList[2].color = UiManager.Instance.EnableColor;
            }
            if (GameManager.Instance.herosInfos[SystemManager.CollectionHero4[int.Parse(this.name)]].isGetHero == false)
            {
                HeroList[3].color = UiManager.Instance.DisableColor;
            }
            else
            {
                HeroList[3].color = UiManager.Instance.EnableColor;
            }
        }
        
        //TitleText.text = SystemManager.CollectionName
        if (GameManager.Instance.CollectionList[int.Parse(this.name)]==0)
        {
            
        }
        else
        {

        }
    }
    public void GetNewHero()
    {
        GameManager.Instance.CollectionList[(int)GameManager.CollectionType.hero] = 2;
        GameManager.Instance.Save(GameManager.saveType.collection);
        GetButton.SetActive(false);

        UiManager.Instance.GatchaPanel.SetActive(true);
        UiManager.Instance.GatchaPanel.GetComponent<GatchaManager>().gatchaType = GatchaManager.GatchaType.legend;
        pPanel.SetActive(false);
    }
}
