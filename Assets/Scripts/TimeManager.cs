using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance = null;
    public System.DateTime OldTime;

    public delegate void GetTimeEvent(float time);
    public event GetTimeEvent getTImeEventHandler;

    public static TimeManager Instance
    {
        get
        {
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
            OldTime = System.DateTime.UtcNow;
            LoadData();
        }        
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            SaveData();

        }
        else
        {
            OldTime = System.DateTime.UtcNow;
            LoadData();

        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    public void SaveData()
    {
        if (GameManager.Instance == null)
            return;
       
        OldTime = System.DateTime.UtcNow;
        AddTimer("AppQuitTime");
        bLoad = false;
        //Debug.Log("save Time");
    }
    bool bLoad = false;
    void LoadData()
    {
        float Time = (float)GetTime("AppQuitTime");
        if(getTImeEventHandler == null)
        {
            StartCoroutine(SetTimeRoutine(Time));
        }
        else
        {
            if(bLoad ==false)
            {
                getTImeEventHandler?.Invoke(Time);
                bLoad = true;
            }
            
        }
        
    }
    IEnumerator SetTimeRoutine(float gettime)
    {
        yield return new WaitForSeconds(0.2f);
        if (getTImeEventHandler == null)
        {
            StartCoroutine(SetTimeRoutine(gettime));
        }
        else
        {
            if (bLoad == false)
            {
                getTImeEventHandler?.Invoke(gettime);
                bLoad = true;
            }
        }
    }
    public double GetTime(string key)
    {
        System.TimeSpan SpanTime;
        SpanTime = CheckTimer(key);
        if (SpanTime != System.TimeSpan.Zero)
        {
            return SpanTime.TotalSeconds;
        }
        else
        {
            return 0;
        }
    }

    System.TimeSpan CheckTimer(string name)
    {
        System.TimeSpan AAA = System.TimeSpan.Zero;
        string key = name;
        string startTimeStr = PlayerPrefs.GetString(key);
        if (startTimeStr != "")
        {
            System.DateTime start = System.DateTime.Parse(startTimeStr);
            System.DateTime LoginTime = start;
            AAA = OldTime - LoginTime;
            return AAA;
        }
        else
        {
            return AAA;
        }
    }
    public void AddTimer(string key)
    {
        string timerKeyStr = key;
        string now = System.DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
        PlayerPrefs.SetString(timerKeyStr, now);
        PlayerPrefs.Save();

    }
}
