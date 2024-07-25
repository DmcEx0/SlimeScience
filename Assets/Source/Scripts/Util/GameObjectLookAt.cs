using UnityEngine;

namespace SlimeScience.Util
{
    public class GameObjectLookAt : MonoBehaviour
    {
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (_camera == null)
            {
                return;
            }

            transform.LookAt(_camera.transform);
        }
    }
}