using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Enums;
using Dan.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Dan.Main
{
    public class LeaderboardCreatorBehaviour : MonoBehaviour
    {
        internal void SendGetRequest(string url, Action<bool> callback = null)
        {
            var request = UnityWebRequest.Get(url);
            StartCoroutine(HandleRequest(request, isSuccessful =>
            {
                callback?.Invoke(isSuccessful);

                if (isSuccessful)
                {
                    LeaderboardCreator.Log(request.downloadHandler.text);
                    return;
                }
                
                HandleError(request);
            }));
        }
        
        internal void SendGetRequest(string url, Action<Entry[]> callback = null)
        {
            var request = UnityWebRequest.Get(url);
            StartCoroutine(HandleRequest(request, isSuccessful =>
            {
                if (!isSuccessful)
                {
                    HandleError(request);
                    return;
                }
                var response = JsonConvert.DeserializeObject<List<Entry>>(request.downloadHandler.text);
                callback?.Invoke(response.ToArray());
                LeaderboardCreator.Log("Successfully retrieved leaderboard data!");
            }));
        }
        
        internal void SendPostRequest(string url, List<IMultipartFormSection> form, Action<bool> callback = null)
        {
            var request = UnityWebRequest.Post(url, form);
            callback += isSuccessful =>
            {
                if (!isSuccessful)
                    LeaderboardCreator.LogError("Uploading entry data failed!");
                else
                    LeaderboardCreator.Log("Successfully uploaded entry data to leaderboard!");
            };
            StartCoroutine(HandleRequest(request, callback));
        }
        
        private static IEnumerator HandleRequest(UnityWebRequest request, Action<bool> onComplete)
        {
            yield return request.SendWebRequest();

            if (request.responseCode != 200)
            {
                onComplete.Invoke(false);
                yield break;
            }

            onComplete.Invoke(true);
            request.Dispose();
        }

        private static void HandleError(UnityWebRequest request)
        {
            var message = Enum.GetName(typeof(StatusCode), (StatusCode) request.responseCode).SplitByUppercase();
                
            var downloadHandler = request.downloadHandler;
            var text = downloadHandler.text;
            if (!string.IsNullOrEmpty(text))
                message = $"{message}: {text}";
            LeaderboardCreator.LogError(message);
        }
    }
}