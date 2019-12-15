using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace RGJ{
[RequireComponent(typeof(ObjectSpawner))]
public class ObjectSpawnAmountController : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private int maxAmount = 5;
    [SerializeField, BoxGroup("Settings"), Required] private float increasePerTile;
    [SerializeField, BoxGroup("Atom Events"), Required] private Vector3Event onGroundTileChangedPosition;
    [SerializeField, ReadOnly] private ObjectSpawner objectSpawner;
    
    private void OnEnable() => onGroundTileChangedPosition.Register(UpdateAmount);
    private void OnDisable() => onGroundTileChangedPosition.Unregister(UpdateAmount);

    private void UpdateAmount(Vector3 _position)
    {
        var currentAmount = objectSpawner.objectAmountPerTile;
        var initialAmount = objectSpawner.initialObjectAmountPerTile;
        var newAmount = initialAmount + (_position.y / 19.2f) * increasePerTile;
        var newAmountClamped = Mathf.RoundToInt(Mathf.Clamp(newAmount, 0f, maxAmount));

        objectSpawner.objectAmountPerTile = newAmountClamped;
    }

    private void OnValidate()
    {
        objectSpawner = GetComponent<ObjectSpawner>();
    }
}
}