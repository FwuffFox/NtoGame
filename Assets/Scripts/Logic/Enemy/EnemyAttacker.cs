using System.Collections;
using Logic.Player;
using StaticData.ScriptableObjects;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        private float _damage;
        private float _attackCooldown;

        private bool _canAttack = true;

        public void SetProperties(EnemyData enemyData)
        {
            _damage = enemyData.damage;
            _attackCooldown = enemyData.attackCooldown;
        }
        
        public void AttackPlayer(GameObject player)
        {
            if (!_canAttack) return;
            player.GetComponent<PlayerHealth>().GetDamage(_damage);
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }
    }
}