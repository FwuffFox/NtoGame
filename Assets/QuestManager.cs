using GameScripts.Services;
using GameScripts.StaticData.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public QuestData curQuest;
    private QuestBookService questBookService;
    public int kills = 0;

    void Awake() 
    { 
        questManager = this;
        questBookService = new QuestBookService();
    }
    public void initQuest()
    {
        kills = 0;
    }
    private void Update()
    {
        if (curQuest.questType == questType.kills)
            if (kills >= curQuest.value)
            { 
                QuestDone();
                kills = 0;
            }
    }
    public void QuestDone()
    {
        Debug.Log("quest doned!");
    }
}
