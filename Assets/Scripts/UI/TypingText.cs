using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypingText : MonoBehaviour
{
    public Text m_TypingText;
    public List<string> m_Messages = new List<string>();
    public float m_Speed = 0.2f;

    // Start is called before the first frame update 
    void Start()
    {
        

        StartCoroutine(Typing(m_TypingText, m_Messages, m_Speed));
    }

    IEnumerator Typing(Text typingText, List<string> message, float speed)
    {
        for(int k =0; k < message.Count; k++)
        {
            for (int i = 0; i < message[k].Length; i++)
            {
                typingText.text = message[k].Substring(0, i + 1);
                yield return new WaitForSeconds(speed);
            }
        }
        
    }
}
