//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ {
public class EnemyStateBehaviour_Stunned : StateBehaviour_base
{
    [SerializeField, BoxGroup("Settings"), Required] private float stunTime;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController behaviourController;
    [SerializeField, FoldoutGroup("References"), Required] private EnemyCarryingPiece carryingPieceScript;
    
    protected override void OnEnterState()
    {
        Timing.RunCoroutine(_Stun());
        carryingPieceScript.DropPiece();
    }
    
    protected override void OnLeaveState()
    {
        
    }

    private IEnumerator<float> _Stun()
    {
        yield return Timing.WaitForSeconds(stunTime);
        
        behaviourController.StunEnded();
    }
}
}