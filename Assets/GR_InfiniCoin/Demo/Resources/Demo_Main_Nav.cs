using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo_Main_Nav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool loading = false;
    public void LoadScene(string scene)
    {
        if (loading)
            return;

        _ = SceneManager.LoadSceneAsync(scene);
    }
}
