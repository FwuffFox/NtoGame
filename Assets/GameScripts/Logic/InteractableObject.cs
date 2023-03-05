using System;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractableObject : MonoBehaviour
    {
        public void ActivateObject(PlayerInteractions playerInteractions)
        {
            playerInteractions.ReadyToInteract = true;
            playerInteractions.InteractableObject = this;
        }

        public virtual void Interact()
        {
            
        }
    }
}