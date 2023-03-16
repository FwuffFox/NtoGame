using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts.Logic.CurseObject
{
    [RequireComponent(typeof(SphereCollider))]
    public class CurseObject : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private CurseObjectMover _curseObjectMover;
        [SerializeField] private bool _isEnabled;
        [FormerlySerializedAs("curseCooldown")] [SerializeField] private float _curseCooldown;
        public CurseType CurseType;
        [FormerlySerializedAs("cleanseItem")] [SerializeField] private GameObject _cleanseItem;
        private bool _canCurse = true;
        [FormerlySerializedAs("afterCleanseCooldown")] [SerializeField] private float _afterCleanseCooldown;
        private bool _cleansedRecently;
        private bool _isCleanseSpawned;
        [FormerlySerializedAs("curseObjectMask")] [SerializeField] private GameObject _curseObjectMask;


        private TestCleanseObject _cleanseObj;

        public void Enable(bool isTrue) 
        {
            _isEnabled = isTrue;
            _curseObjectMover.Enable(isTrue);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled) return;
            if (_cleansedRecently) return;
            if (_isCleanseSpawned) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            if (curseSystem.Curses[CurseType].IsMaxed) return;
            var myPos = transform.position;
            var playerPos = other.transform.position;
            var oppositeVector = new Vector3(myPos.x - (playerPos.x - myPos.x), myPos.y, myPos.z - (playerPos.z - myPos.z));
            var obj = Instantiate(_cleanseItem, oppositeVector, Quaternion.identity, transform);
            var cleanse = obj.GetComponent<TestCleanseObject>();
            cleanse.SetParentCurseObject(this);
            _cleanseObj = cleanse;
            _isCleanseSpawned = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isEnabled) return;
            if (_cleansedRecently) return;
            if (!_isCleanseSpawned) OnTriggerEnter(other);
            var myPos = transform.position;
            Debug.DrawLine(myPos, other.transform.position, Color.red, 0.1f);
            if (!_canCurse) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            if (curseSystem.Curses[CurseType].IsLastStack && _isCleanseSpawned) {
                _cleanseObj.DestroyMe();
                _isCleanseSpawned = false;
                Enable(false);
            }
            if (!curseSystem.Curses[CurseType].ShouldBeUnVisible && _isCleanseSpawned)
            {
                _curseObjectMask.SetActive(true);
                _cleanseObj.EnableMask(true);
            }
            else if (_isCleanseSpawned)
            {
                _curseObjectMask.SetActive(false);
                _cleanseObj.EnableMask(false);
            }
            
            curseSystem.AddStack(CurseType);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            _canCurse = false;
            yield return new WaitForSeconds(_curseCooldown);
            _canCurse = true;
        }

        public void CleanseWasTaken()
        {
            _isCleanseSpawned = false;
            StartCoroutine(CleanseCooldown());
            _curseObjectMask.SetActive(false);
        }

        private IEnumerator CleanseCooldown()
        {
            _cleansedRecently = true;
            yield return new WaitForSeconds(_afterCleanseCooldown);
            _cleansedRecently = false;
        }

        private void OnDrawGizmos()
        {
            if (!_isEnabled) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _collider.radius * transform.lossyScale.x);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position + Vector3.up * 5, Vector3.one);
        }
    }
}