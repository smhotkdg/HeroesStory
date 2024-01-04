using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UiTargetManager : MonoBehaviour
{
    public GameObject WorldObject;
    public RectTransform CanvasRect;
    public float y_Margin = 0;
    public float x_Margin = 0;
    RectTransform UI_Element;
    
    GameObject Text;
    public bool isText = true;
    bool bOne =false;
    
    public enum TextType
    {
        gold,
        attack,
        critical
    }
    public TextType textType = TextType.gold;

    void Start()
    {
        //this is the ui element
        UI_Element = GetComponent<RectTransform>();
        if(isText)
            Text = transform.GetComponent<Text>().gameObject;        
    }
    public void SetWorldObjectCritial(GameObject game)
    {        
        bOne = true;

        UI_Element = GetComponent<RectTransform>();
       
        Text = transform.Find("Cost").gameObject.GetComponent<Text>().gameObject;
      

        WorldObject = game;
        transform.localPosition = new Vector3(0, 0, 0);
    
        Text.GetComponent<Text>().color = new Color32(255,100, 0, 255);
        Text.GetComponent<Text>().fontSize = 25;
        Text.GetComponent<Outline>().effectColor = new Color32(0, 0, 0, 255);

        FIndWorldPos();

        transform.DOLocalMoveY(transform.localPosition.y + 30, 0.7f);
        transform.DOLocalMoveX(transform.localPosition.x + 30, 0.7f);

        GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 1.5f);
        Text.GetComponent<Outline>().DOColor(new Color32(0, 0, 0, 0), 1.2f);
        Text.GetComponent<Text>().DOColor(new Color32(255, 100, 0, 0), 1.5f).OnComplete(OnCompleteCritical);
        
    }
    void OnCompleteCritical()
    {
        gameObject.SetActive(false);
    }
    public void SetWorldObject(GameObject game,TextType text)
    {
        textType = text;
        bOne = true;

        UI_Element = GetComponent<RectTransform>();
        if(isText)
        {
            Text = transform.GetComponent<Text>().gameObject;
        }
            
        WorldObject = game;
        transform.localPosition = new Vector3(0,0,0);
        if (isText)
        {
            if(textType == TextType.gold)
            {
                Text.GetComponent<Text>().color = new Color32(255, 220, 0, 255);
                Text.GetComponent<Outline>().effectColor = new Color32(0, 0, 0, 255);
                
                Text.GetComponent<Text>().fontSize = 20;
            }
            else if(textType == TextType.attack)
            {
                Text.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
                Text.GetComponent<Outline>().effectColor = new Color32(0, 0, 0, 255);
                
                Text.GetComponent<Text>().fontSize = 25;
            }          


        }

        FIndWorldPos();
        
        transform.DOLocalMoveY(transform.localPosition.y + 30, 1);
        if (isText)
        {            
            if (textType == TextType.gold)
            {
                Text.GetComponent<Outline>().DOColor(new Color32(0, 0, 0, 0), 1.2f);
                
                Text.GetComponent<Text>().DOColor(new Color(255, 220, 0, 0), 1.2f).OnComplete(OnCompleteTween);
            }
            else if (textType == TextType.attack)
            {
                Text.GetComponent<Outline>().DOColor(new Color32(0, 0, 0, 0), 1.2f);
                
                Text.GetComponent<Text>().DOColor(new Color(255, 0, 0, 0), 1.2f).OnComplete(OnCompleteTween);
            }
        }    
    }
    void OnCompleteTween()
    {
        gameObject.SetActive(false);
    }
    void FIndWorldPos()
    {
        if (WorldObject == null)
            return;
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(WorldObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)) + x_Margin,
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)) + y_Margin);

        //now you can set the position of the ui element
        UI_Element.anchoredPosition = WorldObject_ScreenPosition;
    }
    private void LateUpdate()
    {
        if (WorldObject == null)
            return;
        if(bOne ==false)
        {
            FIndWorldPos();
        } 
    }
}
