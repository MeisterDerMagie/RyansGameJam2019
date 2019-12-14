using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class MainMenuManager : MonoBehaviour
    {
        //Simle main menu script
        //Nothing unusual

        public void NewGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}