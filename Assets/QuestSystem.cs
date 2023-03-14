using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public Quest activeQuest;
    public List<Quest> quests;
    void OnEnable()
    {
        if (activeQuest) 
            activeQuest.background.color = Color.green;
    }
    public void SelectQuest(int id)
    {
        for (int i=0;i<quests.Count;i++)
            quests[i].background.color = new Color(0, 0, 100);
        if (activeQuest)
            activeQuest.background.color = Color.green;
        quests[id].background.color = new Color(0, 0, 63);
    }
}
