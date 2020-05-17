using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayCLick()
    {
        var loader = SceneManager.LoadSceneAsync("main");
        loader.completed += (a) => Debug.unityLogger.Log("Scene was loaded");
    }
    
}
