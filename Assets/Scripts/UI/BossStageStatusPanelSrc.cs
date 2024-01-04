using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossStageStatusPanelSrc : MonoBehaviour
{
    public GameObject NormalObject;
    public GameObject FailObject;
    public Text TitleText;
    public Text TimeText;
    public GameObject Effect;
    public GameObject Effect2;
    float time = 5;
    public bool bBoss = true;
    private void OnDisable()
    {
        if(bBoss ==false)
        {
            Effect.SetActive(false);
            Effect2.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if(bBoss ==false)
        {
            time = 3;
            Effect.SetActive(true);
            Effect2.SetActive(true);
        }
    }
    public void SetData(bool bClear)
    {
        if(bBoss== true)
        {
            if (bClear == true)
            {
                NormalObject.SetActive(true);
                FailObject.SetActive(false);
                TitleText.text = "보스 제압 성공!!";
            }
            else
            {
                FailObject.SetActive(true);
                NormalObject.SetActive(false);
                TitleText.text = "보스 제압 실패!!";
            }
            time = 5;
        }
        else
        {
            time = 3;
        }
        
        
    }
    private void Update()
    {
        if(time >1)
        {
            time -= Time.deltaTime;
            TimeText.text = GameManager.Instance.getTime(time) +" 후 자동 닫힘";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
