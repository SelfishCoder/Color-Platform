using System;
using Extensions;
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
        private Vector3 movementDirection = default;
        private Rigidbody rigidbodyComponent = default;
        private Animator animator = default;
        private SkinnedMeshRenderer meshRenderer = default;
        private GameManager gameManager = default;
        private PlatformManager platformManager = default;
        private TrailRenderer rightHandTrail = default;
        private TrailRenderer leftHandTrail = default;

        public void Init(GameManager currentGameManager, PlatformManager currentPlatformManager)
        {
            this.gameManager = currentGameManager;
            this.platformManager = currentPlatformManager;
            animator = GetComponent<Animator>();
            rigidbodyComponent = GetComponent<Rigidbody>();
            gameManager.StateChanged += OnGameStateChanged;
            platformManager.ColorChanged += OnColorChanged;
            meshRenderer = transform.FindWithName("Whiteman").GetComponent<SkinnedMeshRenderer>();
            CacheTrailsAndSetInitialValues();
            SetColor(platformManager.SelectedColor);
        }

        private void CacheTrailsAndSetInitialValues()
        {
            rightHandTrail = transform.FindWithName($"Right Hand Trail").GetComponent<TrailRenderer>();
            leftHandTrail = transform.FindWithName($"Left Hand Trail").GetComponent<TrailRenderer>();
            rightHandTrail.startWidth = .1f;
            rightHandTrail.endWidth = .025f;
            leftHandTrail.startWidth = .1f;
            leftHandTrail.endWidth = .025f;
        }
        
        private void Update()
        {
            if (gameManager.CurrentGameState != GameState.GamePlay) return;
            
            if (!rigidbodyComponent.useGravity)
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }
            if (platformManager.CurrentPlatformIndex <= platformManager.CurrentPlatforms.Count - 1)
            {
                if (rigidbodyComponent.useGravity)
                {
                    movementDirection = platformManager.GetPlatform(platformManager.CurrentPlatformIndex).EndPoint.transform.position -
                        transform.position;
                }
                else
                {
                    movementDirection = Vector3.forward;
                }
            }
            else
            {
                movementDirection = platformManager.FinishLine.transform.position - transform.position;
            }
            movementDirection.y = 0;
            transform.Translate(movementDirection.normalized * (Time.deltaTime * movementSpeed));
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
            transform.DOLookAt(targetPosition, .1f);
            if (!targetPlatform.IsActive) return;
            rigidbodyComponent.useGravity = targetWallType == PlatformType.Straight;
            if (targetWallType != PlatformType.Straight)
            {
                rigidbodyComponent.velocity = Vector3.zero;
            }
        }

        private void OnHitPlatformStartPoint()
        {
            Platform targetPlatform = platformManager.GetPlatform(platformManager.CurrentPlatformIndex);
            if (!targetPlatform.IsActive) return;
            PlatformType targetWallType = targetPlatform.PlatformType;
            if (targetWallType == PlatformType.Wall)
            {
                animator.SetBool($"IsWallRunning", true);
                rigidbodyComponent.useGravity = false;
            }
            transform.DOLookAt(targetPlatform.EndPoint.transform.position, .1f);
        }

        private void SetColor(PlatformColor color)
        {
            switch (color)
            {
                case PlatformColor.Red:
                    meshRenderer.sharedMaterial = Resources.Load<Material>($"Materials/Character/Red");
                    break;
                case PlatformColor.Green:
                    meshRenderer.sharedMaterial = Resources.Load<Material>($"Materials/Character/Green");
                    break;
                case PlatformColor.Blue:
                    meshRenderer.sharedMaterial = Resources.Load<Material>($"Materials/Character/Blue");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
        
        private void OnColorChanged(PlatformColor color)
        {
            SetColor(color);
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