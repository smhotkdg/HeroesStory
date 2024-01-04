using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDisable : MonoBehaviour
{
    public void EndAnimation()
    {
        gameObject.SetActive(false);
    }
}
