using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePhrase : MonoBehaviour
{
    public Text text;
    public DialogueSystem dialogueSystem;
    public int id;

    public void chooseDialoge()
    {
        dialogueSystem.loadDialogue(id);
    }
}
