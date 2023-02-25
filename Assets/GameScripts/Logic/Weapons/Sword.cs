using GameScripts.Logic.Units.Enemy;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic.Weapons
{
    [RequireComponent(typeof(Collider))]
    public class Sword : MonoBehaviour
    {
        [SerializeField] private Collider _weaponCollider;
        [Range(0f, 2f)] [SerializeField] private float _damageMult = 1f;
        
        public PlayerAttack PlayerAttack { get; set; }
        public bool IsColliderActive
        {
            get => _weaponCollider.enabled;
            set => _weaponCollider.enabled = value;
        }

        private void OnEnable()
        {
            _weaponCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyHealth>(out var enemy))
            {
                enemy.GetDamage((int) (PlayerAttack.Damage * _damageMult));
            }
        }
    }
}