using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AttackSO", menuName = "StaticData/Attack")]
    public class AttackSO : ScriptableObject
    {
        public string AttackName;
        public float AttackDuration;
        public AudioClip AttackAudio;
    }
}