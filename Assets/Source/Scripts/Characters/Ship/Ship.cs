using System.Collections;
using System.Collections.Generic;
using SlimeScience.Configs;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Characters.Ship
{
    [RequireComponent(typeof(Collider))]
    public class Ship : MonoBehaviour, ISeekable
    {
        [SerializeField] private ShipUseButtonsManager _buttonsManager;
        [SerializeField] private Collider _collider;
        [SerializeField] private SlimeInventoryRenderer _inventoryRenderer;
        [SerializeField] private ParticleSystem[] _particlesShip;
        [SerializeField] private ParticleSystem _zoneParticle;
        [SerializeField] private int _timeToRepeat;

        [Space] [SerializeField] private ShipConfig _config;

        private GameVariables _gameVariables;
        private Inventory<Slime> _inventory;
        private Player _player;

        private bool _isEnabled;
        private Coroutine _coroutine;

        private void OnDestroy()
        {
            _buttonsManager.Used -= Used;
            _buttonsManager.Unused -= Reset;

            if (_gameVariables != null)
            {
                _gameVariables.UpgradedShipCapacity -= OnCapacityUpgraded;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _isEnabled == false)
            {
                if (_player == null)
                {
                    _player = player;
                }

                if (_inventory.IsFull == false)
                {
                    int neededAmount = 0;

                    if (_player.PullGun.GetSlimeWeightInInventory != 0)
                    {
                        neededAmount = _inventory.AvailableSpace / _player.PullGun.GetSlimeWeightInInventory;
                        neededAmount *= _player.PullGun.GetSlimeWeightInInventory;
                    }

                    for (int i = 0; i < neededAmount; i++)
                    {
                        if (_player.PullGun.SlimesAmount == 0)
                        {
                            _player.PullGun.RenderInventory();
                            break;
                        }

                        _inventory.Add(_player.PullGun.ReleaseSingleSlime());
                    }
                }

                _buttonsManager.ShowUsedButton();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player) && _isEnabled == false)
            {
                _buttonsManager.HideUsedButton();
            }
        }

        public void Init(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _inventory = new Inventory<Slime>(_gameVariables.ShipCapacity);

            _buttonsManager.Used += Used;
            _buttonsManager.Unused += Reset;

            SetParticlesState(false);
            _inventoryRenderer.Init(_inventory);

            _gameVariables.UpgradedShipCapacity += OnCapacityUpgraded;
        }

        public List<Slime> ReleaseSlimes()
        {
            List<Slime> releasedSlimes = _inventory.Free();
            _inventoryRenderer.Render();

            return releasedSlimes;
        }

        private void Used()
        {
            _player.TranslateToShip(transform, _config);
            _player.PullGun.gameObject.SetActive(false);
            transform.SetParent(_player.transform);
            SetParticlesState(true);
            _isEnabled = true;
            _collider.isTrigger = false;
            _buttonsManager.ShowUnusedButton();
        }

        private void Reset()
        {
            _player.PullGun.gameObject.SetActive(true);
            StartCoroutine(Timer());
            _zoneParticle.Stop();
            _isEnabled = false;
            SetParticlesState(false);
            _player.LeaveShip();
            transform.SetParent(null);

            _collider.isTrigger = true;
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
            yield return new WaitForSeconds(_timeToRepeat);

            _zoneParticle.Play();
        }

        private void OnCapacityUpgraded(float newCapacity)
        {
            _inventory.Expand((int)newCapacity - _inventory.MaxItems);
            _inventoryRenderer.Render();
        }
    }
}