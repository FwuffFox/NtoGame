using UnityEngine;

namespace Services
{
    public interface IUnitSpawner
    {
        GameObject SpawnPlayer();
        GameObject GetPlayer();
    }
}