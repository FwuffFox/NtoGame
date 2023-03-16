using GameScripts.StaticData.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using static GameScripts.StaticData.ScriptableObjects.LevelData;

[CreateAssetMenu(fileName = "Level", menuName = "StaticData/New level")]
public class LevelSO : ScriptableObject
{
    public LevelData levelData;
    public List<TileWithPower> objs;

    [ContextMenu("Magic")]
    void Magic()
    {
        objs.Clear();
        for (int w = 0; w < levelData.mapSize; w++)
        {
            for (int h = 0; h < levelData.mapSize; h++)
            {
                objs.Add(levelData.GeneratorTiles[Random.Range(0, levelData.GeneratorTiles.Length)]);
            }
        }
    }
}
