using System;
using UnityEngine;

namespace Logic.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void SetDeath()
        {
            _animator.SetTrigger(Death);
        }

        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool(IsRunning, isRunning);
        }
    }
}