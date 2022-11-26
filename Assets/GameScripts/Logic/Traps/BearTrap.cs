using GameScripts.Logic.Debuffs;
using GameScripts.Logic.Player;
using UnityEngine;

namespace GameScripts.Logic.Traps
{
    public class BearTrap : MonoBehaviour
    {
        [SerializeField] private AudioSource _bearTrapSound;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth)) return;
            var debuffer = other.gameObject.GetComponent<PlayerDebuffSystem>();
            var bleedingDebuff = new PeriodicalTimedDebuff(PeriodicalTimedDebuffType.Health, 5, 5);
            var speedDebuff = new TimedDebuff(TimedDebuffType.Speed, 5, 1);
            debuffer.AddDebuff(bleedingDebuff);
            debuffer.AddDebuff(speedDebuff);
            _bearTrapSound.Play();
            Destroy(GetComponentInParent<MeshRenderer>());
            playerHealth.GetDamage(10);
            Destroy(gameObject);
        }
    }
}
