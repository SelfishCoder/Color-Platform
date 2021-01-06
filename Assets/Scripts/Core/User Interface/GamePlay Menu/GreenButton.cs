using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class GreenButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Green);
        }
    }
}