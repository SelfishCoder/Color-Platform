using UnityEngine;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField, Range(0, 25f)] private float movementSpeed = default;

        private void Update()
        {
            transform.Translate(transform.forward * (Time.deltaTime * movementSpeed));
        }
    }
}