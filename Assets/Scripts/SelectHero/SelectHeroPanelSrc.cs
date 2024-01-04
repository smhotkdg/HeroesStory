using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHeroPanelSrc : MonoBehaviour
{
    public List<BottomButtonController> bottomButtonControllers;
    public GameObject SelectHeroObject;
    public Transform heroParent;
    List<GameObject> SelectHeroList = new List<GameObject>();

    public delegate void SelectItemAltar_Panel(int index);
    public event SelectItemAltar_Panel selectItemAltar;

    public delegate void unSelectItemAltar_Panel(int index);
    public event unSelectItemAltar_Panel unselectItemAltar;

    public delegate void disablePanel();
    public event disablePanel disablePanelevent;

    public delegate void raidClick(int heroindex);
    public event raidClick raidClickEvent;

    public delegate void TransClick(int heroindex);
    public event TransClick TransClickEvent;

    public enum PanelType
    {
        select,
        info,
        altar,
        Expedition,
        raid,
        trans
            
    };

    public PanelType panelType;
    //
    //
    int iHeroSize = 53;
    //
    void Start()
    {

    }
    void InitData()
    {
        iHeroSize = SystemManager.data.Count;
        if (SelectHeroList.Count == 0)
        {
            for (int i = 0; i < iHeroSize; i++)
            {
                GameObject temp = Instantiate(SelectHeroObject);
                temp.name = i.ToString();
                temp.transform.SetParent(heroParent);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.transform.localScale = new Vector3(1, 1, 1);
                SelectHeroList.Add(temp);
                temp.GetComponent<SelectHeroItem>().selectItem += SelectHeroPanelSrc_selectItem;
                if(temp.GetComponent<SelectHeroItem>().heroType == SelectHeroItem.selectHeroType.altar)
                {
                    temp.GetComponent<SelectHeroItem>().selectItemAltar += SelectHeroPanelSrc_selectItemAltar;
                    temp.GetComponent<SelectHeroItem>().UnselectItemAltar += SelectHeroPanelSrc_UnselectItemAltar;
                }
                if (temp.GetComponent<SelectHeroItem>().heroType == SelectHeroItem.selectHeroType.expedition)
                {
                    temp.GetComponent<SelectHeroItem>().ExpeditionSelecEventt += SelectHeroPanelSrc_ExpeditionSelecEventt; 
                    
                }
                if (temp.GetComponent<SelectHeroItem>().heroType == SelectHeroItem.selectHeroType.raid)
                {
                    temp.GetComponent<SelectHeroItem>().selectRaidEvent += SelectHeroPanelSrc_selectRaidEvent;
                }
                if (temp.GetComponent<SelectHeroItem>().heroType == SelectHeroItem.selectHeroType.trans)
                {
                    temp.GetComponent<SelectHeroItem>().selectTransEvent += SelectHeroPanelSrc_selectTransEvent;
                }
                temp.SetActive(true);
            }
        }
        for (int i = 0; i < bottomButtonControllers.Count; i++)
        {
            if(panelType != PanelType.altar)
            {
                bottomButtonControllers[i].SetDisable();
            }
                
        }
        if(panelType == PanelType.select)
        {
            GameManager.Instance.MoveCamera(true);
        }
    }

    private void SelectHeroPanelSrc_selectTransEvent(int index)
    {
        TransClickEvent?.Invoke(index);
    }

    private void SelectHeroPanelSrc_selectRaidEvent(int index)
    {
        raidClickEvent?.Invoke(index);
    }
  
    public GameObject ExpeditionPrev;
    private void SelectHeroPanelSrc_ExpeditionSelecEventt(GameObject gameObject)
    {
        if (ExpeditionPrev != null)
        {
            ExpeditionPrev.GetComponent<SelectHeroItem>().SelectImage.SetActive(false);

        }
        if (ExpeditionPrev != gameObject)
        {
            ExpeditionPrev = gameObject;
        }
        else
        {
            ExpeditionPrev = null;
        }
        CheckHero();
    }

    private void SelectHeroPanelSrc_UnselectItemAltar(int index)
    {
        unselectItemAltar?.Invoke(index);
    }

    private void SelectHeroPanelSrc_selectItemAltar(int index)
    {
        selectItemAltar?.Invoke(index);
    }

    private void OnEnable()
    {
        if (prevObject != null)
        {
            prevObject.GetComponent<SelectHeroItem>().SelectImage.SetActive(false);
        }
        InitData();
        CheckHero();        
        
        if (panelType == PanelType.select)
        {
            for (int i = 0; i < GameManager.Instance.HeroList.Count; i++)
            {
                GameManager.Instance.HeroList[i].GetComponent<BoxCollider2D>().enabled = false;
            }            
        }
        if(panelType == PanelType.raid )
        {
            CheckRaidHero();
        }
        if(panelType == PanelType.altar)
        {
            CheckAltarHero();
        }
        if(panelType == PanelType.trans)
        {
            CheckTransHero();
        }
      
    }
    public void CheckTransHero()
    {
        List<int> heroCheck = new List<int>();
        for (int i = 0; i < SelectHeroList.Count; i++)
        {
            SelectHeroList[i].SetActive(true);
            if (GameManager.Instance.herosInfos[i].isGetHero == true)
            {               
                if (GameManager.Instance.heroTransPos == i)
                {
                    heroCheck.Add(i);
                }              
            }
            else
            {
                if (panelType == PanelType.trans)
                    heroCheck.Add(i);
            }
        }
        for (int i = 0; i < heroCheck.Count; i++)
        {
            if (panelType == PanelType.trans)
                SelectHeroList[heroCheck[i]].SetActive(false);
        }

    
      
    }
    bool bend = false;
    private void OnApplicationQuit()
    {
        bend = true;
    }
    public void SelectTutorial()
    {
        SelectHeroList[0].GetComponent<SelectHeroItem>().Select();
    }
    private void OnDisable()
    {
        if (panelType == PanelType.info)
        {
            if (GameManager.Instance == null)
                return;
            bool bAweaking = false;
            for (int i = 0; i < GameManager.Instance.herosInfos.Count; i++)
            {
                if (GameManager.Instance.herosInfos[i].HeroCount >= 2)
                {
                    bAweaking = true;
                }
            }
            if (bAweaking == true)
            {
                GameManager.Instance.isNewHero = true;
                GameManager.Instance.Save(GameManager.saveType.isNewHero);
            }
            else
            {
                GameManager.Instance.isNewHero = false;
                GameManager.Instance.Save(GameManager.saveType.isNewHero);
            }
        }
        if (ExpeditionPrev != null)
        {
            ExpeditionPrev.GetComponent<SelectHeroItem>().SelectImage.SetActive(false);
        }
        ExpeditionPrev = null;
        if (panelType == PanelType.select || panelType == PanelType.Expedition)
        {
            if (bend == true)
                return;
            for (int i = 0; i < GameManager.Instance.HeroList.Count; i++)
            {
                if (bend == true)
                    return;
                GameManager.Instance.HeroList[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            if(panelType == PanelType.select)
                GameManager.Instance.MoveCamera(false);
        }
        if (panelType == PanelType.altar)
        {
            disablePanelevent?.Invoke();
        }

        
        //GameManager.Instance.AllSave(GameManager.saveType.herosInfos);        
    }
    public void CheckHero()
    {
        List<int> heroCheck = new List<int>();
        for (int i = 0; i < SelectHeroList.Count; i++)
        {
            SelectHeroList[i].SetActive(true);
            if (GameManager.Instance.herosInfos[i].isGetHero == true)
            {
                for (int k = 0; k < GameManager.Instance.heroPos.Length; k++)
                {
                    if (GameManager.Instance.heroPos[k] == i)
                    {
                        heroCheck.Add(i);
                    }
                }
                for(int j = 0; j< GameManager.Instance.heroExpeditionPos.Length;j++)
                {
                    if(GameManager.Instance.heroExpeditionPos[j]==i)
                    {
                        heroCheck.Add(GameManager.Instance.heroExpeditionPos[j]);
                    }
                }
                for (int m = 0; m < GameManager.Instance.heroExpeditionComplete.Length; m++)
                {
                    if (GameManager.Instance.heroExpeditionComplete[m] == i)
                    {
                        heroCheck.Add(GameManager.Instance.heroExpeditionComplete[m]);
                    }
                }
            }
            else
            {
                if (panelType == PanelType.select || panelType == PanelType.Expedition)
                    heroCheck.Add(i);
            }
        }
        for (int i = 0; i < heroCheck.Count; i++)
        {
            if (panelType == PanelType.select || panelType == PanelType.Expedition)
                SelectHeroList[heroCheck[i]].SetActive(false);
        }

    }
    public void CheckAltarHero()
    {
        List<int> heroCheck = new List<int>();
        for (int i = 0; i < SelectHeroList.Count; i++)
        {
            SelectHeroList[i].SetActive(true);
            if (GameManager.Instance.herosInfos[i].isGetHero == true)
            {
                if (GameManager.Instance.herosInfos[i].isGetHero == false)
                {
                    heroCheck.Add(i);
                }
                
            }
            else
            {
                if (panelType == PanelType.altar)
                    heroCheck.Add(i);
            }
        }
        for (int i = 0; i < heroCheck.Count; i++)
        {
            if (panelType == PanelType.altar)
                SelectHeroList[heroCheck[i]].SetActive(false);
        }
    }
    public void CheckRaidHero()
    {
        List<int> heroCheck = new List<int>();
        for (int i = 0; i < SelectHeroList.Count; i++)
        {
            SelectHeroList[i].SetActive(true);
            if (GameManager.Instance.herosInfos[i].isGetHero == true)
            {
                for (int k = 0; k < GameManager.Instance.heroraidPos.Length; k++)
                {
                    if (GameManager.Instance.heroraidPos[k] == i)
                    {
                        heroCheck.Add(i);
                    }
                }               
            }
            else
            {
                if (panelType == PanelType.raid )
                    heroCheck.Add(i);
            }
        }
        for (int i = 0; i < heroCheck.Count; i++)
        {
            if (panelType == PanelType.raid )
                SelectHeroList[heroCheck[i]].SetActive(false);
        }
    }
    GameObject prevObject;
    private void SelectHeroPanelSrc_selectItem(GameObject gameObject)
    {
        if (prevObject != null)
        {
            prevObject.GetComponent<SelectHeroItem>().SelectImage.SetActive(false);

        }
        if (prevObject != gameObject)
        {
            prevObject = gameObject;
        }
        else
        {
            prevObject = null;
        }
        CheckHero();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelUpSetData(int index)
    {
        if (SelectHeroList.Count == 0)
            InitData();
        SelectHeroList[index].GetComponent<SelectHeroItem>().SetData();
    }
}
