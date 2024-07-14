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

        [SerializeField] private UpgradeButton _force;
        [SerializeField] private UpgradeButton _radius;
        [SerializeField] private UpgradeButton _angle;
        [SerializeField] private UpgradeButton _capacity;
        [SerializeField] private UpgradeButton _assistant;

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

            if (_force != null)
            {
                _force.Clicked += OnForceUpgraded;
            }

            if (_radius != null)
            {
                _radius.Clicked += OnRadiusUpgraded;
            }

            if (_angle != null)
            {
                _angle.Clicked += OnAngleUpgraded;
            }

            if (_capacity != null)
            {
                _capacity.Clicked += OnCapacityUpgraded;
            }

            if (_wallet != null)
            {
                _wallet.MoneyChanged += UpdateUI;
            }

            if (_assistant != null)
            {
                _assistant.Clicked += OnAssistantUpgraded;
            }

            UpdateUI();
        }

        private void OnDisable()
        {
            foreach (var closeButton in _closeButtons)
            {
                closeButton.onClick.RemoveListener(OnCloseClicked);
            }

            if (_force != null)
            {
                _force.Clicked -= OnForceUpgraded;
            }

            if (_radius != null)
            {
                _radius.Clicked -= OnRadiusUpgraded;
            }

            if (_angle != null)
            {
                _angle.Clicked -= OnAngleUpgraded;
            }

            if (_capacity != null)
            {
                _capacity.Clicked -= OnCapacityUpgraded;
            }

            if (_wallet != null)
            {
                _wallet.MoneyChanged -= UpdateUI;
            }

            if (_assistant != null)
            {
                _assistant.Clicked -= OnAssistantUpgraded;
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

            _force.Init(_gameVariables.AbsorptionForce);
            _radius.Init(_gameVariables.AbsorptionRadius);
            _angle.Init(_gameVariables.AbsorptionAngle);
            _capacity.Init(_gameVariables.AbsorptionCapacity);
            _assistant.Init(_gameVariables.AbsorptionAssistantCount);

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
            _force.Render();
            _radius.Render();
            _angle.Render();
            _capacity.Render();
            _assistant.Render();

            MakeUpgradeAccessible(_force, _force.Cost);
            MakeUpgradeAccessible(_radius, _radius.Cost);
            MakeUpgradeAccessible(_angle, _angle.Cost);
            MakeUpgradeAccessible(_capacity, _capacity.Cost);
            MakeUpgradeAccessible(_assistant, _assistant.Cost);
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

        private void OnAssistantUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                _gameVariables.UpgradeAssistant,
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
