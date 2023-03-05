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

        private void Update()
        {
            if (Input.GetKey(KeyCode.E))
                InteractionButtonPressed();
        }

        public void InteractionButtonPressed()
        {
            InteractableObject.Interact();
        }
    }
}