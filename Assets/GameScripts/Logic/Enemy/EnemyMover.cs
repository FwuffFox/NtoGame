using System;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace GameScripts.Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        public Action<float> OnSpeedChange;
        private float _speed;

        public void SetProperties(EnemyData enemyData)
        {
            _speed = enemyData.speed;
        }

        public void Follow(GameObject obj)
        {
            navMeshAgent.speed = _speed;
            OnSpeedChange?.Invoke(_speed);
            navMeshAgent.SetDestination(obj.transform.position);
        }
    }
}