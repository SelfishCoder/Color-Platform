using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class BluePanelsButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Blue);
        }
    }
}