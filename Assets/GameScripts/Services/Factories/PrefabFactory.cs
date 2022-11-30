using GameScripts.Extensions;
using GameScripts.Logic.Enemy;
using GameScripts.Logic.Player;
using GameScripts.Services.AssetManagement;
using GameScripts.Services.Data;
using GameScripts.StaticData.Constants;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameScripts.Services.Factories
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private readonly IStaticDataService _staticDataService;

        public PrefabFactory(IAssetProvider assetProvider, DiContainer diContainer, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            _staticDataService = staticDataService;
        }
        
        public GameObject InstantiatePlayer(Vector3 position)
        {
            PlayerData playerData = _staticDataService.PlayerData;
            return _assetProvider.Instantiate(PrefabPaths.Player, position)
                .With(player =>
                {
                    player.name = "Player";
                    
                    _diContainer.InjectGameObject(player);

                    var movement = player.GetComponent<PlayerMovement>()
                        .With(x => x.SetProperties(playerData));
                    
                    var rotator = player.GetComponent<PlayerRotator>()
                        .With(x => x.camera = Camera.main);
                    
                    var health = player.GetComponent<PlayerHealth>()
                        .With(x => x.SetProperties(playerData));

                    var animator = player.GetComponent<PlayerAnimator>()
                        .With(x => health.OnPlayerDeath += x.SetDeath)
                        .With(x => movement.OnMovementSpeedChange += x.SetSpeed)
                        .With(x => movement.OnIsRunningChange += x.SetIsRunning);
                    
                });
        }

        [CanBeNull] private Transform _enemyParent;
        
        public GameObject InstantiateEnemy(Vector3 position, EnemyType enemyType)
        {
            _enemyParent ??= new GameObject(ParentObjects.Enemies).transform;
            EnemyData enemyData = _staticDataService.Enemies[enemyType];

            return _assetProvider.Instantiate(PrefabPaths.Enemies[enemyType], position, _enemyParent)
                .With(enemy =>
                {
                    enemy.name = enemyData.enemyName;

                    enemy.GetComponent<EnemyAI>()
                        .With(x => x.SetProperties(enemyData));

                    enemy.GetComponent<EnemyMover>()
                        .With(x => x.SetProperties(enemyData));

                    enemy.GetComponent<EnemyAttacker>()
                        .With(x => x.SetProperties(enemyData));

                });
        }

        [CanBeNull] private Transform _trapParent;
        public GameObject InstantiateTrap(Vector3 position)
        {
            _trapParent ??= new GameObject(ParentObjects.Enemies).transform;
            return _assetProvider.Instantiate(PrefabPaths.BearTrap, position, _trapParent);
        }

        public GameObject InstantiateUI() =>
            _assetProvider.Instantiate(PrefabPaths.UI)
                .With(ui => _diContainer.InjectGameObject(ui));
    }
}