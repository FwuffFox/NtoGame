using GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase;
using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects.Dialogue
{
    [CreateAssetMenu(fileName = "NpcDialogueSO", menuName = "DialogueSystem/NpcDialogueSO", order = 0)]
    public class NpcDialogueSO : ScriptableObject
    {
        public string NpcName;
        public DialoguePhraseBase FirstPhrase;
    }
}