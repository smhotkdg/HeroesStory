using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
public class AltarPanelSrc : MonoBehaviour
{
    public Text TotalAltarCount;
    public Button AllAltarButton;
    public GameObject BG;
    public ScrollRect tempRect;
    public GameObject EffectObject;
    public AnimController animController;
    public SelectHeroPanelSrc HeroListAltar;
    public Text AltarCoinCount;
    public Image FillLevelImage;
    public Text LevelPercentText;
    public Text Level;
    public Text AttacPowerText;
    public Text CriticalPowerText;
    public Button AltarButton;
    public Text AltarButtonText;

    public Button AltarHeroButton;
    public Text AltarHeroButtonText;
    int defalultLevelCost = 30;

    public Text GetAltarCoinCountText;
    public int GetAltarCoinCount=0;
    bool bStartAltar = false;
    public List<int> altarQueue = new List<int>();

    public GameObject EffectUpgrade;
    public GameObject AltarTutori;
    private void Start()
    {
        if(GameManager.Instance.TutorialList[9] ==1)
        {
            GameManager.Instance.TutorialList[9] = 2;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            AltarTutori.SetActive(true);
        }
        HeroListAltar.selectItemAltar += HeroListAltar_selectItemAltar;
        HeroListAltar.unselectItemAltar += HeroListAltar_unselectItemAltar;
        HeroListAltar.disablePanelevent += HeroListAltar_disablePanelevent;
        animController.EndAnimEventHandler += AnimController_EndAnimEventHandler;
        animController.MidlleAnimEventHandler += AnimController_MidlleAnimEventHandler;
        GetAltarCoinCount = 0;
    }
    private void OnDisable()
    {

        SaveAltar();
        EffectObject.SetActive(false);
        EffectUpgrade.SetActive(false);
        SoundManager.Instance.ChangeAltar(false);

    }
    
    private void AnimController_MidlleAnimEventHandler()
    {
        Start_LevelUPEffect();
    }

    private void AnimController_EndAnimEventHandler()
    {
        //Text Effect

        //캐릭터 삭제
        EffectObject.SetActive(false);        

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
    IEnumerator EffectAnim()
    {                
        BG.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.3f).SetEase(Ease.InOutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        AltarCoinCount.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(LevelUpAnimComplete);
    }
    void RemoveHero()
    {
        for (int i = 0; i < altarQueue.Count; i++)
        {
            GameManager.Instance.achivementData.AchivementCount[3]++;
            GameManager.Instance.Save(GameManager.saveType.achivementData);
            for (int k = 0; k < GameManager.Instance.heroPos.Length; k++)
            {
                if (altarQueue[i] == GameManager.Instance.heroPos[k])
                {
                    GameManager.Instance.UnsetHero(altarQueue[i],k);
                }                
            }
            for (int k = 0; k < GameManager.Instance.heroraidPos.Length; k++)
            {
                if (altarQueue[i] == GameManager.Instance.heroraidPos[k])
                {
                    GameManager.Instance.UnsetHeroRaid(k);
                }
            }
            for(int e =0; e < GameManager.Instance.heroExpeditionPos.Length;e++)
            {
                if(altarQueue[i] == GameManager.Instance.heroExpeditionPos[e])
                {
                    GameManager.Instance.UnsetExpedition(e);
                }
            }
            
            if(GameManager.Instance.herosInfos[altarQueue[i]].isAblity_1 ==true)
            {
                UnsetBuff(GameManager.Instance.herosInfos[altarQueue[i]].specialAblity1,altarQueue[i]);
            }
            if(GameManager.Instance.herosInfos[altarQueue[i]].isAblity_2 ==true)
            {
                UnsetBuff(GameManager.Instance.herosInfos[altarQueue[i]].specialAblity2, altarQueue[i]);
            }
            if(GameManager.Instance.herosInfos[altarQueue[i]].isAblity_3 ==true)
            {
                UnsetBuff(GameManager.Instance.herosInfos[altarQueue[i]].specialAblity3, altarQueue[i]);
            }
            GameManager.HerosInfo heros = GameManager.Instance.MakeHero(altarQueue[i]);
            heros.InitDPS = GameManager.Instance.herosInfos[altarQueue[i]].InitDPS;
            //heros.DPS = heros.InitDPS;
            GameManager.Instance.herosInfos[altarQueue[i]] = heros;

            GameManager.Instance.Save(GameManager.saveType.herosInfos, altarQueue[i]);
        }
    }
    void SaveAltar()
    {
        GameManager.Instance.Save(GameManager.saveType.TotalAltarCoin);
        GameManager.Instance.Save(GameManager.saveType.achivementData);
        GameManager.Instance.Save(GameManager.saveType.AltarLevel);
    }
    void UnsetBuff(string ablity,int heroPosition)
    {
       
        string[] splitStr = ablity.Split('_');        
        switch (splitStr[0])
        {
            case "dps":
                //ablityName = "공격력 + " + splitStr[1] + " % 증가";
                GameManager.Instance.DpsDowngrade(float.Parse(splitStr[1]) * 0.01f, heroPosition);                

                break;          
            case "alldps":
                //ablityName = "모든 영웅 공격력 + " + splitStr[1] + " % 증가";
                GameManager.Instance.AllDpsDpsDowngrade(float.Parse(splitStr[1]) * 0.01f);                
                break;
        }

    }
    public void CompleteAltar()
    {
        RemoveHero();
        EffectObject.SetActive(true);
        tempRect.enabled = false;
        GameManager.Instance.TotalAltarCoin += GetAltarCoinCount;        
        HeroListAltar.gameObject.SetActive(false);
        AltarCoinCount.text = UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin);
        TotalAltarCount.text = UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin);
    }

    private void HeroListAltar_disablePanelevent()
    {
        GetAltarCoinCount = 0;
        bStartAltar = false;
        altarQueue.Clear();
        SetAltarText();
        CheckAltarButton();
    }

    void SetAltarText()
    {
        GetAltarCoinCountText.text = GetAltarCoinCount + " 획득 가능";
    }
    private void HeroListAltar_unselectItemAltar(int index)
    {
        if(bStartAltar ==true)
        {
            GetAltarCoinCount -= GameManager.Instance.GetAlterCount(index);
            SetAltarText();
            altarQueue.Remove(index);
            CheckAltarButton();
        }
        
    }

    private void HeroListAltar_selectItemAltar(int index)
    {
        bStartAltar = true;
        GetAltarCoinCount += GameManager.Instance.GetAlterCount(index);
        SetAltarText();
        altarQueue.Add(index);
        CheckAltarButton();
    }
    void CheckAltarButton()
    {
        if (GetAltarCoinCount > 0)
        {
            AltarHeroButton.image.color = UiManager.Instance.enableButtonColor;
            AltarHeroButtonText.color = UiManager.Instance.enableButtonColor;

            TotalAltarCount.color = UiManager.Instance.enableButtonColor;
            AllAltarButton.image.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            AltarHeroButton.image.color = UiManager.Instance.disableButtonColor;
            AltarHeroButtonText.color = UiManager.Instance.disableButtonColor;

            TotalAltarCount.color = UiManager.Instance.disableButtonColor;
            AllAltarButton.image.color = UiManager.Instance.disableButtonColor;
        }
    }
    private void OnEnable()
    {
        SetData();
        SetAltarText();
        CheckAltarButton();
        SoundManager.Instance.ChangeAltar(true);
    }
    void SetData()
    {
        AltarCoinCount.text = UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin);
        TotalAltarCount.text = UiManager.Instance.SetCost(GameManager.Instance.TotalAltarCoin);
        FillLevelImage.fillAmount = (GameManager.Instance.AltarPercent / defalultLevelCost);
        float percent = (GameManager.Instance.AltarPercent / defalultLevelCost) * 100;
        LevelPercentText.text = percent.ToString("N1") + " %";
        Level.text = "Lv. "+GameManager.Instance.AltarLevel.ToString("N0");
        int powerDps = GameManager.Instance.AltarLevel * 100;
        AttacPowerText.text = "모든 영웅\n기본 공격력\n+ " + powerDps.ToString("N0") +" %";
        CriticalPowerText.text = "모든 영웅\n치명타 공격력\n+ " + powerDps.ToString("N0") + " %";
        
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance.TotalAltarCoin >=1)
        {
            AltarButton.image.color = UiManager.Instance.enableButtonColor;
            AltarButtonText.color = UiManager.Instance.enableButtonColor;

            TotalAltarCount.color = UiManager.Instance.enableButtonColor;
            AllAltarButton.image.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            AltarButton.image.color = UiManager.Instance.disableButtonColor;
            AltarButtonText.color = UiManager.Instance.disableButtonColor;

            TotalAltarCount.color = UiManager.Instance.disableButtonColor;
            AllAltarButton.image.color = UiManager.Instance.disableButtonColor;
        }
    }
    public void AllAltar()
    {
        if(GameManager.Instance.TotalAltarCoin> 0)
        {
            InfiniCoin count = GameManager.Instance.TotalAltarCoin;
            for (int i = 0; i < count; i++)
            {
                Altar();
            }
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.altar);
        }
        
    }
    
    public void Altar()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.TotalAltarCoin >= 1)
        {
            //여기서 레벨업
            GameManager.Instance.TotalAltarCoin -= 1;
            
            GameManager.Instance.AltarPercent++;            
            if(GameManager.Instance.AltarPercent >= defalultLevelCost)
            {
                GameManager.Instance.AltarPercent = 0;
                GameManager.Instance.AltarLevel++;
                
                GameManager.Instance.achivementData.AchivementCount[10]++;
                
                //레벨업 이펙트
                GameManager.Instance.AltarDpsUpgrade();
                StarEffect();

                if (GameManager.Instance.subquestType == GameManager.SubQuestType.altarUpgrade)
                {
                    GameManager.Instance.subQuestNow++;
                    GameManager.Instance.Save(GameManager.saveType.subQuestNow);
                    UiManager.Instance.SetSubQuestText();
                }
            }
            SoundManager.Instance.PlayFX(SoundManager.SoundFXType.AltarEffect);
            SetData();
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.altar);
        }
    }
    bool bEffect = false;
    public void StarEffect()
    {
        if (bEffect == false)
        {
            bEffect = true;
            StartCoroutine(EffectUpAnim());
        }
    }
    void AnimComplete()
    {
        bEffect = false;      

    }
    IEnumerator EffectUpAnim()
    {
        EffectUpgrade.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Level.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.3f).SetEase(Ease.InOutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        AttacPowerText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        CriticalPowerText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(AnimComplete);
        yield return new WaitForSeconds(0.5f);
        EffectUpgrade.SetActive(false);
    }
}
