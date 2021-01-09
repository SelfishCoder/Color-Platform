using DG.Tweening;
using UnityEngine;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public abstract class Platform : MonoBehaviour
    {
        private Collider colliderComponent = default;
        private MeshRenderer meshRenderer = default;
        public abstract PlatformColor Color { get; }

        protected void Awake()
        {
            colliderComponent = transform.GetChild(0).GetComponent<Collider>();
            meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        }

        public void Activate()
        {
            meshRenderer.sharedMaterial.DOFade(1f, .25f).SetEase(Ease.InOutSine);
            colliderComponent.enabled = true;
        }

        public void Deactivate()
        {
            colliderComponent.enabled = false;
            meshRenderer.sharedMaterial.DOFade(.6f, .25f).SetEase(Ease.InOutSine);
        }
    }
}