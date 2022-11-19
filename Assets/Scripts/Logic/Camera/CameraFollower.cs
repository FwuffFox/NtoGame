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
            var position = transform.position;
            var z = position.z;
            position = Vector3.Lerp(position,
                _target.transform.position + cameraDistance,
                lerpValue * Time.deltaTime * lerpSpeed);
            position = new Vector3(position.x, position.y, z);
            transform.position = position;
        }
    }
}