using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GR_InfiniCoin;

public class TextPoolController : MonoBehaviour
{
    public GameObject CriticalObject;
    public GameObject TextObject;
    List<GameObject> TextPool = new List<GameObject>();
    List<GameObject> CritlacalTextPool = new List<GameObject>();
    // Start is called before the first frame update

    void Start()
    {
        for(int i =0; i< 100; i++)
        {
            GameObject temp = Instantiate(TextObject);
            temp.transform.SetParent(TextObject.transform.parent);
            temp.transform.localPosition = TextObject.transform.localPosition;
            temp.transform.localScale = TextObject.transform.localScale;
            TextPool.Add(temp);


            GameObject temp2 = Instantiate(CriticalObject);
            temp2.transform.SetParent(CriticalObject.transform.parent);
            temp2.transform.localPosition = CriticalObject.transform.localPosition;
            temp2.transform.localScale = CriticalObject.transform.localScale;
            CritlacalTextPool.Add(temp2);

        }
        iTextIndex = 0;
        iCriticalIndex = 0;
    }
    int iCriticalIndex;
    int iTextIndex;
    public void SetCriticalText(GameObject pObject, InfiniCoin coin)
    {
        if (iCriticalIndex >= CritlacalTextPool.Count)
        {
            iCriticalIndex = 0;

        }
        CritlacalTextPool[iCriticalIndex].SetActive(true);
        CritlacalTextPool[iCriticalIndex].GetComponent<UiTargetManager>().SetWorldObjectCritial(pObject);
        CritlacalTextPool[iCriticalIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);


        CritlacalTextPool[iCriticalIndex].transform.Find("Cost").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(coin);

        iCriticalIndex++;
    }
    public void SetCriticalTextTarget(GameObject pObject, InfiniCoin coin,GameObject target)
    {
        if (iCriticalIndex >= CritlacalTextPool.Count)
        {
            iCriticalIndex = 0;

        }
        CritlacalTextPool[iCriticalIndex].SetActive(true);
        CritlacalTextPool[iCriticalIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        CritlacalTextPool[iCriticalIndex].GetComponent<UiTargetManager>().SetWorldObjectCritial(pObject);
        CritlacalTextPool[iCriticalIndex].GetComponent<UiTargetManager>().WorldObject = target;



        CritlacalTextPool[iCriticalIndex].transform.Find("Cost").gameObject.GetComponent<Text>().text = UiManager.Instance.SetCost(coin);

        iCriticalIndex++;
    }
    public void SetText(GameObject pObject,InfiniCoin coin, UiTargetManager.TextType textType)
    {
        if (iTextIndex >= TextPool.Count)
        {
            iTextIndex = 0;
            
        }
        TextPool[iTextIndex].SetActive(true);
        TextPool[iTextIndex].GetComponent<UiTargetManager>().SetWorldObject(pObject,textType);

        //if (coin < 1000)
        //{
        //    string[] temp = coin.ToString().Split('.');
        //    TextPool[iTextIndex].GetComponent<Text>().text = temp[0].ToString();
        //}
        //else
        {
            TextPool[iTextIndex].GetComponent<Text>().text = UiManager.Instance.SetCost(coin);
        }
        iTextIndex++;
    }

    public void SetTextTarget(GameObject pObject, InfiniCoin coin, UiTargetManager.TextType textType,GameObject target)
    {
        if (iTextIndex >= TextPool.Count)
        {
            iTextIndex = 0;

        }
        TextPool[iTextIndex].SetActive(true);
        TextPool[iTextIndex].GetComponent<UiTargetManager>().SetWorldObject(pObject, textType);
        TextPool[iTextIndex].GetComponent<UiTargetManager>().WorldObject = target;

        //if (coin < 1000)
        //{
        //    string[] temp = coin.ToString().Split('.');
        //    TextPool[iTextIndex].GetComponent<Text>().text = temp[0].ToString();
        //}
        //else
        {
            TextPool[iTextIndex].GetComponent<Text>().text = UiManager.Instance.SetCost(coin);
        }
        iTextIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
