﻿using System;
using System.Collections;
using StaticData.ScriptableObjects;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Action OnPlayerDeath;
        
        private float _maxHealth;
        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }
        
        public Action<float> OnPlayerHealthChange;
        private float _currentHealth;
        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value >= _maxHealth ? _maxHealth : value;
                OnPlayerHealthChange?.Invoke(_currentHealth);
            }
        }
        
        private float _healthRegenPerSecond;

        public void SetProperties(PlayerData playerData)
        {
            MaxHealth = playerData.maxHealth;
            CurrentHealth = MaxHealth;
            _healthRegenPerSecond = playerData.healthRegenPerSecond;
        }
        
        private void OnEnable()
        {
            StartCoroutine(RegenerationCoroutine());
        }

        private bool _isDead;
        public void GetDamage(float damage)
        {
            if (_isDead) return;
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                _isDead = true;
                OnPlayerDeath?.Invoke();
            }
        }

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                CurrentHealth += _healthRegenPerSecond;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(RegenerationCoroutine());
        }
    }
}