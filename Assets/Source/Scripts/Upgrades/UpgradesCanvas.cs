using SlimeScience.Money;
using SlimeScience.Saves;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class UpgradesCanvas : MonoBehaviour
    {
        [SerializeField] private Button[] _closeButtons;

        [SerializeField] private UpgradeButton _forceUpgradeButton;
        [SerializeField] private UpgradeButton _radiusUpgradeButton;
        [SerializeField] private UpgradeButton _angleUpgradeButton;
        [SerializeField] private UpgradeButton _capacityUpgradeButton;


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

        private void OnCloseClicked()
        {
            gameObject.SetActive(false);
            Closed?.Invoke();
        }

        private void OnForceUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeForce,
                upgradeButton,
                cost);
        }

        private void OnRadiusUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeRadius,
                upgradeButton,
                cost);
        }

        private void OnAngleUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeAngle,
                upgradeButton,
                cost);
        }

        private void OnCapacityUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeCapacity,
                upgradeButton,
                cost);
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
        }
    }
}
