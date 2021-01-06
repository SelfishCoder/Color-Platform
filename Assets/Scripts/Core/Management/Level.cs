using System;
using UnityEngine;

namespace ColorPlatform.Management
{
    [Serializable]
    public class Level : MonoBehaviour
    {
        private int id = default;
        
        public int ID => id;
        
        public event Action LevelStarted;
        public event Action LevelCleared;
        public event Action LevelEnded;

        public void OnLevelStarted()
        {
            LevelStarted?.Invoke();
        }

        public void OnLevelCleared()
        {
            LevelCleared?.Invoke();
        }

        public void OnLevelEnded()
        {
            LevelEnded?.Invoke();
        }
    }
}