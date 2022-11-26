using System.Collections.Generic;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services
{
    public interface IUnitSpawner
    {
        GameObject Player { get; }
        List<GameObject> Enemies { get; }
        GameObject SpawnPlayer() => SpawnPlayer(Vector3.zero);
        GameObject SpawnPlayer(Vector3 spawnPoint);
        GameObject SpawnEnemy(Vector3 spawnPosition, EnemyType enemyType);
    }
}