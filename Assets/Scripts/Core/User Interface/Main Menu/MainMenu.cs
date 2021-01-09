using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    public class MainMenu : UIMenu
    {
        public override void Init(UIManager currentUIManager)
        {
            base.Init(currentUIManager);
            activationState = GameState.MainMenu;
        }
    }
}