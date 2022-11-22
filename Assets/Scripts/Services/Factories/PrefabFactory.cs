using Extensions;
using Logic.Enemy;
using Services.AssetManagement;
using Logic.Player;
using Services.Data;
using StaticData.Constants;
using StaticData.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Services.Factories
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

        public GameObject InstantiateEnemy(Vector3 position, string enemyName)
        {
            EnemyData enemyData = _staticDataService.Enemies[enemyName];

            return _assetProvider.Instantiate(PrefabPaths.Enemies[enemyName], position)
                .With(enemy =>
                {
                    enemy.name = enemyName;

                    enemy.GetComponent<EnemyAI>()
                        .With(x => x.SetProperties(enemyData));
                });
        }
        public GameObject InstantiateUI() =>
            _assetProvider.Instantiate(PrefabPaths.UI)
                .With(ui => _diContainer.InjectGameObject(ui));
    }
}