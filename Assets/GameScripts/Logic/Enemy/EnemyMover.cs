using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace GameScripts.Logic.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        private float _speed;

        public void SetProperties(EnemyData enemyData)
        {
            _speed = enemyData.speed;
        }

        public void Follow(GameObject obj)
        {
            navMeshAgent.speed = _speed;
            navMeshAgent.SetDestination(obj.transform.position);
        }
    }
}