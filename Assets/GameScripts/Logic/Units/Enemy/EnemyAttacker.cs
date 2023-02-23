using System;
using System.Collections;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Units.Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        public Action OnAttack;
        public AudioSource Sound;
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
            Sound.Play();
            StartCoroutine(AttackCooldown());
            OnAttack?.Invoke();
        }

        private IEnumerator AttackCooldown()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }
    }
}