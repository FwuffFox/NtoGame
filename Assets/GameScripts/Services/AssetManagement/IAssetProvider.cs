using UnityEngine;

namespace GameScripts.Services.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 position, Transform parent = null);
        GameObject Instantiate(string path);
    }
}