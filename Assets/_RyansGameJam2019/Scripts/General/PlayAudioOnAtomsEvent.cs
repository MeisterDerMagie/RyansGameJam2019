using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class PlayAudioOnAtomsEvent : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private AudioSource audioSourceToPlay;
    [SerializeField, BoxGroup("Atom Events"), Required] private AtomEvent atomEvent;

    private void OnEnable()
    {
        atomEvent.Register(PlaySound);
    }

    private void OnDisable()
    {
        atomEvent.Unregister(PlaySound);
    }


    private void PlaySound()
    {
        audioSourceToPlay.Play();
    }
}
