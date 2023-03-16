using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Quest Data", menuName = "StaticData/QuestData")]
    public class QuestData : ScriptableObject
    {
        [Header("Current quest")]
        public string questName;
        [TextArea]
        public string description;
        public questType questType;
        public questImportance questImportance;
        public int value;
        public int reward=100;
        [Header("Quests branch")]
        public int additionalQuest = -1;
        public int nextQuest=-1;
    }

    public enum questType
    {
        kills,
        talk,
        goToCoster,
        fireCoster,
        interactCoster
    };
    public enum questImportance
    {
        main,
        additional
    };
}