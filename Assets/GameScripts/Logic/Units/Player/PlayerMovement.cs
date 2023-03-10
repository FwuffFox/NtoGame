using System;
using System.Collections;
using GameScripts.Services.InputService;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace GameScripts.Logic.Units.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rigidbody;

        public Action<float> OnMovementSpeedChange;

        private float _baseSpeed;
        private float _runningSpeedModifier;

        public float MovementSpeedModifier { get; set; } = 1f;

        private float _movementSpeed;
        public float MovementSpeed
        {
            get => _movementSpeed;
            set
            {
                OnMovementSpeedChange?.Invoke(value);
                _movementSpeed = value;
            }
        }

        public float MaxStamina { get; set; }

        public Action<float> OnPlayerStaminaChange;
        private float _currentStamina;
        public float CurrentStamina
        {
            get => _currentStamina;
            set
            {
                _currentStamina = value >= MaxStamina ? MaxStamina : value;
                OnPlayerStaminaChange?.Invoke(_currentStamina);
            } 
        }
        
        private float _staminaRegenPerSecond;
        private float _staminaConsumptionPerSecondOfRunning;

        public Action<bool> OnIsRunningChange;

        private bool _isRunning;
        private bool IsRunning
        {
            get => _isRunning;
            set
            {
                OnIsRunningChange?.Invoke(value);
                _isRunning = value;
            }
        }

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetProperties(PlayerData playerData)
        {
            _baseSpeed = playerData.speed.baseSpeed;
            _runningSpeedModifier = playerData.speed.runningSpeedModifier;
            MaxStamina = playerData.stamina.maxStamina;
            CurrentStamina = MaxStamina;
            _staminaRegenPerSecond = playerData.stamina.staminaRegenPerSecond;
            _staminaConsumptionPerSecondOfRunning = playerData.stamina.staminaConsumptionPerSecondOfRunning;
        }
        
        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            StartCoroutine(StaminaRegenerationCoroutine());
            StartCoroutine(StaminaConsumptionByRunningCoroutine());
        }

        private void FixedUpdate()
        {
            if (!PlayerInputSystem.InGame.Move.IsPressed())
            {
                MovementSpeed = 0;
                IsRunning = false;
                return;
            }

            var movementAxis = PlayerInputSystem.InGame.Move.ReadValue<Vector2>().Vector2ToVector3();
            IsRunning = _inputService.IsPressed(KeyCode.LeftShift) && CurrentStamina >= _staminaConsumptionPerSecondOfRunning / 100f;
            MovementSpeed = _baseSpeed * Time.deltaTime;
            MovementSpeed = IsRunning ? MovementSpeed * _runningSpeedModifier : MovementSpeed;
            MovePlayer(movementAxis, MovementSpeed);
        }

        private void MovePlayer(Vector3 movementAxis, float movementSpeed)
        {
            var skewedInput = movementAxis.SkewVector3();
            _rigidbody.MovePosition(transform.position + skewedInput * movementSpeed);
            transform.LookAt(transform.position + skewedInput * movementSpeed);
        }

        private IEnumerator StaminaRegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f/100f);
                CurrentStamina += _staminaRegenPerSecond / 100f;
            }
        }
        
        private IEnumerator StaminaConsumptionByRunningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f/100f);
                if (IsRunning) CurrentStamina -= _staminaConsumptionPerSecondOfRunning / 100f;
            }
        }
    }
}
