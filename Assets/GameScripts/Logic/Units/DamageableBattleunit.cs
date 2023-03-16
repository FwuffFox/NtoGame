using System;
using EditorScripts.Inspector;
using UnityEngine;

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

        public Action<float> OnBattleUnitGetDamage;
        public virtual void GetDamage(float damage)
        {
            if (IsDead) return;
            
            Health -= damage;
            OnBattleUnitGetDamage?.Invoke(damage);
            if (!(Health <= 0)) return;
            
            IsDead = true;
            OnlineManager.OnlineManager.Manager.SetLeaderBoard();
            OnHealthReachZero();
        }

        public Action OnBattleUnitDeath;

        protected virtual void OnHealthReachZero()
        {
            OnBattleUnitDeath?.Invoke();
        }

        public void Heal(float value) => Health += value;
        public void HealToFull() => Health = MaxHealth;

        private void OnDisable()
        {
            OnBattleUnitDeath = null;
            OnBattleUnitHealthChange = null;
            OnBattleUnitMaxHealthChange = null;
        }
        
#if UNITY_EDITOR
        [InspectorButton(nameof(HealToFull))]
        [SerializeField] private bool _healButton;

        [InspectorButton(nameof(TestDamage))]
        [SerializeField] private bool _damageButton;

        public void TestDamage() => GetDamage(1);
#endif
    }
}