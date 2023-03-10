using System;
using System.Collections.Generic;
using System.Linq;
using GameScripts.StaticData.Constants;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScripts.Services.Data
{
    public class StaticDataService : IStaticDataService
    {
        public Dictionary<string, LevelData> Levels { get; private set; } = new();
        
        public Dictionary<EnemyType, EnemyData> Enemies { get; private set; } = new();

        public Dictionary<string, AttackSO> AttackDictionary { get; private set; } = new();

        public GameData GameData { get; private set; }
        
        public PlayerData PlayerData { get; private set; }
        

        public void Load()
        {
            Levels = LoadResources<LevelData>(StaticDataPaths.LevelsData)
                .ToDictionary(data => data.sceneName, data => data);
            Enemies = LoadResources<EnemyData>(StaticDataPaths.EnemiesData)
                .ToDictionary(data => data.enemyType, data => data);
            AttackDictionary = LoadResources<AttackSO>(StaticDataPaths.AttacksData)
                .ToDictionary(data => data.AttackName, data => data);

            GameData = LoadResource<GameData>(StaticDataPaths.GameData);
            PlayerData = LoadResource<PlayerData>(StaticDataPaths.PlayerData);
        }

        public T LoadResource<T>(string path) where T : Object =>
            Resources.Load<T>(path);
        
        public T LoadResource<T>(string path, Func<T, bool> predicate) where T : Object =>
            Resources.LoadAll<T>(path).First(predicate);

        public T[] LoadResources<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);
        
        public T[] LoadResources<T>(string path, Func<T, bool> predicate) where T : Object =>
            Resources.LoadAll<T>(path).Where(predicate).ToArray();
    }
}