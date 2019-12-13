//(c) copyright by Martin M. Klöckener
#pragma warning disable 0649 //field is never assigned to
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ {
public class PlayParticleSystemOnAtomEvent : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Events"), Required] private Vector2Event vector2Event;
    [SerializeField, FoldoutGroup("References"), Required] private ParticleSystem particleSystemReference;

    private void OnEnable() => vector2Event.Register(PlayParticleSystem);
    private void OnDisable() => vector2Event.Unregister(PlayParticleSystem);

    private void PlayParticleSystem(Vector2 _position)
    {
        particleSystemReference.transform.position = _position;
        particleSystemReference.Stop();
        particleSystemReference.Play();
    }
}
}