using System;
using System.Collections;
using GameScripts.Services.InputService;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace GameScripts.Logic.Units.Player
{
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
        private float _staminaPerDodge;

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
            _staminaPerDodge = playerData.stamina.staminaPerDodge;
        }

        private void OnEnable()
        {
            if (!TryGetComponent(out _rigidbody))
                Debug.LogError("Attach rigidbody to player");
            StartCoroutine(StaminaRegenerationCoroutine());
            StartCoroutine(StaminaConsumptionByRunningCoroutine());
        }

        private void FixedUpdate()
        {
            Vector3 movementAxis = _inputService.GetMovementAxis();
            if (movementAxis != Vector3.zero)
            {
                IsRunning = _inputService.IsPressed(KeyCode.LeftShift) && CurrentStamina >= _staminaConsumptionPerSecondOfRunning / 100f;
                MovementSpeed = _baseSpeed * Time.deltaTime;
                MovementSpeed = IsRunning ? MovementSpeed * _runningSpeedModifier : MovementSpeed;
                Move(movementAxis, MovementSpeed);
            }
            else
            {
                MovementSpeed = 0;
                IsRunning = false;
            }
        }

        private void Move(Vector3 movementAxis, float movementSpeed)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(movementAxis);
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
