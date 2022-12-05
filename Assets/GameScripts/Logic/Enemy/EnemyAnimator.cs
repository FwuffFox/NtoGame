using UnityEngine;

namespace GameScripts.Logic.Enemy
{
    [RequireComponent((typeof(Animator)))]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void SetDeath()
        {
            _animator.SetTrigger(Death);
        }

        public void SetAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }
    }
}