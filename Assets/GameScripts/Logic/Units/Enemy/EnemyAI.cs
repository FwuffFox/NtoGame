using GameScripts.Logic.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic.Enemy
{
    [RequireComponent(typeof(EnemyMover), typeof(EnemyAttacker))]
    public class EnemyAI : MonoBehaviour
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;
		[SerializeField] private int _health=100;
		private bool _died;
		
		[FormerlySerializedAs("enemyAnimator")] [SerializeField] private EnemyAnimator _enemyAnimator;
        [FormerlySerializedAs("enemyMover")] [SerializeField] private EnemyMover _enemyMover;
        [FormerlySerializedAs("enemyAttacker")] [SerializeField] private EnemyAttacker _enemyAttacker;

        [SerializeField] private LayerMask playerMask;
        
        private float _seeRange;
        private float _attackRange;

		public void SetDamage(int damage) 
		{
			_health-=damage;
			if (_health > 0 || _died) return;
			_died=true;
			_player.GetComponent<PlayerMoney>().AddMoney(100);
			_enemyAnimator.SetDeath();
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
	        if (_died) return;
	        var position = transform.position;
	        var canSeePlayer = Physics.CheckSphere(position, _seeRange, playerMask);
	        if (!canSeePlayer) return;
	        //var isSomethingInTheWay = Physics.Linecast(_raycaster.position, _player.transform.position);
	        //if (isSomethingInTheWay) return;
	        var canAttackPlayer = Physics.CheckSphere(position, _attackRange, playerMask);
	        if (canAttackPlayer)
	        {
		        _enemyAttacker.AttackPlayer(_playerHealth);
		        _enemyMover.Stop();
	        }
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