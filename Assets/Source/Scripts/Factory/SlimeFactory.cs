using SlimeScience.Characters;
using SlimeScience.Characters.Slimes;
using SlimeScience.Configs;
using System;
using System.Collections.Generic;
using SlimeScience.Input;
using UnityEngine;
using SlimeScience.FSM;
using SlimeScience.FSM.States.Slimes;

namespace SlimeScience.Factory
{
    public abstract class SlimeFactory : GameObjectFactory
    {
        public MobileObject Get(Transform playerTransform)
        {
            var config = GetConfig();
            Slime instance = CreateInstance(config.Prefab);

            PlayerDetector playerDetector = new PlayerDetector(instance.transform, playerTransform, config.DistanceFofFear);
            var inputRouter = new SlimeInputRouter(playerDetector);

            instance.Init(CreateStateMachine(instance, playerDetector), inputRouter, config);

            return instance;
        }

        protected abstract SlimeConfig GetConfig();

        private StateMachine CreateStateMachine(Slime instance, IDetectable detector)
        {
            Dictionary<Type, IBehaviour> states = new Dictionary<Type, IBehaviour>()
            {
                [typeof(PatrollState)] = new PatrollState(instance, detector),
                [typeof(FearState)] = new FearState(instance, detector)
            };

            IBehaviour startBehaviour = new PatrollState(instance, detector);
            return new StateMachine(startBehaviour, states);
        }
    }
}