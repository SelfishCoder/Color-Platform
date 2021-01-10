using System;
using DG.Tweening;
using UnityEngine;
using ColorPlatform.Management;

namespace ColorPlatform.Gameplay
{
    [DisallowMultipleComponent]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField, Range(0, 25f)] private float movementSpeed = default;
        [SerializeField, Range(0, 250f)] private float jumpForce = default;
        private Rigidbody rigidbodyComponent = default;
        private Animator animator = default;
        private GameManager gameManager = default;
        private PlatformManager platformManager = default;

        public void Init(GameManager currentGameManager, PlatformManager currentPlatformManager)
        {
            this.gameManager = currentGameManager;
            this.platformManager = currentPlatformManager;
            animator = GetComponent<Animator>();
            rigidbodyComponent = GetComponent<Rigidbody>();
            gameManager.StateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            if (gameManager.CurrentGameState != GameState.GamePlay) return;
            Vector3 direction;
            if (!rigidbodyComponent.useGravity)
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }
            if (platformManager.CurrentPlatformIndex <= platformManager.CurrentPlatforms.Count - 1)
            {
                if (rigidbodyComponent.useGravity)
                {
                    direction = platformManager.GetPlatform(platformManager.CurrentPlatformIndex).EndPoint.transform.position -
                        transform.position;
                }
                else
                {
                    direction = Vector3.forward;
                }
            }
            else
            {
                direction = platformManager.FinishLine.transform.position - transform.position;
            }
            direction.y = 0;
            transform.Translate(direction.normalized * (Time.deltaTime * movementSpeed));
        }
        
        private void OnTriggerEnter(Collider other)
        {
            PlatformEndPoint platformEndPoint = other.GetComponent<PlatformEndPoint>();
            PlatformStartPoint platformStartPoint = other.GetComponent<PlatformStartPoint>();
            
            if (platformEndPoint) OnHitPlatformEndPoint();
            if (platformStartPoint) OnHitPlatformStartPoint();
        }

        private void OnHitPlatformEndPoint()
        {
            Platform currentPlatform = platformManager.GetPlatform(platformManager.CurrentPlatformIndex);
            if (currentPlatform.IsActive)
            {
                animator.SetTrigger($"Jump");
                animator.SetBool($"IsWallRunning", false);
                rigidbodyComponent.AddForce(Vector3.up * jumpForce);
            }
            rigidbodyComponent.useGravity = true;
            platformManager.OnPlatformPassed();
            if (platformManager.CurrentPlatformIndex > platformManager.CurrentPlatforms.Count - 1) return;
            Platform targetPlatform = platformManager.GetPlatform(platformManager.CurrentPlatformIndex);
            PlatformType targetWallType = targetPlatform.PlatformType;
            Vector3 targetPosition = targetWallType == PlatformType.Straight ? 
                targetPlatform.EndPoint.transform.position
                : targetPlatform.StartPoint.transform.position;
            rigidbodyComponent.useGravity = targetWallType == PlatformType.Straight;
            if (targetWallType != PlatformType.Straight)
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }
            transform.DOLookAt(targetPosition, .1f);
        }

        private void OnHitPlatformStartPoint()
        {
            Platform targetPlatform = platformManager.GetPlatform(platformManager.CurrentPlatformIndex);
            PlatformType targetWallType = targetPlatform.PlatformType;
            if (targetWallType == PlatformType.Wall) animator.SetBool($"IsWallRunning", true);
            transform.DOLookAt(targetPlatform.EndPoint.transform.position, .1f);
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