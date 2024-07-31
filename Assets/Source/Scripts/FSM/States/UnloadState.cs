using System;
using SlimeScience.Input;
using UnityEngine;

namespace SlimeScience.FSM.States
{
    public class UnloadState : IState
    {
        private const float MinDistanceForReleaseZone = 1f;
        private readonly VacuumingSupport _vacuumingSupport;
        private readonly IDetectable _detector;
        
        private readonly Action<StatesType> _changeState;
        
        public UnloadState(Action<StatesType> changeState, VacuumingSupport vacuumingSupport, IDetectable detector)
        {
            _changeState = changeState;
            _vacuumingSupport = vacuumingSupport;
            _detector = detector;
        }

        public void Enter()
        {
            _vacuumingSupport.Movement.SetMovementSpeed(_vacuumingSupport.BaseSpeed);
            
            Debug.Log("State = UNLOAD");
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _vacuumingSupport.Movement.Move(_vacuumingSupport.UnloadPosition);

            float distanceForReleaseZone =
                (_vacuumingSupport.UnloadPosition - _vacuumingSupport.transform.position).magnitude;
            
            if(distanceForReleaseZone <= MinDistanceForReleaseZone)
            {
                _changeState?.Invoke(StatesType.Patrol);
            }
        }
    }
}
