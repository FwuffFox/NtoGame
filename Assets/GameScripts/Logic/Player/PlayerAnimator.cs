using UnityEngine;

namespace GameScripts.Logic.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        public void SetSpeed(float speed)
        {
            animator.SetFloat(Speed, speed);
        }

        public void SetDeath()
        {
            animator.SetTrigger(Death);
        }

        public void SetIsRunning(bool isRunning)
        {
            animator.SetBool(IsRunning, isRunning);
        }
    }
}