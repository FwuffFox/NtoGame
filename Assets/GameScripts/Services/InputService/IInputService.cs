using UnityEngine;

namespace GameScripts.Services.InputService
{
    public interface IInputService
    {
        Vector3 GetMovementAxis();
        Vector3 GetMousePosition();
        bool IsPressed(KeyCode key);
    }
}