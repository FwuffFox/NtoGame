using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractableObject : MonoBehaviour
    {
        protected void ActivateObject(PlayerInteractions playerInteractions)
        {
            playerInteractions.ReadyToInteract = true;
            playerInteractions.InteractableObject = this;
        }

        protected void DisableObject(PlayerInteractions playerInteractions)
        {
            playerInteractions.ReadyToInteract = false;
            playerInteractions.InteractableObject = null;
        }

        public virtual void Interact()
        {
            
        }
    }
}