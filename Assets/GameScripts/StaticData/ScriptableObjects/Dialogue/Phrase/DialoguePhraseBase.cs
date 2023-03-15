using UnityEngine;

namespace GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase
{
    public abstract class DialoguePhraseBase : ScriptableObject
    {
        [TextArea] public string Text;
        public AudioClip DialogueAudio;
    }
}