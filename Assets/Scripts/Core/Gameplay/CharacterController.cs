using System;
using UnityEngine;
using ColorPlatform.Management;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField, Range(0, 25f)] private float movementSpeed = default;
        private Animator animator = default;
        private GameManager gameManager = default;

        public void Init(GameManager currentGameManager)
        {
            this.gameManager = currentGameManager;
            animator = GetComponent<Animator>();
            gameManager.StateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            if (gameManager.CurrentGameState != GameState.GamePlay) return;
            transform.Translate(transform.forward * (Time.deltaTime * movementSpeed));
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    animator.SetBool($"IsRunning", false);
                    break;
                case GameState.GamePlay:
                    animator.SetBool($"IsRunning", true);
                    break;
                case GameState.GameOver:
                    animator.SetBool($"Victory", true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}