using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndAnim()
    {        
        if(GameManager.Instance.bstartDungoen ==true)
        {
            GameManager.Instance.StartTransDungeon();
            GameManager.Instance.bstartDungoen = false;
        }
        else
        {
            GameManager.Instance.EndTransDungeon();
            GameManager.Instance.bstartDungoen = true;
        }

        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
