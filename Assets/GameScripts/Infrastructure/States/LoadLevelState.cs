using GameScripts.Logic.Camera;
using GameScripts.Logic.Generators;
using GameScripts.Logic.UI.InGame;
using GameScripts.Logic.Units.Player;
using GameScripts.Services.Data;
using GameScripts.Services.Factories;
using GameScripts.Services.UnitSpawner;
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

            var playerPosition = levelData.playerSpawnPoint;
            if (PlayerPrefs.HasKey("CheckpointX"))
            {
                playerPosition = new Vector3(PlayerPrefs.GetFloat("CheckpointX"), 1, PlayerPrefs.GetFloat("CheckpointX"));
            }
            
            if (_unitSpawner.Player != null) Object.Destroy(_unitSpawner.Player);
            var player = _unitSpawner.SpawnPlayer(playerPosition);
            generator.PlaceUnits(player);
            
            SetCurses(player);
            
            Camera.main!.GetComponentInParent<CameraFollower>().SetTarget(player);            
            
            var ui = _prefabFactory.InstantiateUI<LoadLevelState>();
            
            ui.GetComponentInChildren<HealthUI>().SetPlayer(player.GetComponent<PlayerHealth>());
            ui.GetComponentInChildren<StaminaUI>().SetPlayer(player.GetComponent<PlayerMovement>());
            ui.GetComponentInChildren<CurseUI>().SetPlayer(player.GetComponent<PlayerCurseSystem>());
            ui.GetComponentInChildren<PointsUI>().SetPlayer(player.GetComponent<PlayerMoney>());
            
            _stateMachine.Enter<GameLoopState, GameObject>(ui);
        }

        private void SetCurses(GameObject player)
        {
            Curses.HealthCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerHealth>().MaxHealth = 1;
                Debug.Log("Max stack on health curse");
            }, player);
            
            Curses.StaminaCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerMovement>().MovementSpeedModifier *= 0.5f;
                Debug.Log("Max stack on stamina curse");
            }, player);
            
            Curses.DamageCurse.SetOnMaxStacksFunction(p =>
            {
                p.GetComponent<PlayerAttack>().Damage *= Mathf.CeilToInt(0.5f);
                Debug.Log("Max stack on damage curse");
            }, player);
        }
    }
}