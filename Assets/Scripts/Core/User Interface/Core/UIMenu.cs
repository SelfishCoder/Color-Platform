using System;
using UnityEngine;
using ColorPlatform.Management;
using System.Collections.Generic;

namespace ColorPlatform.UI
{
    [DisallowMultipleComponent]
    public abstract class UIMenu : MonoBehaviour
    {
        protected GameState activationState = default;
        protected List<CanvasGroup> panels = default;
        protected List<UIButton> buttons = default;
        protected UIManager uiManager = default;

        public GameState ActivationState => activationState;
        public event Action Activated;
        public event Action Deactivated;

        public virtual void Init(UIManager currentUIManager)
        {
            uiManager = currentUIManager;

            panels = new List<CanvasGroup>();
            buttons = new List<UIButton>();
            
            CachePanels();
            buttons = CacheButtonsInTransform(transform);
            buttons.ForEach(button => button.Init(uiManager));
        }

        protected virtual void CachePanels()
        {
            foreach (Transform child in transform.GetChild(0).transform)
            {
                panels.Add(child.GetComponent<CanvasGroup>());
            }
        }

        protected virtual List<UIButton> CacheButtonsInTransform(Transform parentTransform, bool includeInactive = true)
        {
            List<UIButton> result = new List<UIButton>();
            foreach (Transform child in parentTransform)
            {
                UIButton button = child.GetComponent<UIButton>();
                if (button) result.Add(button);
                if (child.childCount > 0)
                {
                    result.AddRange(CacheButtonsInTransform(child));
                }
            }

            return result;
        }
        
        public virtual void Activate()
        {
            gameObject.SetActive(true);
            Activated?.Invoke();
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
            Deactivated?.Invoke();
        }
    }
}