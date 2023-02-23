using System;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Enemy;
using UnityEngine;

namespace GameScripts.Logic.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class Sword : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField, SerializeReadOnly] private Collider _weaponCollider;

        public bool IsColliderActive
        {
            get => _weaponCollider.enabled;
            set => _weaponCollider.enabled = value;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyAI>(out var enemy))
            {
                enemy.GetDamage(_damage);
            }
        }
    }
}