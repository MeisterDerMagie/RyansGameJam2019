using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class UIManager : MonoBehaviour
    {
        public enum ScreenState { Game, Pause, Lose }

        public static UIManager Instance;

        [Header("Components")]
        public GameObject gameScreen, pauseScreen, loseScreen;

        public ScreenState screenState;

        public Image hpBar;

        public GameObject mobileUI;

        void SingletonInit()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        void Awake()
        {
            SingletonInit();
        }

        private void Start()
        {
#if UNITY_ANDROID || UNITY_IOS
            mobileUI.SetActive(true); //if mobile platform, mobile UI'll turn on
#endif

            UpdateHP(GameManager.Instance.playerStats.statsData.HP); //clear UI
        }

        //Player hp update method
        public void UpdateHP(float hpValue)
        {
            hpBar.fillAmount = hpValue / 100;
        }
        //Method to start new game from menu
        public void NewGame()
        {
            Time.timeScale = 1; //return time
            SceneManager.LoadScene("GameScene"); //Reload scene
        }

        //Method calls pause from UI
        public void Pause()
        {
            ChangeScreen(ScreenState.Pause);
        }
        public void UnPause()
        {
            ChangeScreen(ScreenState.Game);
        }
        //Method for change screen
        public void ChangeScreen(ScreenState screenState)
        {
            switch (screenState)
            {
                case ScreenState.Game: //if game
                    //turn off other and turn on game
                    gameScreen.SetActive(true);
                    pauseScreen.SetActive(false);

                    //Disable pause
                    GameManager.Instance.isPause = false;
                    //return normal time
                    Time.timeScale = 1;
                    break;
                case ScreenState.Pause:
                    gameScreen.SetActive(false);
                    pauseScreen.SetActive(true);

                    GameManager.Instance.isPause = true;
                    Time.timeScale = 0;
                    break;
                case ScreenState.Lose:
                    loseScreen.SetActive(true);
                    Time.timeScale = 0;
                    break;
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.Instance.isPause)
                    ChangeScreen(ScreenState.Game);
                else
                    ChangeScreen(ScreenState.Pause);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}