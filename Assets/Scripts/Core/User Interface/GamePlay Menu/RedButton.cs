using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class RedButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Red);
        }
    }
}