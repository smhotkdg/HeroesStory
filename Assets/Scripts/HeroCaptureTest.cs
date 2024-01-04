using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCaptureTest : MonoBehaviour
{
    public List<GameObject> HerosList;
    // Start is called before the first frame update
    int count;
    void Start()
    {
        count = 0;
        ShuffleList(HerosList);
        StartCoroutine(SetHeroRoutine());
    }
    IEnumerator SetHeroRoutine()
    {        
        
        yield return new WaitForSeconds(0.2f);
        for(int i =0; i< 5;i++)
        {           
            if(count < HerosList.Count)
            {
                float randY = Random.Range(-7.7f, -5.8f);
                float randX = Random.Range(-1.64f, 1.64f);
                HerosList[count].transform.position = new Vector3(randX, randY);
                HerosList[count].gameObject.SetActive(true);
            }            
            count++;
            yield return new WaitForSeconds(0.1f);
        }
        if (count < HerosList.Count)
        {
            StartCoroutine(SetHeroRoutine());
        }


    }
    public static void ShuffleList<T>(List<T> list)
    {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index)
        {
            random1 = UnityEngine.Random.Range(0, list.Count);
            random2 = UnityEngine.Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
