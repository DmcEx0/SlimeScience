using System;
using UnityEngine;

namespace SlimeScience.Characters
{
    [RequireComponent(typeof(Collider))]
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Transform _childShip;
        [SerializeField] private float _time;

        [Space]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        private Player _player;

        private float _accumulatedTime;
        private bool _isEnabled;

        private void Update()
        {
            if (_isEnabled == false)
            {
                return;
            }

            _accumulatedTime += Time.deltaTime;

            if (_accumulatedTime >= _time)
            {
                _accumulatedTime = 0f;
                _isEnabled = false;
                _player.LeaveShip();
                _childShip.transform.position = transform.position;
                _childShip.transform.rotation = transform.rotation;
                _childShip.SetParent(transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _isEnabled == false)
            {
                _player = player;
                _player.TranslateToShip(transform, _moveSpeed, _rotateSpeed);

                _childShip.SetParent(player.transform);
                
                _isEnabled = true;
            }
        }
    }
}