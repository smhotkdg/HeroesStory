using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectHeroItem : MonoBehaviour
{
    public enum selectHeroType
    {
        select,
        List,
        Gatcha,
        altar,
        expedition,
        raid,
        trans
    };
    public Text StatusText;
    public GameObject NotifiCation;
    public GameObject AltarSelect;
    public Text NameText;
    public Text TierText;
    public Text LvText;
    public Text DpsText;
    public GameObject SelectImage;
    public Image HeroIcon;

    public Text HeroCardCountText;
    public Image FillImage;

    public delegate void ExpeditionSelect(GameObject gameObject);
    public event ExpeditionSelect ExpeditionSelecEventt;

    public delegate void SelectItem(GameObject gameObject);
    public event SelectItem selectItem;

    public delegate void SelectItemAltar(int index);
    public event SelectItemAltar selectItemAltar;

    public delegate void UnSelectItemAltar(int index);
    public event UnSelectItemAltar UnselectItemAltar;


    public delegate void selectRaid(int index);
    public event selectRaid selectRaidEvent;


    public delegate void selectTrans(int index);
    public event selectTrans selectTransEvent;

    public selectHeroType heroType;
    UiManager uiManager;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = UiManager.Instance;
        gameManager = GameManager.Instance;
        uiManager.SetHeroIcon_UI(HeroIcon, int.Parse(this.name));
        if(heroType == selectHeroType.List)
        {
            uiManager.CheckHeroIconColor(HeroIcon, int.Parse(this.name));
        }
        else if (heroType == selectHeroType.Gatcha)
        {
            if (NameText != null)
                NameText.text = gameManager.herosInfos[int.Parse(this.name)].Name;
            uiManager.CheckHeroIconColor(HeroIcon, int.Parse(this.name), true);
        }

        if(heroType == selectHeroType.List || heroType == selectHeroType.select || heroType == selectHeroType.altar || heroType == selectHeroType.expedition || heroType== selectHeroType.raid || heroType == selectHeroType.trans)
        {
            uiManager.SetName_UIText(TierText, int.Parse(this.name));
        }
        else
        {
            uiManager.SetTier_UIText(TierText, int.Parse(this.name));
        }
        
        //uiManager.SetHeroCount_fillUI(HeroCardCountText, FillImage, int.Parse(this.name));
    }
    public void SetData()
    {
        if (heroType == selectHeroType.List)
        {
            UiManager.Instance.CheckHeroIconColor(HeroIcon, int.Parse(this.name));
            UiManager.Instance.SetHeroLevel_UI(LvText, int.Parse(this.name));
            UiManager.Instance.SetHeroDPS_UI(DpsText, int.Parse(this.name));
            UiManager.Instance.SetHeroCount_fillUI(HeroCardCountText, FillImage, int.Parse(this.name));
        }
    }
    private void OnEnable()
    {
        uiManager = UiManager.Instance;
        int index = -1;
        if(int.TryParse(this.name,out index))
        {
            uiManager.SetHeroIcon_UI(HeroIcon, int.Parse(this.name));
            if(heroType == selectHeroType.List)
            {
                if(GameManager.Instance.herosInfos[int.Parse(this.name)].HeroCount >=2)
                {
                    NotifiCation.SetActive(true);
                    GameManager.Instance.isNewHero = true;                    
                }
                else
                {
                    NotifiCation.SetActive(false);                    
                }
            }
            if(heroType == selectHeroType.List)
            {
                if (StatusText == null)
                    return;
                UiManager.Instance.CheckHeroStatus(StatusText, int.Parse(this.name));
            }
            if (heroType == selectHeroType.List || heroType == selectHeroType.raid || heroType == selectHeroType.trans)
            {
                UiManager.Instance.CheckHeroIconColor(HeroIcon, int.Parse(this.name));
                UiManager.Instance.SetHeroLevel_UI(LvText, int.Parse(this.name));
                UiManager.Instance.SetHeroDPS_UI(DpsText, int.Parse(this.name));
                UiManager.Instance.SetHeroCount_fillUI(HeroCardCountText, FillImage, int.Parse(this.name));
            }
            else if(heroType == selectHeroType.select)
            {
                UiManager.Instance.CheckHeroIconColor(HeroIcon, int.Parse(this.name), true);
                UiManager.Instance.SetHeroLevel_UI(LvText, int.Parse(this.name));
                UiManager.Instance.SetHeroDPS_UI(DpsText, int.Parse(this.name));
            }
            if(heroType ==selectHeroType.altar)
            {
                UiManager.Instance.CheckHeroIconColor(HeroIcon, int.Parse(this.name));
                UiManager.Instance.SetHeroLevel_UI(LvText, int.Parse(this.name));
                UiManager.Instance.SetHeroDPS_UI(DpsText, int.Parse(this.name));
                UiManager.Instance.SetHeroCount_fillUI(HeroCardCountText, FillImage, int.Parse(this.name));
                UiManager.Instance.CheckHeroStatus(StatusText, int.Parse(this.name));
                UnSetAltar();
            }
            if(heroType == selectHeroType.expedition)
            {
                UiManager.Instance.CheckHeroIconColor(HeroIcon, int.Parse(this.name));
                UiManager.Instance.SetHeroLevel_UI(LvText, int.Parse(this.name));
                UiManager.Instance.SetHeroDPS_UI(DpsText, int.Parse(this.name));
                UiManager.Instance.SetHeroCount_fillUI(HeroCardCountText, FillImage, int.Parse(this.name));
            }
            //else if(heroType == selectHeroType.Gatcha)
            //{
            //    if(NameText !=null)
            //        NameText.text = gameManager.HeroList[index].name;
            //    uiManager.CheckHeroIconColor(HeroIcon, int.Parse(this.name), true);
            //}
        }
        //GameManager.Instance.Save(GameManager.saveType.isNewHero);
    }
    public void Select()
    {
        //SelectImage.SetActive(true);
        gameManager.SetHeroPos(int.Parse(this.name));
        selectItem?.Invoke(this.gameObject);

                
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);

    }
    // Update is called once per frame
    void UnSetAltar()
    {
        bSelectAlter = false;
        AltarSelect.SetActive(false);
        UnselectItemAltar?.Invoke(int.Parse(this.name));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    bool bSelectAlter = false;
    void SetAltar()
    {
        AltarSelect.SetActive(true);
        bSelectAlter = true;
        selectItemAltar?.Invoke(int.Parse(this.name));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    public void SelectTrans()
    {
        selectTransEvent?.Invoke(int.Parse(this.name));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    public void SelectAltar()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (heroType == selectHeroType.altar)
        {
            if(bSelectAlter ==false)
            {
                if(GameManager.Instance.herosInfos[int.Parse(this.name)].isGetHero ==true)
                {
                    SetAltar();
                }
                else
                {
                    UiManager.Instance.SetNotification("보유하지 않은 영웅은 선택할 수 없습니다.");
                }
                
            }
            else
            {
                UnSetAltar();
            }
        }
    }
    public void SelectRaid()
    {
        selectRaidEvent?.Invoke(int.Parse(this.name));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    public void SelectExpedition()
    {
        //gameManager.SetHeroPos(int.Parse(this.name));
        SelectImage.SetActive(true);
        ExpeditionSelecEventt?.Invoke(this.gameObject);

        if (GameManager.Instance.subquestType ==GameManager.SubQuestType.expedtionGo)
        {
            GameManager.Instance.subQuestNow++;
            GameManager.Instance.Save(GameManager.saveType.subQuestNow);
            UiManager.Instance.SetSubQuestText();
        }
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (heroType == selectHeroType.List)
        {
            if (GameManager.Instance.herosInfos[int.Parse(this.name)].HeroCount >= 2)
            {
                NotifiCation.SetActive(true);
                gameManager.isNewHero = true;
                //GameManager.Instance.Save(GameManager.saveType.isNewHero);
            }
            else
            {
                NotifiCation.SetActive(false);                
            }
        }
    }
    public void ShowHeroInfo()
    {
        UiManager.Instance.ShowHeroInfoPanel(int.Parse(this.name));
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
}
