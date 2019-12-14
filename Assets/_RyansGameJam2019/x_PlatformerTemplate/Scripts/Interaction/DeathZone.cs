using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.tag == "Player")
            {
                //if player enter to this trigger = gameover
                collision.gameObject.GetComponent<PlayerController>().Death();
            }
        }

    }
}