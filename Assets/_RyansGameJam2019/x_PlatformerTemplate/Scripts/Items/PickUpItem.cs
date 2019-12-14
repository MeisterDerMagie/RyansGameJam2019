using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Collider2D))]
    public class PickUpItem : MonoBehaviour
    {
        //Pick up event
        public delegate void PickUpEvent();
        public PickUpEvent OnPickedUP;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                //if player enter in trigger event calls
                OnPickedUP();
            }
        }

    }
}
