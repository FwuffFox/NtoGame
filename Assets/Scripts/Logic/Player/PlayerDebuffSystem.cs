using System.Collections;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerDebuffSystem : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;

        public enum DebuffType
        {
            Stamina,
            Health,
            Speed
        }
        
        public struct TimedDebuff
        {
            public DebuffType Type;
            public int Duration;
            public float Power;

            public TimedDebuff(DebuffType type, int duration, float power)
            {
                Type = type;
                Duration = duration;
                Power = power;
            }
        }

        public void AddDebuff(TimedDebuff timedDebuff)
        {
            StartCoroutine(TimedDebuffCoroutine(timedDebuff));
        }

        private IEnumerator TimedDebuffCoroutine(TimedDebuff timedDebuff)
        {
            switch (timedDebuff.Type)
            {
                case DebuffType.Speed: playerMovement.Speed -= timedDebuff.Power; break;
                default: yield break;
            }

            while (timedDebuff.Duration > 0)
            {
                yield return new WaitForSeconds(1);
                timedDebuff.Duration -= 1;
            }
            
            switch (timedDebuff.Type)
            {
                case DebuffType.Speed: playerMovement.Speed += timedDebuff.Power; break;
                default: yield break;
            }
        }
    }
}