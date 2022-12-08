using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Debuffs;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Player
{
    public class PlayerDebuffSystem : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;

        public void AddDebuff(SimpleDebuff simpleDebuff)
        {
            StartCoroutine(SimpleDebuffCoroutine(simpleDebuff));
        }

        private IEnumerator SimpleDebuffCoroutine(SimpleDebuff simpleDebuff)
        {
            switch (simpleDebuff.DebuffType)
            {
                case SimpleDebuffType.Speed: _playerMovement.Speed -= simpleDebuff.DebuffValue; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            while (simpleDebuff.Duration > 0)
            {
                yield return new WaitForSeconds(1);
                simpleDebuff.Duration -= 1;
            }
            
            switch (simpleDebuff.DebuffType)
            {
                case SimpleDebuffType.Speed: _playerMovement.Speed += simpleDebuff.DebuffValue; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddDebuff(PeriodicalDebuff debuff)
        {
            StartCoroutine(PeriodicalDebuffCoroutine(debuff));
        }

        private IEnumerator PeriodicalDebuffCoroutine(PeriodicalDebuff debuff)
        {
            while (debuff.Duration > 0)
            {
                switch (debuff.DebuffType)
                {
                    case PeriodicalDebuffType.Health: _playerHealth.GetDamage(debuff.DebuffValuePerSecond);
                        break;
                    case PeriodicalDebuffType.Stamina: _playerMovement.CurrentStamina -= debuff.DebuffValuePerSecond;
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