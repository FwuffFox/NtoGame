using UnityEngine;

namespace GameScripts.Logic.Units.Player
{
    public class PlayerInputSystem
    {
        public static PlayerInputActions Input { get; } = new();

        public static PlayerInputActions.InGameActions InGame = Input.InGame;
    }

    public static class Vector2Extensions
    {
        public static Vector3 Vector2ToVector3(this Vector2 val)
            => new Vector3(val.x, 0, val.y);
    }

    public static class Vector3Extensions
    {
        private static Matrix4x4 _matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        public static Vector3 SkewVector3(this Vector3 val)
            => _matrix.MultiplyPoint3x4(val);
            
    }
}