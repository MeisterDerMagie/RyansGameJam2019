//(c) copyright by Martin M. Klöckener
#pragma warning disable 0649 //field is never assigned to
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class ATM_ToggleGameObjectOnAtomsEvent : MonoBehaviour, IInitSelf
{
    [SerializeField, BoxGroup("Settings"), Required] private bool isEnabled = true;
    [SerializeField, BoxGroup("Settings"), Required] private GameObject gameObjectToToggle;
    [SerializeField, BoxGroup("Settings"), Required] private bool setGameObjectToThisActiveStateOnEventTrigger;
    [SerializeField, BoxGroup("Atom Events"), Required] private AtomEvent triggerEvent;

    public void InitSelf() => triggerEvent.Register(ToggleGameObject);
    private void OnDestroy() => triggerEvent.Unregister(ToggleGameObject); 

    private void ToggleGameObject()
    {
        if (!isEnabled) return;
        
        gameObjectToToggle.SetActive(setGameObjectToThisActiveStateOnEventTrigger);
    }
    
}
}