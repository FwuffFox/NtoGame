using System;
using System.Collections;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic.Campfire
{
    public enum CampfireType
    {
        Start,
        Checkpoint,
        Final
    }
    
    public class Campfire : InteractableObject
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _audio;
        
        private bool _goToCampfireQm = false;
        private bool _playerNear = false;

        public CampfireType Type;

        public Action OnCampfireInteracted;
        public Action OnFinalCampfireReached;

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other)) return;
            //send quest
            if (!_goToCampfireQm)
            {
                _goToCampfireQm = true;
                QuestManager.questManager.goToCoster += 1;
            }
            //torch
            if (!QuestManager.questManager.haveTorch) return;
            if (!_playerNear)
            {
                _playerNear = true;
                QuestManager.questManager.fireCoster = true;
            }
            if (Type == CampfireType.Final) StartCoroutine(OnFinalReach());
            if (Type == CampfireType.Checkpoint)
            {
                var position = transform.position;
                PlayerPrefs.SetFloat("CheckpointX", position.x + 1);
                PlayerPrefs.SetFloat("CheckpointZ", position.z + 1);
            }
            _particle.Play();
            _audio.Play();
            if (QuestManager.questManager.talked)
                ActivateObject(other.GetComponent<PlayerInteractions>());
        }

        private IEnumerator OnFinalReach()
        {
            yield return new WaitForSeconds(5);
            PlayerPrefs.SetFloat("CheckpointX", 0);
            PlayerPrefs.SetFloat("CheckpointZ", 0);
            OnFinalCampfireReached?.Invoke();
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (!IsPlayer(other)) return;
            if (!QuestManager.questManager.haveTorch) return;
            if (!_playerNear)
            {
                _playerNear = true;
                QuestManager.questManager.fireCoster= true;
            }

            if (!_particle.isPlaying) 
                _particle.Play();
        
            if (!_audio.isPlaying)
                _audio.Play();
            if (QuestManager.questManager.talked) 
                ActivateObject(other.GetComponent<PlayerInteractions>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsPlayer(other)) return;

            _particle.Stop();
            _audio.Stop();
            DisableObject(other.GetComponent<PlayerInteractions>());
        }

        private bool IsPlayer(Collider other)
        {
            return other.TryGetComponent<PlayerMovement>(out _);
        }
        
        public override void Interact()
        {
            OnCampfireInteracted?.Invoke();
            QuestManager.questManager.interactWithCoster = true;
        }
    }
}