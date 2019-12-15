using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float growAmount = 0.15f;
    [SerializeField, BoxGroup("Values"), Required] private IntReference collectedPieces;
    [SerializeField, BoxGroup("Atom Events"), Required] private IntEvent onCollectedPiecesChanged;

    [SerializeField, ReadOnly] private Vector2 initialScale;
    [SerializeField, ReadOnly] private int initialCollectedPieces;
    
    private void OnEnable() => onCollectedPiecesChanged.Register(UpdateBallSize);
    private void OnDisable() => onCollectedPiecesChanged.Unregister(UpdateBallSize);

    private void UpdateBallSize()
    {
        Debug.Log("initialScale = " + initialScale.x);
        var newScale = initialScale.x - ((float)initialCollectedPieces * growAmount) + ((float)collectedPieces.Value * growAmount);
        
        var newScaleVector = new Vector3(newScale, newScale, 1f);
        transform.DOScale(newScaleVector, 0.8f).SetEase(Ease.OutElastic);
    }

    private void OnValidate()
    {
        initialScale = transform.localScale;
        collectedPieces._variable.Reset();
        initialCollectedPieces = collectedPieces._variable.InitialValue;
    }
}
