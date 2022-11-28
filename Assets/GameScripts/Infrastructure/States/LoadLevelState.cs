using GameScripts.Extensions;
using GameScripts.Logic.Camera;
using GameScripts.Logic.Enemy;
using GameScripts.Logic.Player;
using GameScripts.Logic.UI;
using GameScripts.Services;
using GameScripts.Services.Data;
using GameScripts.Services.Factories;
using GameScripts.Services.Unity;
using GameScripts.StaticData;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameScripts.Infrastructure.States
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
            
            Curses.HealthCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerHealth>().GetDamage(999);
                Debug.Log("Max stack on health curse");
                return true;
            }, player);
            
            
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