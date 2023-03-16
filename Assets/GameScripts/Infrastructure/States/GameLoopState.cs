using System;
using System.Collections;
using System.Linq;
using GameScripts.Logic;
using GameScripts.Logic.Campfire;
using GameScripts.Logic.UI.InGame;
using GameScripts.Logic.UI.InGame.Campfire;
using GameScripts.Logic.UI.InGame.Dialogue;
using GameScripts.Logic.UI.InGame.PauseMenu;
using GameScripts.Logic.Units.Player;
using GameScripts.Services.UnitSpawner;
using GameScripts.Services.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Object = UnityEngine.Object;

namespace GameScripts.Infrastructure.States
{
    public class GameLoopState : IStateWithPayload<GameObject>
    {
        private ICoroutineRunner _coroutineRunner;
        private IUnitSpawner _unitSpawner;
        private GameStateMachine _gameStateMachine;

        private GameObject _ui;

        private PlayerInputActions _playerInput;

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
            _ui.GetComponentInChildren<PauseMenu>(true).OnExitButtonPressed += ReturnToMenu;

            SetupControls();
            
            _unitSpawner.Player.GetComponent<PlayerHealth>().OnBattleUnitDeath += ManagePlayerDeath;
            _unitSpawner.Campfires.ForEach(f => f.OnCampfireInteracted += OnCampfireInteracted );
            Object.FindObjectsOfType<TalkingNpc>().ToList()
                .ForEach(x => x.OnNpcDialogueOpen += () => OnNpcInteracted(x));

            var finalCampfire = _unitSpawner.Campfires
                .FirstOrDefault(f => f.Type == CampfireType.Final);
            if (finalCampfire != null)
                finalCampfire.OnFinalCampfireReached += ReturnToMenu;
        }

        private void SetupControls()
        {
            PlayerInputSystem.InGame.Enable();
            PlayerInputSystem.InGame.PauseButton.performed
                += PauseButton_Performed;
        }

        private void PauseButton_Performed(InputAction.CallbackContext obj)
        {
            Debug.Log("Pause button pressed");
            var pauseMenu = _ui.GetComponentInChildren<PauseMenu>(true);
            if (!pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.Enter();
            }
            else
            {
                pauseMenu.Resume();
            }
        }

        public void OnCampfireInteracted()
        {
            _ui.GetComponentInChildren<CampfireUI>(true).TurnOn(_unitSpawner.Player);
            Time.timeScale = 0f;
        }

        private void OnNpcInteracted(TalkingNpc npc)
        {
            _ui.GetComponentInChildren<DialogueUI>(true)
                .Enter(npc.NpcDialogue, _unitSpawner.Player.GetComponent<PlayerHealth>());
        }

        private void ManagePlayerDeath()
        {
            Object.Destroy(_unitSpawner.Player.GetComponent<PlayerMovement>());
            Object.Destroy(_unitSpawner.Player.GetComponent<PlayerAttack>());
            _coroutineRunner.StartCoroutine(ManagePlayerDeathCoroutine());
        }
        
        private IEnumerator ManagePlayerDeathCoroutine()
        {
            yield return new WaitForSeconds(5);
            ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            Object.Destroy(_unitSpawner.Player.GetComponent<PlayerMovement>());
            Object.Destroy(_unitSpawner.Player.GetComponent<PlayerAttack>());
            _gameStateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
            PlayerInputSystem.InGame.PauseButton.performed
                -= PauseButton_Performed;
            _unitSpawner.Clear();
        }
    }
}