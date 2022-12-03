using System;
using GameScripts.Logic.Player;
using UnityEngine;

namespace GameScripts.Logic
{
    public class TestCleanseObject : MonoBehaviour
    {
        private TestCurseObject _curseObject;

        public void SetParentCurseObject(TestCurseObject curseObject)
        {
            _curseObject = curseObject;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerCurseSystem curseSystem)) return;
            _curseObject.CleanseWasTaken();
            curseSystem.ClearCurse(_curseObject.CurseType);
            Destroy(gameObject);
        }
    }
}