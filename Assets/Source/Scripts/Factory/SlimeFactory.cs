using SlimeScience.Characters;
using System;
using System.Collections.Generic;
using SlimeScience.Configs;
using SlimeScience.Input;
using UnityEngine;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
using SlimeScience.FSM.States.Slimes;
using SlimeScience.Pool;
using SlimeScience.Util;
using SlimeScience.Configs.Slimes;

namespace SlimeScience.Factory
{
    public abstract class SlimeFactory : GameObjectFactory
    {
        [SerializeField] private int _poolSize;

        private ObjectPool<Slime> _pool;
        private SlimeConfig _config;
        private SlimeTypeValues _slimeType;

        public void CreatePool(Transform parent)
        {
            _pool = new ObjectPool<Slime>(parent);
            _config = GetConfig();

            for (int i = 0; i < _poolSize; i++)
            {
                var slime = CreateInstance(_config.BuildData.GetRandomPrefab, parent.transform.position);
                BuildSlime(slime, _config.BuildData);

                _pool.InitializePool(slime);
            }
        }

        public MobileObject Get(SlimeType type, Transform playerTransform, Vector3 position)
        {
            var typeConfig = GetTypeConfig(type);
            var slime = _pool.GetAvailable();
            TargetDetector targetDetector = new TargetDetector(_config.DistanceFofFear);
            targetDetector.SetParentTransforms(slime.transform);
            targetDetector.SetTargetTransform(playerTransform);

            var inputRouter = new SlimeInputRouter(targetDetector);

            _config.SetType(typeConfig.Type);
            _config.SetWeight(typeConfig.Weight);

            slime.Init(CreateStateMachine(type, slime, targetDetector), inputRouter, _config);
            slime.transform.position = position;
            slime.gameObject.SetActive(true);
            slime.SetOriginPosition(position);

            slime.transform.localScale = typeConfig.Scale;
            slime.SetOriginalScale(typeConfig.Scale);

            if (type == SlimeType.Boss)
            {
                var hatPos = slime.GetComponentInChildren<HatPosition>();
                var hatPrefab = _config.BuildData.GetRandomHat;

                Instantiate(hatPrefab, hatPos.transform.position, Quaternion.identity, hatPos.transform);
            }

            return slime;
        }

        protected abstract SlimeConfig GetConfig();
        protected abstract SlimeTypeValues GetTypeConfig(SlimeType type);

        private StateMachine CreateStateMachine(SlimeType type, Slime instance, IDetectable detector)
        {
            StateMachine stateMachine = new StateMachine();
            Action<StatesType> changeStateAction = stateMachine.ChangeState;
            Dictionary<StatesType, IState> states;

            if (type == SlimeType.Boss)
            {
                states = new Dictionary<StatesType, IState>()
                {
                    [StatesType.SlimeIdle] = new SlimeIdleState(changeStateAction, instance, detector)
                };

                stateMachine.SetStates(StatesType.SlimeIdle, states);

                return stateMachine;
            }

            states = new Dictionary<StatesType, IState>()
            {
                [StatesType.Patrol] = new PatrolState(changeStateAction, instance, detector),
                [StatesType.Fear] = new FearState(changeStateAction, instance, detector),
                [StatesType.SlimeIdle] = new SlimeIdleState(changeStateAction, instance, detector)
            };

            stateMachine.SetStates(StatesType.Patrol, states);

            return stateMachine;
        }

        private void BuildSlime(Slime instance, SlimeBuildData buildData)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = instance.GetComponentInChildren<SkinnedMeshRenderer>();

            Material[] oldMaterials = skinnedMeshRenderer.materials;
            oldMaterials[0] = buildData.GetRandomBodyMaterial;
            oldMaterials[1] = buildData.GetRandomFaceMaterial;
            skinnedMeshRenderer.materials = oldMaterials;
        }
    }
}