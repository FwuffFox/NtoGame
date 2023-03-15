using System;
using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Serializable]
        public struct Health
        {
            public float maxHealth;
            public float healthRegenPerSecond;
        }

        [Space]
        public Health health;
        
        [Serializable]
        public struct Speed
        {
            public float baseSpeed;
            public float runningSpeedModifier;
        }
        
        [Space]
        public Speed speed;
        
        [Serializable]
        public struct Stamina
        {
            public float maxStamina;
            public float staminaConsumptionPerSecondOfRunning;
            public float staminaPerDodge;
            public float staminaRegenPerSecond;
        }
        [Space]
        public Stamina stamina;

        [Serializable]
        public struct Attack
        {
            public int Damage;
        }

        [Space] public Attack attack = new() { Damage = 35 };
    }
}