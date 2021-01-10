using DG.Tweening;
using UnityEngine;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public abstract class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformType platformType = default;
        private Collider colliderComponent = default;
        private MeshRenderer meshRenderer = default;
        public PlatformType PlatformType => platformType;
        public Transform StartPoint { get; protected set; }
        public Transform EndPoint { get; protected set; }
        public bool IsActive { get; protected set; }
        public abstract PlatformColor Color { get; }

        public virtual void Init()
        {
            colliderComponent = transform.GetChild(0).GetComponent<Collider>();
            meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            StartPoint = transform.Find("Start Point");
            EndPoint = transform.Find("End Point");
        }

        public void Activate()
        {
            meshRenderer.sharedMaterial.DOFade(1f, .25f).SetEase(Ease.InOutSine);
            colliderComponent.enabled = true;
            IsActive = true;
        }

        public void Deactivate()
        {
            colliderComponent.enabled = false;
            meshRenderer.sharedMaterial.DOFade(.5f, .25f).SetEase(Ease.InOutSine);
            IsActive = false;
        }
    }
}