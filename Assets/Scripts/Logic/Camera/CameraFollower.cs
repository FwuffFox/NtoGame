using JetBrains.Annotations;
using UnityEngine;
using Zenject.Asteroids;

namespace Logic.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] 
        private Vector3 cameraDistance;

        [Range(0f, 1f)] [SerializeField] 
        private float lerpValue;

        [SerializeField] 
        private float lerpSpeed;

        [CanBeNull] private GameObject _target = null;

        public void SetTarget(GameObject target) => _target = target;

        private void LateUpdate()
        {
            if (_target is null) return;
            var targetPos = _target.transform.position;
            transform.position = new Vector3(targetPos.x, 0, targetPos.z);
        }
    }
}