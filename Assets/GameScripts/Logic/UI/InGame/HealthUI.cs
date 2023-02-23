using System;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
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

        [SerializeReadOnly] private PlayerHealth _playerHealth;

        public void SetPlayer(PlayerHealth player)
        {
            _playerHealth = player;
            _healthSlider.maxValue = _playerHealth.MaxHealth;
            _healthSlider.value = _playerHealth.Health;
            _playerHealth.OnBattleUnitHealthChange += SetNewHealth;
            _playerHealth.OnBattleUnitMaxHealthChange += SetNewMaxHealth;
        }

        private void SetNewHealth(float health)
        {
            _healthSlider.value = health;
            _healthText.text = $"{health}/{_playerHealth.MaxHealth}";
            ManageColors();
        }

        void ManageColors()
        {
            if (Math.Abs(_playerHealth.MaxHealth - 1) < 1)
            {
                _fill.color = Color.black;
                _background.color = Color.black;
                return;
            }
            switch (_playerHealth.Health)
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
            _healthText.text = $"{_playerHealth.Health}/{health}";
            ManageColors();
        }
    }
}
