//(c) copyright by Martin M. Klöckener
#pragma warning disable 0649 //field is never assigned to
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ {
public class SCN_LoadScenes : MonoBehaviour
{
    [SerializeField, BoxGroup("Scene Loader"), Required] private SO_LoadScenes sceneLoader;
    [SerializeField, BoxGroup("Optional loading screen"), AssetsOnly] private GameObject loadingScreen_Prefab;

    [Button]
    public void LoadScenes()
    {
        if (loadingScreen_Prefab != null) sceneLoader.loadingScreen = loadingScreen_Prefab;
        sceneLoader.LoadScenes();
    }
}
}