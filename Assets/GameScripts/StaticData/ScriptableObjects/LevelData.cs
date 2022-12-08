using System;
using GameScripts.Logic.Tiles;
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

        public int unitCount;
        
        [Serializable]
        public struct LevelCurses
        {
            public CurseType Type;
            public int Amount;
        }

        [Space] public LevelCurses[] Curses;

        [Serializable]
        public struct TileWithPower
        {
            public Tile Tile;
            public int Power;
        }
        [Space] public TileWithPower[] GeneratorTiles;

        [Serializable]
        public struct XZCoord
        {
            public int X;
            public int Z;
        }

        [Space] public XZCoord[] CheckpointsCoors;
    }
}