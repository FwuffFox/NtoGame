using StaticData.Enums;
using UnityEngine;

namespace Services.Factories
{
    public interface IPrefabFactory
    {
        GameObject InstantiatePlayer(Vector3 position);
        GameObject InstantiateEnemy(Vector3 position, EnemyType enemyType);
        GameObject InstantiateUI();
    }
}