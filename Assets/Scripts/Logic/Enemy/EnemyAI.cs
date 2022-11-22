using System;
using System.Collections;
using Logic.Player;
using StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;
        // [SerializeField] private Transform _raycaster;
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;
        private float _speed;

        private float _damage;
        private float _attackCooldown;

        private bool _canAttack = true;

        private void OnEnable()
        {
            if (!TryGetComponent(out _navMeshAgent))
                Debug.LogError("Enemy doesn't have NavMeshAgent");
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
            _speed = enemyData.speed;
            _damage = enemyData.damage;
            _attackCooldown = enemyData.attackCooldown;
        }

        public void Update()
        {
            var position = transform.position;
            var canSeePlayer = Physics.CheckSphere(position, _seeRange, playerMask);
            if (!canSeePlayer) return;
            //var isSomethingInTheWay = Physics.Linecast(_raycaster.position, _player.transform.position);
            //if (isSomethingInTheWay) return;
            var canAttackPlayer = Physics.CheckSphere(position, _attackRange, playerMask);
            if (canAttackPlayer) AttackPlayer();
            else ApproachPlayer();
        }

        private void AttackPlayer()
        {
            if (!_canAttack) return;
            _playerHealth.GetDamage(_damage);
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }
        
        private void ApproachPlayer()
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.destination = _player.transform.position;
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