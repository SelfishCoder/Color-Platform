using UnityEngine;
using UnityEngine.UI;
using ColorPlatform.Management;

namespace ColorPlatform.UI
{
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        protected UIManager uiManager = default;

        public void Init(UIManager currentUIManager)
        {
            this.uiManager = currentUIManager;
            GetComponent<Button>().onClick.AddListener(OnClicked);
        }

        protected virtual void OnClicked()
        {
        }
    }
}