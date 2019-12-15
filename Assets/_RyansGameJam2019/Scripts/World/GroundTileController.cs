using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;

namespace RGJ{
public class GroundTileController : MonoBehaviour
{
    [SerializeField, FoldoutGroup("References"), Required] private Transform groundTile1;
    [SerializeField, FoldoutGroup("References"), Required] private Transform groundTile2;
    [SerializeField, FoldoutGroup("References"), Required] private Transform groundTile3;
    [SerializeField, FoldoutGroup("References"), Required] private Transform player;
    
    private Dictionary<Transform, float> groundTileDistances = new Dictionary<Transform, float>();

    private void Start()
    {
        GetDistances();
        Timing.RunCoroutine(_UpdateGroundTilePositions());
    }

    private IEnumerator<float> _UpdateGroundTilePositions()
    {
        while (true)
        {
            GetDistances();

            foreach (var tile in groundTileDistances)
            {
                var tilePosition = tile.Key.position;
                if(tile.Value > (19.2f * 1.5f))        tilePosition = tilePosition.With(y: tilePosition.y + (19.2f * 3f));
                else if (tile.Value < (-19.2f * 1.5f)) tilePosition = tilePosition.With(y: tilePosition.y - (19.2f * 3f));

                tile.Key.position = tilePosition;
            }
            
            
            yield return Timing.WaitForSeconds(0.5f);
        }
    }

    private void GetDistances()
    {
        groundTileDistances.Clear();
        
        //distance to player
        var playerDistanceYTile1 = player.position.y - groundTile1.position.y;
        var playerDistanceYTile2 = player.position.y - groundTile2.position.y;
        var playerDistanceYTile3 = player.position.y - groundTile3.position.y;
        
        groundTileDistances.Add(groundTile1, playerDistanceYTile1);
        groundTileDistances.Add(groundTile2, playerDistanceYTile2);
        groundTileDistances.Add(groundTile3, playerDistanceYTile3);
    }
}
}