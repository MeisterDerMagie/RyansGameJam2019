using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Ladder : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController playerController = collision.GetComponent<PlayerController>();
                LadderEnterStatus(playerController, true); //can move up
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController playerController = collision.GetComponent<PlayerController>();
                LadderEnterStatus(playerController, false); //block move up
            }
        }

        void LadderEnterStatus(PlayerController playerController, bool status)
        {
            playerController.onLadder = status; //Set status to player
        }
    }








}