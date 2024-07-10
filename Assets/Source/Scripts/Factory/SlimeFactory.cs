using SlimeScience.Characters;
using SlimeScience.Configs;
using System;
using System.Collections.Generic;
using SlimeScience.Input;
using UnityEngine;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
using SlimeScience.FSM.States.Slimes;
using SlimeScience.Pool;
using SlimeScience.Util;

namespace SlimeScience.Factory
{
    public abstract class SlimeFactory : GameObjectFactory
    {
        private ObjectPool<Slime> _pool;
        private SlimeConfig _config;

        public void CreatePool(int poolSize, Transform parent)
        {
            _pool = new ObjectPool<Slime>(parent);
            _config = GetConfig();

            for (int i = 0; i < poolSize; i++)
            {
                var slime = CreateInstance(_config.BuildData.GetRandomPrefab, parent.transform.position);
                BuildSlime(slime, _config.BuildData);
                
                _pool.InitializePool(slime);
            }
        }

        public MobileObject Get(Transform playerTransform, Vector3 position)
        {
            var slime = _pool.GetAvailable();
            TargetDetector targetDetector = new TargetDetector(_config.DistanceFofFear);
            targetDetector.SetParentTransforms(slime.transform);
            targetDetector.SetTargetTransform(playerTransform);

            var inputRouter = new SlimeInputRouter(targetDetector);

            slime.Init(CreateStateMachine(slime, targetDetector), inputRouter, _config);
            slime.transform.position = position;
            slime.gameObject.SetActive(true);

            return slime;
        }

        protected abstract SlimeConfig GetConfig();

        private StateMachine CreateStateMachine(Slime instance, IDetectable detector)
        {
            StateMachine stateMachine = new StateMachine();
            Action<StatesType> changeStateAction = stateMachine.ChangeState;

            Dictionary<StatesType, IState> states = new Dictionary<StatesType, IState>()
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