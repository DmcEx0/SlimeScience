using System;
using System.Collections.Generic;
using SlimeScience.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Blocks
{
    public class OpenBlocksPopupsManager : MonoBehaviour
    {
        [SerializeField] private List<OpenBlocksPopup> _popups;
        [SerializeField] private OpenBlocksPopup _finishGamePopup;
        [SerializeField] private Image _fadeScreen;

        private Player _player;

        private void Start()
        {
            _fadeScreen.gameObject.SetActive(false);
        }
        
        public void Init(Player player)
        {
            _player = player;
        }

        public void ShowPopup(int index)
        {
            _player.Movement.Disable();
            _popups[index - 1].Show(DisableFadeScreen);
            _fadeScreen.gameObject.SetActive(true);
        }
        
        public void ShowEndGamePopup(Action onClosed)
        {
            _finishGamePopup.Show(onClosed);
            _fadeScreen.gameObject.SetActive(true);
        }
        
        private void DisableFadeScreen()
        {
            _player.Movement.Enable();
            _fadeScreen.gameObject.SetActive(false);
        }
    }
}
