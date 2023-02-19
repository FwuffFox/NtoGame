using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class SwordEntryState : BaseState
    {
        private static readonly int AnimId = Animator.StringToHash("Attack1");
        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);
            
            AttackIndex = 1;
            Duration = 1.5f;
            PlayerAnimator.SetTrigger(AnimId);
            Debug.Log($"Player Attack{AttackIndex}fired!");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if (FixedTime >= Duration)
            {
                if (ShouldCombo)
                    ComboStateMachine.SetNextState(new SwordFinisherState());
                else
                    ComboStateMachine.SetNextStateToMain();
            }
        }
    }
}