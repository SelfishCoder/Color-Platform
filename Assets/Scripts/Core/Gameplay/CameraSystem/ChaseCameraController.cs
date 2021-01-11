using System;
using UnityEngine;

namespace ColorPlatform.Gameplay
{
    public class ChaseCameraController : CameraController
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private Vector3 offset = default;
        [SerializeField] private FollowType followType = default;
        [SerializeField] private Color redLight = default;
        [SerializeField] private Color greenLight = default;
        [SerializeField] private Color blueLight = default;
        private Camera cameraComponent = default;
        private Vector3 initialPosition = default;

        public void Init(PlatformManager platformManager)
        {
            initialPosition = transform.position;
            platformManager.ColorChanged += OnColorChanged;
            cameraComponent = GetComponent<Camera>();
        }

        private enum FollowType
        {
            All, Depth, Horizontal, Vertical, HorizontalAndDepth, VerticalAndDepth, HorizontalAndVertical, None
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            Vector3 targetPosition = target.transform.position + offset;
            if (followType == (FollowType.Vertical | FollowType.VerticalAndDepth | FollowType.Depth | FollowType.None))
            {
                targetPosition.x = initialPosition.x;
            }
            transform.position = targetPosition;
        }

        public void SetTarget(Transform targetTransform)
        {
            target = targetTransform;
        }

        public void SetOffset(Vector3 targetOffset)
        {
            offset = targetOffset;
        }

        private void SetColor(PlatformColor color)
        {
            switch (color)
            {
                case PlatformColor.Red:
                    cameraComponent.backgroundColor = redLight;
                    break;
                case PlatformColor.Green:
                    cameraComponent.backgroundColor = greenLight;
                    break;
                case PlatformColor.Blue:
                    cameraComponent.backgroundColor = blueLight;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
        
        private void OnColorChanged(PlatformColor color)
        {
            SetColor(color);            
        }
    }
}