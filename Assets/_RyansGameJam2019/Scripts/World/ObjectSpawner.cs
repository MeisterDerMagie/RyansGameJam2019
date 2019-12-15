using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RGJ{
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] public int objectAmountPerTile;
    [SerializeField, FoldoutGroup("References"), Required, AssetsOnly] private GameObject prefabToSpawn;
    [SerializeField, BoxGroup("Atom Events"), Required] private Vector3Event onGroundTileChangedPosition;
    
    [SerializeField, ReadOnly] private List<Vector3> alreadyCoveredTiles = new List<Vector3>();

    [HideInInspector] public int initialObjectAmountPerTile;
    private Transform player;
    private Vector3 playerStartingPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStartingPosition = player.transform.position;
        initialObjectAmountPerTile = objectAmountPerTile;
    }

    private void OnEnable() => onGroundTileChangedPosition.Register(CheckIfObjectsNeedToBeGenerated);
    private void OnDisable() => onGroundTileChangedPosition.Unregister(CheckIfObjectsNeedToBeGenerated);

    private void CheckIfObjectsNeedToBeGenerated(Vector3 _atPosition)
    {
        if (alreadyCoveredTiles.Contains(_atPosition)) return;
        if (_atPosition.y < player.position.y) return;
        //if (_atPosition.y < playerStartingPosition.y) return;
        
        alreadyCoveredTiles.Add(_atPosition);
        GenerateNewObject(_atPosition);
    }

    private void GenerateNewObject(Vector3 _atPosition)
    {
        var randomAmountPerTile = Mathf.RoundToInt(Random.Range(objectAmountPerTile - objectAmountPerTile * 0.75f,
            objectAmountPerTile + objectAmountPerTile * 1.25f));
        
        for (int i = 0; i < randomAmountPerTile; i++)
        {
            Vector2 spawnPosition = new Vector2();
            spawnPosition.x = Random.Range(_atPosition.x - 7f, _atPosition.x + 7f);
            spawnPosition.y = Random.Range(_atPosition.y - 9.6f, _atPosition.y + 9.6f);
            
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, transform);
        }
    }
}
}