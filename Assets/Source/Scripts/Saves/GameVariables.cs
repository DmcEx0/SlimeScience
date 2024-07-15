using SlimeScience.Effects;
using System;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Saves
{
    public class GameVariables
    {
        private const string LeaderbordName = "TotalSlimes";

        public static GameVariables s_Instance;

        private AbsorptionModel _absorptionModel = new AbsorptionModel();
        private ProgressModel _progressModel = new ProgressModel();
        private EffectsModel _effectsModel = new EffectsModel();

        public float AbsorptionForce => _absorptionModel.Force * _effectsModel.ForceModifier;

        public float AbsorptionRadius => _absorptionModel.Radius * _effectsModel.RadiusModifier;

        public float AbsorptionAngle => _absorptionModel.Angle * _effectsModel.AngleModifier;

        public float AbsorptionCapacity => _absorptionModel.Capacity;

        public int Money => _progressModel.Money;

        public event Action Loaded;

        public event Action<float> RadiusUpgraded;

        public event Action<float> AngleUpgraded;

        public event Action<float> CapacityUpgraded;

        public GameVariables()
        {
            s_Instance = this;
        }

        public void Load(MonoBehaviour obj)
        {
#if UNITY_EDITOR
            obj.StartCoroutine(SimulateInit());
            return;
#elif UNITY_WEBGL && !UNITY_EDITOR
            Agava.YandexGames.PlayerAccount.GetCloudSaveData((data) =>
            {
                if (string.IsNullOrEmpty(data) == true || data == "{}")
                {
                    Loaded?.Invoke();
                    return;
                }

                var json = JsonUtility.FromJson<ProgressDTO>(data);

                _absorptionModel = new AbsorptionModel(
                    json.AbsorptionForce,
                    json.AbsorptionRadius,
                    json.AbsorptionAngle,
                    json.AbsorptionCapacity);

                _progressModel = new ProgressModel(
                    json.Money,
                    json.Slimes);

                Loaded?.Invoke();
            });

            return;
#endif
        }

        public void Save()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ProgressDTO saves = new ProgressDTO (
                _progressModel.Money,
                _progressModel.Slimes,
                _absorptionModel.Force,
                _absorptionModel.Radius,
                _absorptionModel.Angle,
                _absorptionModel.Capacity
                );

            Agava.YandexGames.PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(saves));
            Agava.YandexGames.Leaderboard.SetScore(LeaderbordName, _progressModel.Slimes);
#endif
        }

        public void AddMoney(int amount)
        {
            _progressModel.AddMoney(amount);
        }

        public void SpendMoney(int amount)
        {
            _progressModel.SpendMoney(amount);
        }

        public void AddSlimes(int slimes)
        {
            _progressModel.AddSlimes(slimes);
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

        public void AddModifier(EffectModifiers effect, float percent)
        {
            _effectsModel.AddModifier(effect, percent);
        }

        public void ResetModifier(EffectModifiers effect)
        {
            _effectsModel.ResetModifier(effect);
        }

        public void RemoveModifier(EffectModifiers effect, float percent)
        {
            _effectsModel.RemoveModifier(effect, percent);
        }

        private IEnumerator SimulateInit()
        {
            yield return new WaitForSeconds(1);

            Loaded?.Invoke();
        }
    }
}