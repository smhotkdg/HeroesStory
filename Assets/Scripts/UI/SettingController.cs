using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class SettingController : MonoBehaviour
{
    public GameObject ChangeNicknameButton;
    public GameObject NicknameSettingPanel;
    public InputField NicknameField;
    public Text UserId;
    public Toggle FxToggle;
    public Toggle BGMToggle;
    public Text NickNameText;
    private void OnEnable()
    {
        FxToggle.isOn = GameManager.Instance.isFx;
        BGMToggle.isOn = GameManager.Instance.isBGM;
        UserId.text = "UserID : " +GameManager.Instance.UserID;
        if(GameManager.Instance.strNickName !="Empty")
        {
            NickNameText.text = GameManager.Instance.strNickName;
            ChangeNicknameButton.SetActive(false);
        }
        else
        {
            NickNameText.text = "닉네임 설정  -->";
            ChangeNicknameButton.SetActive(true);
        }
    }
    string nicknameTemp;
    public void InputNickname(Text text)
    {
        text.text = NicknameField.text;
        nicknameTemp = text.text;
    }
    public void SaveData()
    {
        //UiManager.Instance.DataSavePanel.SetActive(true);
        GameManager.Instance.AsyncSaveStart();
        //UiManager.Instance.SetNotification(UiManager.NotificationType.save);
        
    }
    bool IsValidStr(string text)
    {
        string pattern = @"^[a-zA-Z0-9가-힣]*$";
        return Regex.IsMatch(text, pattern);
    }   

    public void SetNickname()
    {
        
        if (IsValidStr(nicknameTemp) == false)
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.nicknameerror1);
            NicknameField.text = "";
            return;
        }
        if(nicknameTemp.Length >10)
        {
            UiManager.Instance.SetNotification(UiManager.NotificationType.nicknameerror2);
            NicknameField.text = "";
            return;
        }
        GameManager.Instance.strNickName = nicknameTemp;
        NickNameText.text = GameManager.Instance.strNickName;
        GameManager.Instance.Save(GameManager.saveType.NickName);
        NicknameField.text = "";
        NicknameSettingPanel.SetActive(false);
    }

    public void SetFx()
    {
        GameManager.Instance.isFx = FxToggle.isOn;
        GameManager.Instance.Save(GameManager.saveType.isFx);
        SoundManager.Instance.UpdateSound();
    }
    public void SetBGM()
    {
        GameManager.Instance.isBGM = BGMToggle.isOn;
        GameManager.Instance.Save(GameManager.saveType.isBGM);
        SoundManager.Instance.UpdateSound();
    }
}
