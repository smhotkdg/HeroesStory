using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TierLevelupPanelSrc : MonoBehaviour
{
    public delegate void OnDisablePanel();
    public event OnDisablePanel OnDIsablePanelEventHandler;

    public GameObject Effects_1;
    public GameObject Effects_2;

    public Text TierText;
    public Text TierMonsterCount;
    public Image FillMonsterCount;
    public Text TierXText;
    public GameObject XTier;
    public GameObject Fill;

    private void OnDisable()
    {
        Effects_1.SetActive(false);
        Effects_2.SetActive(false);
        OnDIsablePanelEventHandler?.Invoke();
    }
    public void StartRoutine()
    {
        StartCoroutine(TierEffectRoutine());
    }
    IEnumerator TierEffectRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        int tier = GameManager.Instance.TransTier + 1;
        int montserCount = tier * 10;

        yield return new WaitForSeconds(0.1f);
        Effects_1.SetActive(true);
        TierXText.text = "x " + tier;
        XTier.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBounce).From(false);
        yield return new WaitForSeconds(0.5f);
        Effects_2.SetActive(true);
        if (GameManager.Instance.TransTier > 20)
        {
            TierText.text = "Tier. Max";
            TierMonsterCount.text = "Max";
            FillMonsterCount.fillAmount = 1;
        }
        else
        {
            TierText.text = "Tier. " + tier;
            TierMonsterCount.text = GameManager.Instance.TransTierMonsterCount + " / " + montserCount;
            FillMonsterCount.fillAmount = (float)(GameManager.Instance.TransTierMonsterCount) / (float)(montserCount);
        }


        Fill.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetEase(Ease.InOutBounce).From(false);
    }
}
