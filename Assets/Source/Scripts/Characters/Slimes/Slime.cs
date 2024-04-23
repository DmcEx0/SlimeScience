using UnityEngine;
using SlimeScience.Configs;
using System.Collections;

namespace SlimeScience.Characters.Slimes
{
    public class Slime : MobileObject
    {
        private const float ResetVelocityTime = 1f;

        [SerializeField] private Rigidbody _rigidbody;

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

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force);
            StartCoroutine(ResetVelocity());
        }

        private IEnumerator ResetVelocity()
        {
            yield return new WaitForSeconds(ResetVelocityTime);
            _rigidbody.velocity = Vector3.zero;
        }
    }
}