using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class DestroyGameObjectOnDistance : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float maxDistance;
    [SerializeField, FoldoutGroup("References"), Required] private Transform ball;
    [SerializeField, FoldoutGroup("References"), Required] private Transform player;

    private void Start()
    {
        ball   = GameObject.FindGameObjectWithTag("Ball").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        Timing.RunCoroutine(_DestroyIfTooFarAway().CancelWith(gameObject));
    }

    private IEnumerator<float> _DestroyIfTooFarAway()
    {
        while (true)
        {
            //distance to player
            var heading = transform.position - player.position;
            var distanceToPlayer = heading.magnitude;
            
            //distance to ball
            heading = transform.position - ball.position;
            var distanceToBall = heading.magnitude;

            bool objectIsBehindPlayer = transform.position.y < player.position.y;
            bool objectIsBehindBall   = transform.position.y < ball.position.y;
            
            if(distanceToBall > maxDistance && distanceToPlayer > maxDistance && objectIsBehindPlayer && objectIsBehindBall)
                Destroy(gameObject);

            yield return Timing.WaitForSeconds(0.8f);
        }
    }
}
}