using GameScripts.StaticData.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public QuestData curQuest;
    public int kills = 0;

    void Awake() => questManager = this;
    public void initQuest()
    {
        kills = 0;
    }
}
