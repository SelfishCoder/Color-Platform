using System;
using UnityEngine;
using ColorPlatform.Gameplay;
using CharacterController = ColorPlatform.Gameplay.CharacterController;

namespace ColorPlatform.Management
{
    public class GameManager : MonoBehaviour
    {
        private GameState initialGameState = GameState.MainMenu;
        private GameState currentGameState = default;
        private CharacterController characterController = default;
        private PlatformManager platformManager = default;
        private LevelManager levelManager = default;
        private UIManager uiManager = default;
        private CameraManager cameraManager = default;

        public GameState CurrentGameState => currentGameState;

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
            characterController = FindObjectOfType<CharacterController>();
            levelManager = new LevelManager(this);
            platformManager = new PlatformManager(this, levelManager, characterController);
            uiManager = new UIManager(this);
            cameraManager = new CameraManager(platformManager);

            cameraManager.Init();
            platformManager.Init();
            levelManager.Init();
            uiManager.Init();
            characterController.Init(this, platformManager);
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