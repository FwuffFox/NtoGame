using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class FightingStateMachine : MonoBehaviour
    {
        private FightState _currentState;
        private FightState _nextState;

        public PlayerAnimator Animator;

        void Update()
        {
            if (_nextState != null)
                SetState(_nextState);
            if (_currentState != null)
                _currentState.OnUpdate();
        }

        private void SetState(FightState newState)
        {
            _nextState = null;
            if (_currentState != null)
            {
                _currentState.OnExit();
            }

            _currentState = newState;
            _currentState.OnEnter(this);
        }
    }
}