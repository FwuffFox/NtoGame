using System;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Debuffs
{
    /// <summary>
    /// A debuff that is applying an effect each second.
    /// </summary>
    /// <typeparam name="TPlayerComponent">Component that will be changed during debuff.</typeparam>
    public class PeriodicalDebuff<TPlayerComponent> where TPlayerComponent : MonoBehaviour
    {
        public int Duration;
        
        public readonly Action<TPlayerComponent> ActionOnTick;

        public PeriodicalDebuff(int duration, Action<TPlayerComponent> actionOnTick)
        {
            Duration = duration;
            ActionOnTick = actionOnTick;
        }
    }
}