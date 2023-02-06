using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic.CurseObject
{
    [RequireComponent(typeof(SphereCollider))]
    public class CurseObjectMover : MonoBehaviour
    {
        [SerializeField] private float _moveRadius;
        [SerializeField] private float _moveCooldown;
        [SerializeField] private bool _canMove = true;
        [SerializeField] private SphereCollider _collider;
        [SerializeReadOnly, SerializeField] private bool _isEnable;

        public void Enable(bool isEnable)
        {
            _isEnable = isEnable;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_canMove) return;
            if (!_isEnable) return;
            if (!other.TryGetComponent(out PlayerMovement _)) return;
            var playerPos = other.transform.position;
            var myTransform = transform;
            var myPos = myTransform.position;
            var oppositeVector =
                new Vector3(playerPos.x - myPos.x, 0, playerPos.z - myPos.z).normalized;
            myTransform.parent.parent.position -= oppositeVector * _moveRadius;
            StartCoroutine(MoveCooldown());
            print("should tp");
        }

        private void OnDrawGizmos()
        {
            if (!_isEnable) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _collider.radius * transform.lossyScale.x);
        }

        private IEnumerator MoveCooldown()
        {
            _canMove = false;
            yield return new WaitForSeconds(_moveCooldown);
            _canMove = true;
        }
        
    }
}