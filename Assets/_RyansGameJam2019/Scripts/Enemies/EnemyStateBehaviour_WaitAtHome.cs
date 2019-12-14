//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ {
public class EnemyStateBehaviour_WaitAtHome : StateBehaviour_base
{
    [SerializeField, BoxGroup("Settings"), Required] private float waitTime;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController behaviourController;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyCarryingPiece carryingPieceScript;

    private CoroutineHandle coroutine;
    
    protected override void OnEnterState()
    {
        coroutine = Timing.RunCoroutine(WaitAtHomeThenFollowBall());
        gameObject.SetActive(false);
        carryingPieceScript.EnemyIsCarryingAPiece = false;
    }
    
    protected override void OnLeaveState()
    {
        
    }

    private void OnDestroy()
    {
        coroutine.IsRunning = false;
    }

    private IEnumerator<float> WaitAtHomeThenFollowBall()
    {
        yield return Timing.WaitForSeconds(waitTime);
        
        behaviourController.SetNewEnemyState(EnemyStates.FollowBall);
        
        gameObject.SetActive(true);
    }
}
}