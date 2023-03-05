using System;
using EditorScripts.Inspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic.Units
{
    public abstract class DamageableBattleunit : MonoBehaviour
    {
        
        public Action<float> OnBattleUnitMaxHealthChange;
        [Header("Battle Unit")] [SerializeReadOnly, SerializeField] private float _maxHealth;
        
        public float MaxHealth
        {
            get => _maxHealth;
            set 
            {
                _maxHealth = value;
                if (Health > MaxHealth) Health = MaxHealth;
                OnBattleUnitMaxHealthChange?.Invoke(_maxHealth);
            }
        }
        
        public Action<float> OnBattleUnitHealthChange;
        [SerializeReadOnly, SerializeField] private float _health;
        public float Health
        {
            get => _health;
            protected set
            {
                _health = value >= MaxHealth ? MaxHealth : value;
                _health = value <= 0 ? 0 : value;
                OnBattleUnitHealthChange?.Invoke(_health);
            }
        }

        public bool IsDead { get; private set; }
        
        public virtual void GetDamage(float damage)
        {
            if (IsDead) return;
            
            Health -= damage;
            if (Health <= 0)
            {
                IsDead = true;
                OnlineManager.onlineManager.SetLeaderBoard();
                OnHealthReachZero();
            }
        }

        public Action OnBattleUnitDeath;

        protected virtual void OnHealthReachZero()
        {
            OnBattleUnitDeath?.Invoke();
        }
        
        public void HealToFull() => Health = MaxHealth;

#if UNITY_EDITOR
        [InspectorButton(nameof(HealToFull))]
        [SerializeField] private bool _healButton;
#endif
    }
}