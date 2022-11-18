using Services.Factories;
using UnityEngine;

namespace Services
{
    public class UnitSpawner : IUnitSpawner
    {
        private readonly IPrefabFactory _prefabFactory;

        private GameObject _player;
        
        public UnitSpawner(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public GameObject SpawnPlayer()
        {
            return _player = _prefabFactory.InstantiatePlayer(Vector3.zero);
        }

        public GameObject GetPlayer()
        {
            return _player;
        }
    }
}