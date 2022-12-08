using EditorScripts.Inspector;
using GameScripts.Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Text _healthText;

        [SerializeReadOnly] public PlayerHealth playerHealth;

        public void SetPlayer(PlayerHealth player)
        {
            playerHealth = player;
            _healthSlider.maxValue = playerHealth.MaxHealth;
            _healthSlider.value = playerHealth.CurrentHealth;
            playerHealth.OnPlayerHealthChange += SetNewHealth;
            playerHealth.OnPlayerMaxHealthChange += SetNewMaxHealth;
        }

        private void OnDestroy()
        {
            playerHealth.OnPlayerHealthChange -= SetNewHealth;
        }

        private void SetNewHealth(float health)
        {
            _healthSlider.value = health;
            _healthText.text = $"{health}/{playerHealth.MaxHealth}";
        }
        
        private void SetNewMaxHealth(float health)
        {
            _healthSlider.maxValue = health;
            _healthText.text = $"{playerHealth.CurrentHealth}/{health}";
        }
    }
}
