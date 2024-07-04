using System;
using System.Collections.Generic;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
using SlimeScience.FSM.States.Slimes;
using SlimeScience.Input;
using SlimeScience.Util;
using UnityEngine;

namespace SlimeScience.Factory
{
    public abstract class VacuumingSupportFactory : GameObjectFactory
    {
        public VacuumingSupport Get(Vector3 position)
        {
            var config = GetConfig();
            VacuumingSupport instance = CreateInstance(config.Prefab, position);
            
            var observer = new SlimeObserver(instance.transform);
            var targetDetector = new TargetDetector(config.DistanceFofIncreaseSpeed);
            targetDetector.SetParentTransforms(instance.transform);
            var inputRouter = new VacuumingSupportInputRouter(targetDetector, observer);

            instance.Init(CreateStateMachine(instance, targetDetector), inputRouter, config);

            return instance;
        }

        protected abstract VacuumingSupportConfig GetConfig();

        private StateMachine CreateStateMachine(VacuumingSupport instance, IDetectable detector)
        {
            StateMachine stateMachine = new StateMachine();
            Action<StatesType> changeStateAction = stateMachine.ChangeState;
            
            Dictionary<StatesType, IState> states = new Dictionary<StatesType, IState>()
            {
                [StatesType.Patrol] = new PatrolState(changeStateAction, instance, detector),
                [StatesType.Hunting] = new HuntingState(changeStateAction, instance, detector),
                [StatesType.Unloading] = new UnloadState(changeStateAction, instance, detector)
            };
            
            stateMachine.SetStates(StatesType.Patrol, states);
            
            return stateMachine;
        }
    }
}