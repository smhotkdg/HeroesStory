using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public delegate void EndAnimEvent();
    public event EndAnimEvent EndAnimEventHandler;


    public delegate void MidlleAnimEvent();
    public event MidlleAnimEvent MidlleAnimEventHandler;

    public void EndAnim()
    {
        EndAnimEventHandler?.Invoke();
    }
    public void MiddleEvent()
    {
        MidlleAnimEventHandler?.Invoke();
    }
}
