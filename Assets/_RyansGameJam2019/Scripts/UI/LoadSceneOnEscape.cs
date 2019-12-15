using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class LoadSceneOnEscape : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private SCN_LoadScenes sceneLoader;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) sceneLoader.LoadScenes();
    }
}
}