using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    public class GamePlayMenu : UIMenu
    {
        public override void Init(UIManager currentUIManager)
        {
            base.Init(currentUIManager);
            activationState = GameState.GamePlay;
        }
    }
}