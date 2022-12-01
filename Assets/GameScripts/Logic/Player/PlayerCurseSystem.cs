using System;
using System.Collections.Generic;
using GameScripts.Logic.Curses;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Player
{
    public class PlayerCurseSystem : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        public Action<Dictionary<CurseType, StackableCurse>> OnCurseChange;
        private Dictionary<CurseType, StackableCurse> _curses = new()
        {
            { CurseType.Health, StaticData.Curses.HealthCurse }
        };

        private void Start()
        {
            foreach (var curse in _curses.Values)
            {
                curse.OnNewStack += OnNewStack;
            }
        }

        private void OnNewStack(StackableCurse curse)
        {
            switch (curse.CurseType)
            {
                case CurseType.Health: _playerHealth.MaxHealth -= curse.CurseValuePerStack; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            OnCurseChange?.Invoke(_curses);
        }

        public void AddStack(CurseType curseType)
        {
            _curses[curseType].AddStack();
            OnCurseChange?.Invoke(_curses);
        } 
    }
}