using System.Collections;
using SlimeScience.Equipment.Guns;
using UnityEngine;

namespace SlimeScience.Traps
{
    public class Bomb : MonoBehaviour, IPullable
    {
        private const float ResetVelocityTime = 0.1f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _bomb;

        private Coroutine _resetVelocityCoroutine;

        public Vector3 Position => transform.position;

        public void Explode()
        {
            gameObject.SetActive(false);
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

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
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            _rigidbody.velocity = Vector3.zero;
        }

        private IEnumerator ResetVelocityCoroutine()
        {
            yield return new WaitForSeconds(ResetVelocityTime);

            ResetVelocity();
        }
    }
}
