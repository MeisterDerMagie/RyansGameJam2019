using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        //This script is needed so that you can customize calls to the animator's methods by adding your own logic.

        Animator animator; //Cached Animator component 

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetBool(string name, bool state)
        {
            animator.SetBool(name, state);
        }

        public void SetTrigger(string name)
        {
            animator.SetTrigger(name);
        }

        public void ResetTrigger(string name)
        {
            animator.ResetTrigger(name);
        }

        public void SetInt(string name, int value)
        {
            animator.SetInteger(name, value);
        }
    }
}