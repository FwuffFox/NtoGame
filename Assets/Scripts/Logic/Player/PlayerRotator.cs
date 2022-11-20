using Services;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        public new UnityEngine.Camera camera;

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void FixedUpdate() 
        {
            LookAtMouseCursor();
        }
    
        private void LookAtMouseCursor()
        {
            Vector3 mousePosition = _inputService.GetMousePosition(); 
            Ray cameraRay = camera.ScreenPointToRay(mousePosition);
        
            if (!Physics.Raycast(cameraRay, out var cameraRayHit)) return;
        
            var targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
            transform.LookAt(targetPosition);
        }
    }
}
