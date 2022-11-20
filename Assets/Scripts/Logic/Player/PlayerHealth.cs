using System;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action<int> OnPlayerHealthChange;
        public int maxHealth;
        public int currentHealth;

        public void GetDamage(int damage)
        {
            currentHealth -= damage;
            OnPlayerHealthChange?.Invoke(currentHealth);
        }
    }
}