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

            var inputRouter = new SlimeInputRouter(playerTransform, config.DistanceFofFear);

            instance.Init(CreateStateMachine(instance), inputRouter, config);

            return instance;
        }

        protected abstract SlimeConfig GetConfig();

        private StateMachine CreateStateMachine(Slime instance)
        {
            Dictionary<Type, IBehaviour> states = new Dictionary<Type, IBehaviour>()
            {
                [typeof(PatrollState)] = new PatrollState(instance),
                [typeof(FearState)] = new FearState(instance)
            };

            IBehaviour startBehaviour = new PatrollState(instance);
            return new StateMachine(startBehaviour, states);
        }
    }
}