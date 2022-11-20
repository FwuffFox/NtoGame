using System.Collections.Generic;
using StaticData.ScriptableObjects;
using UnityEngine;
namespace Services.Data
{
    public interface IStaticDataService
    {
        Dictionary<string, LevelData> Levels { get; } 
        GameData GameData { get; }
        void Load();
        T LoadResource<T>(string path) where T : Object;
        T[] LoadResources<T>(string path) where T : Object;
    }
}