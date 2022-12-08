using System.Collections.Generic;
using GameScripts.Services.Factories;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services.UnitSpawner
{
    public class UnitSpawner : IUnitSpawner
    {
        private readonly IPrefabFactory _prefabFactory;

        public GameObject Player { get; private set; }
        public List<GameObject> Enemies { get; private set; } = new();
        public List<GameObject> Traps { get; private set; } = new();

        public List<GameObject> Fireplaces { get; private set; } = new();

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

        public GameObject SpawnTrap(Vector3 trapPosition)
        {
            var trap = _prefabFactory.InstantiateTrap(trapPosition);
            Traps.Add(trap);
            return trap;
        }

        public GameObject SpawnFireplace(Vector3 pos, bool isFinal)
        {
            var fireplace = _prefabFactory.InstantiateFireplace(pos, isFinal);
            fireplace.transform.rotation = Quaternion.Euler(-70.524f, 1.212f, -1.714f); 
            Fireplaces.Add(fireplace);
            return fireplace;
        }
		public GameObject SpawnEnemy(Vector3 enemyPosition)
        {
            var enemy = _prefabFactory.InstantiateEnemy(enemyPosition,0);
            Enemies.Add(enemy);
            return enemy;
        }
    }
}