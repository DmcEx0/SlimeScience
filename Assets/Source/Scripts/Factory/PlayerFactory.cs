using System;
using System.Collections.Generic;
using SlimeScience.Characters.Playable;
using SlimeScience.Configs;
using SlimeScience.FSM;
using SlimeScience.FSM.States.Players;
using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class PlayerFactory : GameObjectFactory
    {
        public Player Get()
        {
            var config = GetConfig();
            Player instance = CreateInstance(config.Prefab, Vector3.zero);

            IInputRouter inputRouter = new PlayerInputRouter();

            instance.Init(CreateStateMachine(instance), inputRouter, config);

            return instance;
        }

        protected abstract PlayerConfig GetConfig();

        private StateMachine CreateStateMachine(Player instance)
        {
            Dictionary<Type, IBehaviour> states = new Dictionary<Type, IBehaviour>()
            {
                [typeof(IdleState)] = new IdleState(instance),
                [typeof(MovementState)] = new MovementState(instance)
            };

            IBehaviour startBehaviour = new IdleState(instance);
            return new StateMachine(startBehaviour, states);
        }
    }
}