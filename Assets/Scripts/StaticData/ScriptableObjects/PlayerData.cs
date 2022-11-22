using UnityEngine;

namespace StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("PlayerHealth")]
        public float maxHealth;
        public float healthRegenPerSecond;
        
        [Header("PlayerMovement")]
        public float speed;
        public float runningSpeedModifier;
        public float maxStamina;
        public float staminaConsumptionPerSecondOfRunning;
        public float staminaPerDodge;
        public float staminaRegenPerSecond;
    }
}