using UnityEngine;

namespace SlimeScience.Effects
{
    public class EffectRenderer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _debuffParticles;
        [SerializeField] private ParticleSystem _buffParticles;
        
        private EffectSystem _effectSystem;

        public void Init(EffectSystem effectSystem)
        {
            _effectSystem = effectSystem;
        }

        public void Render()
        {
            TurnOffStatuses();

            if (_effectSystem.BuffActive)
            {
                RenderBuff();
            }

            if (_effectSystem.DebuffActive)
            {
                RenderDebuff();
            }
        }

        public void TurnOffStatuses()
        {

            _debuffParticles.Stop();
           _buffParticles.Stop();
        }

        private void RenderBuff()
        {
           _buffParticles.Play();
        }

        private void RenderDebuff()
        {
           _debuffParticles.Play();
        }
    }
}
