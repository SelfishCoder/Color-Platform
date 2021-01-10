using UnityEngine;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent, RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        protected virtual void LateUpdate()
        {
        }
    }
}