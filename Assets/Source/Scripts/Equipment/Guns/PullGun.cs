using System;
using SlimeScience.Characters;
using SlimeScience.Effects;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using SlimeScience.Traps;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PullGun : MonoBehaviour
    {
        [SerializeField] private LayerMask _slimeLayerMask;
        [SerializeField] private PullZoneRenderer _pullZoneRenderer;
        [SerializeField] private SlimeInventoryRenderer _inventoryRenderer;
        [SerializeField] private EffectRenderer _effectRenderer;

        private GameVariables _gameVariables;
        private Finder _slimeFinder;
        private Catcher _slimeCatcher;
        private Inventory<Slime> _inventory;
        private EffectSystem _effectSystem;

        private bool _isInitialized = false;

        public event Action Catched;

        public bool InventoryIsFull => _inventory.IsFull;

        private void OnEnable()
        {
            if (_isInitialized == false)
            {
                return;
            }

            if (_gameVariables != null)
            {
                _gameVariables.CapacityUpgraded += OnCapacityUpgraded;
            }

            if (_effectSystem != null)
            {
                _effectSystem.EffectApplied += OnEffectChanged;
                _effectSystem.EffectEnded += OnEffectChanged;
            }

            _slimeCatcher.Caught += OnCatch;
        }

        private void OnDisable()
        {
            if (_gameVariables != null)
            {
                _gameVariables.CapacityUpgraded -= OnCapacityUpgraded;
            }

            if (_effectSystem != null)
            {
                _effectSystem.EffectApplied -= OnEffectChanged;
                _effectSystem.EffectEnded -= OnEffectChanged;
            }

            _slimeCatcher.Caught -= OnCatch;
        }
        
        private void FixedUpdate()
        {
            if (_isInitialized == false || _inventory.IsFull)
            {
                return;
            }

            var slimes = _slimeFinder.GetPullables(
                transform,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            foreach (var slime in slimes)
            {
                _slimeCatcher.Absorb(
                    slime,
                    transform.position,
                    _gameVariables.AbsorptionForce);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isInitialized == false || _inventory.IsFull)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out Slime slime))
            {
                _slimeCatcher.Catch(slime, transform.position);
            }

            if (collision.gameObject.TryGetComponent(out Bomb bomb))
            {
                bomb.Explode();
                _inventory.Reset();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Trap trap))
            {
                _effectSystem.ApplyEffect(
                    trap.Modifier,
                    trap.ModifierPercent,
                    trap.DurationInSeconds);

                trap.gameObject.SetActive(false);
            }
        }

        public void Init(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _effectSystem = new EffectSystem(this, gameVariables);
            _effectRenderer.Init(_effectSystem);

            _slimeFinder = new Finder(_slimeLayerMask);
            _slimeCatcher = new Catcher();

            _inventory = new Inventory<Slime>(gameVariables.AbsorptionCapacity);

            _pullZoneRenderer.Init(
                _gameVariables,
                _inventory,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            _slimeCatcher.Caught += OnCatch;

            _inventoryRenderer.Init(_inventory);

            _gameVariables.CapacityUpgraded += OnCapacityUpgraded;

            _effectSystem.EffectApplied += OnEffectChanged;
            _effectSystem.EffectEnded += OnEffectChanged;

            _isInitialized = true;
        }

        public void Init(GameVariables gameVariables, int inventoryCapacity)
        {
            _gameVariables = gameVariables;
            _effectSystem = new EffectSystem(this, gameVariables);
            _effectRenderer.Init(_effectSystem);

            _slimeFinder = new Finder(_slimeLayerMask);
            _slimeCatcher = new Catcher();

            _inventory = new Inventory<Slime>(inventoryCapacity);

            _slimeCatcher.Caught += OnCatch;
            _gameVariables.CapacityUpgraded += OnCapacityUpgraded;

            _isInitialized = true;
        }

        public List<Slime> ReleaseInventory()
        {
            List<Slime> currentSlimesInInventory = _inventory.Free();

            return currentSlimesInInventory;
        }

        public void RenderInventory()
        {
            if (_inventoryRenderer != null)
            {
                _inventoryRenderer.Render();
            }
        }

        private void OnCatch(IPullable pullable)
        {
            if (pullable is not Slime)
            {
                return;
            }

            Slime slime = pullable as Slime;

            slime.Disable();
            _inventory.Add(slime);
            Catched?.Invoke();
        }

        private void OnCapacityUpgraded(float newCapacity)
        {
            _inventory.Expand((int)newCapacity - _inventory.MaxItems);
            RenderInventory();
        }

        private void OnEffectChanged()
        {
           _pullZoneRenderer.UpdateConeSettings();
           _effectRenderer.Render();
        }
    }
}