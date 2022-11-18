using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rigidbody;

    [Range(0f, 10f)] public float speed;

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
        Vector3 movementAxis = _inputService.GetMovementAxis();
        if (movementAxis != Vector3.zero) Move(movementAxis);
    }

    private void Move(Vector3 movementAxis)
    {
        _rigidbody.MovePosition(transform.position + movementAxis * (speed * Time.deltaTime));
    }

}
