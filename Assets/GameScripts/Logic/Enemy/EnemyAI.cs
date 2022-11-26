using GameScripts.Logic.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Enemy
{
    [RequireComponent(typeof(EnemyMover), typeof(EnemyAttacker))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;

        [SerializeField] private EnemyMover enemyMover;
        [SerializeField] private EnemyAttacker enemyAttacker;

        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;

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
            if (canAttackPlayer) enemyAttacker.AttackPlayer(_playerHealth);
            else enemyMover.Follow(_player);
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