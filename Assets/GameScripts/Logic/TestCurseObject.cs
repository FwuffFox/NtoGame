using System;
using System.Collections;
using EditorScripts.Inspector;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TestCurseObject : MonoBehaviour
    {
        [SerializeField] private float curseCooldown;
        public CurseType CurseType;
        [SerializeField] private GameObject cleanseItem;
        [SerializeReadOnly, SerializeField] private bool canCurse = true;
        [SerializeField] private float afterCleanseCooldown;
        [SerializeReadOnly, SerializeField] private bool cleansedRecently;
        [SerializeReadOnly, SerializeField] private bool isCleanseSpawned;

        private void OnTriggerEnter(Collider other)
        {
            if (cleansedRecently) return;
            if (isCleanseSpawned) return;
            var myPos = transform.position;
            var playerPos = other.transform.position;
            var oppositeVector = new Vector3(myPos.x - (playerPos.x - myPos.x), myPos.y, myPos.z - (playerPos.z - myPos.z));
            var cleanse = Instantiate(cleanseItem, oppositeVector, Quaternion.identity, transform);
            cleanse.GetComponent<TestCleanseObject>().SetParentCurseObject(this);
            isCleanseSpawned = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (cleansedRecently) return;
            var myPos = transform.position;
            Debug.DrawLine(myPos, other.transform.position, Color.red, 0.1f);
            if (!canCurse) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
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
        }

        private IEnumerator CleanseCooldown()
        {
            cleansedRecently = true;
            yield return new WaitForSeconds(afterCleanseCooldown);
            cleansedRecently = false;
        }
    }
}