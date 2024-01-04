using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SoundFXType
    {
        AltarEffect,
        ExpedtionComplete,
        GetBuff,
        Coin,
        bowAttack,
        fireAttack,
        GunAttack,
        GunAttack2,
        MagicAttack,
        MagicAttack2,
        PunchAttack,
        ShortAttack,
        HeroinfoUpgrade,
        Touch,
        TouchAttack
    };
    public AudioSource AltarBGM;
    public AudioSource DungeonBGM;
    public AudioSource BossBGM;
    public List<AudioSource> ListBGM;
    public List<AudioSource> ListFx;
    private static SoundManager _instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton UIMaSoundManagernager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
        }
        else
        {
            _instance = this;
        }
    }
    bool bPlay = false;
    int rand = 0;
    public void MuteByAds()
    {
        if(GameManager.Instance.isFx == false)
        {
            AdsSound(true);
        }
    }
    public void MuteFalseAds()
    {
        if (GameManager.Instance.isFx == false)
        {
            AdsSound(false);
        }
    }
    public void PlayFX(SoundFXType soundFXType)
    {
        if (GameManager.Instance.isFx == true)
        {            
            ListFx[(int)(soundFXType)].Play();   
        }
    }
    public void PlayClick()
    {
        if (GameManager.Instance.isFx == true)
        {
            ListFx[(int)(SoundFXType.Touch)].Play();
        }
    }
  
    public void AdsSound(bool flag)
    {
        for (int i = 0; i < ListFx.Count; i++)
        {
            ListFx[i].mute = flag;
        }

        for (int i = 0; i < ListBGM.Count; i++)
        {
            ListBGM[i].mute = flag;
        }
    }
    public void ChangeBoss(bool flag)
    {
        if (GameManager.Instance.isBGM == true)
        {
            for (int i = 0; i < ListBGM.Count; i++)
            {
                ListBGM[i].mute = flag;
            }
            BossBGM.Play();
            BossBGM.mute = !flag;
        }
    }
    public void ChangeAltar(bool flag)
    {
        if (GameManager.Instance.isBGM == true)
        {
            for (int i = 0; i < ListBGM.Count; i++)
            {
                ListBGM[i].mute = flag;
            }
            AltarBGM.Play();
            AltarBGM.mute = !flag;
        }
    }
    public void ChangeDungeon(bool flag)
    {
        if (GameManager.Instance.isBGM == true)
        {
            for (int i = 0; i < ListBGM.Count; i++)
            {
                ListBGM[i].mute = flag;
            }
            if (DungeonBGM.isPlaying == false)
            {
                DungeonBGM.Play();
                DungeonBGM.mute = !flag;
            }
        }
    }
    public void UpdateSound()
    {        
        if(GameManager.Instance.isBGM == true)
        {
            for (int i = 0; i < ListBGM.Count; i++)
            {
                ListBGM[i].mute = false;
            }
        
            if (bPlay == false)
            {
                rand = Random.Range(0, ListBGM.Count);
                ListBGM[rand].Play();
                StartCoroutine(BGMPlay());
                bPlay = true;
            }          
        }
        else
        {
            for (int i = 0; i < ListBGM.Count; i++)
            {
                ListBGM[i].mute = true;
            }

        }
        if (GameManager.Instance.isFx == true)
        {
            for (int i = 0; i < ListFx.Count; i++)
            {
                ListFx[i].mute = false;
            }
        }      
        else
        {

            for (int i = 0; i < ListFx.Count; i++)
            {
                ListFx[i].mute = true;
            }
           
        }
    }
    IEnumerator BGMPlay()
    {
        yield return new WaitForSeconds(1f);
        if (ListBGM[rand].isPlaying == false)
        {
            rand = Random.Range(0, ListBGM.Count);
            ListBGM[rand].Play();
        }
        StartCoroutine(BGMPlay());
    }
    void Start()
    {
        UpdateSound();
    }
}
