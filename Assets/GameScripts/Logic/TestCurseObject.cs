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
        [SerializeField] private CurseType curseType;
        [SerializeReadOnly, SerializeField] private bool canCurse = true;

        private void OnTriggerStay(Collider other)
        {
            Debug.DrawLine(transform.position, other.transform.position, Color.red, 0.1f);
            if (!canCurse) return;
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            curseSystem.AddStack(curseType);
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            canCurse = false;
            yield return new WaitForSeconds(curseCooldown);
            canCurse = true;
        }
    }
}