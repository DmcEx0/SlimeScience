using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using SlimeScience.Saves;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PullZoneRenderer : MonoBehaviour
    {
        private const int Segments = 11;
        private const int TrianglesPerSegment = 3;

        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private float _radius;
        private float _angle;

        private Mesh _mesh;
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

            _mesh = new Mesh();
            _meshFilter.mesh = _mesh;
            UpdateConeSettings(radius, angle);
        }

        public void UpdateConeSettings(float newRadius, float newAngle)
        {
            _radius = newRadius;
            _angle = newAngle;
            UpdateConeMesh();
        }

        void UpdateConeMesh()
        {
            int additionalVertices = 2;

            Vector3[] vertices = new Vector3[Segments + additionalVertices];
            int[] triangles = new int[Segments * TrianglesPerSegment];

            vertices[0] = Vector3.zero;

            float angleStep = _angle / Segments;
            float startAngle = -_angle / 2;

            for (int i = 1; i <= Segments + 1; i++)
            {
                float currentAngle = Mathf.Deg2Rad * (startAngle + (angleStep * (i - 1)));
                float x = Mathf.Sin(currentAngle);
                float z = Mathf.Cos(currentAngle);
                float y = 0;

                vertices[i] = new Vector3(x, y, z) * _radius;

                if (i > 1)
                {
                    int index = (i - 2) * 3;
                    triangles[index] = 0;
                    triangles[index + 1] = i - 1;
                    triangles[index + 2] = i;
                }
            }

            _mesh.vertices = vertices;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
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