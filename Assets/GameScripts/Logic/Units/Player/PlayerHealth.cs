using System.Collections;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Units.Player
{
    public class PlayerHealth : BattleUnitHealth
    {
        private float _healthRegenPerSecond;

        public void SetProperties(PlayerData playerData)
        {
            MaxHealth = playerData.health.maxHealth;
            Health = MaxHealth;
            _healthRegenPerSecond = playerData.health.healthRegenPerSecond;
        }
        
        private void OnEnable()
        {
            StartCoroutine(RegenerationCoroutine());
        }

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                Health += _healthRegenPerSecond;
            }
        }
    }
}