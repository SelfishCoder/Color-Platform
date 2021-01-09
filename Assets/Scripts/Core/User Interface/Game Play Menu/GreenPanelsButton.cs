using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class GreenPanelsButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Green);
        }
    }
}