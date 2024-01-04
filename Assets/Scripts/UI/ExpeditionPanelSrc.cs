using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpeditionPanelSrc : MonoBehaviour
{
    public GameObject BuyGemPanel;
    public Text GemCount;
    
    public ExpeditionGetSometingPanel expeditionGetSometingPanel;
    public SelectHeroPanelSrc heroListPanel;
    public GameObject TempExpedition;        
    public RectTransform contentSize;
    public GameObject ExTutorial;
    List<GameObject> ExpedtionHeroList = new List<GameObject>();

    private void Start()
    {
        initData();
    }
    void initData()
    {
        if(GameManager.Instance.TutorialList[7]==1)
        {
            GameManager.Instance.TutorialList[7] = 2;
            GameManager.Instance.Save(GameManager.saveType.TutorialList);
            ExTutorial.SetActive(true);
        }
        TempExpedition.SetActive(false);
        if (ExpedtionHeroList.Count == 0)
        {
            int totalCount = 8;
            Vector2 myvec = contentSize.sizeDelta;
            myvec.y = 160 * totalCount;
            contentSize.sizeDelta = myvec;

            for (int i = 0; i < 8; i++)
            {

                GameObject temp = Instantiate(TempExpedition);
                temp.transform.SetParent(TempExpedition.transform.parent);
                temp.transform.localScale = new Vector3(1, 1, 1);
                //temp.transform.localPosition = new Vector3(0, 0, 0);

                temp.name = i.ToString();
                ExpedtionHeroList.Add(temp);

                RectTransform myrect = temp.GetComponent<RectTransform>();
                myrect.offsetMin = new Vector2(0, myrect.offsetMin.y);
                myrect.offsetMax = new Vector2(0, myrect.offsetMax.y);

                Vector3 myVec = temp.transform.localPosition;
                myVec.z = 0;
                //myVec.x = 0;
                myVec.y = -75 - (160 * i);
                temp.transform.localPosition = myVec;
                //ExpedtionHeroList[i].GetComponent<ExpeditionItem>().CheckData();
                ExpedtionHeroList[i].GetComponent<ExpeditionItem>().buyPositionEvent += ExpeditionPanelSrc_buyPositionEvent;
                ExpedtionHeroList[i].GetComponent<ExpeditionItem>().selectHeroEvent += ExpeditionPanelSrc_selectHeroEvent;
                ExpedtionHeroList[i].GetComponent<ExpeditionItem>().GetSometingEventHandler += ExpeditionPanelSrc_GetSometingEventHandler;
                ExpedtionHeroList[i].GetComponent<ExpeditionItem>().GemCompleteEventHandler += ExpeditionPanelSrc_GemCompleteEventHandler;
                temp.SetActive(true);
            }
        }        
    }
    int iCompletePos = -1;
    int gemCount = -1;
    private void ExpeditionPanelSrc_GemCompleteEventHandler(int ipos, int gemcount)
    {
        iCompletePos = ipos;
        gemCount = gemcount;
        GemCount.text = gemCount.ToString();
        BuyGemPanel.SetActive(true);
       
    }
    public void BuyCompleteGem()
    {
        if (iCompletePos == -1)
            return;
        BuyGemPanel.SetActive(false);
        GameManager.Instance.TotalGem -= gemCount;
        GameManager.Instance.Save(GameManager.saveType.TotalGem);
        UiManager.Instance.SetGemText();
        ExpedtionHeroList[iCompletePos].GetComponent<ExpeditionItem>().MissionComplete();
        iCompletePos = -1;
        gemCount = -1;
    }

    private void ExpeditionPanelSrc_GetSometingEventHandler(int iheroIndex,int expedtionPos)
    {
        expeditionGetSometingPanel.GetheroIndex = GameManager.Instance.heroExpeditionComplete[iheroIndex];
        expeditionGetSometingPanel.ExpeditionPos = expedtionPos;
        expeditionGetSometingPanel.gameObject.SetActive(true);

        GameManager.Instance.heroExpeditionComplete[iheroIndex] =-1;
        GameManager.Instance.Save(GameManager.saveType.heroExpeditionComplete);
    }

    int heroIndex = -1;
    private void ExpeditionPanelSrc_selectHeroEvent(int iheroIndex)
    {
        heroListPanel.CheckHero();
        heroListPanel.gameObject.SetActive(true);
        heroIndex = iheroIndex;
    }

    private void ExpeditionPanelSrc_buyPositionEvent()
    {
        for(int i =0; i< ExpedtionHeroList.Count;i++)
        {
            ExpedtionHeroList[i].GetComponent<ExpeditionItem>().CheckData();
        }
    }
    
    public void SetExpedition()
    {       
        if (heroListPanel.ExpeditionPrev == null)
            return;
        if (heroIndex == -1)
            return;

        GameManager.Instance.heroExpeditionPos[heroIndex] = int.Parse(heroListPanel.ExpeditionPrev.name);
        ExpedtionHeroList[heroIndex].GetComponent<ExpeditionItem>().CheckData();
        GameManager.Instance.timerCotroller.bExpedtionTime[heroIndex] = true;
        GameManager.Instance.timerCotroller.ExpedtionTime[heroIndex] = GameManager.Instance.GetExpeditionTime(int.Parse(heroListPanel.ExpeditionPrev.name), heroIndex);
        GameManager.Instance.Save(GameManager.saveType.heroExpeditionPos);
    }
}
