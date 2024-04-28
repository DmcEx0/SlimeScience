using SlimeScience.Characters.Slimes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.InventorySystem
{
    public class SlimeInventoryRenderer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private Inventory<Slime> _slimes;
        private Camera _camera;
        private Coroutine _smoothUpdate;

        private void OnEnable()
        {
            if (_slimes != null)
            {
                _slimes.Changed += OnInventoryChanged;
            }
        }

        private void OnDisable()
        {
            if (_slimes != null)
            {
                _slimes.Changed -= OnInventoryChanged;
            }
        }

        private void LateUpdate()
        {
            if (_camera == null)
            {
                return;
            }

            transform.LookAt(_camera.transform);
        }

        public void Init(Inventory<Slime> inventory)
        {
            _camera = Camera.main;
            _slimes = inventory;
            _slimes.Changed += OnInventoryChanged;
            _slider.value = inventory.Amount;
            _slider.maxValue = inventory.MaxItems;

            _smoothUpdate = StartCoroutine(SmoothUpdateSliderCoroutine());
        }

        private void OnInventoryChanged()
        {
            if (_smoothUpdate != null)
            {
                StopCoroutine(_smoothUpdate);
            }

            _smoothUpdate = StartCoroutine(SmoothUpdateSliderCoroutine());
        }

        private IEnumerator SmoothUpdateSliderCoroutine()
        {
            var startValue = _slider.value;
            var endValue = _slimes.Amount;

            var elapsedTime = 0f;
            var duration = 0.5f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
                yield return null;
            }

            _slider.value = endValue;
        }
    }
}
