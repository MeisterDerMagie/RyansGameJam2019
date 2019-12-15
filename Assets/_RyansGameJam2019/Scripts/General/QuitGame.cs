using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGJ{
public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
}