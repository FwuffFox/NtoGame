using StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private float _speed;

        public void SetProperties(EnemyData enemyData)
        {
            _speed = enemyData.speed;
        }
        
        private void OnEnable()
        {
            if (!TryGetComponent(out _navMeshAgent))
                Debug.LogError("Enemy doesn't have NavMeshAgent");
        }
        
        public void Follow(GameObject obj)
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.SetDestination(obj.transform.position);
        }
    }
}