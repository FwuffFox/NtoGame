using UnityEngine;

namespace Services.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 position);
        GameObject Instantiate(string path);
    }
}