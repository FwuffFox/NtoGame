using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameScripts.Logic.Campfire;
using GameScripts.Logic.UI.InGame;
using GameScripts.Logic.Units.Player;
using GameScripts.Services.Data;
using GameScripts.Services.UnitSpawner;
using GameScripts.Services.Unity;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameScripts.Infrastructure.States
{
    public class GameLoopState : IStateWithPayload<GameObject>
    {
        private ICoroutineRunner _coroutineRunner;
        private IUnitSpawner _unitSpawner;
        private GameStateMachine _gameStateMachine;

        public Action<int> OnMoneyAmountChanged;
        private GameObject _player;
        private IEnumerator _eachSecondCoroutine;
        private List<Campfire> _fireplaces;

        private GameObject _ui;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner, IUnitSpawner unitSpawner,
            GameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _unitSpawner = unitSpawner;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter(GameObject ui)
        {
            _ui = ui;
            _player = _unitSpawner.Player;
            _player.GetComponent<PlayerHealth>().OnBattleUnitDeath += ManagePlayerDeath;
            _player.GetComponent<PlayerMoney>().OnMoneyChanged += (a) => OnMoneyAmountChanged?.Invoke(a);
            
            _fireplaces = _unitSpawner.Fireplaces.Select(f => f.GetComponent<Campfire>()).ToList();
            _fireplaces.ForEach(f => f.OnCampfireInteracted += OnCampfireInteracted );
            var finalFireplace = _fireplaces.FirstOrDefault(f => f.Type == CampfireType.Final);
            if (finalFireplace != null)
                finalFireplace.OnFinalCampfireReached += () => _gameStateMachine.Enter<MenuState>();
        }

        public void OnCampfireInteracted()
        {
            _ui.GetComponentInChildren<CampfireUI>().TurnOn(_player);
            Time.timeScale = 0f;
        }
        
        public void Exit()
        {
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