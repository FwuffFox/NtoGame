using GameScripts.Logic.Units.Player;
using GameScripts.Services.UnitSpawner;
using GameScripts.StaticData.ScriptableObjects;
using System.Collections;
using GameScripts.Logic.UI.InGame.Quests;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [FormerlySerializedAs("questsJournal")] public GameObject QuestsJournal;
    [FormerlySerializedAs("questSystemUI")] public QuestSystemUI QuestSystemUI;
    [FormerlySerializedAs("curQuest")] public QuestData CurQuest;

    [FormerlySerializedAs("questDoneName")] public Text QuestDoneName;
    [FormerlySerializedAs("questDone")] public GameObject DoneQuest;
    private PlayerMoney _playerMoney;
    [FormerlySerializedAs("kills")] public int Kills = 0;
    [FormerlySerializedAs("goToCoster")] public int GoToCoster = 0;
    [FormerlySerializedAs("fireCoster")] public bool FireCoster = false;
    [FormerlySerializedAs("interactWithCoster")] public bool InteractWithCoster = false;
    [FormerlySerializedAs("talked")] public bool Talked = false;
    [FormerlySerializedAs("haveTorch")] public bool HaveTorch = false;
    [FormerlySerializedAs("canInterectWithNpc")] public bool CanInterectWithNpc = false;

    [Inject]
    public void Constructor(IUnitSpawner unitSpawner)
    {
        _playerMoney = unitSpawner.Player.GetComponent<PlayerMoney>();
    }

    private void Awake() 
    { 
        Instance = this;
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftAlt)) UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
#endif
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (QuestsJournal.activeSelf)
                PlayerInputSystem.InGame.Enable();
            else
                PlayerInputSystem.InGame.Disable();
            QuestsJournal.SetActive(!QuestsJournal.activeSelf);
        }
        //check additional quests
        if (Talked)
                QuestSystemUI.Quests[5].gameObject.SetActive(true);
        if (!CurQuest) return;
        //quest doned?
        switch (CurQuest.questType)
        {
            case questType.kills:
                if (Kills >= CurQuest.value)
                    QuestDone();
                break;
            case questType.goToCoster:
                if (GoToCoster >= CurQuest.value)
                    QuestDone();
                break;
            case questType.talk:
                if (Talked)
                {
                    Talked = false;
                    QuestDone();
                }
                break;
            case questType.fireCoster:
                if (FireCoster)
                {
                    CanInterectWithNpc = true;
                    QuestDone();
                }
                break;
            case questType.interactCoster:
                if (InteractWithCoster)
                {
                    QuestDone();
                }
                break;
        }
    }

    private void QuestDone()
    {
        _playerMoney.Money += CurQuest.reward;
        StopCoroutine(QuestIdentification());
        StartCoroutine(QuestIdentification());
        //find active quest
        foreach (var questBox in QuestSystemUI.Quests)
        {
            if (questBox.QuestData != CurQuest) continue;
            
            questBox.Done = true;
            QuestSystemUI.ActiveQuest!.Background.color = Color.white;
            QuestSystemUI.ActiveQuest = null;
        }
        if (CurQuest.nextQuest != -1) QuestSystemUI.Quests[CurQuest.nextQuest].gameObject.SetActive(true);
        if (CurQuest.additionalQuest != -1) QuestSystemUI.Quests[CurQuest.additionalQuest].gameObject.SetActive(true);
        QuestSystemUI.SelectQuest(0);
        CurQuest = null;
    }

    private IEnumerator QuestIdentification()
    {
        QuestDoneName.text = "Квест " + "\"" + CurQuest.questName + "\"" + " выполнен!";
        DoneQuest.SetActive(true);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForEndOfFrame();
            DoneQuest.transform.Translate(1, 0, 0);
        }
        DoneQuest.SetActive(false);
    }
}
