using GameScripts.Logic.UI.InMenu;
using GameScripts.Services.Factories;
using GameScripts.Services.Unity;
using GameScripts.StaticData.Constants;
using JetBrains.Annotations;
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

        private MenuUI _menuUI;
        private void OnMenuLoaded()
        {
            if (_ui == null)
                _ui = _prefabFactory.InstantiateUI<MenuState>();
            if (_menuUI == null)
                _menuUI = _ui.GetComponent<MenuUI>();
            _menuUI.PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            _menuUI.StartLoadingScreen();
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }

        public void Exit() { }
    }
}