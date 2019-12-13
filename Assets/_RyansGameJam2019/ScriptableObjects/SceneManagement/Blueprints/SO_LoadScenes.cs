//(c) copyright by Martin M. Klöckener
#pragma warning disable 0649 //field is never assigned to
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RGJ{
[CreateAssetMenu(fileName = "LoadScenes", menuName = "Scriptable Objects/Scene Management/Load Scenes", order = 0)]
public class SO_LoadScenes : ScriptableObject
{
    [SerializeField, BoxGroup("Settings")] private bool loadAdditively;
    
    [HideIf("loadAdditively")]
    [SerializeField, BoxGroup("Scenes")] private SceneReference newScene;
    [SerializeField, BoxGroup("Scenes")] private List<SceneReference> scenesToLoadAdditively;
    
    [ReadOnly] public GameObject loadingScreen;
    
    public void LoadScenes()
    {
        Timing.RunCoroutine(_LoadScenes());
    }

    private IEnumerator<float> _LoadScenes()
    {
        //Show loading screen
        GameObject loadingScreenInstance = null;
        if (loadingScreen != null)
        {
            loadingScreenInstance = Instantiate(loadingScreen);
            DontDestroyOnLoad(loadingScreenInstance);
        }
        
        //load single scene if not additively
        if(!loadAdditively)
            yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single));
        
        //load additive scenes
        foreach (var scene in scenesToLoadAdditively)
        {
            yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
        }

        //hide and clean up loading screen
        if(loadingScreenInstance != null) Destroy(loadingScreenInstance);
        loadingScreen = null;
    }
}
}