using UnityEngine;
using ColorPlatform.Management;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public class FinishLine : MonoBehaviour
    {
        private LevelManager levelManager = default;

        public void Init(LevelManager currentLevelManager)
        {
            this.levelManager = currentLevelManager;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController)
            {
                levelManager.CurrentLevel.OnLevelEnded();
            }
        }
    }
}