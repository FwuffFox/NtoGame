using System;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects.Dialogue;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TalkingNpc : InteractableObject
    {
        public NpcDialogueSO NpcDialogue;
        public Animator animator;
        private static readonly int playerNearBy=Animator.StringToHash("PlayerNearBy");

        public Action OnNpcDialogueOpen;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            ActivateObject(playerInteractions);
            animator.SetBool(playerNearBy, true);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            ActivateObject(playerInteractions);
            Vector3 direction = Vector3.RotateTowards(transform.forward, other.transform.position - transform.position, 20f, 0);
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            DestroyImmediate(playerInteractions);
            animator.SetBool(playerNearBy, false);
        }

        public override void Interact()
        {
            OnNpcDialogueOpen?.Invoke();
        }
    }
}