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
        [FormerlySerializedAs("animator")] public Animator Animator;
        private static readonly int PlayerNearBy = Animator.StringToHash("PlayerNearBy");

        public Action OnNpcDialogueOpen;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            if (!QuestManager.questManager.canInterectWithNpc) return;
            ActivateObject(playerInteractions);
            Animator.SetBool(PlayerNearBy, true);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            Vector3 direction = Vector3.RotateTowards(transform.forward, other.transform.position - transform.position, 20f, 0);
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            if (!QuestManager.questManager.canInterectWithNpc) return;
            ActivateObject(playerInteractions);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerInteractions>(out var playerInteractions))
                return;
            if (!QuestManager.questManager.canInterectWithNpc) return;
            DisableObject(playerInteractions);
            Animator.SetBool(PlayerNearBy, false);
        }

        public override void Interact()
        {
            OnNpcDialogueOpen?.Invoke();
            QuestManager.questManager.talked = true;
        }
    }
}