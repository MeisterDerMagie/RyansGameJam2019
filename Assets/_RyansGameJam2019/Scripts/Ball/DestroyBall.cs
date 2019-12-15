using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class DestroyBall : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Events"), Required] private VoidEvent onPlayerLost;

    private void OnEnable() => onPlayerLost.Register(OnPlayerLost);
    private void OnDisable() => onPlayerLost.Unregister(OnPlayerLost);


    private void OnPlayerLost()
    {
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InCubic);
    }
}
}