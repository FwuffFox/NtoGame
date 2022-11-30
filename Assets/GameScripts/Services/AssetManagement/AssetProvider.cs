using GameScripts.Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace GameScripts.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path, Vector3 position, Transform parent = null)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}