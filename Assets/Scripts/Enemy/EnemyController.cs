using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
public class EnemyController : MonoBehaviour
{
    DOTweenAnimation tweenAnimation;
    private void Start()
    {
        tweenAnimation = GetComponent<DOTweenAnimation>();
    }
    private void OnMouseDown()
    {
        if (IsPointerOverUIObject(Input.mousePosition))
        {
            return;
        }
        
        GameManager.Instance.TouchAttack();
        UiManager.Instance.CheckBottomUI();
        //tweenAnimation.DORestart();
        //Debug.Log("click");
    }
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
