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
    public QuestData curQuest;
    private QuestBookService questBookService;
    public Text questDoneName;
    public GameObject questDone;
    private PlayerMoney playerMoney;
    public int kills = 0;

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
    public void initQuest()
    {
        kills = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questsJournal.SetActive(!questsJournal.activeSelf);
        }
        //quest doned?
        if (curQuest.questType == questType.kills)
            if (kills >= curQuest.value)
            { 
                QuestDone();
                kills = 0;
            }
    }
    public void QuestDone()
    {
        playerMoney.Money += curQuest.reward;
        Debug.Log("quest doned!");
        StopCoroutine(questIdentification());
        StartCoroutine(questIdentification());
    }

    IEnumerator questIdentification()
    {
        questDoneName.text = "Квест " + "\"" + curQuest.questName + "\"" + " выполнен!";
        questDone.SetActive(true);
        yield return new WaitForSeconds(4);
        questDone.SetActive(false);
    }
}
