using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
    public QuestBox activeQuest;
    public Text description;
    public GameObject buttons;
    public List<QuestBox> quests;
    private int selectedQuestId = 0;

    private void Awake()
    {
        //read from file
        readTextFile("Quest.txt");
        void readTextFile(string file_path)
        {
            StreamReader inp_stm = new StreamReader(file_path);

            while (!inp_stm.EndOfStream)
            {
                string inp_ln = inp_stm.ReadLine();
                Debug.Log(inp_ln);
                // Do Something with the input. 
            }

            inp_stm.Close();
        }
    }

    void OnEnable()
    {
        if (activeQuest)
        {
            SetColor();
            description.text =
                activeQuest.questData.questName
                + "\n" + activeQuest.questData.description
                + "\n" + "Награда: "+activeQuest.questData.reward+" монет";
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
        if (!quests[id].doned) buttons.SetActive(true);
        else buttons.SetActive(false);
    }

    public void SetActiveQuest()
    {
        if (activeQuest) activeQuest.background.color = Color.white;
        activeQuest = quests[selectedQuestId];
        activeQuest.background.color = Color.green;
        QuestManager.questManager.curQuest = activeQuest.questData;
    }

    private void SetColor(int selectedId=-1)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (!quests[i].doned)
                quests[i].background.color = Color.white;
            else quests[i].background.color = Color.yellow;
        }
        if (activeQuest)
            activeQuest.background.color = Color.green;

        if (selectedId!=-1) 
            quests[selectedId].background.color = Color.gray;
    }
}
