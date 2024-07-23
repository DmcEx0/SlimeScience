using SlimeScience.Money;
using SlimeScience.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class ShipViewport : UpgradeViewport
    {
        [SerializeField] private UpgradeButton _capacity;
        [SerializeField] private UpgradeButton _speed;

        private void OnEnable()
        {
            if (_capacity != null)
            {
                _capacity.Clicked += OnCapacityUpgraded;
            }

            if (_speed != null)
            {
                _speed.Clicked += OnSpeedUpgraded;
            }
        }

        private void OnDisable()
        {
            if (_capacity != null)
            {
                _capacity.Clicked -= OnCapacityUpgraded;
            }

            if (_speed != null)
            {
                _speed.Clicked -= OnSpeedUpgraded;
            }
        }

        public override void Init(Wallet wallet, GameVariables gameVariables)
        {
            base.Init(wallet, gameVariables);

            _capacity.Init(Variables.ShipCapacity);
            _speed.Init(Variables.ShipSpeed);

            UpdateUI();
        }

        public override void UpdateUI()
        {
            _capacity.Render();
            _speed.Render();

            MakeUpgradeAccessible(_capacity, _capacity.Cost);
            MakeUpgradeAccessible(_speed, _speed.Cost);
        }

        private void OnCapacityUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeShipCapacity,
                upgradeButton,
                cost);
        }

        private void OnSpeedUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeShipSpeed,
                upgradeButton,
                cost);
        }
    }
}
