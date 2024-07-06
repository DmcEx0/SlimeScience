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

        [SerializeField] private UpgradeButton _forceUpgradeButton;
        [SerializeField] private UpgradeButton _radiusUpgradeButton;
        [SerializeField] private UpgradeButton _angleUpgradeButton;
        [SerializeField] private UpgradeButton _capacityUpgradeButton;

        private Tweener _closeTweener;

        private Wallet _wallet;

        private GameVariables _gameVariables;

        public event Action Closed;

        private void OnEnable()
        {
            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.AddListener(OnCloseClicked);
            }

            if (_forceUpgradeButton != null)
            {
                _forceUpgradeButton.Clicked += OnForceUpgraded;
            }

            if (_radiusUpgradeButton != null)
            {
                _radiusUpgradeButton.Clicked += OnRadiusUpgraded;
            }

            if (_angleUpgradeButton != null)
            {
                _angleUpgradeButton.Clicked += OnAngleUpgraded;
            }

            if (_capacityUpgradeButton != null)
            {
                _capacityUpgradeButton.Clicked += OnCapacityUpgraded;
            }

            if (_wallet != null)
            {
                _wallet.MoneyChanged += UpdateUI;
            }

            UpdateUI();
        }

        private void OnDisable()
        {
            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.RemoveListener(OnCloseClicked);
            }

            if (_forceUpgradeButton != null)
            {
                _forceUpgradeButton.Clicked -= OnForceUpgraded;
            }

            if (_radiusUpgradeButton != null)
            {
                _radiusUpgradeButton.Clicked -= OnRadiusUpgraded;
            }

            if (_angleUpgradeButton != null)
            {
                _angleUpgradeButton.Clicked -= OnAngleUpgraded;
            }

            if (_capacityUpgradeButton != null)
            {
                _capacityUpgradeButton.Clicked -= OnCapacityUpgraded;
            }

            if (_wallet != null)
            {
                _wallet.MoneyChanged -= UpdateUI;
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

            _forceUpgradeButton.Init(_gameVariables.AbsorptionForce);
            _radiusUpgradeButton.Init(_gameVariables.AbsorptionRadius);
            _angleUpgradeButton.Init(_gameVariables.AbsorptionAngle);
            _capacityUpgradeButton.Init(_gameVariables.AbsorptionCapacity);

            wallet.MoneyChanged += UpdateUI;

            UpdateUI();
        }

        private void MakeUpgradeAccessible(UpgradeButton upgradeButton, int cost)
        {
            bool isEnoughMoney = _wallet.IsEnoughMoney(cost);

            if (isEnoughMoney)
            {
                upgradeButton.SetInteractable();
            }
            else
            {
                upgradeButton.SetNotInteractable();
            }
        }

        private void UpdateUI()
        {
            _forceUpgradeButton.Render();
            _radiusUpgradeButton.Render();
            _angleUpgradeButton.Render();
            _capacityUpgradeButton.Render();

            MakeUpgradeAccessible(_forceUpgradeButton, _forceUpgradeButton.Cost);
            MakeUpgradeAccessible(_radiusUpgradeButton, _radiusUpgradeButton.Cost);
            MakeUpgradeAccessible(_angleUpgradeButton, _angleUpgradeButton.Cost);
            MakeUpgradeAccessible(_capacityUpgradeButton, _capacityUpgradeButton.Cost);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _upgradesView.gameObject.SetActive(true);

            _upgradesView.transform.localScale = Vector3.zero;

            _upgradesView.transform
                .DOScale(Vector3.one, OpenDuration)
                .SetEase(Ease.OutBack);
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

        private void OnForceUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeForce,
                upgradeButton,
                cost);
            
            SoundsManager.PlayTapUI();
        }

        private void OnRadiusUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeRadius,
                upgradeButton,
                cost);
            
            SoundsManager.PlayTapUI();
        }

        private void OnAngleUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeAngle,
                upgradeButton,
                cost);
            
            SoundsManager.PlayTapUI();
        }

        private void OnCapacityUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeCapacity,
                upgradeButton,
                cost);
            
            SoundsManager.PlayTapUI();
        }

        private void UpgradeClick(Action<float> upgradeCallback, UpgradeButton upgradeButton, int cost)
        {
            if (_wallet.IsEnoughMoney(cost))
            {
                _wallet.Spend(cost);
                upgradeButton.Upgrade();
                upgradeCallback?.Invoke(upgradeButton.Value);
            }

            UpdateUI();
            
            SoundsManager.PlayTapUI();
        }
    }
}
