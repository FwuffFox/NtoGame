using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class BaseFightingState : FightState
    {
        private PlayerAnimator _animator;

        protected bool ShouldCombo;

        private float _attackPressedTimer = 0;
        public override void OnEnter(FightingStateMachine stateMachine)
        {
            _animator = stateMachine.Animator;
            base.OnEnter(stateMachine);
        }

        public override void OnUpdate()
        {
            _attackPressedTimer -= Time.deltaTime;

            if (Input.GetMouseButton(0))
                _attackPressedTimer = 2;

            if (_attackPressedTimer > 0)
                ShouldCombo = true;
            
            base.OnUpdate();
        }

        protected void Attack()
        {
            var collidersToDamage = new Collider[20];
            // TODO: Add Attack logic
        }
    }
}