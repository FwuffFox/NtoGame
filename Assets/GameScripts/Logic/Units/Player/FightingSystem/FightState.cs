namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public abstract class FightState
    {
        public FightingStateMachine StateMachine;
        public virtual void OnEnter(FightingStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}