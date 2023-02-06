using System;
using System.Collections;
using System.Collections.Generic;
using EditorScripts.Inspector;
using GameScripts.Logic.Debuffs;
using UnityEngine;

namespace GameScripts.Logic.Player
{
    public class PlayerDebuffSystem : MonoBehaviour
    {
        private readonly Dictionary<Type, MonoBehaviour> _playerComponentDictionary = new();

        public void RegisterComponent<TPlayerComponent>(TPlayerComponent playerComponent)
            where TPlayerComponent : MonoBehaviour
        {
            _playerComponentDictionary.Add(typeof(TPlayerComponent), playerComponent);
        }

        public void AddDebuff<TPlayerComponent>(SimpleDebuff<TPlayerComponent> simpleDebuff)
            where TPlayerComponent : MonoBehaviour
        {
            StartCoroutine(SimpleDebuffCoroutine(simpleDebuff));
        }

        private IEnumerator SimpleDebuffCoroutine<TPlayerComponent>
            (SimpleDebuff<TPlayerComponent> simpleDebuff)
            where TPlayerComponent : MonoBehaviour
        {
            var component = _playerComponentDictionary[typeof(TPlayerComponent)] as TPlayerComponent;

            simpleDebuff.ActionOnDebuffStart(component);
            
            while (simpleDebuff.DurationInSeconds > 0)
            {
                yield return new WaitForSeconds(1);
                simpleDebuff.DurationInSeconds -= 1;
            }

            simpleDebuff.ActionOnDebuffEnd(component);
        }

        public void AddDebuff<TPlayerComponent>(PeriodicalDebuff<TPlayerComponent> debuff)
            where TPlayerComponent : MonoBehaviour
        {
            StartCoroutine(PeriodicalDebuffCoroutine(debuff));
        }

        private IEnumerator PeriodicalDebuffCoroutine<TPlayerComponent>
            (PeriodicalDebuff<TPlayerComponent> debuff)
            where TPlayerComponent : MonoBehaviour
        {
            var component = _playerComponentDictionary[typeof(TPlayerComponent)] as TPlayerComponent;
            while (debuff.DurationInSeconds > 0)
            {
                debuff.ActionOnSecondPass.Invoke(component);

                yield return new WaitForSeconds(1);
                debuff.DurationInSeconds -= 1;
            }
        }
       
        
        
        
        
        #if UNITY_EDITOR
        [InspectorButton("RemoveAllDebuffsButton", ButtonWidth = 200)]
        [SerializeField] private bool removeAllDebuffs;
        
        private void RemoveAllDebuffsButton() => StopAllCoroutines();
        #endif
    }
}