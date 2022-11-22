using UnityEngine;

namespace Services.Factories
{
    public interface IPrefabFactory
    {
        GameObject InstantiatePlayer(Vector3 position);
        GameObject InstantiateEnemy(Vector3 position, string enemyName);
        GameObject InstantiateUI();
    }
}