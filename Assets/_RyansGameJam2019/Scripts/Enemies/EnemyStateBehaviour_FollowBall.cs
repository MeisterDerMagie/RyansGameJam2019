//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ {
public class EnemyStateBehaviour_FollowBall : StateBehaviour_base
{
    [SerializeField, BoxGroup("Settings"), Required] private float speed;
    [SerializeField, BoxGroup("Atom Values"), Required] private IntReference collectedPieces;
    [SerializeField, FoldoutGroup("References"), Required] private Rigidbody2D enemyRigidbody;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController behaviourController;
    [SerializeField, FoldoutGroup("References"), ReadOnly] private GameObject ball;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyCarryingPiece carryingPieceScript;
    [SerializeField, BoxGroup("Atom Events"), Required] private VoidEvent onPlayerLost;

    private void OnEnable()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    protected override void OnEnterState()
    {
        if (carryingPieceScript.EnemyIsCarryingAPiece) behaviourController.SetNewEnemyState(EnemyStates.HeadHome);
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;

        Vector2 dir = ball.transform.position - enemyRigidbody.transform.position;
        dir.Normalize();
        enemyRigidbody.AddForce(dir * speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsActive) return;
        
        if (!other.gameObject.CompareTag("Ball")) return;
        
        collectedPieces.Value--;
        behaviourController.SetNewEnemyState(EnemyStates.HeadHome);
        carryingPieceScript.EnemyIsCarryingAPiece = true;
        
        if(collectedPieces.Value <= 0) onPlayerLost.Raise();
    }

    protected override void OnLeaveState()
    {
        
    }
}
}