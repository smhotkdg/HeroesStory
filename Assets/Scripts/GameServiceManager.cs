using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
public class GameServiceManager : MonoBehaviour
{
    private static GameServiceManager _instance = null;

    public static GameServiceManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton GameServiceManager == null");
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
        DontDestroyOnLoad(gameObject);        
    }

    private void Start()
    {
        if (!GameServices.IsInitialized())
        {
            //GameServices.Init();
            GameServices.ManagedInit();
        }
    }

    void OnEnable()
    {
        GameServices.UserLoginSucceeded += GameServices_UserLoginSucceeded;
        GameServices.UserLoginFailed += GameServices_UserLoginFailed;
    }

    private void GameServices_UserLoginFailed()
    {
        Debug.Log("Login Failed");
    }

    private void GameServices_UserLoginSucceeded()
    {
        //Debug.Log("USER ID =: " + GameServices.LocalUser.id);
        try
        {
            UserID = GameServices.LocalUser.id;
            UserNickName = GameServices.LocalUser.userName;
        }
        catch
        {
            UserID = string.Empty;
            UserNickName = string.Empty;
        }
    }
    // Event handlers
    public string UserID = string.Empty;
    public string UserNickName = string.Empty;

}
