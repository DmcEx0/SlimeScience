using System.Collections;
using SlimeScience.Characters.Slimes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.InventorySystem
{
    public class SlimeInventoryRenderer : MonoBehaviour
    {
        private const string AmountTextFormat = "{0}/{1}";

        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _amountText;

        private Inventory<Slime> _inventory;
        private Camera _camera;
        private Coroutine _smoothUpdate;

        private void OnEnable()
        {
            if (_inventory != null)
            {
                _inventory.Changed += OnInventoryChanged;
                _inventory.Expanded += OnInventoryExpanded;
            }
        }

        private void OnDisable()
        {
            if (_inventory != null)
            {
                _inventory.Changed -= OnInventoryChanged;
                _inventory.Expanded -= OnInventoryExpanded;
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
            _inventory = inventory;
            _inventory.Changed += OnInventoryChanged;
            _slider.value = inventory.Amount;
            _slider.maxValue = inventory.MaxItems;

            _inventory.Expanded += OnInventoryExpanded;

            Render();
        }

        public void Render()
        {
            if (_smoothUpdate != null)
            {
                StopCoroutine(_smoothUpdate);
            }

            if (_amountText != null)
            {
                _amountText.text = string.Format(AmountTextFormat, _inventory.Amount, _inventory.MaxItems);
            }

            _smoothUpdate = StartCoroutine(SmoothUpdateSliderCoroutine());
        }

        private void OnInventoryExpanded()
        {
            _slider.maxValue = _inventory.MaxItems;
            Render();
        }

        private void OnInventoryChanged()
        {
            Render();
        }

        private IEnumerator SmoothUpdateSliderCoroutine()
        {
            var startValue = _slider.value;
            var endValue = _inventory.Amount;

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
