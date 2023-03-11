using System.Collections.Generic;
using GameScripts.Logic.Campfire;
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

        public List<Campfire> Campfires { get; private set; } = new();

        public UnitSpawner(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public GameObject SpawnPlayer(Vector3 spawnPosition)
        {
            Player = _prefabFactory.InstantiatePlayer(spawnPosition);
            return Player;
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

        public GameObject SpawnCampfire(Vector3 pos, CampfireType type)
        {
            var campfire = _prefabFactory.InstantiateFireplace(pos, type);
            campfire.transform.rotation = Quaternion.Euler(-70.524f, 1.212f, -1.714f); 
            Campfires.Add(campfire.GetComponent<Campfire>());
            return campfire;
        }

        public void Clear()
        {
            Enemies.Clear();
            Campfires.Clear();
            Traps.Clear();
        }

        public GameObject SpawnEnemy(Vector3 enemyPosition)
        {
            var enemy = _prefabFactory.InstantiateEnemy(enemyPosition,0);
            Enemies.Add(enemy);
            return enemy;
        }
    }
}