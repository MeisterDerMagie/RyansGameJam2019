using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class EnemySpawner : MonoBehaviour
{
    [SerializeField, BoxGroup("Settings"), Required] private float activationRadius;
    [SerializeField, BoxGroup("Values"), ReadOnly] private bool spawnerIsActive;
    [SerializeField, BoxGroup("Values"), ReadOnly] private bool enemyIsAlive; 
    [SerializeField, BoxGroup("Values"), ReadOnly] private bool enemyIsOnTheWayBack;
    [SerializeField, FoldoutGroup("References"), Required, AssetsOnly] private GameObject enemyPrefab;

    private GameObject ball;
    private GameObject enemy;
    
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        
        Timing.RunCoroutine(_CheckBallDistance());
    }

    private IEnumerator<float> _CheckBallDistance()
    {
        while (true)
        {
            var heading = transform.position - ball.transform.position;
            var distanceToBall = heading.magnitude;

            spawnerIsActive = distanceToBall < activationRadius;
            
            if (spawnerIsActive)
            {
                if (!enemyIsAlive)
                {
                    SpawnEnemy();
                }
                else if (enemyIsAlive && enemyIsOnTheWayBack)
                {
                    ActivateExistingEnemy();
                }
            }
            else if (enemyIsAlive && !enemyIsOnTheWayBack)
            {
                CallBackEnemy();
            }

            yield return Timing.WaitForSeconds(0.5f);
        }
    }

    private void ActivateExistingEnemy()
    {
        Debug.Log("Activate existing enemy!");
        enemyIsOnTheWayBack = false;
        var enemyBehaviourController = enemy.GetComponent<EnemyBehaviourController>();
        enemyBehaviourController.spawnerIsActive = true;
        enemyBehaviourController.SetNewEnemyState(EnemyStates.FollowBall);
    }

    private void SpawnEnemy()
    {
        Debug.Log("Spawn new enemy!");
        
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyIsAlive = true;

        enemy.GetComponent<EnemyStateBehaviour_HeadHome>().home = gameObject;
        enemy.GetComponent<EnemyBehaviourController>().spawnerIsActive = true;;
    }

    private void CallBackEnemy()
    {
        Debug.Log("Call back enemy");
        
        enemyIsOnTheWayBack = true;
        
        var enemyBehaviourController = enemy.GetComponent<EnemyBehaviourController>();
        enemyBehaviourController.SetNewEnemyState(EnemyStates.HeadHome);
        enemyBehaviourController.spawnerIsActive = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != enemy) return;
        if (spawnerIsActive) return;
        if (enemyIsAlive) DespawnEnemy();
    }

    private void DespawnEnemy()
    {
        Debug.Log("Despawn enemy!");
        
        enemyIsAlive = false;
        enemyIsOnTheWayBack = false;
        Destroy(enemy);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
}