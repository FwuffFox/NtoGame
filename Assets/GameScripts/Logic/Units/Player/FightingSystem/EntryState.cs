namespace GameScripts.Logic.Units.Player.FightingSystem
{
    public class EntryState : State
    {
        public override void OnEnter(ComboStateMachine comboStateMachine)
        {
            base.OnEnter(comboStateMachine);

            var nextState = new SwordEntryState();
            comboStateMachine.SetNextState(nextState);
        }
    }
}