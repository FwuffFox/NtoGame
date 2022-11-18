using System.Collections.Generic;
using System.Linq;
using StaticData.Constants;
using StaticData.ScriptableObjects;
using UnityEngine;

namespace Services.Data
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelData> _levels = new Dictionary<string, LevelData>();
        
        public void Load()
        {
            _levels = LoadResources<LevelData>(StaticDataPaths.LevelsData)
                .ToDictionary(_ => _.SceneName, _ => _);
        }

        public T LoadResource<T>(string path) where T : Object =>
            Resources.Load<T>(path);

        public T[] LoadResources<T>(string path) where T : Object =>
            Resources.LoadAll<T>(path);

        public Dictionary<string, LevelData> GetLevels() =>
            _levels;
    }
}