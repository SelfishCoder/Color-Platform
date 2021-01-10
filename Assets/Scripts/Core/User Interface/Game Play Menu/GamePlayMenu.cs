using Extensions;
using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    public class GamePlayMenu : UIMenu
    {
        private ProgressBar progressBar = default;
        
        public override void Init(UIManager currentUIManager)
        {
            base.Init(currentUIManager);
            progressBar = transform.FindWithName($"Progress Bar").GetComponent<ProgressBar>();
            activationState = GameState.GamePlay;
        }
    }
}