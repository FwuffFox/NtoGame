using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameScripts.Logic.Fireplace;
using GameScripts.Logic.Units.Player;
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

        public Action<int> OnMoneyAmountChanged;
        private GameObject _player;
        private IEnumerator _eachSecondCoroutine;
        private List<Fireplace> _fireplaces;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner, IUnitSpawner unitSpawner,
            GameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitSpawner = unitSpawner;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _player = _unitSpawner.Player;
            _player.GetComponent<PlayerHealth>().OnBattleUnitDeath += ManagePlayerDeath;
            _player.GetComponent<PlayerMoney>().OnMoneyChanged += (a) => OnMoneyAmountChanged?.Invoke(a);
            _fireplaces = _unitSpawner.Fireplaces.Select(f => f.GetComponent<Fireplace>()).ToList();
            var finalFireplace = _fireplaces.FirstOrDefault(f => f.Type == FireplaceType.Final);
            if (finalFireplace != null)
                finalFireplace.OnFinalCampfireReached += () => _gameStateMachine.Enter<MenuState>();
        }
        
        public void Exit()
        {
            _player.GetComponent<PlayerHealth>().OnBattleUnitDeath -= ManagePlayerDeath;
            Object.Destroy(_player);
        }
        
        private void ManagePlayerDeath()
        {
            Object.Destroy(_player.GetComponent<PlayerMovement>());
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