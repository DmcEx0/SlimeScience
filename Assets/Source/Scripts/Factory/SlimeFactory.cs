using SlimeScience.Characters;
using SlimeScience.Characters.Slimes;
using SlimeScience.Configs;
using System;
using System.Collections.Generic;
using SlimeScience.Input;
using UnityEngine;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
using SlimeScience.FSM.States.Slimes;

namespace SlimeScience.Factory
{
    public abstract class SlimeFactory : GameObjectFactory
    {
        public MobileObject Get(Transform playerTransform, Vector3 position)
        {
            var config = GetConfig();
            Slime instance = CreateInstance(config.BuildData.GetRandomPrefab, position);

            PlayerDetector playerDetector = new PlayerDetector(instance.transform, playerTransform, config.DistanceFofFear);
            var inputRouter = new SlimeInputRouter(playerDetector);

            BuildSlime(instance, config.BuildData);

            instance.Init(CreateStateMachine(instance, playerDetector), inputRouter, config);

            return instance;
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
            
            stateMachine.SetStates(StatesType.SlimeIdle, states);
            
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