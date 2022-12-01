using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InMenu
{
    public class MenuUI : MonoBehaviour
    {
        public Button PlayButton;
        [SerializeField] private GameObject _loadPanel;

        public void StartLoadingScreen()
        {
            _loadPanel.SetActive(true);
        }
    }
}