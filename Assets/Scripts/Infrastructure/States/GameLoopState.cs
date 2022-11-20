using System;
using System.Collections;
using Services;
using Services.Data;
using Services.Unity;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private ICoroutineRunner _coroutineRunner;
        public Action<int> OnPointsAmountChanged;
        private int _points = 0;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Exit()
        {
            _coroutineRunner.StopCoroutine(AddPointEachSecond());
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(AddPointEachSecond());
        }
        
        //Test method
        private IEnumerator AddPointEachSecond()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                AddPoints(1);
            }
        }
        
        private void AddPoints(int amount)
        {
            _points += amount;
            OnPointsAmountChanged?.Invoke(_points);
        }
    }
}