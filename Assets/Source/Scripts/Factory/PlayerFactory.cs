using System;
using System.Collections.Generic;
using SlimeScience.Characters;
using SlimeScience.Configs;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
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
            StateMachine stateMachine = new StateMachine();
            Action<StatesType> changeStateAction = stateMachine.ChangeState;
            
            Dictionary<StatesType, IState> states = new Dictionary<StatesType, IState>()
            {
                [StatesType.PlayerIdle] = new PlayerIdleState(changeStateAction, instance),
                [StatesType.Movement] = new MovementState(changeStateAction, instance)
            };

            stateMachine.SetStates(StatesType.PlayerIdle ,states);
            
            return stateMachine;
        }
    }
}