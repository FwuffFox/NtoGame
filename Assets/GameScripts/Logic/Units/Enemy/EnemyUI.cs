using EditorScripts.Inspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.Units.Enemy
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeReadOnly, SerializeField] private EnemyHealth _enemy;

        private Transform _transform;

        public void Start()
        {
            if (UnityEngine.Camera.main != null) _transform = UnityEngine.Camera.main.transform;
        }

        public void Update()
        {
            _healthSlider.transform.LookAt(_transform);
        }

        public void SetTarget(EnemyHealth target)
        {
            _enemy = target;
            _enemy.OnBattleUnitHealthChange += SetNewHealth;
            _healthSlider.maxValue = _enemy.MaxHealth;
            _healthSlider.value = _enemy.Health;
        }
        
        public void SetNewHealth(float health)
        {
            _healthSlider.value = health;
        }

        private void OnDestroy()
        {
            _enemy.OnBattleUnitHealthChange -= SetNewHealth;
        }
    }
}