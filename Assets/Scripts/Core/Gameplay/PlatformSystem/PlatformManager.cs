using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColorPlatform.Gameplay
{
    public class PlatformManager
    {
        private static PlatformManager current;
        public static PlatformManager Current => current;
        private PlatformColor selectedColor = default;
        private List<Platform> currentPlatforms = default;

        public event Action<PlatformColor> ColorChanged;

        public void Init()
        {
            current = this;
            ColorChanged += OnColorChanged;
            InitPlatforms();
        }

        public void LateInit()
        {
            
        }

        private void InitPlatforms()
        {
            currentPlatforms = new List<Platform>();
            Transform platformParent = GameObject.Find("Platforms").transform;
            foreach (Transform child in platformParent)
            {
                currentPlatforms.Add(child.GetComponent<Platform>());
            }
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
            currentPlatforms.ForEach(platform => platform.gameObject.SetActive(false));
            foreach (Platform platform in currentPlatforms)
            {
                if (!platform.Color.Equals(color)) continue;
                platform.gameObject.SetActive(true);
            }
        }
    }
}