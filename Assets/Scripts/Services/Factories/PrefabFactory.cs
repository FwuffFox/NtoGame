using Extensions;
using Services.AssetManagement;
using StaticData.Constants;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        public PrefabFactory(IAssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }
        
        public GameObject InstantiatePlayer(Vector3 position)
        {
            return _assetProvider.Instantiate(PrefabPaths.Player, position)
                .With(player =>
                {
                    _diContainer.InjectGameObject(player);
                    player.GetComponent<PlayerMovement>().speed = 5f;
                    player.GetComponent<PlayerRotator>().camera = Camera.main;
                });
        }
        public GameObject InstantiateUI() =>
            _assetProvider.Instantiate(PrefabPaths.UI)
                .With(ui => _diContainer.InjectGameObject(ui));
    }
}