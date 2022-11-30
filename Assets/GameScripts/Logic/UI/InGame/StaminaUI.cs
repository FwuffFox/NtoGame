using GameScripts.Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] private Slider _staminaSlider;

        public PlayerMovement playerMovement;

        public void SetPlayer(PlayerMovement player)
        {
            playerMovement = player;
            _staminaSlider.maxValue = playerMovement.MaxStamina;
            _staminaSlider.value = playerMovement.CurrentStamina;
            playerMovement.OnPlayerStaminaChange += SetNewStamina;
        }

        private void OnDestroy()
        {
            playerMovement.OnPlayerStaminaChange -= SetNewStamina;
        }

        private void SetNewStamina(float stamina)
        {
            _staminaSlider.value = stamina;
        }
    }
}
