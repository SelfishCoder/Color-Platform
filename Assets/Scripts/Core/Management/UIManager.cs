using UnityEngine;
using System.Linq;
using ColorPlatform.UI;
using System.Collections.Generic;

namespace ColorPlatform.Management
{
    public class UIManager
    {
        private GameManager gameManager = default;
        public GameManager GameManager => gameManager;

        /// <summary>
        /// Private constructor in order to ensure gameManager is not empty.
        /// </summary>
        private UIManager()
        {
        }

        /// <summary>
        /// Constructor takes current game manager and caches.
        /// </summary>
        /// <param name="currentGameManager"></param>
        public UIManager(GameManager currentGameManager)
        {
            this.gameManager = currentGameManager;
            gameManager.StateChanged += OnGameStateChanged;
        }

        private List<UIMenu> menus = default;

        public void Init()
        {
            menus = MonoBehaviour.FindObjectsOfType<UIMenu>().ToList();
            foreach (UIMenu menu in menus)
            {
                menu.Init(this);
            }
        }

        private void OnGameStateChanged(GameState gameState)
        {
            menus.ForEach(menu => menu.Deactivate());
            menus.Find(menu => menu.ActivationState == gameState).Activate();
        }
    }
}