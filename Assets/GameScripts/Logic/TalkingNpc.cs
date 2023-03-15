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

        public Action OnNpcDialogueOpen;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            ActivateObject(playerInteractions);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            ActivateObject(playerInteractions);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            DestroyImmediate(playerInteractions);
        }

        public override void Interact()
        {
            OnNpcDialogueOpen?.Invoke();
        }
    }
}