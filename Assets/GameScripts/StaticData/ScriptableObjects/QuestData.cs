using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "StaticData/QuestData")]
    public class QuestData : ScriptableObject
    {
        public string questName;
        [TextArea]
        public string description;
        public questType questType;
        public int value;
        public int reward=100;
    }

    public enum questType
    {
        kills,
        talk,
        goToCoster,
        interactCoster
    };
}