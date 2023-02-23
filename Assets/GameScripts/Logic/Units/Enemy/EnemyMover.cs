using System;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace GameScripts.Logic.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        [FormerlySerializedAs("navMeshAgent")] [SerializeField] private NavMeshAgent _navMeshAgent;
        public Action<float> OnSpeedChange;
        private float _speed;

        public void SetProperties(EnemyData enemyData)
        {
            _speed = enemyData.speed;
        }

        public void Follow(GameObject obj)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = _speed;
            OnSpeedChange?.Invoke(_speed);
            _navMeshAgent.SetDestination(obj.transform.position);
        }
        
        public void Stop()
        {
            _navMeshAgent.speed = 0;
            OnSpeedChange?.Invoke(0);
            _navMeshAgent.isStopped = true;
        }
    }
}