using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
[ExecuteInEditMode]
public class FollowMainCharacter : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private Transform mainCharacter;
    private void FixedUpdate()
    {
        transform.position = mainCharacter.transform.position;
    }

    private void Update()
    {
        transform.position = mainCharacter.transform.position;
    }
}
}