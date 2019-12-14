using System;
using System.Collections;
using System.Collections.Generic;
using RGJ;
using Sirenix.OdinInspector;
using UnityEngine;

public class Stunnable : MonoBehaviour, IDamageable
{
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController enemyBehaviourController;
    public void DealDamage()
    {
        enemyBehaviourController.SetNewEnemyState(EnemyStates.Stunned);
    }

    private void OnValidate()
    {
        enemyBehaviourController = GetComponent<EnemyBehaviourController>();
    }
}
