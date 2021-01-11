using UnityEngine;

namespace ColorPlatform.Gameplay
{
    public class CameraManager
    {
        private Camera main = default;
        private CameraController mainCameraController = default;
        private PlatformManager platformManager = default;

        public Camera Main => main;
        public CameraController MainCameraController => mainCameraController;

        private CameraManager()
        {
            
        }

        public CameraManager(PlatformManager currentPlatformManager)
        {
            this.platformManager = currentPlatformManager;
        }
        
        public void Init()
        {
            main = Camera.main;
            if (main) mainCameraController = main.GetComponent<CameraController>();
            if (mainCameraController) ((ChaseCameraController) mainCameraController).Init(platformManager);
        }
    }
}