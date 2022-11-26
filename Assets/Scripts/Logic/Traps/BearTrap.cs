using System;
using Logic.Player;
using UnityEngine;

namespace Logic.Traps
{
    public class BearTrap : MonoBehaviour
    {
        [SerializeField] private AudioSource bearTrapSound;
        private static readonly int Close = Animator.StringToHash("Close");

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
            var debuffer = other.gameObject.GetComponent<PlayerDebuffSystem>();
            var timedDebuff = new PlayerDebuffSystem.TimedDebuff(PlayerDebuffSystem.DebuffType.Speed, 5, 2);
            debuffer.AddDebuff(timedDebuff);
            bearTrapSound.Play();
            Destroy(GetComponentInParent<MeshRenderer>());
            playerHealth.GetDamage(50);
            Destroy(gameObject);
        }
    }
}
