//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace RGJ {
public class EnemyCarryingPiece : MonoBehaviour
{
    [SerializeField, BoxGroup("Values"), ReadOnly] private bool enemyIsCarryingAPiece;
    [SerializeField, FoldoutGroup("References"), Required, AssetsOnly] private GameObject piecePrefab;
    [SerializeField, FoldoutGroup("References"), Required] private GameObject piece;
    [SerializeField, FoldoutGroup("References"), Required] private Animator graphicAnimator;
    
    
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

        if (EnemyIsCarryingAPiece) //picked piece up
        {
            piece.transform.DOScale(new Vector3(0.6f, 0.6f, 1f), 0.85f).From().SetEase(Ease.OutElastic);
            graphicAnimator.SetTrigger("Collect");
        }
    }

    public void DropPiece()
    {
        if (!EnemyIsCarryingAPiece) return;

        var droppedPiece = Instantiate(piecePrefab, transform.position.With(y: transform.position.y + 0.3f), Quaternion.identity);
        droppedPiece.transform.DOScale(new Vector3(0.6f, 0.6f, 1f), 0.85f).From().SetEase(Ease.OutElastic);
        
        EnemyIsCarryingAPiece = false;
    }
}
}