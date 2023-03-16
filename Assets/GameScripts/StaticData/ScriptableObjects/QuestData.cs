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
        public questImportance questImportance;
        public int value;
        public int reward=100;
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