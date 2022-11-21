using Services;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rigidbody;

        [Range(0f, 10f)] public float speed;
        public bool canMove = true;

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
        }

        private void FixedUpdate()
        {
            print(canMove);
            Vector3 movementAxis = _inputService.GetMovementAxis();
            if (movementAxis != Vector3.zero && canMove) Move(movementAxis);
        }

        private void Move(Vector3 movementAxis)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(movementAxis);
            _rigidbody.MovePosition(transform.position + skewedInput * (speed * Time.deltaTime));
        }

    }
}
