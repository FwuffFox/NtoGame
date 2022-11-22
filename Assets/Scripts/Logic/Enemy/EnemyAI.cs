using System;
using StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;
        private float _speed;

        private void OnEnable()
        {
            if (!TryGetComponent(out _navMeshAgent))
                Debug.LogError("Enemy doesn't have NavMeshAgent");
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
        }

        public void SetProperties(EnemyData enemyData)
        {
            _seeRange = enemyData.seeRange;
            _attackRange = enemyData.attackRange;
            _speed = enemyData.speed;
        }

        public void Update()
        {
            var canSeePlayer = Physics.CheckSphere(transform.position, _seeRange, playerMask);
            if (canSeePlayer) ApproachPlayer();
        }

        private void ApproachPlayer()
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.destination = _player.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _seeRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}