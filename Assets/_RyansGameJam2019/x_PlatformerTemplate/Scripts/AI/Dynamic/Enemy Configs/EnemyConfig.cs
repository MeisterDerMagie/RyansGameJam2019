using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    //This script for creating configs of enemy
    //You can create new config and drap to your enemy

    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Enemy/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        //Your settings
        public float HP; 
        public DoubleFloat damageRange;
    }
}
