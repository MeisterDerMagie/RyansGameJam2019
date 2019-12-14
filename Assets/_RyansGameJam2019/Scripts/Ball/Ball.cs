using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float growAmount = 0.15f;
    [SerializeField, BoxGroup("Values"), Required] private IntReference collectedPieces;
    [SerializeField, BoxGroup("Atom Events"), Required] private IntEvent onCollectedPiecesChanged;

    private Vector2 initialScale;
    private int initialCollectedPieces;
    
    private void OnEnable() => onCollectedPiecesChanged.Register(UpdateBallSize);
    private void OnDisable() => onCollectedPiecesChanged.Unregister(UpdateBallSize);

    private void UpdateBallSize()
    {
        var newScale = initialScale.x - ((float)initialCollectedPieces * growAmount) + ((float)collectedPieces.Value * growAmount);
        
        transform.localScale = new Vector3(newScale, newScale, 1f);
    }

    private void OnValidate()
    {
        initialScale = transform.localScale;
        initialCollectedPieces = collectedPieces._variable.InitialValue;
    }
}
