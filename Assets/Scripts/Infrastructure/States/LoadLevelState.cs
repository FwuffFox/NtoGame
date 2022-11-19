using Logic.Camera;
using Services;
using Services.Data;
using Services.Factories;
using Services.Unity;
using StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelState : IStateWithPayload<string>
    {
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private IUnitSpawner _unitSpawner;
        private IPrefabFactory _prefabFactory;
        private GameObject _ui;

        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, IUnitSpawner unitSpawner, IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _unitSpawner = unitSpawner;
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string name)
        {
            _sceneLoader.LoadAsync(name, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
                
            LevelData levelData = _staticDataService.GetLevels()[SceneManager.GetActiveScene().name];

            var player = _unitSpawner.SpawnPlayer();
            Camera.main.GetComponent<CameraFollower>()?.SetTarget(player);

            if (_ui == null)
                _ui = _prefabFactory.InstantiateUI();

            _stateMachine.Enter<GameLoopState>();
        }
    }
}