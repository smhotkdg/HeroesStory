using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
using DG.Tweening;
public class TranscendenceItem : MonoBehaviour
{

    public Text TItleText;
    public Text LevelText;
    public Text UpgradeValueText;
    public Text CostText;
    public Button CostButton;
    public Image Icon;

    int artifactIndex;
    public void SetData()
    {
        artifactIndex = int.Parse(this.name);
        string strHeroIcon = "Icon/artifact" + artifactIndex;

        Sprite temp = Resources.Load<Sprite>(strHeroIcon);
        if (temp != null)
        {
            Icon.sprite = temp;
        }
        TItleText.text = SystemManager.soulAblityTitle[artifactIndex];

        SetCostData();


    }
    private void FixedUpdate()
    {
        setColor();
    }
    void setColor()
    {
        if (GameManager.Instance.TotalSoul >= GetCost())
        {
            CostText.color = UiManager.Instance.enableButtonColor;
            CostButton.image.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            CostText.color = UiManager.Instance.disableButtonColor;
            CostButton.image.color = UiManager.Instance.disableButtonColor;
        }
    }
    void SetCostData()
    {

        CostText.text = UiManager.Instance.SetCost(GetCost());
        if (artifactIndex == (int)GameManager.AtrifactType.BossHp || artifactIndex == (int)GameManager.AtrifactType.costDiscount)
        {
            UpgradeValueText.text = UiManager.Instance.SetCost(GetValue(), true) + " %";
        }
        else if(artifactIndex == (int)GameManager.AtrifactType.BossTime)
        {
            UpgradeValueText.text = UiManager.Instance.SetCost(GetValue(),true) + " 초";
        }
        else if(artifactIndex == (int)GameManager.AtrifactType.GetGoldx10)
        {
            UpgradeValueText.text = UiManager.Instance.SetCost(GetValue(), true) + " %";
        }
        else
        {
            UpgradeValueText.text = UiManager.Instance.SetCost(GetValue()) + " %";
        }
        LevelText.text = "Lv ." + GameManager.Instance.SoulAblityList[artifactIndex];
    }
    public void LevelUP()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (GameManager.Instance.TotalSoul >= GetCost())
        {
            GameManager.Instance.TotalSoul -= GetCost();
            GameManager.Instance.Save(GameManager.saveType.TotalSoul);
            Start_LevelUPEffect();
            GameManager.Instance.SoulAblityList[artifactIndex]++;
            GameManager.Instance.Save(GameManager.saveType.SoulAblityList);
            UpgradeSoulArtifact();
            //여기서 레벨업
            UiManager.Instance.SetSoulText();
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.soul);
            return;
        }
        SetCostData();
    }
    private void OnEnable()
    {
        GameManager.Instance.TrasnUpgradeAttack = false; 
    }
   
    void UpgradeSoulArtifact()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        switch (artifactIndex)
        {
            case 0:
                //데미지 완료
                //GameManager.Instance.AllDpsUpgrade(0.02f);
                GameManager.Instance.AlldpsUpgradeTranscedence(artifactIndex);
                GameManager.Instance.TrasnUpgradeAttack = true;
                break;
            case 1:
                //치명타 //완료
                break;
            case 2:
                //치명타 확률 //완료
                break;
            case 3:
                //레벨업 비용감소
                break;
            case 4:
                //클릭 //완료
                GameManager.Instance.UpgradeClickPower();
                
                break;
            case 5:
                //보물 고블린
                
                break;
            case 6:
                //몬스터가 10배                
                break;
            case 7:
                //처지시 골드
                GameManager.Instance.buffData.GoldBuff += 0.05f;                
                break;
            case 8:
                //보스 체력감소                
                break;
            case 9:
                //보스 시간 증가                
                break;
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
    IEnumerator EffectAnim()
    {
        LevelText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        UpgradeValueText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false);
        yield return new WaitForSeconds(0.1f);
        CostText.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.3f).SetEase(Ease.OutBounce).From(false).OnComplete(LevelUpAnimComplete);
    }
    double GetValue()
    {
        double value = 1;
        
        switch (artifactIndex)
        {
            case (int)GameManager.AtrifactType.alldps:       
                //데미지
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.alldps] >0)
                {
                    value = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.alldps] * 2;
                }
                else
                {
                    value = 0;
                }
                
                return value;
            case (int)GameManager.AtrifactType.criticalDps:
                //치명타
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalDps] >0)
                {
                    value = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalDps] * 15;
                }
                else
                {
                    value = 0;
                }
                
                return value;
            case (int)GameManager.AtrifactType.criticalChance:
                //치명타 확률
                if (GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalChance] > 0)
                {
                    value = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalChance];
                }
                else
                {
                    value = 0;
                }
                break;
            case (int)GameManager.AtrifactType.costDiscount:
                //레벨업 비용감소
                if (GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.costDiscount] > 0)
                {
                    value = -99.999 * (1 - System.Math.Pow(2.178, -0.01 * GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.costDiscount]));
                }
                else
                {
                    value = 0;
                }
                break;
            case (int)GameManager.AtrifactType.ClickPower:
                //클릭
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.ClickPower] >0)
                {
                    value = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.ClickPower] * 20;
                }
                else
                {
                    value = 0;
                }
                
                return value;
            case (int)GameManager.AtrifactType.GoldMonster:
                //보물고블린
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GoldMonster] >0)
                {           
                    value = 9900 * (1 - System.Math.Pow(2.178, -0.002 * GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GoldMonster])) + 1;
                    value = 1 + (1 * (value * 0.01));
                }
                else
                {
                    value = 0;
                }

                
                return value;
            case (int)GameManager.AtrifactType.GetGoldx10:
                //몬스터가 10배
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GetGoldx10] >0)
                {
                    value = 100 * (1 - System.Math.Pow(2.178, -0.0025 * GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GetGoldx10]));
                }
                else
                {
                    value= 0;
                }
                
                return value;
            case (int)GameManager.AtrifactType.KillGetGold:
                //처지시 골드증가
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.KillGetGold] >0)
                {
                    value = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.KillGetGold] * 5;
                }
                else
                {
                    value = 0;
                }   
                return value;
            case (int)GameManager.AtrifactType.BossHp:
                //보스 체력삼소
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossHp] >0)
                {
                    value = -50 * (1 - System.Math.Pow(2.178, -0.002 * GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossHp]));
                }
                else
                {
                    value = 0;
                }
                return value;
            case (int)GameManager.AtrifactType.BossTime:
                //보스 시간증가
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossTime] >0)
                {

                    value = 30 * (1 - System.Math.Pow(2.178, -0.034 * GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossTime]));
                }
                else
                {
                    value = 0;
                }
                
                return value;
        }
        return value;
    }
    InfiniCoin GetCost()
    {
        InfiniCoin cost = 1;
        switch (artifactIndex)
        {
            case (int)GameManager.AtrifactType.alldps:
                //데미지증가
                cost = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.alldps] +1;
                return cost;
            case (int)GameManager.AtrifactType.criticalDps:
                //치명타 증가
                
                cost = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalDps] +1;
                return cost;
            case (int)GameManager.AtrifactType.criticalChance:
                //치명타 확률

                if (GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalChance] > 0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.criticalChance]);
                }
                else
                {
                    cost = 1;
                }
                return cost;
            case (int)GameManager.AtrifactType.costDiscount:
                //레벨업 비용감소

                if (GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.costDiscount] > 0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.costDiscount]);
                }
                else
                {
                    cost = 1;
                }
                return cost;
            case (int)GameManager.AtrifactType.ClickPower:
                //클릭당 데미지                
                cost = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.ClickPower] +1;                            
                return cost;
            case (int)GameManager.AtrifactType.GoldMonster:
                //보물고블린 확률
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GoldMonster] >0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GoldMonster]);
                }                    
                else
                {
                    cost = 1;
                }
                return cost;
            case (int)GameManager.AtrifactType.GetGoldx10:
                //10배 줄확률
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GetGoldx10] >0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.GetGoldx10]);
                }
                else
                {
                    cost = 1;
                }
                
                return cost;                
            case (int)GameManager.AtrifactType.KillGetGold:
                //처지시 골드
                cost = GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.KillGetGold] +1;
                return cost;
            case (int)GameManager.AtrifactType.BossHp:
                //보스 체력감소
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossHp] >0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossHp]);
                }
                else
                {
                    cost = 1;
                }
                
                return cost;
            case (int)GameManager.AtrifactType.BossTime:
                //보스 전투시간
                if(GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossTime] >0)
                {
                    cost = SystemManager.mathPow(2, GameManager.Instance.SoulAblityList[(int)GameManager.AtrifactType.BossTime]);
                }
                else
                {
                    cost = 1;
                }
                
                return cost;
        }
        return cost;
    }
}
