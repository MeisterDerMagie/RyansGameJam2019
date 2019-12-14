using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CombatTrigger : MonoBehaviour
    {
        public Combat combat;

        //Hit method
        public delegate void HitDetect();
        public HitDetect OnHit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            combat.ColliderDetected = collision; //Cache collider in combat
            OnHit(); //Call event
        }

    }
}