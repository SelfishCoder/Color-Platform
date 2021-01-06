using UnityEngine;
using UnityEngine.UI;

namespace ColorPlatform.UI
{
    [DisallowMultipleComponent, RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        protected  virtual void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClicked);
        }
        
        protected virtual void OnClicked(){}
    }
}