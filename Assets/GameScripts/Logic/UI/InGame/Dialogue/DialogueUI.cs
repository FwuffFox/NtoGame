using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.ScriptableObjects.Dialogue;
using GameScripts.StaticData.ScriptableObjects.Dialogue.Phrase;
using UnityEngine;
using UnityEngine.Rendering;
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
        [SerializeField] private Button _dialogueCancelButton;
        
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
                return;
            }
            UseDialog();
        }

        private void UseDialog()
        {
            _currentPhrase = _currentPhrase.NextPhrase;
            if (_currentPhrase is NpcDialoguePhrase { DoesCallPlayerChoice: true } phrase)
            {
                PlayerAnswer[] temp = new PlayerAnswer[phrase.PlayerAnswers.Count];
                phrase.PlayerAnswers.CopyTo(temp);
                _answers = temp.ToList();
            }
            _dialogueText.text = _currentPhrase.Text;
            _audio.clip = _currentPhrase.DialogueAudio;
            _audio.Play();
            StartCoroutine(PlayDialog());
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

            _dialogueCancelButton.gameObject.SetActive(true);
            _dialogueCancelButton.GetComponentInChildren<Text>().text = _dialogueSo.DialogueCancelAnswer.Text;
            _dialogueCancelButton.onClick.RemoveAllListeners();
            _dialogueCancelButton.onClick
                .AddListener(() => _dialogueCancelButton.gameObject.SetActive(false));
            _dialogueCancelButton.onClick.AddListener(Exit);
        }

        private void Answer(DialoguePhraseBase phrase)
        {
            _answerButtons.ForEach(b => b.gameObject.SetActive(false));
            _currentPhrase = phrase;
            _dialogueText.text = _currentPhrase.Text;
            _audio.clip = _currentPhrase.DialogueAudio;
            _audio.Play();
            _answers.Clear();
            StartCoroutine(PlayDialog());
        }
        
        private IEnumerator PlayDialog()
        {
            _dialogueBusy = true;
            while (_audio.isPlaying && !Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            _dialogueBusy = false;
            if (_currentPhrase.IsFinalPhrase) Exit();
        }

        private void Exit()
        {
            PlayerInputSystem.InGame.Enable();
            _currentPhrase = null;
            gameObject.SetActive(false);
        }
    }
}