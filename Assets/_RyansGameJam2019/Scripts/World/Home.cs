using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
public class Home : MonoBehaviour
{
    [SerializeField, BoxGroup("Atom Events"), Required] private VoidEvent onPlayerWon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        other.transform.DOMove(transform.position, 0.5f);
        
        onPlayerWon.Raise();
    }
}
}