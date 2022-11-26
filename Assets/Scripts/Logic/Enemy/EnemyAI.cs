using System;
using System.Collections;
using Logic.Player;
using StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    [RequireComponent(typeof(EnemyMover), typeof(EnemyAttacker))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;
        // [SerializeField] private Transform _raycaster;
        private EnemyMover _enemyMover;
        private EnemyAttacker _enemyAttacker;

        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;

        private void OnEnable()
        {
            _enemyMover = GetComponent<EnemyMover>();
            _enemyAttacker = GetComponent<EnemyAttacker>();
        }

        public void SetPlayer(GameObject player)
        {
            _player = player;
            _playerHealth = player.GetComponent<PlayerHealth>();
        }

        public void SetProperties(EnemyData enemyData)
        {
            _seeRange = enemyData.seeRange;
            _attackRange = enemyData.attackRange;
        }

        public void Update()
        {
            var position = transform.position;
            var canSeePlayer = Physics.CheckSphere(position, _seeRange, playerMask);
            if (!canSeePlayer) return;
            //var isSomethingInTheWay = Physics.Linecast(_raycaster.position, _player.transform.position);
            //if (isSomethingInTheWay) return;
            var canAttackPlayer = Physics.CheckSphere(position, _attackRange, playerMask);
            if (canAttackPlayer) _enemyAttacker.AttackPlayer(_playerHealth);
            else _enemyMover.Follow(_player);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var position = transform.position;
            Gizmos.DrawWireSphere(position, _seeRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, _attackRange);
        }
    }
}