using GameScripts.Services.Data;
using GameScripts.Services.Unity;
using GameScripts.StaticData.Constants;
using Zenject;

namespace GameScripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;


        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            WarmUp();
            _sceneLoader.LoadAsync(SceneNames.Bootstrap, EnterMenuState);
        }

        public void Exit() { }

        private void EnterMenuState()
        {
            _stateMachine.Enter<MenuState>();
        }

        private void WarmUp()
        {
            _staticDataService.Load();
        }
        
    }
}