using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame.Quests
{
    public class QuestBox : MonoBehaviour
    {
        [FormerlySerializedAs("questData")] public QuestData QuestData;
        [FormerlySerializedAs("titleText")] public Text TitleText;
        [FormerlySerializedAs("background")] public Image Background;
        [FormerlySerializedAs("doned")] public bool Done = false;

        public void Start()
        {
            TitleText.text = QuestData.questName;
        }
    }
}
