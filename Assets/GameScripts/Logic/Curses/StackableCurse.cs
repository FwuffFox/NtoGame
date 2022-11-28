using System;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Curses
{
    public class StackableCurse
    {
        public CurseType CurseType;
        public int MaxStacks;
        public Action<StackableCurse> OnNewStack;
        private int _currentStacks;
        public float CurseValuePerStack;

        private Func<GameObject, bool> _onMaxStacks;
        private GameObject _obj;

        public StackableCurse(CurseType curseType, int maxStacks, float curseValuePerStack)
        {
            CurseType = curseType;
            MaxStacks = maxStacks;
            CurseValuePerStack = curseValuePerStack;
        }

        public void SetOnMaxStacksFunction(Func<GameObject, bool> onMaxStacks, GameObject obj)
        {
            _onMaxStacks = onMaxStacks;
            _obj = obj;
        }

        public void AddStack()
        {
            if (_currentStacks + 1 >= MaxStacks)
            {
                _currentStacks = MaxStacks;
                _onMaxStacks?.Invoke(_obj);
            }
            _currentStacks++;
            OnNewStack?.Invoke(this);
        }
        
    }
}