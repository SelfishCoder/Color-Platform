using UnityEngine;
using UnityEngine.UI;
using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    [DisallowMultipleComponent]
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Text currentLevelText = default;
        [SerializeField] private Text nextLevelText = default;
        [SerializeField] private Image fillImage = default;

        private void Update()
        {
            SetProgress(PlatformManager.Current.GetProgress());
        }

        private void SetProgress(float progress)
        {
            fillImage.fillAmount = Mathf.Clamp01(progress);
        }

        private void SetCurrentLevelText(int levelNumber)
        {
            currentLevelText.text = levelNumber.ToString();
            nextLevelText.text = (levelNumber + 1).ToString();
        }
    }
}