
using Agava.YandexGames;
using SlimeScience.Effects;
using System;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Saves
{
    public class GameVariables
    {
        private const string LeaderbordName = "TotalSlimes";

        private AbsorptionModel _absorptionModel = new AbsorptionModel();
        private ProgressModel _progressModel = new ProgressModel();
        private ShipModel _shipModel = new ShipModel();

        private EffectsModel _effectsModel = new EffectsModel();

        public float AbsorptionForce => _absorptionModel.Force * _effectsModel.ForceModifier;

        public float AbsorptionRadius => _absorptionModel.Radius * _effectsModel.RadiusModifier;

        public float AbsorptionAngle => _absorptionModel.Angle * _effectsModel.AngleModifier;

        public float AbsorptionCapacity => _absorptionModel.Capacity;

        public float AbsorptionAssistantCount => _absorptionModel.AssistantCount;

        public int Money => _progressModel.Money;

        public int RoomIndex => _progressModel.RoomIndex;

        public int CollectedSlimes => _progressModel.CollectedSlimes;

        public int SlimesGoal => _progressModel.SlimesGoal;

        public float ShipCapacity => _shipModel.Capacity;

        public float ShipSpeed => _shipModel.Speed;

        public bool TutorialPassed => _progressModel.TutorialPassed;

        public event Action Loaded;

        public event Action<float> RadiusUpgraded;

        public event Action<float> AngleUpgraded;

        public event Action<float> CapacityUpgraded;

        public event Action<float> AssistantUpgraded;

        public event Action<int> IncreasedRoomIndex;

        public event Action<int> SlimeCollected;

        public event Action SlimeCollectedReset;

        public event Action<int> SlimeGoalChanged;

        public event Action<float> UpgradedShipCapacity;

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
                    json.AbsorptionCapacity,
                    json.AssistantCount);

                _progressModel = new ProgressModel(
                    json.Money,
                    json.Slimes,
                    json.RoomIndex,
                    json.CollectedSlimes,
                    json.SlimesGoal,
                    json.TutorialPassed);

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
                _progressModel.RoomIndex,
                _progressModel.CollectedSlimes,
                _progressModel.SlimesGoal,
                _progressModel.TutorialPassed,
                _absorptionModel.Force,
                _absorptionModel.Radius,
                _absorptionModel.Angle,
                _absorptionModel.Capacity,
                _absorptionModel.AssistantCount
                );

            Agava.YandexGames.PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(saves));
            Agava.YandexGames.Leaderboard.SetScore(LeaderbordName, _progressModel.Slimes);
#endif
        }

        public void ResetSave()
        {
            _progressModel = new ProgressModel();
            _absorptionModel = new AbsorptionModel();
            _effectsModel = new EffectsModel();
            _shipModel = new ShipModel();

            Save();
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

        public void UpgradeAssistant(float assistantCount)
        {
            _absorptionModel.SetAssistant(assistantCount);
            AssistantUpgraded?.Invoke(assistantCount);
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

        public void AddCollectedSlimes(int slimes)
        {
            _progressModel.AddCollectedSlimes(slimes);
            SlimeCollected?.Invoke(_progressModel.CollectedSlimes);
        }

        public void ResetCollectSlimes()
        {
            _progressModel.ResetCollectedSlimes();
            SlimeCollectedReset?.Invoke();
        }

        public void IncreaseRoomIndex()
        {
            _progressModel.IncreaseRoomIndex();
            IncreasedRoomIndex?.Invoke(_progressModel.RoomIndex);
        }

        public void SetSlimesGoal(int goal)
        {
            _progressModel.SetSlimesGoal(goal);
            SlimeGoalChanged?.Invoke(goal);
        }
        
        public void SetTutorialPassed()
        {
            _progressModel.SetTutorialPassed();
        }

        public void UpgradeShipCapacity(float capacity)
        {
            _shipModel.SetCapacity(capacity);
            UpgradedShipCapacity?.Invoke(capacity);
        }

        public void UpgradeShipSpeed(float speed)
        {
            _shipModel.SetSpeed(speed);
        }

        private IEnumerator SimulateInit()
        {
            yield return new WaitForSeconds(1);

            Loaded?.Invoke();
        }
    }
}
