using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeScience.Characters.Ship
{
    public class ShipUseButtonsManager : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _hideTransform;
        [SerializeField] private Button _used;
        [SerializeField] private Button _unused;

        private Vector3 _defaultPos;
        
        public event Action Used;
        public event Action Unused;

        private void Start()
        {
            _used.onClick.AddListener(UseShip);
            _unused.onClick.AddListener(UnusedShip);
        }

        private void OnDestroy()
        {
            _used.onClick.RemoveListener(UseShip);
            _unused.onClick.RemoveListener(UnusedShip);
        }

        public void ShowUsedButton()
        {
            ShowButton(_used);
        }
        
        public void ShowUnusedButton()
        {
            ShowButton(_unused);
        }

        public void HideUsedButton()
        {
            HideButton(_used);
        }
        
        private void HideUnusedButton()
        {
            HideButton(_unused);
        }

        private void ShowButton(Button button)
        {
            button.transform.DOMove(_parent.position, 0.5f);
        }

        private void HideButton(Button button)
        {
            button.transform.DOMove(_hideTransform.position, 0.5f);
        }

        private void UseShip()
        {
            HideUsedButton();
            Used?.Invoke();
        }

        private void UnusedShip()
        {
            HideUnusedButton();
            Unused?.Invoke();
        }
    }
}