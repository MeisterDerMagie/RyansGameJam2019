//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace RGJ {
public class EnemyCarryingPiece : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required, AssetsOnly] private GameObject piecePrefab;
    [SerializeField, FoldoutGroup("References"), Required] private GameObject piece;
    [SerializeField, BoxGroup("Values"), ReadOnly] private bool enemyIsCarryingAPiece;
    public bool EnemyIsCarryingAPiece
    {
        get => enemyIsCarryingAPiece;
        set
        {
            enemyIsCarryingAPiece = value;
            UpdateCarryingState();
        }
    }

    private void Start() => UpdateCarryingState();
    
    private void UpdateCarryingState()
    {
        piece.SetActive(EnemyIsCarryingAPiece);
    }

    public void DropPiece()
    {
        if (!EnemyIsCarryingAPiece) return;

        Instantiate(piecePrefab, transform.position.With(y: transform.position.y + 0.3f), Quaternion.identity);
        EnemyIsCarryingAPiece = false;
    }
}
}