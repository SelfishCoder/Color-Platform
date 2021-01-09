using System;
using UnityEngine;

namespace ColorPlatform.Management
{
    public class LevelManager
    {
        private int currentLevelIndex = default;
        private Level currentLevel = default;
        private Level LevelPrefab = default;
        private GameManager gameManager = default;

        public int CurrentLevelIndex => currentLevelIndex;
        public Level CurrentLevel => currentLevel;
        
        public event Action<Level> LevelLoaded;
        public event Action<Level> LevelUnLoaded;

        public LevelManager(GameManager currentGameManager)
        {
            this.gameManager = currentGameManager;
            gameManager.StateChanged += OnGameStateChanged;
        }

        public void Init()
        {
            LevelPrefab = Resources.Load<Level>($"Management/Level");
        }
        
        public void LoadLevel(int levelIndex)
        {
            currentLevel = MonoBehaviour.Instantiate(LevelPrefab.gameObject).GetComponent<Level>();
            currentLevelIndex = levelIndex;
            LevelLoaded?.Invoke(currentLevel);
            CurrentLevel.LevelEnded += OnLevelEnded;
        }

        public void UnloadLevel(Level level)
        {
            CurrentLevel.LevelEnded -= OnLevelEnded;
            LevelUnLoaded?.Invoke(level);
            MonoBehaviour.Destroy(level.gameObject);
        }

        public void LoadNextLevel()
        {
            UnloadLevel(currentLevel);
            LoadLevel(currentLevelIndex++);
        }
        
        private void OnLevelEnded()
        {
            gameManager.SetGameState(GameState.GameOver);
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    LoadLevel(currentLevelIndex);
                    break;
                case GameState.GamePlay:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}