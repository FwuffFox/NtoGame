using System;
using UnityEngine;

namespace StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "StaticData/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        
        public float seeRange;
        public float attackRange;
        public float speed;

        public float damage;
        public float attackCooldown;
    }
}