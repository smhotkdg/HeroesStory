using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureHeros : MonoBehaviour
{
    // Start is called before the first frame update
    float moveSpeed = 0;
    Vector2 moveVec;
    void Start()
    {
        moveSpeed = Random.Range(0.01f, 0.05f);
        moveVec = transform.position;
        gameObject.GetComponent<Animator>().Play("move");
    }
    private void FixedUpdate()
    {
        moveVec.y += moveSpeed;
        transform.position = moveVec;
    }    
}
