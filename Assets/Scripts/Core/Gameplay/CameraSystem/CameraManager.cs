using UnityEngine;

namespace ColorPlatform.Gameplay
{
    public class CameraManager
    {
        private Camera main = default;
        private CameraController mainCameraController = default;

        public Camera Main => main;
        public CameraController MainCameraController => mainCameraController;
        
        public void Init()
        {
            main = Camera.main;
            if (main) mainCameraController = main.GetComponent<CameraController>();
        }
    }
}