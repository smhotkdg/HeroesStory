using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class tilesClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (IsPointerOverUIObject(Input.mousePosition))
        {
            return;
        }
        GameManager.Instance.TouchAttack();
        //tweenAnimation.DORestart();
        Debug.Log("click");
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
