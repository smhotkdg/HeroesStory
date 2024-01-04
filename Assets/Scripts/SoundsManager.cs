using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public enum FxType
    {
        footStep,
        chopWodd
    }
    public List<AudioSource> FxList;
    public List<AudioSource> BGMList;
    private static SoundsManager _instance = null;
    public static SoundsManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton SoundsManager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void PlayFx(FxType fxType)
    {
        if(GameManager.Instance.isFx == true)
        {
            FxList[(int)fxType].Play();
        }        
    }
}
