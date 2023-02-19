using System;
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
        [SerializeField] private Image _background;
        [SerializeField] private Image _fill;

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
            ManageColors();
        }

        void ManageColors()
        {
            if (Math.Abs(playerHealth.MaxHealth - 1) < 1)
            {
                _fill.color = Color.black;
                _background.color = Color.black;
                return;
            }
            switch (playerHealth.CurrentHealth)
            {
                case < 50:
                    _fill.color = Color.red;
                    _background.color = Color.red + Color.black;
                    return;
                case < 80:
                    _fill.color = Color.yellow;
                    _background.color = Color.yellow + Color.black;
                    return;
                default:
                    _fill.color = Color.green;
                    _background.color = Color.green + Color.black;
                    break;
            }
        }
        
        private void SetNewMaxHealth(float health)
        {
            _healthSlider.maxValue = health;
            _healthText.text = $"{playerHealth.CurrentHealth}/{health}";
            ManageColors();
        }
    }
}
