using System;
using System.Collections.Generic;
using GameScripts.Logic.Curses;
using GameScripts.Logic.Units.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Logic.UI.InGame
{
    public class CurseUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private PlayerCurseSystem _playerCurseSystem;

        public void SetPlayer(PlayerCurseSystem playerCurseSystem)
        {
            _playerCurseSystem = playerCurseSystem;
            _playerCurseSystem.OnCurseChange += UpdateCurses;
        }
        
        private void UpdateCurses(Dictionary<CurseType, StackableCurse> curses)
        {
            _text.text = string.Empty;
            foreach (var stackableCurse in curses)
            {
                var curse = stackableCurse.Value;
                if (curse.CurrentStacks == 0) continue;
                if (curse.ShouldBeUnVisibleUi) continue;
                _text.text += $"{stackableCurse.Key.CurseTypeToString()}: {curse.CurrentStacks}/{curse.MaxStacks}\n";
            }
        }

        private void OnDisable()
        {
            _playerCurseSystem.OnCurseChange -= UpdateCurses;
        }
    }
}