using Logic.Camera;
using Logic.Enemy;
using Logic.Player;
using Logic.UI;
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
                
            LevelData levelData = _staticDataService.Levels[SceneManager.GetActiveScene().name];

            var player = _unitSpawner.SpawnPlayer(levelData.playerSpawnPoint);
            Camera.main.GetComponentInParent<CameraFollower>()?.SetTarget(player);

            foreach (var enemy in levelData.enemies)
            {
                var spawnedEnemy = _unitSpawner.SpawnEnemy(enemy.position, enemy.enemyType);
                spawnedEnemy.GetComponent<EnemyAI>().SetPlayer(player);
            }
            
            if (_ui == null)
                _ui = _prefabFactory.InstantiateUI();
            
            _ui.GetComponentInChildren<HealthUI>().SetPlayer(player.GetComponent<PlayerHealth>());
            _ui.GetComponentInChildren<StaminaUI>().SetPlayer(player.GetComponent<PlayerMovement>());
            _stateMachine.Enter<GameLoopState>();
        }
    }
}