using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class LeaderboardShowcase : MonoBehaviour
    {
        [SerializeField] private string _leaderboardPublicKey = "1e66bc43188b7a0d9029382cec5334b2d0bf046e3bf9d48e648a3e5ffbb6a9e7";
        
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private TextMeshProUGUI[] _entryFields;
        
        [SerializeField] private TMP_InputField _playerUsernameInput;

        private int _playerScore;
        
        private void Start()
        {
            Load();
        }

        public void AddPlayerScore()
        {
            _playerScore++;
            _playerScoreText.text = "Your score: " + _playerScore;
        }
        
        public void Load() => LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, OnLeaderboardLoaded);

        private void OnLeaderboardLoaded(Entry[] entries)
        {
            foreach (var entryField in _entryFields)
            {
                entryField.text = "";
            }

            for (int i = 0; i < entries.Length; i++)
            {
                _entryFields[i].text = $"{i+1}. {entries[i].Username} : {entries[i].Score}";
            }
        }

        public void Submit()
        {
            LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _playerUsernameInput.text, _playerScore, OnUploadComplete);
        }

        private void OnUploadComplete(bool success)
        {
            if (success)
                Load();
        }
    }
}
