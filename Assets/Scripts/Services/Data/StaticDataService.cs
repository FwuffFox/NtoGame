using System.Collections.Generic;
using System.Linq;
using Infrastructure.States;
using StaticData.Constants;
using StaticData.ScriptableObjects;
using UnityEngine;

namespace Services.Data
{
    public class StaticDataService : IStaticDataService
    {
        public Dictionary<string, LevelData> Levels { get; private set; } = new();
        
        public Dictionary<string, EnemyData> Enemies { get; private set; } = new();

        public GameData GameData { get; private set; }
        
        public PlayerData PlayerData { get; private set; }

        public void Load()
        {
            Levels = LoadResources<LevelData>(StaticDataPaths.LevelsData)
                .ToDictionary(data => data.sceneName, data => data);
            Enemies = LoadResources<EnemyData>(StaticDataPaths.EnemiesData)
                .ToDictionary(data => data.enemyName, data => data);

            GameData = LoadResource<GameData>(StaticDataPaths.GameData);
            PlayerData = LoadResource<PlayerData>(StaticDataPaths.PlayerData);
        }

        public T LoadResource<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        public T[] LoadResources<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
    }
}