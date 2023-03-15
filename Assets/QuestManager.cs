using GameScripts.Logic.Units.Player;
using GameScripts.Services;
using GameScripts.Services.UnitSpawner;
using GameScripts.StaticData.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;
    public GameObject questsJournal;
    public QuestSystemUI questSystemUI;
    public QuestData curQuest;
    private QuestBookService questBookService;
    public Text questDoneName;
    public GameObject questDone;
    private PlayerMoney playerMoney;
    public int kills = 0;
    public int goToCoster = 0;

    [Inject]
    public void Constructor(IUnitSpawner unitSpawner)
    {
        playerMoney=unitSpawner.Player.GetComponent<PlayerMoney>();
    }
    void Awake() 
    { 
        questManager = this;
        questBookService = new QuestBookService();
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftAlt)) UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
#endif
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questsJournal.activeSelf)
                PlayerInputSystem.InGame.Enable();
            else
                PlayerInputSystem.InGame.Disable();
            questsJournal.SetActive(!questsJournal.activeSelf);
        }
        //quest doned?
        if (curQuest.questType == questType.kills)
            if (kills >= curQuest.value)
                QuestDone();
        if (curQuest.questType == questType.goToCoster)
            if (goToCoster >= curQuest.value)
                QuestDone();
    }
    public void QuestDone()
    {
        playerMoney.Money += curQuest.reward;
        Debug.Log("quest doned!");
        StopCoroutine(questIdentification());
        StartCoroutine(questIdentification());
        //find active quest
        for (int i = 0; i < questSystemUI.quests.Count;i++) {
            if (questSystemUI.quests[i].questData == curQuest)
            {
                questSystemUI.quests[i].doned = true;
                questSystemUI.activeQuest = null;
            }
        }
    }

    IEnumerator questIdentification()
    {
        questDoneName.text = "Квест " + "\"" + curQuest.questName + "\"" + " выполнен!";
        questDone.SetActive(true);
        yield return new WaitForSeconds(4);
        questDone.SetActive(false);
    }
}
