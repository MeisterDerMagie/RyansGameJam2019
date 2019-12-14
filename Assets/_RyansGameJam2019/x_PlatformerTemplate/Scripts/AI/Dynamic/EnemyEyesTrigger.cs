using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyEyesTrigger : MonoBehaviour
    {
        EnemyController enemyController; //Cached local Enemy Controller component 

        private void Start()
        {
            enemyController = GetComponentInParent<EnemyController>(); //Get component from object
        }
         
        private void OnTriggerEnter2D(Collider2D collision) //if some object entrer in trigger area
        {
            if (collision.gameObject.CompareTag("Player")) //if that object tap is equal to Player
            {
                if (GameManager.Instance.isGame) //is game status is not equal to gameover
                    enemyController.Follow(collision.transform); //start follow object
            }
        }

    }
}