using UnityEngine;

namespace StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "StaticData/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string SceneName { get; set; }
        
        public Vector3 PlayerSpawnPoint { get; set; }
    }
}