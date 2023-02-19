using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class SwordFinisherState : BaseState
    {
        private static readonly int AnimId = Animator.StringToHash("Attack2");
        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);

            AttackIndex = 2;
            Duration = 2f;
            PlayerAnimator.SetTrigger(AnimId);
            Debug.Log($"Player Attack{AttackIndex}fired!");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if (FixedTime >= Duration)
            {
                ComboStateMachine.SetNextStateToMain();
            }
        }
    }
}