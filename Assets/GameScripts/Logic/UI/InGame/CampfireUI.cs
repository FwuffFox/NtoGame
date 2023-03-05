using UnityEngine;

namespace GameScripts.Logic.UI.InGame
{
    public class CampfireUI : MonoBehaviour
    {
        private GameObject _player;
        
        public void TurnOn(GameObject player)
        {
            _player = player;
            gameObject.SetActive(true);
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}