using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearConroller : MonoBehaviour
{
    public Transform Line;
    public List<GameObject> StageList;
    List<Animator> animators = new List<Animator>();
    private void Start()
    {
        for(int i =0; i< StageList.Count;i++)
        {
            animators.Add(StageList[i].transform.Find("Clear").GetComponent<Animator>());
            animators[i].enabled = false;
        }

        //SetNext(baseClicker.Instance.GetStageLevel(), false);
        SetStageCount(GameManager.Instance.GetMaxMonsterCount());
        SetInit(GameManager.Instance.GetMonsterCount());
    }
    public void SetStageView()
    {
        SetStageCount(GameManager.Instance.GetMaxMonsterCount());
        //SetInit(0);
        //SetNext(GameManager.Instance.GetMonsterCount(), false);
        SetInit(GameManager.Instance.GetMonsterCount());
    }
    public void SetStageCount(int Count)
    {
        int delCount = 0;
        for(int i =0; i< StageList.Count; i++)
        {
            if(i < Count)
            {
                StageList[i].SetActive(true);
            }
            else
            {
                StageList[i].SetActive(false);
                delCount++;
            }
        }
        if(Count == 1)
        {
            Line.gameObject.SetActive(false);
        }
        else
        {
            Line.gameObject.SetActive(true);
            float defalut = 300;
            var theBarRectTransfrom = Line.transform as RectTransform;
            theBarRectTransfrom.sizeDelta = new Vector2(defalut - (10 * delCount), theBarRectTransfrom.sizeDelta.y);
        }
        
    }
  
    public void LevelUPSet(int levelIndex)
    {
        animators[levelIndex].enabled = true;
        StageList[levelIndex].transform.Find("Clear").gameObject.SetActive(true);
    }
    public void SetInit(int index)
    {       
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].enabled = false;
        }       
        for (int i = 0; i < StageList.Count; i++)
        {
            StageList[i].transform.Find("Clear").gameObject.SetActive(false);
        }
      
        for (int i = 0; i < index; i++)
        {
            RectTransform rt = StageList[i].transform.Find("Clear").gameObject.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(25, 25);
            StageList[i].transform.Find("Clear").gameObject.SetActive(true);
        }
    }
    public void SetNext(int index,bool isAnim)
    {
        if(index-1 <0)
        {
            return;
        }
        if(isAnim ==false)
        {
            for (int i = 0; i < animators.Count; i++)
            {
                animators[i].enabled = false;
            }
        }
        
        animators[index-1].enabled = true;
        if (isAnim == false)
        {
            for (int i = 0; i < StageList.Count; i++)
            {
                StageList[i].transform.Find("Clear").gameObject.SetActive(false);
            }
        }
        for (int i =0; i< index; i++)
        {            
            StageList[i].transform.Find("Clear").gameObject.SetActive(true);
        }        
    }
}
