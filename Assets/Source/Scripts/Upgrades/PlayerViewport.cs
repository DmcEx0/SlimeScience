using SlimeScience.Money;
using SlimeScience.Saves;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Upgrades
{
    public class PlayerViewport : UpgradeViewport
    {
        [SerializeField] private UpgradeButton _force;
        [SerializeField] private UpgradeButton _radius;
        [SerializeField] private UpgradeButton _angle;
        [SerializeField] private UpgradeButton _capacity;
        [SerializeField] private UpgradeButton _assistant;

        private void OnEnable()
        {
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

            if (_assistant != null)
            {
                _assistant.Clicked += OnAssistantUpgraded;
            }
        }

        private void OnDisable()
        {
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

            if (_assistant != null)
            {
                _assistant.Clicked -= OnAssistantUpgraded;
            }
        }

        public override void Init(Wallet wallet, GameVariables gameVariables)
        {
            base.Init(wallet, gameVariables);

            _force.Init(Variables.AbsorptionForce);
            _radius.Init(Variables.AbsorptionRadius);
            _angle.Init(Variables.AbsorptionAngle);
            _capacity.Init(Variables.AbsorptionCapacity);
            _assistant.Init(Variables.AbsorptionAssistantCount);

            UpdateUI();
        }

        public override void UpdateUI()
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

        private void OnForceUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeForce,
                upgradeButton,
                cost);
        }

        private void OnRadiusUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeRadius,
                upgradeButton,
                cost);
        }

        private void OnAngleUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeAngle,
                upgradeButton,
                cost);
        }

        private void OnCapacityUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeCapacity,
                upgradeButton,
                cost);
        }

        private void OnAssistantUpgraded(UpgradeButton upgradeButton, int cost)
        {
            UpgradeClick(
                Variables.UpgradeAssistant,
                upgradeButton,
                cost);
        }
    }
}
