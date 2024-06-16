using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
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

        private GameVariables _gameVariables;
        private SlimeFinder _slimeFinder;
        private SlimeCatcher _slimeCatcher;
        private Inventory<Slime> _inventory;

        private bool _isInitialized = false;

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

            _slimeCatcher.Caught += OnCatchSlime;
        }

        private void OnDisable()
        {
            if (_gameVariables != null)
            {
                _gameVariables.CapacityUpgraded -= OnCapacityUpgraded;
            }

            _slimeCatcher.Caught -= OnCatchSlime;
        }

        public void Init(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;

            _slimeFinder = new SlimeFinder(_slimeLayerMask);
            _slimeCatcher = new SlimeCatcher();

            _inventory = new Inventory<Slime>(gameVariables.AbsorptionCapacity);

            _pullZoneRenderer.Init(
                _gameVariables,
                _inventory,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            _slimeCatcher.Caught += OnCatchSlime;

            _inventoryRenderer.Init(_inventory);

            _gameVariables.CapacityUpgraded += OnCapacityUpgraded;

            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (_isInitialized == false || _inventory.IsFull)
            {
                return;
            }

            var slimes = _slimeFinder.GetSlimes(
                transform,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);

            foreach (var slime in slimes)
            {
                _slimeCatcher.AbsorbSlime(
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
                _slimeCatcher.CatchSlime(slime, transform.position);
            }
        }

        public List<Slime> ReleaseInventory(Vector3 positionToRelease)
        {
            List<Slime> currentSlimesInInventory = _inventory.Free();

            //foreach (var slime in currentSlimesInInventory)
            //{
            //    slime.transform.Translate(positionToRelease * 4f * 0.02f);
            //}

            return currentSlimesInInventory;
        }

        public void RenderInventory()
        {
            _inventoryRenderer.Render();
        }

        private void OnCatchSlime(Slime slime)
        {
            slime.Disable();
            _inventory.Add(slime);
        }

        private void OnCapacityUpgraded(float newCapacity)
        {
            _inventory.Expand((int)newCapacity - _inventory.MaxItems);
            RenderInventory();
        }
    }
}