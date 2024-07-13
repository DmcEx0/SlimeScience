using UnityEngine;

namespace SlimeScience.Effects
{
    public class EffectRenderer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _debuff;
        [SerializeField] private ParticleSystem _buff;
        [SerializeField] private ParticleSystem _vacuum;
        [SerializeField] private ParticleSystem _catchSlime;
        [SerializeField] private ParticleSystem _explode;
         
        private EffectSystem _effectSystem;
        private bool _isVacuumActive;

        public void Init(EffectSystem effectSystem)
        {
            _effectSystem = effectSystem;
        }

        public void PlayVacuumEffect()
        {
            if (_isVacuumActive)
            {
                return;
            }

            _vacuum.Play();
            _isVacuumActive = true;
        }

        public void StopVacuumEffect()
        {
            if (_isVacuumActive == false)
            {
                return;
            }

            _vacuum.Stop();
            _isVacuumActive = false;
        }

        public void PlayCatchSlimeEffect()
        {
            _catchSlime.Play();
        }

        public void PlayExplodeEffect()
        {
            _explode.Play();
        }

        public void RenderStatuses()
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

            _debuff.Stop();
           _buff.Stop();
        }

        private void RenderBuff()
        {
           _buff.Play();
        }

        private void RenderDebuff()
        {
           _debuff.Play();
        }
    }
}
