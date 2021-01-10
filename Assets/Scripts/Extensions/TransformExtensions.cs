using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform FindWithName(this Transform transform, string transformName)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == transformName) return child;
                if (child.childCount > 0)
                {
                    Transform result = child.FindWithName(transformName);
                    if (result) return result;
                }
            }

            return null;
        }
    }
}