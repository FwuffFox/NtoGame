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
            bearTrapSound.Play();
            Destroy(GetComponentInParent<MeshRenderer>());
            playerHealth.GetDamage(50);
            Destroy(gameObject);
        }
    }
}
