using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewContentEventSrc : MonoBehaviour
{
    public Text TitleText;
    public Image Icon;

    private void OnEnable()
    {
        SoundManager.Instance.PlayFX(SoundManager.SoundFXType.GetBuff);
    }
    public void SetData(string title)
    {
        switch(title)
        {
            case "ex":
                TitleText.text = "원정대 !!";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.menu_expedition);
                break;
            case "raid":
                TitleText.text = "레이드 !!";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.menu_raid);
                break;
            case "altar":
                TitleText.text = "재단 !!";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.menu_altar);
                break;
            case "trans":
                TitleText.text = "시험의 방 !!";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.menu_transcendence);
                break;
            case "mat":
                TitleText.text = "제작소 !!";
                UiManager.Instance.SetIcon(Icon, UiManager.iconType.mat);
                break;

        }
    }

}
