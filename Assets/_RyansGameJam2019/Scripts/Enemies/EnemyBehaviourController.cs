using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{

public enum EnemyStates{ FollowBall, HeadHome, Fight, Stunned, WaitingAtHome }

public class EnemyBehaviourController : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Values"), Required] private EnemyStates currentState;
    [SerializeField, BoxGroup("Values"), ReadOnly] public bool spawnerIsActive;
    
    [SerializeField, FoldoutGroup("References"), Required]
    private Dictionary<EnemyStates, StateBehaviour_base> stateBehaviours;

    private bool enemyIsStunned;
    private EnemyStates stateAfterStun = EnemyStates.FollowBall;

    private void Start()
    {
        SetNewEnemyState(EnemyStates.FollowBall);
    }

    public void StunEnded()
    {
        enemyIsStunned = false;
        SetNewEnemyState(stateAfterStun);
    }

    public void SetNewEnemyState(EnemyStates _newState)
    {
        Debug.Log("RequestNewState: " + _newState);
        if (enemyIsStunned)
        {
            if(_newState != EnemyStates.Stunned) stateAfterStun = _newState;
            return;
        }
        
        Debug.Log("SetNewEnemyState(" + _newState + ")");
        
        if (!spawnerIsActive && _newState == EnemyStates.FollowBall) _newState = EnemyStates.HeadHome; //head home instead going for the ball if the spawner isn't active
        
        foreach (var behaviour in stateBehaviours)
        {
            behaviour.Value.IsActive = false;
        }

        if (_newState == EnemyStates.Stunned) enemyIsStunned = true;
        
        stateBehaviours[_newState].IsActive = true;
        currentState = _newState;
    }
}
}