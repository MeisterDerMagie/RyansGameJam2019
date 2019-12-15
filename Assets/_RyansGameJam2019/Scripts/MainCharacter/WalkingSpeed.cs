using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel;

namespace RGJ{
public class WalkingSpeed : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float maxSpeed;
    [SerializeField, FoldoutGroup("References"), Required] private Animator animator;
    [SerializeField, FoldoutGroup("References"), Required] private Rigidbody2D characterRigidbody;
    
    void Update()
    {
        var relativeSpeed = MathW.Remap(characterRigidbody.velocity.magnitude, 0f, maxSpeed, 0f, 1f);
        
        animator.SetFloat("WalkingSpeed", relativeSpeed);
    }
}
}