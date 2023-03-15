using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase
{
    public abstract class DialoguePhraseBase : ScriptableObject
    {
        [Header(nameof(DialoguePhraseBase))]
        [TextArea] public string Text;
        public AudioClip DialogueAudio;
        public DialoguePhraseBase NextPhrase;
        public bool IsFinalPhrase;
    }
}