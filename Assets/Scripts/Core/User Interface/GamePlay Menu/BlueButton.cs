using ColorPlatform.Gameplay;

namespace ColorPlatform.UI
{
    public class BlueButton : UIButton
    {
        protected override void OnClicked()
        {
            PlatformManager.Current.SetColor(PlatformColor.Blue);
        }
    }
}