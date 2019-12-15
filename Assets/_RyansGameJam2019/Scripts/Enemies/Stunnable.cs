using System;
using System.Collections;
using System.Collections.Generic;
using RGJ;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stunnable : MonoBehaviour, IDamageable
{
    [SerializeField, FoldoutGroup("References"), Required] private EnemyBehaviourController enemyBehaviourController;
    [SerializeField, FoldoutGroup("References"), Required] private AudioSource stunSound;
    
    public void DealDamage()
    {
        if (stunSound != null)
        {
            stunSound.pitch = Random.Range(0.8f, 1.2f);
            stunSound.Play();
        }
        enemyBehaviourController.SetNewEnemyState(EnemyStates.Stunned);
    }

    private void OnValidate()
    {
        enemyBehaviourController = GetComponent<EnemyBehaviourController>();
    }
}
