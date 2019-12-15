using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{

public enum EnemyStates{ FollowBall, HeadHome, Fight, Stunned, WaitingAtHome }

public class EnemyBehaviourController : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Values"), Required] private EnemyStates currentState;
    [SerializeField, BoxGroup("Values"), ReadOnly] public bool spawnerIsActive;
    [SerializeField, BoxGroup("Atom Events"), Required] private VoidEvent onPlayerLost;
    
    [SerializeField, FoldoutGroup("References"), Required]
    private Dictionary<EnemyStates, StateBehaviour_base> stateBehaviours;

    private bool playerLost;
    private bool enemyIsStunned;
    private EnemyStates stateAfterStun = EnemyStates.FollowBall;

    private void OnEnable() => onPlayerLost.Register(OnPlayerLost);
    private void OnDisable() => onPlayerLost.Unregister(OnPlayerLost);

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
        if (playerLost && _newState != EnemyStates.WaitingAtHome) return;
        
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

    private void OnPlayerLost()
    {
        enemyIsStunned = false;
        SetNewEnemyState(EnemyStates.HeadHome);
        playerLost = true;
    }
}
}