using UnityEngine;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public abstract class Platform : MonoBehaviour
    {
        public abstract PlatformColor Color { get; }
    }
}