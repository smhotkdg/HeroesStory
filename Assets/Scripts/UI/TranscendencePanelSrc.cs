using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;
public class TranscendencePanelSrc : MonoBehaviour
{
    public GameObject TOpenPanel;
    public Text GetSoulCountText;
    public Text GetSoulCountText_Panel;

    public SelectHeroPanelSrc HeroList;
    public ScrollRect listRect;

    public Button TranscendenceButton;
    public Text TranscendenceButtonText;

    public GameObject TranscendenceObject;
    List<GameObject> transcendenceItemList = new List<GameObject>();
    public RectTransform contentSize;
    public GameObject tempItem;
    public GameObject Hero;
    public GameObject removeList;
    public Text LevelTextList;
    public Text DpsTextList;
    void Start()
    {
        initData();
        HeroList.TransClickEvent += HeroList_TransClickEvent;
    }
    
    private void HeroList_TransClickEvent(int heroindex)
    {        
        GameManager.Instance.heroTransPos = heroindex;
        GameManager.Instance.Save(GameManager.saveType.heroTransPos);
        if (GameManager.Instance.heroTransPos == -1)
            return;

        listRect.enabled = false;        
        //Debug.Log(heroindex);
        CheckData();
        CheckRemovePoint();
        HeroList.gameObject.SetActive(false);
    }
    public void RemoveHero()
    {
        GameManager.Instance.heroTransPos = -1;
        CheckRemovePoint();
        CheckData();
    }
    void CheckRemovePoint()
    {
       
        if (GameManager.Instance.heroTransPos > -1)
        {
            removeList.SetActive(true);
            LevelTextList.gameObject.SetActive(true);
            DpsTextList.gameObject.SetActive(true);

            UiManager.Instance.SetHeroDPS_UI(DpsTextList, GameManager.Instance.heroTransPos);
            UiManager.Instance.SetHeroLevel_UI(LevelTextList, GameManager.Instance.heroTransPos);

        }
        else
        {
            removeList.SetActive(false);
            LevelTextList.gameObject.SetActive(false);
            DpsTextList.gameObject.SetActive(false);
        }
       
    }
    void CheckData()
    {
        if(GameManager.Instance.heroTransPos >-1)
        {
            Hero.SetActive(true);
            UiManager.Instance.SetHeroIcon_UI(Hero.GetComponent<Image>(), GameManager.Instance.heroTransPos);
        }
        else
        {
            Hero.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        initData();
        CheckData();      
        CheckRemovePoint();
    }
    void initData()
    {
        int isize = SystemManager.soulAblityTitle.Count;
        if (transcendenceItemList.Count == 0)
        {
            int totalCount = isize;
            Vector2 myvec = contentSize.sizeDelta;
            myvec.y = 160 * (totalCount+1);
            contentSize.sizeDelta = myvec;


            RectTransform myrect_temp = TranscendenceObject.GetComponent<RectTransform>();
            myrect_temp.offsetMin = new Vector2(0, myrect_temp.offsetMin.y);
            myrect_temp.offsetMax = new Vector2(0, myrect_temp.offsetMax.y);



            Vector3 myVec_temp = TranscendenceObject.transform.localPosition;
            myVec_temp.z = 0;
            //myVec.x = 0;
            myVec_temp.y = -75;
            TranscendenceObject.transform.localPosition = myVec_temp;

            TranscendenceObject.SetActive(true);


            for (int i = 0; i < isize; i++)
            {

                GameObject temp = Instantiate(tempItem);
                temp.transform.SetParent(tempItem.transform.parent);
                temp.transform.localScale = new Vector3(1, 1, 1);

                temp.name = i.ToString();
                transcendenceItemList.Add(temp);

                RectTransform myrect = temp.GetComponent<RectTransform>();
                myrect.offsetMin = new Vector2(0, myrect.offsetMin.y);
                myrect.offsetMax = new Vector2(0, myrect.offsetMax.y);

                Vector3 myVec = temp.transform.localPosition;
                myVec.z = 0;
                //myVec.x = 0;
                myVec.y = -75 - (160 * (i+1));
                temp.transform.localPosition = myVec;

                temp.SetActive(true);
                transcendenceItemList[i].GetComponent<TranscendenceItem>().SetData();
            }
        }
        else
        {
            for(int i =0; i< transcendenceItemList.Count;i++)
            {
                transcendenceItemList[i].GetComponent<TranscendenceItem>().SetData();
            }
        }
    }

    private void FixedUpdate()
    {
       
    }
    public void Transcendence()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);       
        
            //show Panel
        TOpenPanel.SetActive(true);      
    }
    public InfiniCoin Rebirth()
    {
        double dSoulCount = 0;
        double temp1 = 1, temp2 = 1, temp3 = 1, temp4 = 1;
        if (GameManager.Instance.MaxLevel > 600)
        {
            temp4 = System.Math.Pow(1.003, GameManager.Instance.MaxLevel - 601);
        }
        if (GameManager.Instance.MaxLevel > 400)
        {
            if (GameManager.Instance.MaxLevel - 401 > 200)
            {
                temp3 = System.Math.Pow(1.005, 200);
            }
            else
            {
                temp3 = System.Math.Pow(1.005, GameManager.Instance.MaxLevel - 401);
            }
        }
        if (GameManager.Instance.MaxLevel > 300)
        {
            if (GameManager.Instance.MaxLevel - 301 > 100)
            {
                temp2 = System.Math.Pow(1.01, 100);
            }
            else
            {
                temp2 = System.Math.Pow(1.01, GameManager.Instance.MaxLevel - 301);
            }
        }
        if (GameManager.Instance.MaxLevel >= 100)
        {
            if (GameManager.Instance.MaxLevel - 100 > 201)
            {
                temp1 = System.Math.Pow(1.02, 201);
            }
            else
            {
                temp1 = System.Math.Pow(1.02, GameManager.Instance.MaxLevel - 100);
            }
        }

        dSoulCount = temp1 * temp2 * temp3 * temp4;
        dSoulCount = System.Math.Floor(dSoulCount * 100);

        float power = 0;
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul50] == 1)
        {
            power += 0.5f;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul100] == 1)
        {
            power += 1;
        }
        if (GameManager.Instance.CollectionList[(int)GameManager.CollectionType.soul200] == 1)
        {
            power += 2;
        }
        if (power > 0)
        {
            dSoulCount = dSoulCount + (dSoulCount * power);   
        }

        InfiniCoin Count = dSoulCount;
        return Count;
    }

    public void StartTranscendence(int index)
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (index == 2)
        {
            if (GameManager.Instance.TotalGem >= 1000)
            {
                GameManager.Instance.SetTranscendence(Rebirth(), index, TOpenPanel);
            }
            else
            {
                UiManager.Instance.SetNotification(UiManager.NotificationType.gem);
            }
        }
        else if(index ==1)
        {
            GameManager.Instance.adsType = GameManager.AdsType.Transcendence;
            GameManager.Instance.transCost = Rebirth();
            GameManager.Instance.PanelsTrans = TOpenPanel;
            AdManager.Instance.ShowRewardedAds();
        }
        else
        {
            GameManager.Instance.SetTranscendence(Rebirth(), index, TOpenPanel);
        }
    }

    public void GetReward()
    {
        GameManager.Instance.TotalSoul += GameManager.Instance.transCost;
        GameManager.Instance.Save(GameManager.saveType.TotalSoul);
        GameManager.Instance.transCost = 0;
        UiManager.Instance.SetSoulText();
    }
}
