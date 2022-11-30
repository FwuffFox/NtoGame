using System;
using GameScripts.Logic.Debuffs;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Traps
{
    public class BearTrap : MonoBehaviour
    {
        [SerializeField] private AudioSource _bearTrapSound;
        
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
            var debuffer = other.gameObject.GetComponent<PlayerDebuffSystem>();
            ApplyDebuffs(debuffer);
            _bearTrapSound.Play();
            Destroy(GetComponentInParent<MeshRenderer>());
            playerHealth.GetDamage(10);
            Destroy(gameObject);
        }

        private void ApplyDebuffs(PlayerDebuffSystem player)
        {
            var bleedingDebuff = new PeriodicalDebuff(PeriodicalDebuffType.Health, _bleeding.duration, _bleeding.damagePerSecond);
            var speedDebuff = new SimpleDebuff(SimpleDebuffType.Speed, _speedDebuff.duration, _speedDebuff.slowness);
            player.AddDebuff(bleedingDebuff);
            player.AddDebuff(speedDebuff);
        }
    }
}
