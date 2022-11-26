using System.Collections.Generic;
using GameScripts.Services.Factories;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services
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

        public GameObject SpawnEnemy(Vector3 spawnPosition, EnemyType enemyType)
        {
            var enemy = _prefabFactory.InstantiateEnemy(spawnPosition, enemyType);
            Enemies.Add(enemy);
            return enemy;
        }
    }
}