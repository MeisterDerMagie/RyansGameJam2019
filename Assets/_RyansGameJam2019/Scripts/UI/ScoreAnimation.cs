using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class ScoreAnimation : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private Animator scorePopAnimator;
    [SerializeField, BoxGroup("Atom Events"), Required] private IntEvent onCollectedPiecesChanged;

    private void OnEnable() => onCollectedPiecesChanged.Register(OnCollectedPiecesChanged);
    private void OnDisable() => onCollectedPiecesChanged.Unregister(OnCollectedPiecesChanged);

    private void OnCollectedPiecesChanged()
    {
        scorePopAnimator.SetTrigger("ScorePop");
    }
}
}