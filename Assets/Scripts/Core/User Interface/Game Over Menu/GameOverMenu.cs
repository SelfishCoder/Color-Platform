using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    public class GameOverMenu : UIMenu
    {
        public override void Init(UIManager currentUIManager)
        {
            base.Init(currentUIManager);
            activationState = GameState.GameOver;
        }
    }
}