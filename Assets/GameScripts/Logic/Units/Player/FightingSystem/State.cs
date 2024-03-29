namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public abstract class State
    {
        protected ComboStateMachine ComboStateMachine;
        
        protected float Time { get; set; }
        protected float FixedTime { get; set; }
        protected float LateTime { get; set; }
        
        public virtual void OnEnter(ComboStateMachine comboStateMachine)
        {
            ComboStateMachine = comboStateMachine;
        }

        public virtual void OnUpdate()
        {
            Time += UnityEngine.Time.deltaTime;
        }

        public virtual void OnFixedUpdate()
        {
            FixedTime += UnityEngine.Time.deltaTime;
        }

        public virtual void OnLateUpdate()
        {
            LateTime += UnityEngine.Time.deltaTime;
        }

        /// <summary>
        /// Called on the exit of a state.
        /// </summary>
        public virtual void OnExit()
        {
            
        }
    }
}