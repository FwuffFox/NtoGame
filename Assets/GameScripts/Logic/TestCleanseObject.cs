using System;
using GameScripts.Logic.Units.Player;
using UnityEngine;

namespace GameScripts.Logic
{
    public class TestCleanseObject : MonoBehaviour
    {
        private CurseObject.CurseObject _curseObject;
        [SerializeField] private GameObject _cleanseObjMask;

        public void EnableMask(bool isTrue) => _cleanseObjMask.SetActive(isTrue);

        public void SetParentCurseObject(CurseObject.CurseObject curseObject)
        {
            _curseObject = curseObject;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            if (curseSystem.Curses[_curseObject.CurseType].IsMaxed) return;
            _curseObject.CleanseWasTaken();
            curseSystem.ClearCurse(_curseObject.CurseType);
            Destroy(gameObject);
        }

        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}