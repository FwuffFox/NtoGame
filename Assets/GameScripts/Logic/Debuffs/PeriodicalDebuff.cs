using System;
using UnityEngine;

namespace GameScripts.Logic.Debuffs
{
    /// <summary>
    /// A debuff that is applying an effect each second.
    /// </summary>
    /// <typeparam name="TPlayerComponent">Component that will be changed during debuff.</typeparam>
    public class PeriodicalDebuff<TPlayerComponent> where TPlayerComponent : MonoBehaviour
    {
        public int DurationInSeconds;
        
        public readonly Action<TPlayerComponent> ActionOnSecondPass;

        public PeriodicalDebuff(int durationInSeconds, Action<TPlayerComponent> actionOnSecondPass)
        {
            DurationInSeconds = durationInSeconds;
            ActionOnSecondPass = actionOnSecondPass;
        }
    }
}