using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PullZoneRenderer : MonoBehaviour
    {
        private const float ScaleCoof = 0.2f;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _material;

        private float _radius;
        private float _angle;

        private Inventory<Slime> _inventory;
        private GameVariables _gameVariables;

        private void OnEnable()
        {
            if (_inventory != null)
            {
                _inventory.Filled += OnInventoryFilled;
                _inventory.Released += OnInventoryChanged;
                _inventory.Expanded += OnInventoryChanged;
            }

            if (_gameVariables != null)
            {
                _gameVariables.RadiusUpgraded += OnRadiusUpgraded;
                _gameVariables.AngleUpgraded += OnAngleUpgraded;
            }
        }

        private void OnDisable()
        {
            if (_inventory != null)
            {
                _inventory.Filled -= OnInventoryFilled;
                _inventory.Released -= OnInventoryChanged;
                _inventory.Expanded -= OnInventoryChanged;
            }

            if (_gameVariables != null)
            {
                _gameVariables.RadiusUpgraded -= OnRadiusUpgraded;
                _gameVariables.AngleUpgraded -= OnAngleUpgraded;
            }
        }

        public void Init(
            GameVariables gameVariables,
            Inventory<Slime> inventory,
            float radius,
            float angle)
        {
            _radius = radius;
            _angle = angle;

            _inventory = inventory;
            _inventory.Filled += OnInventoryFilled;
            _inventory.Released += OnInventoryChanged;
            _inventory.Expanded += OnInventoryChanged;

            _gameVariables = gameVariables;
            _gameVariables.RadiusUpgraded += OnRadiusUpgraded;
            _gameVariables.AngleUpgraded += OnAngleUpgraded;

            UpdateConeSettings(radius, angle);
        }

        public void UpdateConeSettings()
        {
            _material.SetFloat("_Angle", _gameVariables.AbsorptionAngle);
            _meshRenderer.gameObject.transform.localScale = new Vector3(
                _gameVariables.AbsorptionRadius,
                _gameVariables.AbsorptionRadius, 
                _gameVariables.AbsorptionRadius) * ScaleCoof;
        }

        private void UpdateConeSettings(float newRadius, float newAngle)
        {
            _material.SetFloat("_Angle", newAngle);
            _meshRenderer.gameObject.transform.localScale = new Vector3(
                newRadius, 
                newRadius, 
                newRadius) * ScaleCoof;
        }

        private void OnInventoryFilled()
        {
            if (_meshRenderer.enabled == true)
            {
                _meshRenderer.enabled = false;
            }
        }

        private void OnInventoryChanged()
        {
            if (_meshRenderer.enabled == false)
            {
                _meshRenderer.enabled = true;
            }
        }

        private void OnRadiusUpgraded(float newRadius)
        {
            UpdateConeSettings(newRadius, _angle);
        }

        private void OnAngleUpgraded(float newAngle)
        {
            UpdateConeSettings(_radius, newAngle);
        }
    }
}