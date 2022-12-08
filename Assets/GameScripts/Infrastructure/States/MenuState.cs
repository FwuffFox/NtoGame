using GameScripts.Logic.UI.InMenu;
using GameScripts.Services.Factories;
using GameScripts.Services.Unity;
using GameScripts.StaticData.Constants;
using UnityEngine;
using Zenject;

namespace GameScripts.Infrastructure.States
{
    public class MenuState : IState
    {
        private GameObject _ui;
        
        private ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IPrefabFactory _prefabFactory;

        [Inject]
        void Construct(ISceneLoader sceneLoader, GameStateMachine stateMachine, IPrefabFactory prefabFactory)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _prefabFactory = prefabFactory;
        }
        
        public void Enter()
        {
            _sceneLoader.LoadAsync(SceneNames.Menu, OnMenuLoaded);
        }

        private void OnMenuLoaded()
        {
            if (_ui == null)
                _ui = _prefabFactory.InstantiateUI<MenuState>();
            _ui.GetComponent<MenuUI>().PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            _ui.GetComponent<MenuUI>().StartLoadingScreen();
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }
        public void Exit() { }
    }
}