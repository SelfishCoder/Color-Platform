using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    public class PlayButton : UIButton
    {
        protected override void OnClicked()
        {
            uiManager.GameManager.SetGameState(GameState.GamePlay);
        }
    }
}