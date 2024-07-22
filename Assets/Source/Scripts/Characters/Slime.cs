using UnityEngine;
using SlimeScience.Configs;
using System.Collections;
using SlimeScience.Equipment.Guns;

namespace SlimeScience.Characters
{
    public class Slime : MobileObject, IPullable
    {
        private const float ResetVelocityTime = 0.1f;

        [SerializeField] private Rigidbody _rigidbody;
        
        private Coroutine _resetVelocityCoroutine;
        
        public float FearSpeed { get; private set; }
        public float BaseSpeed{ get; private set; }

        public Vector3 Position => transform.position;

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
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            Disable();

            _rigidbody.AddForce(force);

            if (_resetVelocityCoroutine != null)
            {
                StopCoroutine(_resetVelocityCoroutine);
            }

            _resetVelocityCoroutine = StartCoroutine(ResetVelocityCoroutine());
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void ResetVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            
            Enable();
        }

        private IEnumerator ResetVelocityCoroutine()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            ResetVelocity();
        }
    }
}