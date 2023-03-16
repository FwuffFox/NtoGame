using GameScripts.Logic.Units.Player;
using GameScripts.Services.UnitSpawner;
using GameScripts.StaticData.ScriptableObjects;
using System.Collections;
using GameScripts.Logic.UI.InGame.Quests;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [FormerlySerializedAs("questsJournal")] public GameObject QuestsJournal;
    [FormerlySerializedAs("questSystemUI")] public QuestSystemUI QuestSystemUI;
    [FormerlySerializedAs("curQuest")] public QuestData CurQuest;
    [FormerlySerializedAs("questDoneName")] public Text QuestDoneName;
    public Text QuestDoneMoney;
    [FormerlySerializedAs("questDone")] public GameObject DoneQuest;
    private PlayerMoney _playerMoney;
    private Vector3 _notificationPos;
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

    private void Start() 
    { 
        Instance = this;
        _notificationPos = DoneQuest.transform.position;
        if (!FileUsing.SaveExists()) return;
        var nums = FileUsing.ReadFile().ToArray();
        for (int i = 0; i < nums.Count(); i++)
        {
            CurQuest = QuestSystemUI.Quests[i].QuestData;
            QuestSystemUI.Quests[i].Done = nums[i] == 1;
            if (nums[i] == 0) continue;
            
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
            if (QuestSystemUI.ActiveQuest)
            {
                QuestSystemUI.ActiveQuest!.Background.color = Color.white;
                QuestSystemUI.ActiveQuest = null;
            }
        }
        if (CurQuest.nextQuest != -1) QuestSystemUI.Quests[CurQuest.nextQuest].gameObject.SetActive(true);
        if (CurQuest.additionalQuest != -1) QuestSystemUI.Quests[CurQuest.additionalQuest].gameObject.SetActive(true);
        QuestSystemUI.SelectQuest(0);
        CurQuest = null;
        //file
        var questState = QuestSystemUI.Quests
            .Select(x => x.Done ? 1 : 0);
        FileUsing.WriteFile(questState);
    }

    private IEnumerator QuestIdentification()
    {
        QuestDoneName.text = "Квест " + "\"" + CurQuest.questName + "\"" + " выполнен!"
            +"\n"+"Доступен новый квест!";
        DoneQuest.transform.position = _notificationPos;
        QuestDoneMoney.text = CurQuest.reward.ToString();
        DoneQuest.SetActive(true);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 360; i++)
        {
            yield return new WaitForSeconds(1/60);
            DoneQuest.transform.Translate(1f, 0, 0);
        }
        DoneQuest.SetActive(false);
    }
}
