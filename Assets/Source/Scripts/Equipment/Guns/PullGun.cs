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

            _gameVariables = gameVariables;
            _slimeCatcher.Caught += OnCatchSlime;

            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized == false || _inventory.IsFull)
            {
                return;
            }

            var slimes = _slimeFinder.GetSlimes(
                transform,
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionAngle);  //FixedUpdate?

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

        public void ReleaseInventory(Vector3 positionToRelease)
        {
            foreach (var slime in _inventory.Free())
            {
                slime.gameObject.SetActive(true);

                slime.transform.Translate(positionToRelease * 4f * 0.02f);
            }
        }

        private void OnCatchSlime(Slime slime)
        {
            _inventory.Add(slime);
        }
    }
}