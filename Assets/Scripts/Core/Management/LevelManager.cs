using System;
using UnityEngine;

namespace ColorPlatform.Management
{
    public class LevelManager
    {
        private int currentLevelIndex = default;
        private Level currentLevel = default;
        private Level LevelPrefab = default;

        public int CurrentLevelIndex => currentLevelIndex;
        public Level CurrentLevel => currentLevel;
        
        public event Action<Level> LevelLoaded;
        public event Action<Level> LevelUnLoaded;

        public void LoadLevel(int levelIndex)
        {
            currentLevel = MonoBehaviour.Instantiate(LevelPrefab.gameObject).GetComponent<Level>();
            currentLevelIndex = levelIndex;
            LevelLoaded?.Invoke(currentLevel);
        }

        public void UnloadLevel(Level level)
        {
            LevelUnLoaded?.Invoke(level);
            MonoBehaviour.Destroy(level.gameObject);
        }

        public void LoadNextLevel()
        {
            UnloadLevel(currentLevel);
            LoadLevel(currentLevelIndex++);
        }
    }
}