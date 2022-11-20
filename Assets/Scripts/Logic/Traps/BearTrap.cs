using System;
using Logic.Player;
using UnityEngine;

namespace Logic.Traps
{
    public class BearTrap : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
            playerHealth.GetDamage(50);
            Destroy(gameObject);
        }
    }
}
