using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic.Fireplace
{
    public class Fireplace : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _audio;
    
        [SerializeReadOnly] public FireplaceType Type;
        public Action OnFinalCampfireReached;


        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other)) return;
            if (Type == FireplaceType.Final) StartCoroutine(OnFinalReach());
            if (Type == FireplaceType.Checkpoint)
            {
                PlayerPrefs.SetFloat("CheckpointX", transform.position.x + 1);
                PlayerPrefs.SetFloat("CheckpointZ", transform.position.z + 1);
            }
            _particle.Play();
            _audio.Play();
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

            if (!_particle.isPlaying) 
                _particle.Play();
        
            if (!_audio.isPlaying)
                _audio.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsPlayer(other)) return;
            _particle.Stop();
            _audio.Stop();
        }

        private bool IsPlayer(Collider collider)
        {
            return collider.TryGetComponent<PlayerMovement>(out _);
        }
    }

    public enum FireplaceType
    {
        Start,
        Checkpoint,
        Final
    }
}