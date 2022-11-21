using System;
using Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public PlayerHealth playerHealth;

        public void SetPlayer(PlayerHealth player)
        {
            playerHealth = player;
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.CurrentHealth;
            playerHealth.OnPlayerHealthChange += SetNewHealth;
        }

        private void OnDestroy()
        {
            playerHealth.OnPlayerHealthChange -= SetNewHealth;
        }

        private void SetNewHealth(float health)
        {
            healthSlider.value = health;
        }
    }
}
