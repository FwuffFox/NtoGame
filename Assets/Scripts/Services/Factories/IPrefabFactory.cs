using UnityEngine;

namespace Services.Factories
{
    public interface IPrefabFactory
    {
        GameObject InstantiatePlayer(Vector3 position);
        GameObject InstantiateUI();
    }
}