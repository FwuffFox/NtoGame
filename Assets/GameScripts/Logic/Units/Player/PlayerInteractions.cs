using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameScripts.Logic.Units.Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private GameObject _interactButtonUI;
        

        private bool _readyToInteract;
        public bool ReadyToInteract
        {
            get => _readyToInteract;
            set => _interactButtonUI.SetActive(value);
        }

        public InteractableObject InteractableObject;

        private void OnEnable()
        {
            PlayerInputSystem.InGame.InteractButton.performed 
                += InteractButton_Pressed;
        }

        private void InteractButton_Pressed(InputAction.CallbackContext context)
        {
            if (InteractableObject != null)
                InteractableObject.Interact();
        }

        private void OnDisable()
        {
            PlayerInputSystem.InGame.InteractButton.performed 
                -= InteractButton_Pressed;
        }
    }
}