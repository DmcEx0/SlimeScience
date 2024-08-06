using SlimeScience.Characters;
using SlimeScience.Effects;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using SlimeScience.Traps;
using SlimeScience.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PullGun : MonoBehaviour
    {
        private const float SpreadDistance = 3f;

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

        public bool InventoryIsFull => _inventory?.IsFull ?? false;
        public int AvailableSpace => _inventory.AvailableSpace;

        public int SlimesAmount => _inventory.Amount;

        private void OnEnable()
        {
            if (_isInitialized == false)
            {
                return;
            }

            _inventoryRenderer.gameObject.SetActive(true);

            if (_gameVariables != null)
            {
                _gameVariables.CapacityUpgraded += OnCapacityUpgraded;
            }

            if (_effectSystem != null)
            {
                _effectSystem.EffectApplied += OnEffectChanged;
                _effectSystem.EffectEnded += OnEffectChanged;
            }

            if (_slimeCatcher != null)
            {
                _slimeCatcher.Caught += OnCatch;
                _slimeCatcher.Pulled += OnPulled;
            }

            UpdateInventory();
        }

        private void OnDisable()
        {
            if (_inventoryRenderer != null)
            {
                _inventoryRenderer.gameObject.SetActive(false);
            }

            if (_gameVariables != null)
            {
                _gameVariables.CapacityUpgraded -= OnCapacityUpgraded;
            }

            if (_effectSystem != null)
            {
                _effectSystem.Dispose();
                _effectSystem.EffectApplied -= OnEffectChanged;
                _effectSystem.EffectEnded -= OnEffectChanged;
            }

            if (_slimeCatcher != null)
            {
                _slimeCatcher.Caught -= OnCatch;
                _slimeCatcher.Pulled -= OnPulled;
            }
        }

        private void FixedUpdate()
        {
            if (_isInitialized == false || _inventory.IsFull)
            {
                _effectRenderer?.StopVacuumEffect();
                return;
            }

            var pullables = _slimeFinder.GetPullables(
                transform,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            if (_effectRenderer != null)
            {
                if (pullables.Count > 0)
                {
                    _effectRenderer.PlayVacuumEffect();
                }
                else
                {
                    _effectRenderer.StopVacuumEffect();
                }
            }

            foreach (var pullable in pullables)
            {
                _slimeCatcher.Absorb(
                    pullable,
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
                if (slime.IsBoss && slime.Weight > AvailableSpace)
                {
                    _slimeCatcher.Pull(slime, AvailableSpace);
                    return;
                }

                if (slime.CanTeleport)
                {
                    slime.Teleport();
                    return;
                }

                if (slime.isActiveAndEnabled && HasEnoughSpace(slime.Weight))
                {
                    _slimeCatcher.Catch(slime, transform.position);
                    _effectRenderer.PlayCatchSlimeEffect();
                }
            }

            if (collision.gameObject.TryGetComponent(out Bomb bomb))
            {
                bomb.Explode();
                _effectRenderer.PlayExplodeEffect();
                SoundsManager.PlayExplode();
                FreeSlimes();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Trap trap))
            {
                trap.CallActivatedEvent();
                _effectSystem.ApplyEffect(
                    trap.Config.Modifier,
                    trap.Config.ModifierPercent,
                    trap.Config.DurationInSeconds);

                trap.gameObject.SetActive(false);

                SoundsManager.PlayTrap();
            }
        }

        public void Init(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;
            _effectSystem = new EffectSystem(this, gameVariables);
            _effectRenderer.Init(_effectSystem);
            _effectRenderer.StopVacuumEffect();

            _slimeFinder = new Finder(_slimeLayerMask);
            _slimeCatcher = new Catcher();

            _inventory = new Inventory<Slime>(gameVariables.AbsorptionCapacity);

            _pullZoneRenderer.Init(
                _gameVariables,
                _inventory,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            _slimeCatcher.Caught += OnCatch;
            _slimeCatcher.Pulled += OnPulled;

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
            _effectRenderer?.Init(_effectSystem);

            _slimeFinder = new Finder(_slimeLayerMask);
            _slimeCatcher = new Catcher();

            _inventory = new Inventory<Slime>(inventoryCapacity);

            _slimeCatcher.Caught += OnCatch;
            _slimeCatcher.Pulled += OnPulled;

            _gameVariables.CapacityUpgraded += OnCapacityUpgraded;

            _isInitialized = true;
        }
        
        public int GetSlimeWeightInInventory()
        {
            var slime = _inventory.GetTypeInInventory;
            if(slime == null || slime.IsBoss)
            {
                return 1;
            }

            return slime.Weight;
        }

        public List<Slime> ReleaseInventory()
        {
            List<Slime> currentSlimesInInventory = _inventory.Free();

            return currentSlimesInInventory;
        }

        public Slime ReleaseSingleSlime()
        {
            return _inventory.GetItem(0);
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

            for (int i = 0; i < slime.Weight; i++)
            {
                _inventory.Add(slime);
            }

            Catched?.Invoke();

            SoundsManager.PlaySlimeCatch();
        }

        private void OnPulled(IPullable pullable)
        {
            _inventory.Fill(AvailableSpace);
            Catched?.Invoke();
            SoundsManager.PlaySlimeCatch();
        }

        private void OnCapacityUpgraded(float newCapacity)
        {
            _inventory.Expand((int)newCapacity - _inventory.MaxItems);
            RenderInventory();
        }

        private void UpdateInventory()
        {
            if (_gameVariables.AbsorptionCapacity != _inventory.MaxItems)
            {
                _inventory.Expand((int)_gameVariables.AbsorptionCapacity - _inventory.MaxItems);
            }

            RenderInventory();
        }

        private void OnEffectChanged()
        {
            _pullZoneRenderer.UpdateConeSettings();
            _effectRenderer.RenderStatuses();
        }

        private void FreeSlimes()
        {
            for (int i = _inventory.Amount - 1; i >= 0; i--)
            {
                var randomDirectionX = Random.Range(-1f, 1f);
                var randomDirectionZ = Random.Range(-1f, 1f);
                var randomOffset = new Vector3(randomDirectionX, 0, randomDirectionZ);
                var newPos = transform.position - randomOffset * SpreadDistance;

                Slime slime = _inventory.GetItem(i);
                slime.SetActive(true);
                slime.ResetVelocity();
                slime.SetPosition(newPos);
            }
        }

        private bool HasEnoughSpace(int weight) => _inventory.Amount + weight <= _inventory.MaxItems;
    }
}