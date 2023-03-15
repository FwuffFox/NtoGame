using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public QuestBox activeQuest;
    public Text description;
    public List<QuestBox> quests;
    private int selectedQuestId = 0;
    void OnEnable()
    {
        if (activeQuest)
        {
            activeQuest.background.color = Color.green;
            description.text =
                activeQuest.questData.questName + "\n" + activeQuest.questData.description;
        }
    }
    public void SelectQuest(int id)
    {
        for (int i=0;i<quests.Count;i++)
            quests[i].background.color = Color.white;
        if (activeQuest)
            activeQuest.background.color = Color.green;
        quests[id].background.color = Color.gray;
        description.text =
            quests[id].questData.questName +"\n"+quests[id].questData.description;
        selectedQuestId = id;
    }

    public void SetActiveQuest()
    {
        activeQuest.background.color = Color.white;
        activeQuest = quests[selectedQuestId];
        activeQuest.background.color = Color.green;
        QuestManager.questManager.curQuest = activeQuest.questData;
    }
}
