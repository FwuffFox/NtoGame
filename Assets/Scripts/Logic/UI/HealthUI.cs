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
            healthSlider.value = playerHealth.currentHealth;
            player.OnPlayerHealthChange += SetNewHealth;
        }

        private void SetNewHealth(int health)
        {
            healthSlider.value = health;
        }
    }
}
