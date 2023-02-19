using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic.Units.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("animator")] [SerializeField] private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void SetDeath()
        {
            _animator.SetTrigger(Death);
        }

        public void SetAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void SetTrigger(string animName)
        {
            _animator.SetTrigger(animName);
        }
        
        public void SetTrigger(int animId)
        {
            _animator.SetTrigger(animId);
        }

        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool(IsRunning, isRunning);
        }
    }
}