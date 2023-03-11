using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts.Logic.UI.InGame.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public Action OnExitButtonPressed;
        public void Enter()
        {
            Debug.Log("Entered pause menu");
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }

        public void Resume()
        {
            Debug.Log("Exited pause menu");
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        
        public void ExitToMenu()
        {
            Time.timeScale = 1f;
            OnExitButtonPressed?.Invoke();
        }
    }
}