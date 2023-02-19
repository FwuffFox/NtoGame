using GameScripts.Infrastructure.States;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Services.Factories
{
    public interface IPrefabFactory
    {
        GameObject InstantiatePlayer(Vector3 position);
        GameObject InstantiateEnemy(Vector3 position, EnemyType enemyType);
        GameObject InstantiateTrap(Vector3 position);
        GameObject InstantiateMapGenerator(string sceneName);
        GameObject InstantiateFireplace(Vector3 position, FireplaceType type);
        GameObject InstantiateUI<TState>() where TState : class, IStateWithExit;
    }
}