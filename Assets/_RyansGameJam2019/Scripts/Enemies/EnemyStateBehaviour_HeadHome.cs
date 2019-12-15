//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ {
public class EnemyStateBehaviour_HeadHome : StateBehaviour_base
{
    [SerializeField, BoxGroup("Settings"), Required] private float speed;
    [SerializeField, FoldoutGroup("References"), Required] private Rigidbody2D enemyRigidbody;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController behaviourController;
    [SerializeField, FoldoutGroup("References")] public GameObject home;
    
    protected override void OnEnterState()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        if (home == null || enemyRigidbody.gameObject == null) return;
        
        Vector2 dir = home.transform.position - enemyRigidbody.transform.position;
        dir.Normalize();
        enemyRigidbody.AddForce(dir * speed, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!IsActive) return;
        if (!(other.gameObject == home)) return;
        
        behaviourController.SetNewEnemyState(EnemyStates.WaitingAtHome);
    }

    protected override void OnLeaveState()
    {
        
    }
}
}