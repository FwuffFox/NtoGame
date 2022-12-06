using GameScripts.Logic.Camera;
using GameScripts.Logic.Enemy;
using GameScripts.Logic.Generators;
using GameScripts.Logic.Player;
using GameScripts.Logic.UI.InGame;
using GameScripts.Services.Data;
using GameScripts.Services.Factories;
using GameScripts.Services.UnitSpawner;
using GameScripts.Services.Unity;
using GameScripts.StaticData;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameScripts.Infrastructure.States
{
    public class LoadLevelState : IStateWithPayload<string>
    {
        private GameObject _ui;
        
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private IUnitSpawner _unitSpawner;
        private IPrefabFactory _prefabFactory;

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
            var sceneName = SceneManager.GetActiveScene().name;
            LevelData levelData = _staticDataService.Levels[sceneName];
            var mapGenerator = _prefabFactory.InstantiateMapGenerator(sceneName);
            var generator = mapGenerator.GetComponent<GroundGenerator>();
            generator.GenerateMapAndTraps();
            var player = _unitSpawner.SpawnPlayer(levelData.playerSpawnPoint);
            generator.PlaceUnits(player);
            Curses.HealthCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerHealth>().GetDamage(999);
                Debug.Log("Max stack on health curse");
                return true;
            }, player);
            
            Curses.StaminaCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerMovement>().Speed -= 1;
                Debug.Log("Max stack on stamina curse");
                return true;
            }, player);
            
            
            Camera.main.GetComponentInParent<CameraFollower>()?.SetTarget(player);            
            
            if (_ui == null)
                _ui = _prefabFactory.InstantiateUI<LoadLevelState>();
            
            _ui.GetComponentInChildren<HealthUI>().SetPlayer(player.GetComponent<PlayerHealth>());
            _ui.GetComponentInChildren<StaminaUI>().SetPlayer(player.GetComponent<PlayerMovement>());
            player.GetComponent<PlayerCurseSystem>().OnCurseChange += _ui.GetComponentInChildren<CurseUI>().UpdateCurses;
            _stateMachine.Enter<GameLoopState>();
        }
    }
}