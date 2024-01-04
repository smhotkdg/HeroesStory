using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirdMoveManager : MonoBehaviour
{
    
    public delegate void MoveEndDelegate();
    public event MoveEndDelegate onMoveEndEvent;


    public delegate void MoveHalfEndDelegate();
    public event MoveHalfEndDelegate onMoveHalfEndEvent;

    bool bMove = false;
    // Start is called before the first frame update
    int moveCount = 0;
    void Start()
    {
        
    }
    void InitMovePos()
    {
        transform.position = new Vector3(0,0,0);
    }
    public void MoveStart()
    {
        if(bMove ==false)
        {
            bMove = true;
            StartCoroutine(MoveStartRoutine());
            
        }
        
    }

    IEnumerator MoveStartRoutine()
    {
        //float xDelta = 2f / 100f;
        //float yDelta = 10f / 1000f;
#if UNITY_IOS
        //xDelta = 2f / 50f;
        //yDelta = 1f / 50f;
#endif
        int count = 100;
#if UNITY_IOS
        count = 50;
#endif

        for (int i =0; i< count; i++)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y - yDelta,25);
            if(i == count/2)
            {
                onMoveHalfEndEvent?.Invoke();
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.01f);
        bMove = false;
        onMoveEndEvent?.Invoke();
        InitMovePos();
    }



}
