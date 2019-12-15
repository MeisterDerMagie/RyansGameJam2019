using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class PlayPoof : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private GameObject poofPrefab;
    [SerializeField, BoxGroup("Atom Events"), Required] private Vector3Event onPlayPoof;

    private void OnEnable() => onPlayPoof.Register(PlayPoofAnimation);
    private void OnDisable() => onPlayPoof.Unregister(PlayPoofAnimation);

    private void PlayPoofAnimation(Vector3 _poofPosition)
    {
        Instantiate(poofPrefab, _poofPosition, Quaternion.identity, transform);
    }
}
}