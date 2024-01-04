using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSrc : MonoBehaviour
{
    public SmithyMainPanelSrc SmithyMainPanelSrc;
    int hammerCount = 0;
    
    private void OnEnable()
    {
        hammerCount = 0;
    }
    public void SetExpolsion()
    {        
        
        gameObject.SetActive(false);
    }
    public void MakeItme()
    {
        SmithyMainPanelSrc.GetEffect();
    }
    public void SetHammer()
    {
        hammerCount++;
        int rand = Random.Range(1, 5);
        if(hammerCount >rand)
        {
            SmithyMainPanelSrc.SetGetItemEffect();
        }
    }
}
