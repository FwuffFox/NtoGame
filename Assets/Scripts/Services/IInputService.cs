using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        Vector3 GetMovementAxis();
        Vector3 GetMousePosition();
        bool IsPressed(KeyCode key);
    }
}