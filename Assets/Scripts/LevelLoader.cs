using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LevelLoader : MonoBehaviour
{    
    //public Text progrssText;
    bool start = false;
    public float couSpeed;
    public float speed;
    public Text m_TypingText;
    public GameObject ConnectPanel;
    public List<string> m_Messages = new List<string>();
    public float m_Speed = 0.2f;
    private void Awake()
    {

    }
    private void Start()
    {
        PlayNanooManager.Instance.CheckCheatEventHandler += Instance_CheckCheatEventHandler;
        
    }

    private void Instance_CheckCheatEventHandler(bool bChect)
    {
        if(bChect ==false)
        {
            StartCoroutine(StartLogoInit());
            
        }
        else
        {
            ConnectPanel.SetActive(true);
        }
    }

    IEnumerator StartLogoInit()
    {
        yield return new WaitForSeconds(0.5f);
        LoadLevel("Game");    
    }
   
    public void LoadSecen()
    {
        LoadLevel("Game");      
    }

    public void LoadLevel(string nameScene)
    {
        if (start == false)
        {

            StartCoroutine(LoadAsynchronously(nameScene));
            start = true;
        }
    }


    IEnumerator LoadAsynchronously(string nameScen)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation opertation = SceneManager.LoadSceneAsync(nameScen);
        //AsyncOperation opertation =  Application.LoadLevelAsync(0);

        while (!opertation.isDone)
        {
            for (int k = 0; k < m_Messages.Count; k++)
            {
                for (int i = 0; i < m_Messages[k].Length; i++)
                {
                    m_TypingText.text = m_Messages[k].Substring(0, i + 1);
                    yield return new WaitForSeconds(speed);
                }                
            }
            yield return null;
        }
    }
    private void OnDisable()
    {
        if(PlayNanooManager.Instance!=null)
            PlayNanooManager.Instance.CheckCheatEventHandler -= Instance_CheckCheatEventHandler;
    }

    float GetResolution(int width, int height)
    {
        float scRatio = (float)width / (float)height;
        float scRound = Mathf.Round(scRatio * 100.0f);
        scRound = scRound / 100f;
        return scRound;
    }
}
