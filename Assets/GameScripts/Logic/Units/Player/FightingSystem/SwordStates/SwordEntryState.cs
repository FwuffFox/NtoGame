using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class SwordEntryState : BaseState
    {
        protected override string StateName => "SwordEntry";

        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);
            AttackIndex = 1;
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