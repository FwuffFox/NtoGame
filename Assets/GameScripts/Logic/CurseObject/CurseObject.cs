using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic
{
    [RequireComponent(typeof(SphereCollider))]
    public class CurseObject : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private CurseObjectMover _curseObjectMover;
        [SerializeField] private bool _isEnabled;
        [SerializeField] private float curseCooldown;
        [SerializeReadOnly] public CurseType CurseType;
        [SerializeField] private GameObject cleanseItem;
        [SerializeReadOnly, SerializeField] private bool canCurse = true;
        [SerializeField] private float afterCleanseCooldown;
        [SerializeReadOnly, SerializeField] private bool cleansedRecently;
        [SerializeReadOnly, SerializeField] private bool isCleanseSpawned;
        [SerializeField] private GameObject curseObjectMask;


        private TestCleanseObject _cleanseObj;

        public void Enable(bool isTrue) 
        {
            _isEnabled = isTrue;
            _curseObjectMover.Enable(isTrue);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled) return;
            if (cleansedRecently) return;
            if (isCleanseSpawned) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            if (curseSystem.Curses[CurseType].IsMaxed) return;
            var myPos = transform.position;
            var playerPos = other.transform.position;
            var oppositeVector = new Vector3(myPos.x - (playerPos.x - myPos.x), myPos.y, myPos.z - (playerPos.z - myPos.z));
            var obj = Instantiate(cleanseItem, oppositeVector, Quaternion.identity, transform);
            var cleanse = obj.GetComponent<TestCleanseObject>();
            cleanse.SetParentCurseObject(this);
            _cleanseObj = cleanse;
            isCleanseSpawned = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isEnabled) return;
            if (cleansedRecently) return;
            if (!isCleanseSpawned) OnTriggerEnter(other);
            var myPos = transform.position;
            Debug.DrawLine(myPos, other.transform.position, Color.red, 0.1f);
            if (!canCurse) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            if (curseSystem.Curses[CurseType].IsLastStack && isCleanseSpawned) {
                _cleanseObj.DestroyMe();
                isCleanseSpawned = false;
                Enable(false);
            }
            if (!curseSystem.Curses[CurseType].ShouldBeUnVisible && isCleanseSpawned)
            {
                curseObjectMask.SetActive(true);
                _cleanseObj.EnableMask(true);
            }
            else if (isCleanseSpawned)
            {
                curseObjectMask.SetActive(false);
                _cleanseObj.EnableMask(false);
            }
            
            curseSystem.AddStack(CurseType);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            canCurse = false;
            yield return new WaitForSeconds(curseCooldown);
            canCurse = true;
        }

        public void CleanseWasTaken()
        {
            isCleanseSpawned = false;
            StartCoroutine(CleanseCooldown());
            curseObjectMask.SetActive(false);
        }

        private IEnumerator CleanseCooldown()
        {
            cleansedRecently = true;
            yield return new WaitForSeconds(afterCleanseCooldown);
            cleansedRecently = false;
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