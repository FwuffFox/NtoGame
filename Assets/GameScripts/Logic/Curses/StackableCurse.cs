using System;
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

        public bool ShouldBeUnVisible => CurrentStacks >= MaxStacks / 2;
        public bool ShouldBeUnVisibleUi => IsMaxed;
        public bool IsMaxed => CurrentStacks == MaxStacks;
        public bool IsLastStack => MaxStacks - CurrentStacks == 1;

        private Action<GameObject> _onMaxStacks;
        private GameObject _obj;

        public StackableCurse(CurseType curseType, int maxStacks, float curseValuePerStack)
        {
            CurseType = curseType;
            MaxStacks = maxStacks;
            CurseValuePerStack = curseValuePerStack;
        }

        public void SetOnMaxStacksFunction(Action<GameObject> onMaxStacks, GameObject obj)
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