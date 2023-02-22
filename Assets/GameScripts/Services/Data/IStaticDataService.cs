using System.Collections.Generic;
using GameScripts.StaticData.Enums;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;

namespace GameScripts.Services.Data
{
    public interface IStaticDataService
    {
        Dictionary<string, LevelData> Levels { get; } 
        Dictionary<EnemyType, EnemyData> Enemies { get; } 
        Dictionary<string, AttackSO> AttackDictionary { get; }
        GameData GameData { get; }
        PlayerData PlayerData { get; }
        
        void Load();
        T LoadResource<T>(string path) where T : Object;
        T[] LoadResources<T>(string path) where T : Object;
    }
}