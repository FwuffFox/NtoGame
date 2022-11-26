using GameScripts.Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public PlayerHealth playerHealth;

        public void SetPlayer(PlayerHealth player)
        {
            playerHealth = player;
            _healthSlider.maxValue = playerHealth.MaxHealth;
            _healthSlider.value = playerHealth.CurrentHealth;
            playerHealth.OnPlayerHealthChange += SetNewHealth;
        }

        private void OnDestroy()
        {
            playerHealth.OnPlayerHealthChange -= SetNewHealth;
        }

        private void SetNewHealth(float health)
        {
            _healthSlider.value = health;
        }
    }
}
