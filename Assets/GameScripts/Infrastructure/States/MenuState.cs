using GameScripts.Extensions;
using GameScripts.Logic.UI.InMenu;
using GameScripts.Services.Factories;
using GameScripts.Services.Unity;
using GameScripts.StaticData.Constants;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameScripts.Infrastructure.States
{
    public class MenuState : IState
    {
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
            var ui = _prefabFactory.InstantiateUI<MenuState>();
            // TODO: Refactor this to load ui from factory
            ui.GetComponent<MenuUI>().PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            Debug.Log("Loading level");
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }
        public void Exit() { }
    }
}