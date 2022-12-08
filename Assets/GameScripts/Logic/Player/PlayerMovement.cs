using System;
using System.Collections;
using GameScripts.Services;
using GameScripts.Services.InputService;
using GameScripts.StaticData.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace GameScripts.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rigidbody;

        public Action<float> OnMovementSpeedChange; 
        private float _speed;
        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
            }
        }
        
        public bool canMove = true;

        private float _maxStamina;
        public float MaxStamina
        {
            get => _maxStamina;
            set => _maxStamina = value;
        }

        public Action<float> OnPlayerStaminaChange;
        private float _currentStamina;
        public float CurrentStamina
        {
            get => _currentStamina;
            set
            {
                _currentStamina = value >= _maxStamina ? _maxStamina : value;
                OnPlayerStaminaChange?.Invoke(_currentStamina);
            } 
        }
        
        private float _staminaRegenPerSecond;
        private float _staminaConsumptionPerSecondOfRunning;
        private float _staminaPerDodge;
        private float _runningSpeedModifier;

        public Action<bool> OnIsRunningChange;
        private bool _isRunning = false;

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetProperties(PlayerData playerData)
        {
            Speed = playerData.speed.baseSpeed;
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
            _isRunning = _inputService.IsPressed(KeyCode.LeftShift) && CurrentStamina >= _staminaConsumptionPerSecondOfRunning / 100f;
            var movementSpeed = (Speed * Time.deltaTime);
            movementSpeed = _isRunning ? movementSpeed * _runningSpeedModifier : movementSpeed;
            if (movementAxis != Vector3.zero && canMove)
            {
                Move(movementAxis, movementSpeed);
                OnMovementSpeedChange?.Invoke(movementSpeed);
                OnIsRunningChange?.Invoke(_isRunning);
            }
            else
            {
                OnMovementSpeedChange?.Invoke(0);
                OnIsRunningChange?.Invoke(false);
            }
        }

        private void Move(Vector3 movementAxis, float movementSpeed)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(movementAxis);
            _rigidbody.MovePosition(transform.position + skewedInput * movementSpeed);
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
                if (_isRunning) CurrentStamina -= _staminaConsumptionPerSecondOfRunning / 100f;
            }
        }
    }
}
