using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Combat : MonoBehaviour
    {
        [HideInInspector] public CombatTrigger combatTrigger;

        [HideInInspector] public Stats stats; //Data component

        public GameObject rangeWeaponPrefab;
        public Transform rangeWeaponStartPos; //Weapon spawn position

        public Collider2D ColliderDetected { get; set; } //Here we write the collider which was detected

        //Hit event
        public delegate void HitInfo();
        public HitInfo hitInfo;

        public DoubleFloat damageRange; //min and max damage

        public bool isPlayer; //Check player or not, set in inspector

        public virtual void Start()
        {
            stats = GetComponentInParent<Stats>();

            combatTrigger = GetComponentInChildren<CombatTrigger>();

            combatTrigger.combat = this; //Cache combat in combatTrigger.cs
            combatTrigger.OnHit += HitDetected; //Add event
        }

        //Method to call Hit event
        public virtual void HitDetected()
        {
            hitInfo();
        }
        //Melee attack method
        public virtual void MeleeAttack(Stats targetStats, float damage)
        {
            targetStats.GetDamage(damage);
        }
        //Range attack method
        public virtual void RangeAttack()
        {
            GameObject rangeWeaponGO = Instantiate(rangeWeaponPrefab, rangeWeaponStartPos.position, Quaternion.identity); //spawn weapon

            float rotAngle = Mathf.Acos(transform.localScale.x) * Mathf.Rad2Deg; //calculate rotate angle by rotate our gameobject
            rangeWeaponGO.transform.Rotate(new Vector3(0, rotAngle, 0)); //rotate

            RangeWeapon rangeWeapon = rangeWeaponGO.GetComponent<RangeWeapon>(); 

            rangeWeapon.damage = damageRange.Random(); //Get random damage
            rangeWeapon.fromPlayer = isPlayer;
        }

    }
}