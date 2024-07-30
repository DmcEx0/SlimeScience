using UnityEngine;
using SlimeScience.Configs;
using System.Collections;
using SlimeScience.Equipment.Guns;
using SlimeScience.Configs.Slimes;
using SlimeScience.Util;

namespace SlimeScience.Characters
{
    public class Slime : MobileObject, IPullable
    {
        private const float ResetVelocityTime = 0.1f;
        private const float ResetTeleportTime = 20f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _teleportEffect;
        
        private Coroutine _resetVelocityCoroutine;
        private Vector3 _originPos;
        private SlimeType _type;

        [field: SerializeField] public int Weight {  get; private set; }
        
        public float FearSpeed { get; private set; }
        public float BaseSpeed{ get; private set; }
        public bool CanTeleport { get; private set; }

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
            if (config is not SlimeConfig slimeConfig)
                return;

            SetRigidbodySetting(_rigidbody);

            BaseSpeed = slimeConfig.BaseSpeed;
            FearSpeed = slimeConfig.FearSpeed;

            _type = slimeConfig.Type;
            Weight = slimeConfig.Weight;
            CanTeleport = _type == SlimeType.Teleport;
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

        public void SetOriginPosition(Vector3 position)
        {
            _originPos = position;
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
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            _rigidbody.velocity = Vector3.zero;
            
            Enable();
        }

        public void Teleport()
        {
            if (!CanTeleport)
                return;

            CanTeleport = false;

            SetPosition(_originPos);
            _teleportEffect.Play();
            SoundsManager.PlayTeleport();

            StartCoroutine(ResetTeleportCoroutine());
        }

        private IEnumerator ResetTeleportCoroutine()
        {
            yield return new WaitForSeconds(ResetTeleportTime);

            CanTeleport = true;
        }

        private IEnumerator ResetVelocityCoroutine()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            ResetVelocity();
        }
    }
}