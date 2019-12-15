using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class ToggleGameObjectOnAtomsEvent : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private GameObject gameObjectToToggle;
    [SerializeField, BoxGroup("Settings"), Required] private bool gameObjectNewState;
    [SerializeField, BoxGroup("Atom Events"), Required] private AtomEvent atomEvent;

    private void OnEnable() => atomEvent.Register(OnAtomEvent);
    private void OnDisable() => atomEvent.Unregister(OnAtomEvent);


    private void OnAtomEvent()
    {
        gameObjectToToggle.SetActive(gameObjectNewState);
    }
}
}