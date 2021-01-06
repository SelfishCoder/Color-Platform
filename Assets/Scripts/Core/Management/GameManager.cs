using System;
using ColorPlatform.Gameplay;
using UnityEngine;

namespace ColorPlatform.Management
{
    public class GameManager : MonoBehaviour
    {
        private GameState initialGameState = GameState.MainMenu;
        private GameState currentGameState = default;
        private PlatformManager platformManager = default;

        public event Action<GameState> StateChanged;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            LateInit();
        }

        private void Init()
        {
            platformManager = new PlatformManager();
            
            platformManager.Init();
        }

        private void LateInit()
        {
            SetGameState(initialGameState);
        }
        
        public void SetGameState(GameState state, bool forceSet = false)
        {
            if (currentGameState.Equals(state) && !forceSet) return;
            currentGameState = state;
            StateChanged?.Invoke(currentGameState);
        }
    }
}