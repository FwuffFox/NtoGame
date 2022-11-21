using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action OnPlayerDeath;
        
        public float maxHealth;

        public Action<float> OnPlayerHealthChange;
        private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value >= maxHealth ? maxHealth : value;
                OnPlayerHealthChange?.Invoke(_currentHealth);
            }
        }
        
        public float healthRegeneratedPerSecond;

        private void OnEnable()
        {
            StartCoroutine(RegenerationCoroutine());
        }

        public void GetDamage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                var animation = GetComponent<Animation>();
                animation.clip = animation["die"].clip;
                animation.Play();
                OnPlayerDeath?.Invoke();
            }
        }

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                CurrentHealth += healthRegeneratedPerSecond;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(RegenerationCoroutine());
        }
    }
}