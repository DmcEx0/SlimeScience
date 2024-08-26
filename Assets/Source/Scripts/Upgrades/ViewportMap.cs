using System;
using SlimeScience.Money;
using SlimeScience.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class ViewportMap
    {
        private Color activeColor = Color.gray;
        private Color inactiveColor = Color.white;

        private Button _button;
        private UpgradeViewport _viewport;

        public ViewportMap(Button button, UpgradeViewport viewport)
        {
            _button = button;
            _viewport = viewport;
        }

        public event Action<ViewportMap> Clicked;

        public void Init(Wallet wallet, GameVariables gameVariables)
        {
            _viewport.Init(wallet, gameVariables);
        }

        public void Show()
        {
            SetColor(activeColor);
            _viewport.Show();
        }

        public void Hide()
        {
            SetColor(inactiveColor);
            _viewport.Hide();
        }

        public void Subscribe()
        {
            _button.onClick.AddListener(OnClick);
        }

        public void Unsubscribe()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void SetColor(Color color)
        {
            _button.image.color = color;
        }

        private void OnClick()
        {
            Clicked?.Invoke(this);
        }
    }
}
