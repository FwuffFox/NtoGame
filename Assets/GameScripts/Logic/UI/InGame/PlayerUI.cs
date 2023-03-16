using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class PlayerUI : MonoBehaviour
    {
        [FormerlySerializedAs("ui")] [SerializeField] private Image _ui;

        private Transform _transform;

        public void Start()
        {
            if (UnityEngine.Camera.main != null) _transform = UnityEngine.Camera.main.transform;
        }

        public void Update()
        {
            _ui.transform.LookAt(_transform);
        }
    }
}
