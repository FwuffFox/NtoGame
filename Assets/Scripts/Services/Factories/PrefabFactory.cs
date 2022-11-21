using Extensions;
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
                    _diContainer.InjectGameObject(player);
                    
                    player.GetComponent<PlayerMovement>()
                        .With(x => x.speed = playerData.speed)
                        .With(x => x.runningSpeedModifier = playerData.runningSpeedModifier)
                        .With(x => x.maxStamina = playerData.maxStamina)
                        .With(x => x.CurrentStamina = x.maxStamina)
                        .With(x => x.staminaRegenPerSecond = playerData.staminaRegenPerSecond)
                        .With(x => x.staminaConsumptionPerSecondOfRunning = playerData.staminaConsumptionPerSecondOfRunning);
                    
                    player.GetComponent<PlayerRotator>()
                        .With(x => x.camera = Camera.main);
                    
                    player.GetComponent<PlayerHealth>()
                        .With(x => x.maxHealth = playerData.maxHealth)
                        .With(x => x.CurrentHealth = x.maxHealth)
                        .With(x => x.healthRegeneratedPerSecond = playerData.healthRegenPerSecond);
                });
        }
        public GameObject InstantiateUI() =>
            _assetProvider.Instantiate(PrefabPaths.UI)
                .With(ui => _diContainer.InjectGameObject(ui));
    }
}