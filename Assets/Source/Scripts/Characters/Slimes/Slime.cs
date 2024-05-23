using UnityEngine;
using SlimeScience.Configs;
using System.Collections;

namespace SlimeScience.Characters.Slimes
{
    public class Slime : MobileObject
    {
        private const float ResetVelocityTime = 1f;

        [SerializeField] private Rigidbody _rigidbody;
        
        private Coroutine _resetVelocityCoroutine;
        
        public float FearSpeed { get; private set; }
        public float BaseSpeed{ get; private set; }

        private void OnDisable()
        {
            if (_resetVelocityCoroutine != null)
            {
                StopCoroutine(_resetVelocityCoroutine);
            }
        }

        private void Update()
        {
            UpdateStateMachine();
        }

        protected override void Init(MobileObjectConfig config)
        {
            if (config is not SlimeConfig)
                return;

            SetRigidbodySetting(_rigidbody);

            SlimeConfig slimeConfig = config as SlimeConfig;

            BaseSpeed = slimeConfig.BaseSpeed;
            FearSpeed = slimeConfig.FearSpeed;
        }

        protected override void SetRigidbodySetting(Rigidbody rigidbody)
        {
            base.SetRigidbodySetting(rigidbody);

            rigidbody.isKinematic = false;
        }

        public void AddForce(Vector3 force)
        {
            Disable();
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            _rigidbody.AddForce(force);

            if (_resetVelocityCoroutine != null)
            {
                StopCoroutine(_resetVelocityCoroutine);
            }

            _resetVelocityCoroutine = StartCoroutine(ResetVelocity());
        }

        private IEnumerator ResetVelocity()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            _rigidbody.velocity = Vector3.zero;

            Enable();
            _rigidbody.interpolation = RigidbodyInterpolation.None;
        }
    }
}