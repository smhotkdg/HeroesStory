using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagneticScrollView;
using UnityEngine.UI;
public class MapSelectPanelSrc : MonoBehaviour
{
    public List<Image> PosImageList;
    public List<GameObject> LockList;
    public Text TitleText;
    public Text LevelText;
    public Button PlayButton;
    public Text ClearNeedText;
    public MagneticScrollRect MagneticScrollRect;
    // Start is called before the first frame update
    int selectType = 0;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        UiManager.Instance.bOpenPanel = true;
    }
    private void OnDisable()
    {
        UiManager.Instance.bOpenPanel = false;
    }
    public void SetMove()
    {
        MagneticScrollRect.ScrollTo(GameManager.Instance.MapStageIndex - 1);
    }
   
    public void OnChangeSelect()
    {
        selectType = MagneticScrollRect.CurrentSelectedIndex;
        switch (MagneticScrollRect.CurrentSelectedIndex)
        {
            case 0:
                TitleText.text = "1. 슬라임의 숲";
                LevelText.text = "1 - 200";                
                break;
            case 1:
                TitleText.text = "2. 달팽이 해변";
                LevelText.text = "201 - 400";
              
                break;
            case 2:
                TitleText.text = "3. 버려진 광산";
                LevelText.text = "401 - 600";               
                break;
            case 3:
                TitleText.text = "4. 쥐수구";
                LevelText.text = "601 - 800";
              
                break;
            case 4:
                TitleText.text = "5. 돼지 마을";
                LevelText.text = "801 - 1000";
               
                break;
            case 5:
                TitleText.text = "업데이트 예정";
                LevelText.text = "";                                
                
                break;
        }
        SetData();
    }
    void SetData()
    {
        //이전 스테이지 클리어 해서 해제
        if(GameManager.Instance.MaxLevel>= (selectType)*200)
        {
            LockList[selectType].SetActive(false);
            PlayButton.gameObject.SetActive(true);
            ClearNeedText.gameObject.SetActive(false);
            PosImageList[selectType].color = new Color32(255, 255, 255, 255);
        }
        else
        {
            PlayButton.gameObject.SetActive(false);
            ClearNeedText.gameObject.SetActive(true);
            PosImageList[selectType].color = new Color32(50, 50, 50, 255);
        }
    }
    public void PlayGame()
    {
        switch(selectType)
        {
            case 0:
                GameManager.Instance.Level = GameManager.Instance.level1MapStage;
                break;
            case 1:
                if(GameManager.Instance.MaxLevel >=200)
                {
                    GameManager.Instance.Level = GameManager.Instance.level2MapStage;
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.MapLimit);
                    return;
                }
                
                break;
            case 2:
                if (GameManager.Instance.MaxLevel >= 400)
                {
                    GameManager.Instance.Level = GameManager.Instance.level3MapStage;
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.MapLimit);
                    return;
                }
                break;
            case 3:
                if (GameManager.Instance.MaxLevel >= 600)
                {
                    GameManager.Instance.Level = GameManager.Instance.level4MapStage;
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.MapLimit);
                    return;
                }
                break;
            case 4:
                if (GameManager.Instance.MaxLevel >= 800)
                {
                    GameManager.Instance.Level = GameManager.Instance.level5MapStage;
                }
                else
                {
                    UiManager.Instance.SetNotification(UiManager.NotificationType.MapLimit);
                    return;
                }
                break;

        }
        GameManager.Instance.Save(GameManager.saveType.Level);
        GameManager.Instance.MapStageIndex = selectType + 1;
        GameManager.Instance.Save(GameManager.saveType.MapStageIndex);
        UiManager.Instance.SetInitMap();
        this.gameObject.SetActive(false);
        UiManager.Instance.SetStageName();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
