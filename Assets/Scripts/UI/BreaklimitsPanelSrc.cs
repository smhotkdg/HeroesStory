using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BreaklimitsPanelSrc : MonoBehaviour
{
    
    public Image Icon;
    public GameObject EffectObject;
    public GameObject NowImageObject;
    public Text NowLimitText;
    public Text NowMaxLevelText;

    public GameObject NextImageObject;
    public Text NextLimitText;
    public Text NextMaxLevelText;

    public Text NeedItemCountText;
    public Image ButtonImage;
    int m_heroIndex =-1;
    
    public void SetData(int heroIndex)
    {
        m_heroIndex = heroIndex;
        SetText();
    }
    private void OnEnable()
    {
        EffectObject.SetActive(false);
        bStartEffect = false;
    }
    private void OnDisable()
    {
        m_heroIndex = -1;
    }
    void SetText()
    {
        if (m_heroIndex == -1)
            return;
        NowLimitText.text = "한계 돌파 lv." + GameManager.Instance.herosInfos[m_heroIndex].LimitLevelUpCount;
        NowMaxLevelText.text = "Max Level " + GameManager.Instance.GetLimitMaxLevel(m_heroIndex);
        int nextLevel = GameManager.Instance.herosInfos[m_heroIndex].LimitLevelUpCount + 1;
        NextLimitText.text = "한계 돌파 lv." + nextLevel;
        NextMaxLevelText.text = "Max Level " + GameManager.Instance.GetLimitMaxLevelNext(m_heroIndex);

        NeedItemCountText.text = GameManager.Instance.GetLimitCost(m_heroIndex).ToString();
        UiManager.Instance.SetHeroIcon_UI(Icon, m_heroIndex);

        if (GameManager.Instance.materialCount >= GameManager.Instance.GetLimitCost(m_heroIndex))
        {
            ButtonImage.color = UiManager.Instance.enableButtonColor;
            NeedItemCountText.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            ButtonImage.color = UiManager.Instance.disableButtonColor;
            NeedItemCountText.color = UiManager.Instance.disableButtonColor;
        }
        UiManager.Instance.SetHeroInfoPanel(m_heroIndex);
    }
    bool bStartEffect;
    public void CompleteFirstEffect()
    {
        NextImageObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).From(true).SetEase(Ease.InOutBounce).OnComplete(EndEffect);
        SetText();
        
    }
    void EndEffect()
    {
        bStartEffect = false;
    }
    public void SetEffect()
    {
        bStartEffect = true;
        NowImageObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).From(true).SetEase(Ease.InOutBounce).OnComplete(CompleteFirstEffect);        
        EffectObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        if (m_heroIndex == -1)
            return;
        if (GameManager.Instance.materialCount >= GameManager.Instance.GetLimitCost(m_heroIndex))
        {
            ButtonImage.color = UiManager.Instance.enableButtonColor;
            NeedItemCountText.color = UiManager.Instance.enableButtonColor;
        }
        else
        {
            ButtonImage.color = UiManager.Instance.disableButtonColor;
            NeedItemCountText.color = UiManager.Instance.disableButtonColor;
        }
    }
    public void BreakLimte()
    {
        if (bStartEffect == true)
            return;
        if (m_heroIndex == -1)
            return;
        if(GameManager.Instance.materialCount >= GameManager.Instance.GetLimitCost(m_heroIndex))
        {
            GameManager.Instance.materialCount -= GameManager.Instance.GetLimitCost(m_heroIndex);
            GameManager.Instance.Save(GameManager.saveType.materialCount);
            GameManager.HerosInfo heros = new GameManager.HerosInfo();
            heros = GameManager.Instance.herosInfos[m_heroIndex];
            heros.LimitLevelUpCount++;
            GameManager.Instance.herosInfos[m_heroIndex] = heros;
            SetEffect();
            GameManager.Instance.Save(GameManager.saveType.herosInfos, m_heroIndex);
        }
        else
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.craft);
        }
    }
}
