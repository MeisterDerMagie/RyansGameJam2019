using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class TurretController : MonoBehaviour
    {
        public GameObject weaponPrefab; 
        public Transform shootStartPos; //weapon spawn position

        public DoubleFloat damageRange; //min and max damage

        public float shootRate;
        float shootRateTimer; //local rate timer

        private void FixedUpdate()
        {
            Shoot();
        }

        //Shoot method
        void Shoot()
        {
            if (shootRateTimer > 0)
            {
                shootRateTimer -= Time.deltaTime;
            }
            else
            {
                GameObject weapon = Instantiate(weaponPrefab, shootStartPos.position, shootStartPos.rotation); //Spawn weapon

                RangeWeapon rangeWeapon = weapon.GetComponent<RangeWeapon>(); //Get component
                rangeWeapon.fromPlayer = false; 
                rangeWeapon.damage = damageRange.Random(); //Get damage value

                shootRateTimer = shootRate; //set timer
            }
        }
    }
}