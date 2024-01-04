using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaidTextSync : MonoBehaviour
{
    List<string> m_Messages = new List<string>();
    Text m_TypingText;
    public float speed = 0.15f;
    IEnumerator coroutine;
    private void Start()
    {
        m_Messages.Add("자동 진행 중......");
        m_TypingText = GetComponent<Text>();
        coroutine = TextRoutine();
        StartCoroutine(coroutine);        
    }
    private void OnEnable()
    {
        if(coroutine!=null)
            StartCoroutine(coroutine);
    }
    IEnumerator TextRoutine()
    {
        while (true)
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
        StopCoroutine(coroutine);
    }
}
