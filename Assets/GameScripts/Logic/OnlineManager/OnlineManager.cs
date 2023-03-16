using Dan.Main;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts.Logic.OnlineManager
{
    public class OnlineManager : MonoBehaviour
    {
        private const string PublicKey = "54dbcaf47fa11e5939dd51e664f1ab399d9e1710fb3e3eebb2f678771cc01fab";
    
        public static OnlineManager Manager;
        [FormerlySerializedAs("kills")] public int Kills = 0;
        [FormerlySerializedAs("leaderBoard")] [Header("Menu")]
        public Text LeaderBoard;
        [FormerlySerializedAs("nick")] public InputField Nick;
        [FormerlySerializedAs("inMenu")] public bool InMenu = false;

        private void Start()
        {
            if (!InMenu) return;
#if UNITY_EDITOR
            UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
#endif
            Manager = this;
            GetLeaderBoard();
            if (PlayerPrefs.HasKey("Nick"))
                Nick.text = PlayerPrefs.GetString("Nick");
            else {
                Nick.text = "Player" + Random.Range(1000, 9999);
                SaveNick();
            }
        }

        private void GetLeaderBoard()
        {
            LeaderboardCreator.GetLeaderboard(PublicKey, ((msg) =>
            {
                if (!LeaderBoard) return;
                foreach (var entry in msg)
                {
                    LeaderBoard.text += entry.Username + " - " + entry.Score+'\n';
                }
            }));
        }

        public void SetLeaderBoard()
        {
            var playerNick = PlayerPrefs.GetString("Nick");
            LeaderboardCreator.UploadNewEntry(PublicKey, playerNick, Kills);
        }

        private void SaveNick()
        {
            PlayerPrefs.SetString("Nick", Nick.text);
        }
    }
}
