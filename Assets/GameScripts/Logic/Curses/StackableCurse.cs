using System;
using GameScripts.Logic.Player;
using GameScripts.StaticData.Enums;
using UnityEngine;

namespace GameScripts.Logic.Curses
{
    public class StackableCurse
    {
        public CurseType CurseType;
        public readonly int MaxStacks;
        public Action<StackableCurse> OnNewStack;
        public int CurrentStacks;
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

        private bool _canAddStack = true;
        public void AddStack()
        {
            if (!_canAddStack) return;
            OnNewStack?.Invoke(this);
            if (CurrentStacks + 1 >= MaxStacks)
            {
                CurrentStacks = MaxStacks;
                _onMaxStacks?.Invoke(_obj);
                _canAddStack = false;
                return;
            }
            CurrentStacks++;
        }
        
    }
}