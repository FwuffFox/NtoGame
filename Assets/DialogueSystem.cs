using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public DialoguePhrases playerPhrases;
    public DialoguePhrases npcPhrases;
    public Text dialogue;
    public Transform content;
    public GameObject phrase;
    private void OnEnable() => loadDialogue(0);

    public void loadDialogue(int id)
    {
        dialogue.text = npcPhrases.phrase[id].phrase;
        for (int i = 0; i < content.childCount; i++) Destroy(content.GetChild(i).gameObject);
        for (int i = 0; i < npcPhrases.phrase[id].toPhrase.Count; i++) {
            Transform newPhrase = Instantiate(phrase).transform;
            newPhrase.SetParent(content);
            DialoguePhrase dialoguePhrase = newPhrase.GetComponent<DialoguePhrase>();
            dialoguePhrase.text.text = playerPhrases.phrase[i].phrase;
            dialoguePhrase.id = i;
            dialoguePhrase.dialogueSystem = this;
        }
    }
}
