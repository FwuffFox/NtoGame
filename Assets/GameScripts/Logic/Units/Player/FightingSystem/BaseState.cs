using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class BaseState : State
    {
        protected PlayerAnimator PlayerAnimator;

        /// <summary>
        /// Whether or not the next attack in the combo sequence should be played or not.
        /// </summary>
        protected bool ShouldCombo;

        /// <summary>
        /// The place in the sequence of attacks this state is in.
        /// </summary>
        protected int AttackIndex;
        
        /// <summary>
        /// How long should state be active before moving next.
        /// </summary>
        public float Duration { get; set; }
        
        private float _attackPressedTimer = 0;
        
        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);
            PlayerAnimator = comboStateMachine.Animator;
        }

        public override void OnUpdate()
        {     
            base.OnUpdate();
            _attackPressedTimer -= UnityEngine.Time.deltaTime;
            
            if (Input.GetMouseButtonDown(0))
            {
                _attackPressedTimer = 1;
            }
            
            if (_attackPressedTimer > 0)
                ShouldCombo = true;
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}