using SlimeScience.Configs;
using SlimeScience.FSM;
using SlimeScience.FSM.States;
using SlimeScience.FSM.States.Players;
using System.Collections.Generic;

namespace SlimeScience.Characters.Playable
{
    public class Player : MobileObject
    {
        private float _rangeVacuum;

        private void Update()
        {
            UpdateStateMachine();
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not PlayerConfig)
                return;

            var playerConfig = config as PlayerConfig;

            _rangeVacuum = playerConfig.RangeVacuum;
        }

        protected override Dictionary<StatesType, IBehaviour> GetBehaviours()
        {
            //return new Dictionary<Type, IBehaviour>()
            //{
            //    [typeof(IdleState)] = new IdleState(this),
            //    [typeof(MovementState)] = new MovementState(this)
            //};
            return new Dictionary<StatesType, IBehaviour>()
            {
                //[StatesType.Idle] = new IdleState(this),
                //[StatesType.Movement] = new MovementState(this)
            };
        }

        protected override IBehaviour GetStartState()
        {
            return new IdleState(this);
        }
    }
}