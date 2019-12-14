using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Input/Input Config")]
    public class InputConfig : ScriptableObject
    {
        //Setup in inspector
        public Dictionary<string, KeyCode> Keys;
        public InputData[] inputs;

        public void UpdateDictionary()
        {
            Keys = new Dictionary<string, KeyCode>(); //Create a new instance
            Keys.Clear(); //Clearing it

            MobileController.action = new Dictionary<string, bool>(); //Create a new instance in mobile controller
            MobileController.action.Clear();

            //add all inputs in Dictionary
            for (int i = 0; i < inputs.Length; i++)
            {
                Keys.Add(inputs[i].name, inputs[i].keyCode);
                MobileController.action.Add(inputs[i].name, false);
            }
        }
    }

    [Serializable]
    public struct InputData
    {
        public string name;
        public KeyCode keyCode;
    }
}
