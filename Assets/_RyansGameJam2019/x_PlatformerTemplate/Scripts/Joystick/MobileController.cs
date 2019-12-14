using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class MobileController : MonoBehaviour
    {
        public static Dictionary<string, bool> action; //Cached Dictionary with all actions from Input managers

        //My little buttons interaction, need to mobile UI
        public void ButtonDown(string button) 
        {
            action[button] = true; //button down
            StartCoroutine(IButtonUP(button));
        }

        public IEnumerator IButtonUP(string button)
        {
            yield return new WaitForEndOfFrame();
            action[button] = false; //button up
        }
    }
}