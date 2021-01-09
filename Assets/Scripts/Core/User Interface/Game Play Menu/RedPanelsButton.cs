using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class RedPanelsButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Red);
        }
    }
}