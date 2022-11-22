using System.Collections.Generic;
using Services.Factories;
using UnityEngine;

namespace Services
{
    public class UnitSpawner : IUnitSpawner
    {
        private readonly IPrefabFactory _prefabFactory;

        public GameObject Player { get; private set; }
        public List<GameObject> Enemies { get; private set; } = new();

        public UnitSpawner(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public GameObject SpawnPlayer(Vector3 spawnPosition)
        {
            return Player = _prefabFactory.InstantiatePlayer(spawnPosition);
        }

        public GameObject SpawnEnemy(Vector3 spawnPosition, string enemyName)
        {
            var enemy = _prefabFactory.InstantiateEnemy(spawnPosition, enemyName);
            Enemies.Add(enemy);
            return enemy;
        }
    }
}