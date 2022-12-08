using UnityEngine;

namespace GameScripts.Services.Unity
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(this);
        }
    }
}