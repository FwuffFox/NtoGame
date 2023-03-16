using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Units.Enemy
{
    [RequireComponent(typeof(EnemyMover),
        typeof(EnemyAttacker),
        typeof(EnemyHealth))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;
        
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private EnemyAttacker _enemyAttacker;

        public EnemyHealth EnemyHealth;
        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;

        public void SetPlayer(GameObject player)
        {
            _player = player;
            _playerHealth = player.GetComponent<PlayerHealth>();
            EnemyHealth.PlayerMoney = player.GetComponent<PlayerMoney>();
        }

        public void SetProperties(EnemyData enemyData)
        {
            _seeRange = enemyData.seeRange;
            _attackRange = enemyData.attackRange;
        }

        public void Update()
        {
	        if (EnemyHealth.IsDead) return;
            var position = transform.position;
	        var canSeePlayer = Physics.CheckSphere(position, _seeRange, playerMask);
            if (canSeePlayer)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, _player.transform.position - transform.position, 2*Time.deltaTime,0.0f);
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
                var canAttackPlayer = Physics.CheckSphere(position, _attackRange, playerMask);
                if (canAttackPlayer)
                {
                    _enemyAttacker.AttackPlayer(_playerHealth);
                    _enemyMover.Stop();
                }
                else _enemyMover.Follow(_player);
            }
            else _enemyMover.Stop();
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