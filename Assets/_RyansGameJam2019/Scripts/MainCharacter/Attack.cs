using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class Attack : SerializedMonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private Animator attackAnimator;
    
    [SerializeField, ReadOnly] private List<IDamageable> damageablesInReach = new List<IDamageable>();

    private void Update()
    {
        if (!Input.GetButtonDown("Jump")) return;

        attackAnimator.SetTrigger("Attack");
        foreach (var damageable in damageablesInReach)
        {
            damageable.DealDamage();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherDamageable = other.GetComponent<IDamageable>();
        if(otherDamageable != null) 
            damageablesInReach.Add(otherDamageable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherDamageable = other.GetComponent<IDamageable>();
        if(otherDamageable != null && damageablesInReach.Contains(otherDamageable)) 
            damageablesInReach.Remove(otherDamageable);
    }
}
}