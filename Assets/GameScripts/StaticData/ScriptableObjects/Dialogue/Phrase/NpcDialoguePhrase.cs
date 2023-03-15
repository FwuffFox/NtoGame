using System;
using UnityEngine;
using System.Collections.Generic;

namespace GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase
{
    [CreateAssetMenu(fileName = "NpcDialoguePhrase",
        menuName = "DialogueSystem/NpcDialoguePhrase", order = 0)]
    public class NpcDialoguePhrase : DialoguePhraseBase
    {
        public bool DoesCallPlayerChoice;
        public List<PlayerAnswer> PlayerAnswers;
    }

    [Serializable]
    public struct PlayerAnswer
    {
        public string Text;
        public DialoguePhraseBase NextPhrase;
    }
}