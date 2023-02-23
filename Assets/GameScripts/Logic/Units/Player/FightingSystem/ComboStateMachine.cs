using System;
using EditorScripts.Inspector;
using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class ComboStateMachine : MonoBehaviour
    {
        private State _mainStateType = new IdleState();
        
        public State CurrentState { get; private set; }
        private State _nextState;

        public PlayerAnimator Animator;

        private void Update()
        {
            if (_nextState != null)
            {
                SetState(_nextState);
            }

            if (CurrentState != null)
                CurrentState.OnUpdate();
        }

        public void SetState(State newState)
        {
            _nextState = null;
            if (CurrentState != null)
            {
                CurrentState.OnExit();
            }
            CurrentState = newState;
            CurrentState.OnEnter(this);
        }

        public void SetNextState(State newState)
        {
            if (newState != null)
            {
                _nextState = newState;
            }
        }
        
        private void FixedUpdate()
        {
            if (CurrentState != null)
                CurrentState.OnFixedUpdate();
        }
        
        private void LateUpdate()
        {
            if (CurrentState != null)
                CurrentState.OnLateUpdate();
        }

        public void SetNextStateToMain()
        {
            _nextState = _mainStateType;
        }

        private void Awake()
        {
            SetNextStateToMain();
        }
    }
}