using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using Wichtel.UI;
using Random = UnityEngine.Random;

namespace RGJ{
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] public int objectAmountPerTile;
    [SerializeField, FoldoutGroup("References"), Required, AssetsOnly] private GameObject prefabToSpawn;
    [SerializeField, FoldoutGroup("References"), Required] private Transform blockerContainer;
    [SerializeField, BoxGroup("Atom Events"), Required] private Vector3Event onGroundTileChangedPosition;

    [SerializeField, ReadOnly] private List<Vector3> alreadyCoveredTiles = new List<Vector3>();

    [SerializeField, ReadOnly] public int initialObjectAmountPerTile;
    private Transform player;

    public Transform Player
    {
        get => player == null ? GameObject.FindGameObjectWithTag("Player").transform : player;
        set => player = value;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable() => onGroundTileChangedPosition.Register(CheckIfObjectsNeedToBeGenerated);
    private void OnDisable() => onGroundTileChangedPosition.Unregister(CheckIfObjectsNeedToBeGenerated);

    private void CheckIfObjectsNeedToBeGenerated(Vector3 _atPosition)
    {
        if (alreadyCoveredTiles.Contains(_atPosition)) return;
        if (_atPosition.y < Player.position.y) return;

        alreadyCoveredTiles.Add(_atPosition);
        GenerateNewObject(_atPosition);
    }

    private void GenerateNewObject(Vector3 _atPosition)
    {
        var randomAmountPerTile = Mathf.RoundToInt(Random.Range(objectAmountPerTile - objectAmountPerTile * 0.75f,
            objectAmountPerTile + objectAmountPerTile * 1.25f));
        
        for (int i = 0; i < randomAmountPerTile; i++)
        {
            Vector2 spawnPosition = new Vector2
            {
                x = Random.Range(_atPosition.x - 7f, _atPosition.x + 7f),
                y = Random.Range(_atPosition.y - 9.6f, _atPosition.y + 9.6f)
            };

            bool isInsideBlocker = false;
            foreach (Transform child in blockerContainer)
            {
                var blocker = child.GetComponent<RectTransform>().WorldRect();
                if (blocker.Contains(spawnPosition)) isInsideBlocker = true;
            }

            if(!isInsideBlocker) Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
        }
    }

    private void OnValidate()
    {
        initialObjectAmountPerTile = objectAmountPerTile;
    }
}
}