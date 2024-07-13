using SlimeScience.Equipment.Guns;
using System.Collections;
using UnityEngine;

namespace SlimeScience.Traps
{
    public class Bomb : MonoBehaviour, IPullable
    {
        private const float ResetVelocityTime = 1f;

        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private AudioSource _explosionSound;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _bomb;

        private Coroutine _resetVelocityCoroutine;
        private Coroutine _turnOffCoroutine;

        public Vector3 Position => transform.position;

        private void OnEnable()
        {
            _explosionEffect.Stop();
            _explosionSound.Stop();
        }


        public void Explode()
        {
            _bomb.SetActive(false);
            _explosionEffect.Play();
            _explosionSound.Play();

            if (_turnOffCoroutine != null)
            {
                StopCoroutine(_turnOffCoroutine);
            }

            _turnOffCoroutine = StartCoroutine(TurnOff());
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            _rigidbody.AddForce(force);

            if (_resetVelocityCoroutine != null)
            {
                StopCoroutine(_resetVelocityCoroutine);
            }

            _resetVelocityCoroutine = StartCoroutine(ResetVelocity());
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private IEnumerator ResetVelocity()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            _rigidbody.velocity = Vector3.zero;

            _rigidbody.interpolation = RigidbodyInterpolation.None;
        }

        private IEnumerator TurnOff()
        {
            while (_explosionEffect.isPlaying)
            {
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
