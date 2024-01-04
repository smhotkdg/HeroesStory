using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdsTImerController : MonoBehaviour
{
    public Text AdsTimeText;
    public Button ButtonObject;

    public enum adsTimerType
    {
        ticket,
        gold
    }
    public adsTimerType timerType = adsTimerType.ticket;

    private void FixedUpdate()
    {
        switch (timerType)
        {
            case adsTimerType.gold:
                if (GameManager.Instance.timerCotroller.bAdsGoldTime == true)
                {
                    AdsTimeText.gameObject.SetActive(true);
                    AdsTimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.AdsGoldTime) + " 후 가능";
                    ButtonObject.image.color = UiManager.Instance.disableButtonColor;
                }
                else
                {
                    AdsTimeText.gameObject.SetActive(false);
                    ButtonObject.image.color = UiManager.Instance.enableButtonColor;
                }
                break;
            case adsTimerType.ticket:
                if (GameManager.Instance.timerCotroller.bAdsTicketTime == true)
                {
                    AdsTimeText.gameObject.SetActive(true);
                    AdsTimeText.text = GameManager.Instance.getTime(GameManager.Instance.timerCotroller.AdsTicketTime) + " 후 가능";
                    ButtonObject.image.color = UiManager.Instance.disableButtonColor;
                }
                else
                {
                    AdsTimeText.gameObject.SetActive(false);
                    ButtonObject.image.color = UiManager.Instance.enableButtonColor;
                }
                break;
        }
    }
}
