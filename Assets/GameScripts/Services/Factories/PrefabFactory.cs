using GameScripts.Extensions;
using GameScripts.Infrastructure.States;
using GameScripts.Logic.Fireplace;
using GameScripts.Logic.Generators;
using GameScripts.Logic.Units.Enemy;
using GameScripts.Logic.Units.Player;
using GameScripts.Logic.Units.Player.FightingSystem;
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

                    var health = player.GetComponent<PlayerHealth>()
                        .With(x => x.SetProperties(playerData));

                    var comboSystem = player.GetComponent<ComboStateMachine>()
                        .With(x => x.SetNextStateToMain());
                    
                    var attack = player.GetComponent<PlayerAttack>()
                        .With(x =>
                        {
                            x.SetProperties(playerData);
                            x.MeleeComboStateMachine = comboSystem;
                        });

                    var animator = player.GetComponent<PlayerAnimator>()
                        .With(animator =>
                        {
                            health.OnBattleUnitDeath += animator.SetDeath;
                            movement.OnMovementSpeedChange += animator.SetSpeed;
                            movement.OnIsRunningChange += animator.SetIsRunning;
                        });

                    var debuffSystem = player.GetComponent<PlayerDebuffSystem>().With(x =>
                    {
                        x.RegisterComponent(health);
                        x.RegisterComponent(movement);
                    });

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

                    var ai = enemy.GetComponent<EnemyAI>()
                        .With(x => x.SetProperties(enemyData));

                    var mover = enemy.GetComponent<EnemyMover>()
                        .With(x => x.SetProperties(enemyData));

                    var attacker = enemy.GetComponent<EnemyAttacker>()
                        .With(x => x.SetProperties(enemyData));

                    enemy.GetComponent<EnemyAnimator>()
                        .With(animator =>
                        {
                            mover.OnSpeedChange += animator.SetSpeed;
                            attacker.OnAttack += animator.SetAttack;
                            ai.OnBattleUnitDeath += animator.SetDeath;
                        });

                    enemy.GetComponent<EnemyUI>()
                        .With(ui =>
                        {
                            ui.SetTarget(ai);
                        });

                });
        }

        [CanBeNull] private Transform _trapParent;
        public GameObject InstantiateTrap(Vector3 position)
        {
            _trapParent ??= new GameObject(ParentObjects.Traps).transform;
            return _assetProvider.Instantiate(PrefabPaths.BearTrap, position, _trapParent)
                .With(trap => trap.name = "BearTrap");
        }

        public GameObject InstantiateMapGenerator(string sceneName)
        {
            LevelData levelData = _staticDataService.Levels[sceneName];
            return _assetProvider.Instantiate(PrefabPaths.MapGenerator)
                .With(gen =>
                {
                    _diContainer.InjectGameObject(gen);

                    gen.GetComponent<GroundGenerator>().With(x => x.SetProperties(levelData));
                });
        }

        public GameObject InstantiateFireplace(Vector3 position, FireplaceType type)
        {
            return _assetProvider.Instantiate(PrefabPaths.Fireplace, position)
                .With(fireplace =>
                {
                    _diContainer.InjectGameObject(fireplace);

                    fireplace.GetComponent<Fireplace>().Type = type;
                });
        }

        public GameObject InstantiateUI<TState>() where TState : class, IStateWithExit =>
            _assetProvider.Instantiate(PrefabPaths.UIs[typeof(TState)])
                .With(ui => _diContainer.InjectGameObject(ui));
    }
}