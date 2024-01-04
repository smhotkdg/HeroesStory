using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    public GameObject CollectionTemp;
    public List<GameObject> CollectionItemList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CollectionTemp.SetActive(false);
        SetData();
    }
    private void OnEnable()
    {
        CollectionTemp.SetActive(false);
        SetData();        
    }
    void SetData()
    {
        if(CollectionItemList.Count ==0)
        {
            for(int i=0; i< SystemManager.Collectiontitle.Count;i++)
            {
                GameObject temp = Instantiate(CollectionTemp);
                temp.transform.SetParent(CollectionTemp.transform.parent);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.name = i.ToString();
                temp.SetActive(true);
                CollectionItemList.Add(temp);
            }
        }

        for(int i =0; i< CollectionItemList.Count;i++)
        {
            CollectionItemList[i].GetComponent<CollectionItem>().SetData();
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.IsNewCollection = false;
        GameManager.Instance.Save(GameManager.saveType.isNewCollection);
    }
}
