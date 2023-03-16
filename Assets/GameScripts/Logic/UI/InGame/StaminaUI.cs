using GameScripts.Logic.Units.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class StaminaUI : MonoBehaviour
    {
        [SerializeField] private Slider _staminaSlider;

        [FormerlySerializedAs("playerMovement")] public PlayerMovement PlayerMovement;

        public void SetPlayer(PlayerMovement player)
        {
            PlayerMovement = player;
            _staminaSlider.maxValue = PlayerMovement.MaxStamina;
            _staminaSlider.value = PlayerMovement.CurrentStamina;
            PlayerMovement.OnPlayerStaminaChange += SetNewStamina;
        }

        private void OnDestroy()
        {
            PlayerMovement.OnPlayerStaminaChange -= SetNewStamina;
        }

        private void SetNewStamina(float stamina)
        {
            _staminaSlider.value = stamina;
        }
    }
}
