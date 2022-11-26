using System.Collections;
using GameScripts.Logic.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Enemy
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
        
        public void AttackPlayer(PlayerHealth player)
        {
            if (!_canAttack) return;
            player.GetDamage(_damage);
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