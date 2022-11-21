using System;
using System.Collections;
using Services;
using UnityEngine;
using Zenject;

namespace Logic.Player
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
        
        public float maxStamina;

        public Action<float> OnPlayerStaminaChange;
        private float _currentStamina;
        public float CurrentStamina
        {
            get => _currentStamina;
            set
            {
                _currentStamina = value >= maxStamina ? maxStamina : value;
                OnPlayerStaminaChange?.Invoke(_currentStamina);
            } 
        }
        
        public float staminaRegenPerSecond;
        public float staminaConsumptionPerSecondOfRunning;
        public float staminaPerDodge;
        public float runningSpeedModifier;
        private bool _isRunning = false;

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
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
            var movementSpeed = (Speed * Time.deltaTime);
            movementSpeed = _isRunning ? movementSpeed * runningSpeedModifier : movementSpeed;
            if (movementAxis != Vector3.zero && canMove)
            {
                Move(movementAxis, movementSpeed);
                OnMovementSpeedChange?.Invoke(movementSpeed);
            }
            else OnMovementSpeedChange?.Invoke(0);
        }

        private void Move(Vector3 movementAxis, float movementSpeed)
        {
            _isRunning = _inputService.IsPressed(KeyCode.LeftShift) && CurrentStamina >= staminaConsumptionPerSecondOfRunning / 100f;
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(movementAxis);
            _rigidbody.MovePosition(transform.position + skewedInput * movementSpeed);
        }

        private IEnumerator StaminaRegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                CurrentStamina += staminaRegenPerSecond;
            }
        }
        
        private IEnumerator StaminaConsumptionByRunningCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f/100f);
                if (_isRunning) CurrentStamina -= staminaConsumptionPerSecondOfRunning / 100f;
            }
        }
    }
}
