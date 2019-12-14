using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

       [HideInInspector] public Stats playerStats;

        [Header("Parameters")]
        public bool isGame;
        public bool isPause;

        void SingletonInit()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Awake()
        {
            SingletonInit();

            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>(); //find player data component
        }

        private void Start()
        {
            StartGame(); //when scene load game'll start
        }

        public void StartGame()
        {
            isGame = true;
        }

        //Lose method
        public void GameOver()
        {
            isGame = false; //disable game
            UIManager.Instance.ChangeScreen(UIManager.ScreenState.Lose); //change screen to lose
        }

    }
}