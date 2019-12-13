//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class INIT003_Reset : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private bool resetOnSceneStart = true; 
    [SerializeField, ReadOnly] public List<IResettable> resettables = new List<IResettable>();

    private void Awake()
    {
        if (resetOnSceneStart) ResetAllChildren();
    }

    [Button, DisableInEditorMode]
    public void ResetAllChildren()
    {
        //reset all children
        GetResettables(transform);
        ResetResettables();
    }

    private void GetResettables(Transform _root)
    {
        IResettable[] newResettables = _root.GetComponents<IResettable>();
        resettables.AddRange(newResettables);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;
            GetResettables(t);
        }
    }

    private void ResetResettables()
    {
        foreach (var resettable in resettables)
        {
            resettable.ResetSelf();
        }
    }
}
}