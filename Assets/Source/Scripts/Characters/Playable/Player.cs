using SlimeScience.Configs;
using SlimeScience.Input;

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
    }
}