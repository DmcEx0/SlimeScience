using SlimeScience.Configs;

namespace SlimeScience.Characters.Slimes
{
    public class Slime : MobileObject
    {
        private float _fearSpeed;
        private float _baseSpeed;

        public float BaseSpeed => _baseSpeed;
        public float FearSpeed => _fearSpeed;

        private void Update()
        {
            UpdateStateMachine();
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not SlimeConfig)
                return;

            SlimeConfig slimeConfig = config as SlimeConfig;

            _baseSpeed = slimeConfig.BaseSpeed;
            _fearSpeed = slimeConfig.FearSpeed;
        }
    }
}