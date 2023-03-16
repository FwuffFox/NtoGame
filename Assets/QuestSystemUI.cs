using GameScripts.StaticData.ScriptableObjects;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
    public QuestBox activeQuest;
    public Text description;
    public GameObject buttons;
    public Text declineButton;
    public List<QuestBox> quests;
    private int selectedQuestId = 0;

    void OnEnable()
    {
        if (activeQuest)
        {
            SelectQuest(0);
        }
        else
        {
            description.text = "Выберите квест";
            SetColor();
            buttons.SetActive(false);
        }
    }
    public void SelectQuest(int id)
    {
        SetColor(id);
        description.text = 
            quests[id].questData.questName 
            +"\n"+quests[id].questData.description
            + "\n" + "Награда: " + quests[id].questData.reward + " монет";
        selectedQuestId = id;
        if (quests[id].questData.questImportance == questImportance.main)
            declineButton.text = "[Сюжетный квест]";
        else
            declineButton.text = "Отказаться";
    }

    public void SetActiveQuest()
    {
        if (activeQuest) activeQuest.background.color = Color.white;
        activeQuest = quests[selectedQuestId];
        activeQuest.background.color = Color.green;
        QuestManager.questManager.curQuest = activeQuest.questData;
    }

    public void DeclineQuest()
    {
        if (!activeQuest) return;
        if (quests[selectedQuestId].questData.questImportance == questImportance.main) 
            return;
        activeQuest = null;
        QuestManager.questManager.curQuest = null;
    }

    public void SetColor(int selectedId=-1)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (!quests[i].doned)
                quests[i].background.color = Color.white;
            else quests[i].background.color = Color.yellow;
        }
        if (activeQuest)
            activeQuest.background.color = Color.green;

        if (selectedId != -1)
        {
            quests[selectedId].background.color = Color.gray;

            if (!quests[selectedId].doned) buttons.SetActive(true);
            else buttons.SetActive(false);
        }
    }
}
