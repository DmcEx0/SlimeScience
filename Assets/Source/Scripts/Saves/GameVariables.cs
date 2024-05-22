
using System;
using UnityEngine;

namespace SlimeScience.Saves
{
    public class GameVariables
    {
        private AbsorptionModel _absorptionModel;
        private ProgressModel _progressModel;

        public float AbsorptionForce => _absorptionModel.Force;

        public float AbsorptionRadius => _absorptionModel.Radius;

        public float AbsorptionAngle => _absorptionModel.Angle;

        public float AbsorptionCapacity => _absorptionModel.Capacity;

        public int Money => _progressModel.Money;

        public event Action Loaded;

        public event Action<float> RadiusUpgraded;

        public event Action<float> AngleUpgraded;

        public event Action<float> CapacityUpgraded;

        public void Load(MonoBehaviour obj)
        {
            _absorptionModel = new AbsorptionModel();
            _progressModel = new ProgressModel();

            Loaded?.Invoke();
        }

        public void AddMoney(int amount)
        {
            _progressModel.AddMoney(amount);
        }

        public void SpendMoney(int amount)
        {
            _progressModel.SpendMoney(amount);
        }

        public void UpgradeForce(float force)
        {
            _absorptionModel.SetForce(force);
        }

        public void UpgradeRadius(float radius)
        {
            _absorptionModel.SetRadius(radius);
            RadiusUpgraded?.Invoke(radius);
        }

        public void UpgradeAngle(float angle)
        {
            _absorptionModel.SetAngle(angle);
            AngleUpgraded?.Invoke(angle);
        }

        public void UpgradeCapacity(float capacity)
        {
            _absorptionModel.SetCapacity(capacity);
            CapacityUpgraded?.Invoke(capacity);
        }
    }
}
