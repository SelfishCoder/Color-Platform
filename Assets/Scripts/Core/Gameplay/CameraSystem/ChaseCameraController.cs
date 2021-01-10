using UnityEngine;

namespace ColorPlatform.Gameplay
{
    public class ChaseCameraController : CameraController
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private Vector3 offset = default;
        [SerializeField] private FollowType followType = default;
        private Vector3 initialPosition = default;

        public void Init()
        {
            initialPosition = transform.position;
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
    }
}