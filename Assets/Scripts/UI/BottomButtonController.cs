using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomButtonController : MonoBehaviour
{
    //public GameObject UnsetSelect;
    public GameObject Panel;
    public Color enableColor;
    public Color disableColor;
    public GameObject BGSafe;
    
    public enum ButtonType
    {
        LevelUp,
        Seal,
        Altar,
        Raid,
        Mercenary,
        Transcedence,
        Shop,
        smithy

    };
    public GameManager.TutorialType tutorialType;
    public ButtonType buttonType;
    UiManager uiManager;
    private void Start()
    {        
        uiManager = UiManager.Instance;
        SetDisable();
    }
    public bool bSelect;
    public void SetEnablePanel()
    {
        if (GameManager.Instance.TutorialList[(int)tutorialType] == 0)
            return;
        //if(UnsetSelect.activeSelf ==true)
        //{
        //    UnsetSelect.GetComponent<Button>().onClick.Invoke();
        //}
        if(buttonType == ButtonType.smithy)
        {
            if (GameManager.Instance.IsNewSmith == 0)
                return;
        }
        if(Panel.activeSelf ==true)
        {
            SetDisable();            
            return;
        }
        
        uiManager.SetBottomPanel(gameObject, buttonType);
        this.GetComponent<Image>().color = enableColor;
        transform.Find("Icon").gameObject.GetComponent<Image>().color = enableColor;
        transform.Find("SelectImage").gameObject.SetActive(true);
        Panel.SetActive(true);
        if(BGSafe !=null)
            BGSafe.SetActive(true);
        if (buttonType != ButtonType.Shop && buttonType != ButtonType.Altar)
            GameManager.Instance.MoveCamera(true);

        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
    }
    public void SetDisable()
    {
        if (GameManager.Instance.TutorialList[(int)tutorialType] == 0)
            return;
        if(buttonType == ButtonType.Mercenary)
        {
           
            //if(GameManager.Instance.TutorialList[7] ==2)
            //{
            //    //if(GameManager.Instance.TutorialList[8] ==0)
            //    //{
            //    //    GameManager.Instance.CheckTutorial();
            //    //}
            //}
        }
        this.GetComponent<Image>().color = disableColor;
        transform.Find("Icon").gameObject.GetComponent<Image>().color = disableColor;
        transform.Find("SelectImage").gameObject.SetActive(false);
        Panel.SetActive(false);
        if (BGSafe != null)
            BGSafe.SetActive(false);
        if (buttonType != ButtonType.Shop && buttonType != ButtonType.Altar)
            GameManager.Instance.MoveCamera(false);
    }
}
