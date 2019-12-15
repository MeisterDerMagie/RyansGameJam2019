using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using RGJ;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallCollectPieces : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Values"), Required] private IntVariable collectedPieces;
    [SerializeField, FoldoutGroup("References"), Required] private SO_Collectibles pieceType;
    [SerializeField, FoldoutGroup("References"), Required] private AudioSource collectPieceSound;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger entered!");
        var otherCollectible = other.GetComponent<ICollectible>();
        
        if(otherCollectible == null) return;
        Debug.Log("other is collectible!");

        if (otherCollectible.CollectibleType != pieceType) return;
        Debug.Log("other is a piece!");
        Destroy(other.gameObject);
        collectedPieces.Value++;

        collectPieceSound.pitch = Random.Range(0.75f, 1.25f);
        collectPieceSound.Play();
    }
}
