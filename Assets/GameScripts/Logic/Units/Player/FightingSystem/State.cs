namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public abstract class State
    {
        public ComboStateMachine ComboStateMachine;
        
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

        public virtual void OnExit()
        {
            
        }
    }
}