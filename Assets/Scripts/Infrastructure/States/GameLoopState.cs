using System;
using System.Collections;
using Logic.Player;
using Services;
using Services.Data;
using Services.Unity;
using StaticData.Constants;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private ICoroutineRunner _coroutineRunner;
        private IUnitSpawner _unitSpawner;
        private GameStateMachine _gameStateMachine;
        
        public Action<int> OnPointsAmountChanged;
        private int _points = 0;

        private GameObject _player;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner, IUnitSpawner unitSpawner,
            GameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitSpawner = unitSpawner;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Exit()
        {
            _coroutineRunner.StopCoroutine(AddPointEachSecond());
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(AddPointEachSecond());
            _player = _unitSpawner.GetPlayer();
            _player.GetComponent<PlayerHealth>().OnPlayerDeath += 
                () => _coroutineRunner.StartCoroutine(ManagePlayerDeath());
        }

        private IEnumerator ManagePlayerDeath()
        {
            yield return new WaitForSeconds(5);
            Object.Destroy(_player);
            _points = 0;
            _gameStateMachine.Enter<BootstrapState>();
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