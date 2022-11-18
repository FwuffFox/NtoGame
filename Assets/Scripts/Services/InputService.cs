using UnityEngine;
using StaticData.Constants;

namespace Services
{
    public class InputService : IInputService
    {
        public Vector3 GetMovementAxis()
        {
            return new Vector3(Input.GetAxis(InputConstants.Horizontal), 0, Input.GetAxis(InputConstants.Vertical));
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }
    }
}