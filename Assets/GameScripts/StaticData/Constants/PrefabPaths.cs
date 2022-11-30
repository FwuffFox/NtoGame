using System;
using System.Collections.Generic;
using GameScripts.Infrastructure.States;
using GameScripts.StaticData.Enums;

namespace GameScripts.StaticData.Constants
{
    public static class PrefabPaths
    {
        /// <summary>
        /// Gives a path corresponding to key (Enemy type)
        /// </summary>
        public static readonly Dictionary<EnemyType, string> Enemies = new()
        {
            {EnemyType.Default, "Units/Enemies/Default"},
            {EnemyType.Stronger, "Units/Enemies/Stronger"}
        };
        
        /// <summary>
        /// Gives a path corresponding to key (Current state)
        /// </summary>
        public static readonly Dictionary<Type, string> UIs = new()
        {
            {typeof(MenuState), "UIs/InMenuUI"},
            {typeof(LoadLevelState), "UIs/InGameUI"}
        };
        
        public const string Player = "Units/Player";
        public const string UI = "UI";
        public const string BearTrap = "Traps/BearTrap";
        public const string MapGenerator = "Generators/MapGenerator";
    }
}