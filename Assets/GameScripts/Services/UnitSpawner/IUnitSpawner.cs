using System.Collections.Generic;
using GameScripts.Logic.Campfire;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services.UnitSpawner
{
    public interface IUnitSpawner
    {
        GameObject Player { get; }
        List<GameObject> Enemies { get; }
        List<GameObject> Traps { get; }
        List<Campfire> Campfires { get; }
        GameObject SpawnPlayer() => SpawnPlayer(Vector3.zero);
        GameObject SpawnPlayer(Vector3 spawnPoint);
        GameObject SpawnEnemy(Vector3 spawnPosition, EnemyType enemyType);
        GameObject SpawnTrap(Vector3 trapPosition);
        GameObject SpawnCampfire(Vector3 pos, CampfireType type);
        void Clear();
    }
}