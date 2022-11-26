using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Debuffs;
using UnityEngine;

namespace GameScripts.Logic.Player
{
    public class PlayerDebuffSystem : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;

        public void AddDebuff(TimedDebuff timedDebuff)
        {
            StartCoroutine(TimedDebuffCoroutine(timedDebuff));
        }

        private IEnumerator TimedDebuffCoroutine(TimedDebuff timedDebuff)
        {
            switch (timedDebuff.DebuffType)
            {
                case TimedDebuffType.Speed: _playerMovement.Speed -= timedDebuff.DebuffValue; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            while (timedDebuff.Duration > 0)
            {
                yield return new WaitForSeconds(1);
                timedDebuff.Duration -= 1;
            }
            
            switch (timedDebuff.DebuffType)
            {
                case TimedDebuffType.Speed: _playerMovement.Speed += timedDebuff.DebuffValue; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddDebuff(PeriodicalTimedDebuff debuff)
        {
            StartCoroutine(PeriodicalTimedDebuffCoroutine(debuff));
        }

        private IEnumerator PeriodicalTimedDebuffCoroutine(PeriodicalTimedDebuff debuff)
        {
            while (debuff.Duration > 0)
            {
                switch (debuff.DebuffType)
                {
                    case PeriodicalTimedDebuffType.Health: _playerHealth.GetDamage(debuff.DebuffValuePerSecond);
                        break;
                    case PeriodicalTimedDebuffType.Stamina: _playerMovement.CurrentStamina -= debuff.DebuffValuePerSecond;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                yield return new WaitForSeconds(1);
                debuff.Duration -= 1;
            }
        }
        
        #if UNITY_EDITOR
        [InspectorButton("RemoveAllDebuffsButton", ButtonWidth = 200)]
        [SerializeField] private bool removeAllDebuffs;

        private void RemoveAllDebuffsButton() => StopAllCoroutines();
        #endif
    }
}