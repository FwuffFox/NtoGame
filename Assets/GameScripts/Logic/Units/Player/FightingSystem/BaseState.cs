using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public abstract class BaseState : State
    {
        /// <summary>
        /// Name of that state. Must be unique. Player Animator should have an animation
        /// with the same name. AttackSO should be created with the same name.
        /// </summary>
        protected abstract string StateName { get; }

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
        protected float Duration { get; set; }
        
        /// <summary>
        /// Audio which will be played after entering a state.
        /// </summary>
        protected AudioClip AttackAudio { get; set; }
        
        private float _attackPressedTimer = 0;

        protected AttackSO AttackData;
        
        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);
            if (!ComboStateMachine.StaticDataService.AttackDictionary.TryGetValue(StateName, out AttackData))
            {
                Debug.LogError($"There is no SO for {this} or names are wrong");
                return;
            }

            Duration = AttackData.AttackDuration;
            AttackAudio = AttackData.AttackAudio;
            
            ComboStateMachine.Animator.SetTrigger(StateName);
            ComboStateMachine.PlayerAttack.IsWeaponActive = true;
            
            ComboStateMachine.AudioSource.clip = AttackAudio;
            ComboStateMachine.AudioSource.Play();
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
            ComboStateMachine.PlayerAttack.IsWeaponActive = false;
            base.OnExit();
        }
    }
}