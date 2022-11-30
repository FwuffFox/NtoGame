using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services.Factories
{
    public interface IPrefabFactory
    {
        GameObject InstantiatePlayer(Vector3 position);
        GameObject InstantiateEnemy(Vector3 position, EnemyType enemyType);
        GameObject InstantiateTrap(Vector3 position);
        GameObject InstantiateUI();
    }
}