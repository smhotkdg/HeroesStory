using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemEffectSrc : MonoBehaviour
{
    public Image Icon;
    public Text InfoText;
    public GameObject Effect;
    float disableTime;
    public void SetData(Sprite _icon, string _infoText,float _disableTIme)
    {
        Icon.sprite = _icon;
        InfoText.text = _infoText;
        //Effect.SetActive(true);
        StartCoroutine(DisableRoutine(_disableTIme));
    }
    IEnumerator DisableRoutine(float time)
    {
        yield return new WaitForSeconds(time+0.2f);
        gameObject.SetActive(false);
    }
}
