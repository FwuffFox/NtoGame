using UnityEngine;

namespace GameScripts.Logic.UI.InGame.Campfire
{
    public class CampfireUI : MonoBehaviour
    {
        private GameObject _player;
        
        public void TurnOn(GameObject player)
        {
            _player = player;
            gameObject.SetActive(true);
            foreach (var button in GetComponentsInChildren<BuyButton>())
            {
                button.Player = _player;
            }
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}