﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class MainCharacterMovement : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float maxSpeed;
    [SerializeField, BoxGroup("Settings"), Required] private float speed;
    [SerializeField, BoxGroup("Settings"), Required] private float stopFactor = 0.2f;
    [SerializeField, FoldoutGroup("References"), Required, ReadOnly] private Rigidbody2D rigidbody;

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical   = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) < 0.05f && Mathf.Abs(vertical) < 0.05f) //slow down if nothing is pressed
        {
            rigidbody.AddForce(-stopFactor * rigidbody.velocity);
            return;
        } 
        
        var forceX = Time.fixedDeltaTime * speed * horizontal;
        var forceY = Time.fixedDeltaTime * speed * vertical;
        forceX = Mathf.Clamp(forceX, -maxSpeed, maxSpeed);
        forceY = Mathf.Clamp(forceY, -maxSpeed, maxSpeed);
        
        rigidbody.AddForce(new Vector2(forceX, forceY));
    }


    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
}