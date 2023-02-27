using System;
using Dan.Enums;
using Dan.Models;
using UnityEngine;
using Object = UnityEngine.Object;
using static Dan.ConstantVariables;

namespace Dan.Main
{
    public static class LeaderboardCreator
    {
        public static bool LoggingEnabled { get; set; } = true;
        
        private static LeaderboardCreatorBehaviour _behaviour;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            _behaviour = new GameObject("[LeaderboardCreator]").AddComponent<LeaderboardCreatorBehaviour>();
            Object.DontDestroyOnLoad(_behaviour.gameObject);
        }

        /// <summary>
        /// Pings the server to check if it is reachable.
        /// </summary>
        /// <param name="callback">Returns true if the server is reachable.</param>
        public static void Ping(Action<bool> callback)
        {
            _behaviour.SendGetRequest(GetServerURL(Routes.Ping), callback);
        }

        /// <summary>
        /// Fetches a leaderboard with the given public key.
        /// </summary>
        /// <param name="publicKey">The public key of the leaderboard
        /// (retrieve from https://lcv2.danqzq.games).</param>
        /// <param name="callback">Returns entries of the leaderboard if the request was successful.</param>
        public static void GetLeaderboard(string publicKey, Action<Entry[]> callback)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                LogError("Public key cannot be null or empty!");
                return;
            }
            
            _behaviour.SendGetRequest(GetServerURL(Routes.Get, $"?publicKey={publicKey}"), callback);
        }
        
        /// <summary>
        /// Uploads a new entry to the leaderboard with the given public key.
        /// </summary>
        /// <param name="publicKey">The public key of the leaderboard</param>
        /// <param name="username">The username of the player</param>
        /// <param name="score">The highscore of the player</param>
        /// <param name="callback">Returns true if the request was successful.</param>
        public static void UploadNewEntry(string publicKey, string username, int score, Action<bool> callback = null) => 
            UploadNewEntry(publicKey, username, score, " ", callback);

        /// <summary>
        /// Uploads a new entry to the leaderboard with the given public key.
        /// </summary>
        /// <param name="publicKey">The public key of the leaderboard</param>
        /// <param name="username">The username of the player</param>
        /// <param name="score">The highscore of the player</param>
        /// <param name="extra">Extra data to be stored with the entry</param>
        /// <param name="callback">Returns true if the request was successful.</param>
        public static void UploadNewEntry(string publicKey, string username, int score, string extra, Action<bool> callback = null)
        {
            if (string.IsNullOrEmpty(publicKey))
            {
                LogError("Public key cannot be null or empty!");
                return;
            }

            if (string.IsNullOrEmpty(username))
            {
                LogError("Username cannot be null or empty!");
                return;
            }
            
            if (extra.Length > MaxExtraStringLength)
                Log("Extra string is too long, it will be truncated!");

            _behaviour.SendPostRequest(GetServerURL(Routes.Upload), Requests.Form(
                Requests.Field("publicKey", publicKey),
                Requests.Field("username", username),
                Requests.Field("score", score.ToString()),
                Requests.Field("extra", extra)), callback);
        }
        
        internal static void Log(string message)
        {
            if (!LoggingEnabled) return;
            Debug.Log($"[LeaderboardCreator] {message}");
        }
        
        internal static void LogError(string message)
        {
            if (!LoggingEnabled) return;
            Debug.LogError($"[LeaderboardCreator] {message}");
        }
    }
}