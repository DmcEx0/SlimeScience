using SlimeScience.Characters.Slimes;
using SlimeScience.InventorySystem;
using UnityEngine;

namespace SlimeScience.Equipment.Guns
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PullZoneRenderer : MonoBehaviour
    {
        private const int segments = 11;
        private const int TrianglesPerSegment = 3;
        private const float DegreesToRadians = Mathf.PI / 180f; // Коэффициент для перевода градусов в радианы

        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private float radius;
        private float angle;

        private Mesh mesh;
        private Inventory<Slime> _inventory;

        private void OnEnable()
        {
            if (_inventory != null)
            {
                _inventory.Filled += OnInventoryFilled;
            }
        }

        private void OnDisable()
        {
            if (_inventory != null)
            {
                _inventory.Filled -= OnInventoryFilled;
            }
        }

        public void Init(Inventory<Slime> inventory, float radius, float angle)
        {
            _inventory = inventory;
            _inventory.Filled += OnInventoryFilled;

            mesh = new Mesh();
            _meshFilter.mesh = mesh;
            UpdateConeSettings(radius, angle);
        }

        public void UpdateConeSettings(float newRadius, float newAngle)
        {
            radius = newRadius;
            angle = newAngle;
            UpdateConeMesh();
        }

        void UpdateConeMesh()
        {
            int additionalVertices = 2;

            Vector3[] vertices = new Vector3[segments + additionalVertices];
            int[] triangles = new int[segments * TrianglesPerSegment];

            vertices[0] = Vector3.zero;

            float angleStep = angle / segments;
            float startAngle = -angle / 2;

            for (int i = 1; i <= segments + 1; i++)
            {
                float currentAngle = Mathf.Deg2Rad * (startAngle + (angleStep * (i - 1)));
                float x = Mathf.Sin(currentAngle);
                float z = Mathf.Cos(currentAngle);
                float y = 0;

                vertices[i] = new Vector3(x, y, z) * radius;

                if (i > 1)
                {
                    int index = (i - 2) * 3;
                    triangles[index] = 0;
                    triangles[index + 1] = i - 1;
                    triangles[index + 2] = i;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }

        private void OnInventoryFilled()
        {
            _meshRenderer.enabled = false;
        }
    }
}