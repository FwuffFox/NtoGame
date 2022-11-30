using System;
using System.Collections;
using GameScripts.Logic.Player;
using GameScripts.Services.Data;
using GameScripts.Services.UnitSpawner;
using GameScripts.Services.Unity;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameScripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private ICoroutineRunner _coroutineRunner;
        private IUnitSpawner _unitSpawner;
        private GameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;
        
        public Action<int> OnPointsAmountChanged;
        private int _points;
        private GameObject _player;
        private IEnumerator _eachSecondCoroutine;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner, IUnitSpawner unitSpawner,
            GameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _coroutineRunner = coroutineRunner;
            _unitSpawner = unitSpawner;
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }
        
        public void Enter()
        {
            _player = _unitSpawner.Player;
            _eachSecondCoroutine = AddPointsEachSecond(_staticDataService.GameData.pointsPerSecond);
            _coroutineRunner.StartCoroutine(_eachSecondCoroutine);
            _player.GetComponent<PlayerHealth>().OnPlayerDeath += ManagePlayerDeath;
            _player.GetComponent<PlayerRotator>().canRotate = true;
        }
        
        public void Exit()
        {
            _player.GetComponent<PlayerHealth>().OnPlayerDeath -= ManagePlayerDeath;
            _coroutineRunner.StopCoroutine(_eachSecondCoroutine);
        }
        private void ManagePlayerDeath()
        {
            _player.GetComponent<PlayerMovement>().canMove = false;
            _player.GetComponent<PlayerRotator>().canRotate = false;
            _coroutineRunner.StartCoroutine(ManagePlayerDeathCoroutine());
        }
        
        private IEnumerator ManagePlayerDeathCoroutine()
        {
            yield return new WaitForSeconds(5);
            Object.Destroy(_player);
            _points = 0;
            _gameStateMachine.Enter<MenuState>();
        }
        
        //Test method
        private IEnumerator AddPointsEachSecond(int pointsToAdd)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                AddPoints(pointsToAdd);
            }
        }
        
        private void AddPoints(int amount)
        {
            _points += amount;
            OnPointsAmountChanged?.Invoke(_points);
        }
    }
}