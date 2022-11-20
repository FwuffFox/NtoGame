using UnityEngine;

namespace StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "StaticData/GameData")]
    public class GameData : ScriptableObject
    {
        //Test
        public int pointsPerSecond;
    }
}