using System.Collections;
using SlimeScience.Configs;
using UnityEngine;

namespace SlimeScience.Characters
{
    [RequireComponent(typeof(Collider))]
    public class Ship : MonoBehaviour
    {
        [SerializeField] private ShipPopup _shipPopup;
        [SerializeField] private ParticleSystem[] _particlesShip;
        [SerializeField] private ParticleSystem _zoneParticle;
        [SerializeField] private Transform _childShip;
        [SerializeField] private float _timeToUsed;
        [SerializeField] private int _timeToRepeat;

        [Space] [SerializeField] private ShipConfig _config;

        private Player _player;

        private float _accumulatedTime;
        private bool _isEnabled;
        private bool _isCanUsed;
        private Coroutine _coroutine;

        private void Start()
        {
            SetParticlesState(false);
            _shipPopup.AdShowing += Used;
            _isCanUsed = true;
        }

        private void OnDestroy()
        {
            _shipPopup.AdShowing -= Used;
        }

        private void Update()
        {
            if (_isEnabled == false)
            {
                return;
            }

            _accumulatedTime += Time.deltaTime;

            if (_accumulatedTime >= _timeToUsed)
            {
                StartCoroutine(Timer());
                _accumulatedTime = 0f;
                _isEnabled = false;
                SetParticlesState(false);
                _player.LeaveShip();
                _childShip.transform.position = transform.position;
                _childShip.transform.rotation = transform.rotation;
                _childShip.SetParent(transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _isEnabled == false && _isCanUsed)
            {
                _player = player;
                _shipPopup.Show();
            }
        }

        private void Used()
        {
            _player.TranslateToShip(transform, _config);
            _childShip.SetParent(_player.transform);
            SetParticlesState(true);
            _isEnabled = true;
        }

        private void SetParticlesState(bool state)
        {
            if (state)
            {
                foreach (var particle in _particlesShip)
                {
                    particle.Play();
                }

                _zoneParticle.Stop();

                return;
            }

            foreach (var particle in _particlesShip)
            {
                particle.Stop();
            }

            _zoneParticle.Play();
        }

        private IEnumerator Timer()
        {
            _isCanUsed = false;

            yield return new WaitForSeconds(_timeToRepeat);

            _isCanUsed = true;
        }
    }
}