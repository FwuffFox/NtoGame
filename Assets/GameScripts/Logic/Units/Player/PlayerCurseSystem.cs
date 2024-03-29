﻿using System;
using System.Collections.Generic;
using GameScripts.Logic.Curses;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Units.Player
{
    public class PlayerCurseSystem : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;
        
        public Action<Dictionary<CurseType, StackableCurse>> OnCurseChange;
        private readonly Dictionary<CurseType, StackableCurse> _curses = new()
        {
            { CurseType.Health, StaticData.Curses.HealthCurse },
            { CurseType.Stamina, StaticData.Curses.StaminaCurse },
            { CurseType.Damage, StaticData.Curses.DamageCurse }
        };
        public Dictionary<CurseType, StackableCurse> Curses => _curses;

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
                case CurseType.Stamina: _playerMovement.MaxStamina -= curse.CurseValuePerStack; break;
                case CurseType.Damage: _playerAttack.Damage -= (int)curse.CurseValuePerStack; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            OnCurseChange?.Invoke(_curses);
        }

        public void ClearCurse(CurseType curseType)
        {
            _curses[curseType].CurrentStacks = 0;
            switch (curseType)
            {
                case CurseType.Health:
                    _playerHealth.MaxHealth = 100; 
                    break;
                case CurseType.Stamina:
                    _playerMovement.MaxStamina = 100;
                    break;
                case CurseType.Damage:
                    _playerAttack.ResetDamage();
                    break;
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

        private void OnDisable()
        {
            OnCurseChange = null;
        }
    }
}