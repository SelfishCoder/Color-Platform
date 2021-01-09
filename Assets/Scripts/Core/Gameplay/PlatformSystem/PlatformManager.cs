using System;
using UnityEngine;
using System.Collections.Generic;
using ColorPlatform.Management;

namespace ColorPlatform.Gameplay
{
    public class PlatformManager
    {
        private static PlatformManager current;
        public static PlatformManager Current => current;
        private LevelManager levelManager = default;
        private GameManager gameManager = default;
        private PlatformColor selectedColor = default;
        private List<Platform> currentPlatforms = default;
        private FinishLine finishLine = default;

        public event Action<PlatformColor> ColorChanged;

        private PlatformManager(){}
        
        public PlatformManager(GameManager currentGameManager, LevelManager currentLevelManager)
        {
            this.levelManager = currentLevelManager;
        }
        
        public void Init()
        {
            current = this;
            ColorChanged += OnColorChanged;
            CachePlatforms();
        }

        public void LateInit()
        {
        }

        private void CachePlatforms()
        {
            currentPlatforms = new List<Platform>();
            Transform platformParent = GameObject.Find("Platforms").transform;
            foreach (Transform child in platformParent)
            {
                Platform platform = child.GetComponent<Platform>();
                if (platform is null) continue;
                currentPlatforms.Add(child.GetComponent<Platform>());
            }
            SetPlatformsActiveWith(currentPlatforms[0].Color);
            finishLine = platformParent.Find("Finish Line").GetComponent<FinishLine>();
            finishLine.Init(levelManager);
        }
        
        public void SetColor(PlatformColor color)
        {
            selectedColor = color;
            ColorChanged?.Invoke(selectedColor);
        }

        private void OnColorChanged(PlatformColor color)
        {
            SetPlatformsActiveWith(color);
        }

        private void SetPlatformsActiveWith(PlatformColor color)
        {
            currentPlatforms.ForEach(platform => platform.Deactivate());
            foreach (Platform platform in currentPlatforms)
            {
                if (!platform.Color.Equals(color)) continue;
                platform.Activate();
            }
        }
    }
}