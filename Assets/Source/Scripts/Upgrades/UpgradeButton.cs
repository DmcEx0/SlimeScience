using System;
using Lean.Localization;
using SlimeScience.Configs.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        private const string _levelNumberTranslationName = "level_number";
        private const string _maxTranslationName = "max";

        [SerializeField] private TMP_Text _lvlNumber;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private GunUpgrade _upgrade;

        private int _currentIndex;
        private int _nextIndex;

        public event Action<UpgradeButton, int> Clicked;

        public int Cost => _upgrade.GetCost(_nextIndex);

        public float Value => _upgrade.GetValue(_currentIndex);

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnButtonClickedHandler);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnButtonClickedHandler);
        }

        public void Init(float value) {
            _currentIndex = _upgrade.GetLevel(value);
            _nextIndex = _currentIndex + 1;
        }

        public void Render()
        {
            int cost = _upgrade.GetCost(_nextIndex);

            string lvlText = LeanLocalization.GetTranslationText(_levelNumberTranslationName);

            int currentLevel = _currentIndex + 1;
            _lvlNumber.text = string.Format(lvlText, currentLevel);

            if (cost < 0)
            {
                _cost.text = LeanLocalization.GetTranslationText(_maxTranslationName);
                SetNotInteractable();
            }
            else
            {
                _cost.text = cost.ToString();
            }
        }

        public void SetInteractable()
        {
            _upgradeButton.interactable = true;
        }

        public void SetNotInteractable()
        {
            _upgradeButton.interactable = false;
        }

        public void Upgrade()
        {
            _currentIndex += 1;
            _nextIndex = _currentIndex + 1;
        }

        private void OnButtonClickedHandler()
        {
            Clicked?.Invoke(this, Cost);
        }
    }
}
