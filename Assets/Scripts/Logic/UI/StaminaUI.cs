using Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] private Slider staminaSlider;

        public PlayerMovement playerMovement;

        public void SetPlayer(PlayerMovement player)
        {
            playerMovement = player;
            staminaSlider.maxValue = playerMovement.maxStamina;
            staminaSlider.value = playerMovement.CurrentStamina;
            playerMovement.OnPlayerStaminaChange += SetNewStamina;
        }

        private void OnDestroy()
        {
            playerMovement.OnPlayerStaminaChange -= SetNewStamina;
        }

        private void SetNewStamina(float stamina)
        {
            staminaSlider.value = stamina;
        }
    }
}
