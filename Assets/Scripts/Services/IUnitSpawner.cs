using UnityEngine;

namespace Services
{
    public interface IUnitSpawner
    {
        GameObject SpawnPlayer() => SpawnPlayer(Vector3.zero);
        GameObject SpawnPlayer(Vector3 spawnPoint);
        GameObject GetPlayer();
    }
}