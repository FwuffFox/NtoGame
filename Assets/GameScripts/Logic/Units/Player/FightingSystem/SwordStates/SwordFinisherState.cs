using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class SwordFinisherState : BaseState
    {
        protected override string StateName => "SwordFinisher";

        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);

            AttackIndex = 2;
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