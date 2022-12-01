using System;
using System.Linq;
using GameScripts.Services;
using GameScripts.Services.InputService;
using GameScripts.StaticData.Constants;
using UnityEngine;
using Zenject;

namespace GameScripts.Logic.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        public new UnityEngine.Camera camera;

        public bool canRotate = true;

        private readonly RaycastHit[] _raycastHits = new RaycastHit[50];

        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void FixedUpdate() 
        {
            if (canRotate) LookAtMouseCursor();
        }
    
        private void LookAtMouseCursor()
        {
            Vector3 mousePosition = _inputService.GetMousePosition(); 
            Ray cameraRay = camera.ScreenPointToRay(mousePosition);
            var hits = Physics.RaycastNonAlloc(cameraRay, _raycastHits, int.MaxValue);
            if (hits == 0) return;
            if (!_raycastHits.Any(hit => hit.collider.CompareTag(ConstantTags.Ground))) return;
            RaycastHit groundHit = _raycastHits.First(hit => hit.collider.CompareTag(ConstantTags.Ground));
            var targetPosition = new Vector3(groundHit.point.x, transform.position.y, groundHit.point.z);
            transform.LookAt(targetPosition);
        }
    }
}
