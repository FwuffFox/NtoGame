using UnityEngine;

namespace StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int maxHealth;
        public float speed;
    }
}