using System.Collections;
using System.Collections.Generic;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects.Dialogue;
using GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame.Dialogue
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Text _dialogueText;
        [SerializeField] private Text _npcName;
        [SerializeField, SerializeReadOnly] private NpcDialogueSO _dialogueSo;
        [SerializeField] private DialoguePhraseBase _currentPhrase;

        [SerializeField] private AudioSource _audio;

        [SerializeField, SerializeReadOnly] private bool _dialogueBusy;
        [SerializeField, SerializeReadOnly] private List<PlayerAnswer> _answers;
        [SerializeField] private List<Button> _answerButtons;
        
        public void Enter(NpcDialogueSO dialogueSo)
        {
            PlayerInputSystem.InGame.Disable();
            _dialogueSo = dialogueSo;
            _npcName.text = _dialogueSo.NpcName;
            gameObject.SetActive(true);
            
            _currentPhrase = _dialogueSo.FirstPhrase;
            _dialogueText.text = _currentPhrase.Text;
            _audio.clip = _currentPhrase.DialogueAudio;
            _audio.Play();
            StartCoroutine(PlayDialog());
        }

        private void Update()
        {
            if (!gameObject.activeSelf || _dialogueBusy) return;
            if (_answers.Count != 0)
            {
                UseAnswers();
            }
            UseDialog();
        }

        private void UseDialog()
        {
            _currentPhrase = _currentPhrase.NextPhrase;
            if (_currentPhrase is NpcDialoguePhrase { DoesCallPlayerChoice: true } phrase)
            {
                _answers = phrase.PlayerAnswers;
            }
            _dialogueText.text = _currentPhrase.Text;
            _audio.clip = _currentPhrase.DialogueAudio;
            _audio.Play();
            StartCoroutine(PlayDialog());
        }
        
        private IEnumerator PlayDialog()
        {
            _dialogueBusy = true;
            while (_audio.isPlaying || !Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            _dialogueBusy = false;
        }
        
        private void UseAnswers()
        {
            _dialogueBusy = true;
            for (var i = 0; i < _answers.Count; i++)
            {
                var button = _answerButtons[i];
                button.gameObject.SetActive(true);
                var answer = _answers[i];
                button.GetComponentInChildren<Text>().text = answer.Text;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => Answer(answer.NextPhrase));
            }
        }

        private void Answer(DialoguePhraseBase phrase)
        {
            _answerButtons.ForEach(b => b.gameObject.SetActive(false));
            _currentPhrase = phrase;
            _dialogueText.text = _currentPhrase.Text;
            _audio.clip = _currentPhrase.DialogueAudio;
            _audio.Play();
            StartCoroutine(PlayDialog());
        }

        public void Exit()
        {
            PlayerInputSystem.InGame.Enable();
            _dialogueSo = null;
            gameObject.SetActive(false);
        }
    }
}