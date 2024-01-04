using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectPosSrc : MonoBehaviour
{
    public enum selectPosType
    {
        defaultType,
        ButtonType
    };
    public Text TitleText;
    public Text LevelText;
    public Text DPsText;
    public selectPosType myselectPosType;
    //public GameObject Effect;
    public GameObject SelectHeroPanel;
    public GameObject Button;
    public Image HeroImage;
    Animator animator;
    void Start()
    {
        if (myselectPosType == selectPosType.defaultType)
        {
            animator = GetComponent<Animator>();
            setNormal();
        }
        
    }
    private void OnDisable()
    {
        if (myselectPosType == selectPosType.defaultType)
        {
            //setNormal();
            Button.SetActive(false);
        }
    }
    private void OnEnable()
    {
        setNormal();
        SetInitData();
    }
    void SetInitData()
    {
        if (GameManager.Instance.heroPos[int.Parse(this.name)] >= 0)
        {
            Button.SetActive(true);
            HeroImage.gameObject.SetActive(true);
            UiManager.Instance.SetHeroIcon_UI(HeroImage, GameManager.Instance.heroPos[int.Parse(this.name)]);
        }
        else
        {
            Button.SetActive(false);
            HeroImage.gameObject.SetActive(false);
        }
        if (GameManager.Instance.heroPos[int.Parse(this.name)] >= 0)
        {
            UiManager.Instance.SetHeroName_UI(TitleText, GameManager.Instance.heroPos[int.Parse(this.name)]);
            UiManager.Instance.SetHeroLevel_UI(LevelText, GameManager.Instance.heroPos[int.Parse(this.name)]);
            UiManager.Instance.SetHeroDPS_UI(DPsText, GameManager.Instance.heroPos[int.Parse(this.name)]);
            UiManager.Instance.SetName_UIText(TitleText, GameManager.Instance.heroPos[int.Parse(this.name)]);
            TitleText.gameObject.SetActive(true);
            LevelText.gameObject.SetActive(true);
            DPsText.gameObject.SetActive(true);
        }
        else
        {
            TitleText.gameObject.SetActive(false);
            LevelText.gameObject.SetActive(false);
            DPsText.gameObject.SetActive(false);
        }
    }
    public void setNormal()
    {
        if (myselectPosType == selectPosType.defaultType)
        {
            animator = GetComponent<Animator>();
            if (GameManager.Instance.heroPos[int.Parse(this.name)] > -2)
            {
                animator.Play("normal");
            }
            else
            {
                animator.Play("lock");
            }
        }
        SetInitData();
    }
    public void Click()
    {
        if (myselectPosType == selectPosType.defaultType)
        {
            if (GameManager.Instance.heroPos[int.Parse(this.name)] > -2)
            {
                SelectHeroPanel.SetActive(true);
                //Effect.transform.SetParent(transform);
                //Effect.transform.localPosition = new Vector3(0, -0.1f, 0);
                //Effect.SetActive(true);
                GameManager.Instance.setSelectPosNormal();
                animator.Play("select");
                //Button.transform.SetParent(transform);
                //Button.transform.localPosition = new Vector3(0.15f, 0.1f, 0);
                if (GameManager.Instance.heroPos[int.Parse(this.name)] >= 0)
                {
                    Button.SetActive(true);
                }
                else
                {
                    Button.SetActive(false);
                    animator.Play("normal");
                }
                GameManager.Instance.SelectHeroPos = int.Parse(this.name);
            }
            else
            {
                UiManager.Instance.unlockSlotPanel.SetActive(true);
                UiManager.Instance.unlockSlotPanel.GetComponent<unlockSlotPanel>().SelectHeroPos = int.Parse(this.name);
            }
            //Debug.Log(this.name);
        }
        else
        {
            if (GameManager.Instance.heroPos[int.Parse(transform.parent.name)] >= 0)
            {
                GameManager.Instance.SelectHeroPos = int.Parse(transform.parent.name);
                GameManager.Instance.SetHeroPos(-1);
            }
        }
    }
    public void UnsetObject(int index)
    {
        if (GameManager.Instance.heroPos[index] >= 0)
        {
            GameManager.Instance.SelectHeroPos = index;
            GameManager.Instance.SetHeroPos(-1);
        }
        SetInitData();
    }
    private void OnMouseDown()
    {
        if (IsPointerOverUIObject(Input.mousePosition))
        {
            return;
        }
        if(myselectPosType == selectPosType.defaultType)
        {
            if (GameManager.Instance.heroPos[int.Parse(this.name)] > -2)
            {
                SelectHeroPanel.SetActive(true);
                //Effect.transform.SetParent(transform);
                //Effect.transform.localPosition = new Vector3(0, -0.1f, 0);
                //Effect.SetActive(true);
                GameManager.Instance.setSelectPosNormal();
                animator.Play("select");
                //Button.transform.SetParent(transform);
                //Button.transform.localPosition = new Vector3(0.15f, 0.1f, 0);
                if (GameManager.Instance.heroPos[int.Parse(this.name)] >= 0)
                {
                    Button.SetActive(true);
                }
                else
                {
                    Button.SetActive(false);
                    animator.Play("normal");
                }
                GameManager.Instance.SelectHeroPos = int.Parse(this.name);                
            }
            else
            {
                UiManager.Instance.unlockSlotPanel.SetActive(true);
                UiManager.Instance.unlockSlotPanel.GetComponent<unlockSlotPanel>().SelectHeroPos = int.Parse(this.name);
            }
            //Debug.Log(this.name);
        }
        else
        {
            if(GameManager.Instance.heroPos[int.Parse(transform.parent.name)]>=0)
            {                
                GameManager.Instance.SelectHeroPos = int.Parse(transform.parent.name);
                GameManager.Instance.SetHeroPos(-1);
            }
        }
    }
 
    public void CheckButton()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.Touch);
        if (myselectPosType == selectPosType.defaultType)
        {
            
            if (GameManager.Instance.heroPos[int.Parse(this.name)] >= 0)
            {
                Button.SetActive(true);
            }
            else
            {
                Button.SetActive(false);
            }
     
        }
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
