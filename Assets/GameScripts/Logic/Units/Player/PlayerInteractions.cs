using System;
using UnityEngine;

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

        private PlayerInputActions _playerInput;

        private void OnEnable()
        {
            _playerInput = new PlayerInputActions();
            _playerInput.InGame.Enable();
            _playerInput.InGame.InteractButton.performed 
                += _ => InteractionButtonPressed();
        }

        private void InteractionButtonPressed()
        {
            InteractableObject.Interact();
        }
    }
}