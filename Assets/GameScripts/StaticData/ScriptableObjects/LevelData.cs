using System;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "StaticData/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string sceneName;

        public Vector3 playerSpawnPoint;

        public int mapSize;

        public int trapsCount;
        
        [Serializable]
        public struct Enemy
        {
            public Vector3 position;
            public EnemyType enemyType;
        }
        
        [Space]
        public Enemy[] enemies;
        
    }
}