using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class ScaleMovementDirection : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private Rigidbody2D playerRigidbody;
    [SerializeField, FoldoutGroup("References"), Required] private SpriteRenderer spriteToFlip;
    void Update()
    {
        if (playerRigidbody.velocity.x > 0.1f) spriteToFlip.flipX = false;
        if (playerRigidbody.velocity.x < 0.1f) spriteToFlip.flipX = true;
    }
}
}