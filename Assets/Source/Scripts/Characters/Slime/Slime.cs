using DG.Tweening;
using SlimeScience.Configs;
using SlimeScience.Configs.Slimes;
using SlimeScience.Equipment.Guns;
using SlimeScience.Util;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Characters
{
    public class Slime : MobileObject, IPullable
    {
        private const float MinScale = 1.5f;
        private const float ScaleTime = 0.5f;
        private const float ResetVelocityTime = 0.1f;
        private const float ResetTeleportTime = 20f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _teleportEffect;
        [SerializeField] private ParticleSystem _readyTeleport;

        private Coroutine _resetVelocityCoroutine;
        private Vector3 _originPos;
        private SlimeType _type;
        private int _originWeight;

        private Vector3 _originScale;

        private Tweener _scaler;

        public int Weight { get; private set; }

        public float FearSpeed { get; private set; }
        public float BaseSpeed { get; private set; }
        public bool CanTeleport { get; private set; }

        public bool IsBoss => _type == SlimeType.Boss;

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
            _originWeight = Weight;

            CanTeleport = _type == SlimeType.Teleport;

            if (CanTeleport)
            {
                _readyTeleport.Play();
            }
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

        public void Pull(int size)
        {
            if (size > Weight)
            {
                return;
            }

            Weight -= size;
            float weightFactor = Weight / _originWeight;

            Vector3 scale = _originScale * (Weight / (float)_originWeight);

            scale.x = Mathf.Max(MinScale, scale.x);
            scale.y = Mathf.Max(MinScale, scale.y);
            scale.z = Mathf.Max(MinScale, scale.z);

            if (_scaler != null)
            {
                _scaler.Kill();
            }

            _scaler = transform.DOScale(scale, ScaleTime);
        }

        public void SetOriginalScale(Vector3 scale)
        {
            _originScale = scale;
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

            _readyTeleport.Stop();
            SetPosition(_originPos);
            _teleportEffect.Play();
            SoundsManager.PlayTeleport();

            StartCoroutine(ResetTeleportCoroutine());
        }

        private IEnumerator ResetTeleportCoroutine()
        {
            yield return new WaitForSeconds(ResetTeleportTime);

            CanTeleport = true;

            _readyTeleport.Play();
        }

        private IEnumerator ResetVelocityCoroutine()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            ResetVelocity();
        }
    }
}