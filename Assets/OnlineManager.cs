using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using UnityEngine.UI;

public class OnlineManager : MonoBehaviour
{
    private string publicKey = "54dbcaf47fa11e5939dd51e664f1ab399d9e1710fb3e3eebb2f678771cc01fab";
    public Text leaderBoard;
    public InputField nick;
    public bool inMenu = false;

    private void Start()
    {
        if (!inMenu) return;
        GetLeaderBoard();
        if (PlayerPrefs.HasKey("Nick"))
            nick.text = PlayerPrefs.GetString("Nick");
        else {
            nick.text = "Player" + Random.Range(1000, 9999);
            SaveNick();
        }
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
         {
             for (int i = 0; i < msg.Length; i++)
             {
                 leaderBoard.text += msg[i].Username + " - " + msg[i].Score+'\n';
             }
         }));
    }

    public void SetLeaderBoard(int score)
    {
        string playerNick=PlayerPrefs.GetString("Nick");
        LeaderboardCreator.UploadNewEntry(publicKey, playerNick, score);
    }

    public void SaveNick()
    {
        PlayerPrefs.SetString("Nick", nick.text);
    }
}
