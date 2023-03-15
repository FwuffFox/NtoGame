using GameScripts.StaticData.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguePhrases", menuName = "StaticData/DialoguePhrases")]
public class DialoguePhrases : ScriptableObject
{
    public List<dialogue> phrase;
}
[System.Serializable]
public class dialogue
{
    public string phrase;
    public List<int> toPhrase;
    public QuestData quest;
}
