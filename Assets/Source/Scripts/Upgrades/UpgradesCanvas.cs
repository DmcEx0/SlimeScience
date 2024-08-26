using System;
using DG.Tweening;
using SlimeScience.Money;
using SlimeScience.Saves;
using SlimeScience.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class UpgradesCanvas : MonoBehaviour
    {
        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;

        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private RectTransform _upgradesView;

        [SerializeField] private PlayerViewport _playerUpgrades;
        [SerializeField] private ShipViewport _shipUpgrades;

        [SerializeField] private Button _playerButton;
        [SerializeField] private Button _shipButton;

        private ViewportMap[] _viewports;
        private Tweener _closeTweener;
        private Wallet _wallet;
        private GameVariables _gameVariables;

        private ViewportMap _currentViewport;

        public event Action Closed;

        private void OnEnable()
        {
            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.AddListener(OnCloseClicked);
            }

            if (_viewports != null)
            {
                foreach (var viewport in _viewports)
                {
                    viewport.Subscribe();
                    viewport.Clicked += OnViewportClicked;
                }
            }

            UpdateUI();
        }

        private void OnDisable()
        {
            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.RemoveListener(OnCloseClicked);
            }

            if (_viewports != null)
            {
                foreach (var viewport in _viewports)
                {
                    viewport.Unsubscribe();
                    viewport.Clicked -= OnViewportClicked;
                }
            }
        }

        public void Init(Wallet wallet, GameVariables gameVariables)
        {
            _wallet = wallet;
            _gameVariables = gameVariables;

            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.AddListener(OnCloseClicked);
            }

            wallet.MoneyChanged += UpdateUI;

            _viewports = new ViewportMap[]
            {
                new (_playerButton, _playerUpgrades),
                new (_shipButton, _shipUpgrades),
            };

            foreach (var viewport in _viewports)
            {
                viewport.Init(wallet, gameVariables);
                viewport.Subscribe();
                viewport.Clicked += OnViewportClicked;
            }

            _currentViewport = _viewports[0];
            UpdateUI();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _upgradesView.gameObject.SetActive(true);

            _upgradesView.transform.localScale = Vector3.zero;

            foreach (var viewport in _viewports)
            {
                viewport.Hide();
            }

            _currentViewport.Show();

            _upgradesView.transform
                .DOScale(Vector3.one, OpenDuration)
                .SetEase(Ease.OutBack);
        }

        private void UpdateUI()
        {
            _playerUpgrades.UpdateUI();
            _shipUpgrades.UpdateUI();
        }

        private void OnCloseClicked()
        {
            _closeTweener?.Kill();

            _closeTweener = _upgradesView.transform
                .DOScale(Vector3.zero, CloseDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    _upgradesView.gameObject.SetActive(false);
                    Closed?.Invoke();
                });

            SoundsManager.PlayTapUI();
        }

        private void OnViewportClicked(ViewportMap viewport)
        {
            foreach (var vp in _viewports)
            {
                vp.Hide();
            }

            _currentViewport = viewport;
            _currentViewport.Show();
        }
    }
}
