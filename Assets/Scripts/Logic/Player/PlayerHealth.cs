using System;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action<int> OnPlayerHealthChange;
        public Action OnPlayerDeath;
        
        public int maxHealth;
        public int currentHealth;

        public void GetDamage(int damage)
        {
            currentHealth -= damage;
            OnPlayerHealthChange?.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                var animation = GetComponent<Animation>();
                animation.clip = animation["die"].clip;
                animation.Play();
                OnPlayerDeath?.Invoke();
            }
        }
    }
}