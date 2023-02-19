using System;
using GameScripts.Logic.Debuffs;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Traps
{
    public class BearTrap : MonoBehaviour
    {
        [SerializeField] private AudioSource _bearTrapSound;
        [SerializeField] private Animator _animator;
        
        [Serializable]
        private struct Bleeding
        {
            public int duration;
            public float damagePerSecond;
        }

        [SerializeField] private Bleeding _bleeding;
        
        [Serializable]
        private struct SpeedDebuff
        {
            public int duration;
            public float slowness;
        }

        [SerializeField] private SpeedDebuff _speedDebuff;
        private static readonly int Close = Animator.StringToHash("Close");

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
            var debuffer = other.gameObject.GetComponent<PlayerDebuffSystem>();
            ApplyDebuffs(debuffer);
            _bearTrapSound.Play();
            playerHealth.GetDamage(10);
            _animator.SetTrigger(Close);
            Destroy(gameObject); // DELETE COLLIDER
        }

        private void ApplyDebuffs(PlayerDebuffSystem player)
        {
            var bleedingDebuff = new PeriodicalDebuff<PlayerHealth>(_bleeding.duration,
                health => health.GetDamage(_bleeding.damagePerSecond));
            var speedDebuff = new SimpleDebuff<PlayerMovement>(_speedDebuff.duration,
                movement => movement.MovementSpeedModifier *= _speedDebuff.slowness,
                movement => movement.MovementSpeedModifier /= _speedDebuff.slowness);
            player.AddDebuff(bleedingDebuff);
            player.AddDebuff(speedDebuff);
        }
    }
}
