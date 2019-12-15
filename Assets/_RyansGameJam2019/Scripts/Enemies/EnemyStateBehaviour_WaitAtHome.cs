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
    [SerializeField, BoxGroup("Atom Events"), Required] private VoidEvent onPlayerLost;

    private CoroutineHandle coroutine;
    private bool playerLost;

    private void OnEnable() => onPlayerLost.Register(OnPlayerLost);
    private void OnDisable() => onPlayerLost.Unregister(OnPlayerLost);

    protected override void OnEnterState()
    {
        gameObject.SetActive(false);
        coroutine = Timing.RunCoroutine(WaitAtHomeThenFollowBall());
        carryingPieceScript.EnemyIsCarryingAPiece = false;
    }
    
    protected override void OnLeaveState(){}

    private void OnDestroy()
    {
        coroutine.IsRunning = false;
    }

    private IEnumerator<float> WaitAtHomeThenFollowBall()
    {
        yield return Timing.WaitForSeconds(waitTime);

        if (!playerLost)
        {
            behaviourController.SetNewEnemyState(EnemyStates.FollowBall);
            gameObject.SetActive(true);
        }
    }

    private void OnPlayerLost()
    {
        playerLost = true;
        coroutine.IsRunning = false;
    }
}
}