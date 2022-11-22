using System.Collections.Generic;

namespace StaticData.Constants
{
    public static class PrefabPaths
    {
        /// <summary>
        /// Gives a path corresponding to key (Enemy name)
        /// </summary>
        public static readonly Dictionary<string, string> Enemies = new()
        {
            {"Enemy", "Units/Enemy"}
        };
        public const string Player = "Units/Player";
        public const string UI = "UI";
    }
}