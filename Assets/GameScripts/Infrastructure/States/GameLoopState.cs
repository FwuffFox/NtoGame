using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        public Action<int> OnMoneyAmountChanged;
        private int _points;
        public GameObject _player;
        private IEnumerator _eachSecondCoroutine;
        private List<Fireplace> _fireplaces;

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
            _player.GetComponent<PlayerHealth>().OnPlayerDeath += ManagePlayerDeath;
            _player.GetComponent<PlayerRotator>().canRotate = true;
            _player.GetComponent<PlayerMoney>().onMoneyChanged += (a) => OnMoneyAmountChanged?.Invoke(a);
            _fireplaces = _unitSpawner.Fireplaces.Select(f => f.GetComponent<Fireplace>()).ToList();
            _fireplaces
                .First(f => f.Type == FireplaceType.Final)
                .OnFinalCampfireReached += () => _gameStateMachine.Enter<MenuState>();
        }
        
        public void Exit()
        {
            _player.GetComponent<PlayerHealth>().OnPlayerDeath -= ManagePlayerDeath;
            Object.Destroy(_player);
            _points = 0;
        }
        
        private void ManagePlayerDeath()
        {
            _player.GetComponent<PlayerMovement>().canMove = false;
            _player.GetComponent<PlayerRotator>().canRotate = false;
            Object.Destroy(_player.GetComponent<PlayerAttack>());
            _coroutineRunner.StartCoroutine(ManagePlayerDeathCoroutine());
        }
        
        private IEnumerator ManagePlayerDeathCoroutine()
        {
            yield return new WaitForSeconds(5);
            _gameStateMachine.Enter<MenuState>();
        }
       
    }
}