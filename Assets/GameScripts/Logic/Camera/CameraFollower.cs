using JetBrains.Annotations;
using UnityEngine;

namespace GameScripts.Logic.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [CanBeNull] private GameObject _target;

        public void SetTarget(GameObject target) => _target = target;

        private void LateUpdate()
        {
            if (_target == null) return;
            var targetPos = _target.transform.position;
            transform.position = new Vector3(targetPos.x, 0, targetPos.z);
        }
    }
}