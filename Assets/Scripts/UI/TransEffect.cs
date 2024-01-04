using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TransEffect : MonoBehaviour
{
    public GameObject Effect;
    public Image BG;
    public Image Image_Circle;

    private void OnEnable()
    {
        Effect.SetActive(false);
        StartCoroutine(StartRoutine());
    }
    
    IEnumerator StartRoutine()
    {
        BG.gameObject.SetActive(true);
        Image_Circle.gameObject.SetActive(true);

        BG.DOColor(new Color(1, 1, 1, 1), 1).OnComplete(CompleteStartRaidEffect_2);
        Image_Circle.DOColor(new Color(1, 1, 1, 1), 1);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(true);
    }
    void CompleteStartRaidEffect_2()
    {
        StartCoroutine(EndRoutine());
    }
    void CompleteStartRaidEffect_End()
    {
        this.gameObject.SetActive(false);
    }
    IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(1f);
        BG.DOColor(new Color(1, 1, 1, 0), 1).OnComplete(CompleteStartRaidEffect_End);
        Image_Circle.DOColor(new Color(1, 1, 1, 0), 1);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
        GameManager.Instance.TotalGold = 0;
    }
}
