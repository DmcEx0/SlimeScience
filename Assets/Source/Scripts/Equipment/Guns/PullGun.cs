using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class PullGun : MonoBehaviour
    {
        [SerializeField] private LayerMask _slimeLayerMask;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _collider;
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

            _slimeCatcher.Caught += OnCatchSlime;
        }

        private void OnDisable()
        {
            _slimeCatcher.Caught -= OnCatchSlime;
        }

        public void Init(GameVariables gameVariables)
        {
            _slimeFinder = new SlimeFinder(_slimeLayerMask);
            _slimeCatcher = new SlimeCatcher();

            _inventory = new Inventory<Slime>(gameVariables.AbsorptionCapacity);
            _pullZoneRenderer.Init(
                _inventory,
                gameVariables.AbsorptionRadius,
                gameVariables.AbsorptionAngle);
            _gameVariables = gameVariables;
            _slimeCatcher.Caught += OnCatchSlime;

            _inventoryRenderer.Init(_inventory);

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

        private void OnCatchSlime(Slime slime)
        {
            _inventory.Add(slime);
        }
    }
}