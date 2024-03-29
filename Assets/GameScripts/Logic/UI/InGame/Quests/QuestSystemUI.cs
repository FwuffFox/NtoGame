using System.Collections.Generic;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame.Quests
{
    public class QuestSystemUI : MonoBehaviour
    {
        [FormerlySerializedAs("activeQuest")] public QuestBox ActiveQuest;
        [FormerlySerializedAs("description")] public Text Description;
        [FormerlySerializedAs("buttons")] public GameObject Buttons;
        [FormerlySerializedAs("buttons")] public GameObject DeclineButton;
        [FormerlySerializedAs("quests")] public List<QuestBox> Quests;
        private int _selectedQuestId = 0;

        private void OnEnable()
        {
            if (ActiveQuest)
            {
                SelectQuest(0);
            }
            else
            {
                Description.text = "Выберите квест";
                SetColor();
                Buttons.SetActive(false);
            }
        }
        public void SelectQuest(int id)
        {
            SetColor(id);
            Description.text =
                Quests[id].QuestData.questName
                + "\n" + Quests[id].QuestData.description
                + "\n" + "Награда: " + Quests[id].QuestData.reward + " монет";
            _selectedQuestId = id;
            if (Quests[id].QuestData.questImportance == questImportance.main)
                DeclineButton.SetActive(false);
            else
                DeclineButton.SetActive(true);
        }

        public void SetActiveQuest()
        {
            if (ActiveQuest) ActiveQuest.Background.color = Color.white;
            ActiveQuest = Quests[_selectedQuestId];
            ActiveQuest.Background.color = Color.green;
            QuestManager.Instance.CurQuest = ActiveQuest.QuestData;
        }

        public void DeclineQuest()
        {
            if (Quests[_selectedQuestId].QuestData.questImportance == questImportance.main) 
                return;
            Quests[_selectedQuestId].gameObject.SetActive(false);
            QuestManager.Instance.Talked = false;
            ActiveQuest = null;
            QuestManager.Instance.CurQuest = null;
            SelectQuest(0);
        }

        private void SetColor(int selectedId=-1)
        {
            foreach (var questBox in Quests)
                questBox.Background.color = !questBox.Done ?
                    Color.white : Color.yellow;

            if (ActiveQuest)
                ActiveQuest.Background.color = Color.green;

            if (selectedId == -1) return;
        
            Quests[selectedId].Background.color = Color.gray;

            Buttons.SetActive(!Quests[selectedId].Done);
        }
        public void ExtiButton()
        {
            PlayerInputSystem.InGame.Enable();
            gameObject.SetActive(false);
        }
    }
}
