namespace Infrastructure.States
{
    public interface IStateWithExit
    {
        void Exit();
    }
    
    public interface IState : IStateWithExit
    {
        void Enter();
    }

    public interface IStateWithPayload<TPayload> : IStateWithExit
    {
        void Enter(TPayload payload);
    }
}