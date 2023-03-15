using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public DialoguePhrases playerPhrases;
    public DialoguePhrases npcPhrases;
    public Text dialogue;
    public GameObject dialogueWindow;
    public Transform content;
    public GameObject phrase;
    public bool player = false;
    private void OnEnable()
    {
        dialogue.text = "";
        loadDialogue(0, npcPhrases.phrase[0].phrase);
    }

    public void loadDialogue(int id,string text)
    {
        Debug.Log(player + "," + id);
        for (int i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
        if (id == -1) {
            dialogueWindow.SetActive(false);
            player = false;
            Debug.Log("Exit dialog...");
            return;
        }
        //осн код
        if (!player)
        {
            player = true;
            dialogue.text += "Странник: "+ text + "\n";
            //создаём кнопки
            for (int i = 0; i < npcPhrases.phrase[id].toPhrase.Count; i++)
            {
                Transform newPhrase = Instantiate(phrase).transform;
                newPhrase.SetParent(content);
                DialoguePhrase dialoguePhrase = newPhrase.GetComponent<DialoguePhrase>();
                dialoguePhrase.dialogueSystem = this;
                dialoguePhrase.id =
                    playerPhrases.phrase[npcPhrases.phrase[id].toPhrase[i]].toPhrase[0];
                dialoguePhrase.text.text = 
                    playerPhrases.phrase[npcPhrases.phrase[id].toPhrase[i]].phrase;
            }
        }
        else
        {
            player = false;
            dialogue.text += "Вы: " + text+ "\n";
            loadDialogue(id,npcPhrases.phrase[id].phrase);
        }
    }
}
