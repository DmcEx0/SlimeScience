using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace SlimeScience
{
    public class FloatingJoystick1 : MonoBehaviour
    {
        [SerializeField] private RectTransform _handler;
        [SerializeField] private RectTransform _knob;
        [SerializeField] private OnScreenStick _onScreenStick;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += OnFingerDown;
            ETouch.Touch.onFingerUp += OnFingerUp;

            _onScreenStick.movementRange = _handler.rect.width / 2;
            _handler.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ETouch.Touch.onFingerDown -= OnFingerDown;
            ETouch.Touch.onFingerUp -= OnFingerUp;
            EnhancedTouchSupport.Disable();
        }

        private void OnFingerDown(Finger finger)
        {
            if (IsTouchOverUI(finger.screenPosition))
            {
                return;
            }

            Vector2 position = finger.screenPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_handler, position, null, out Vector2 localPoint))
            {
                _handler.gameObject.SetActive(true);
                _handler.position = _handler.TransformPoint(localPoint);
                _knob.position = _handler.position;
            }
        }

        private void OnFingerUp(Finger finger)
        {
            _knob.position = _handler.position;
            _handler.gameObject.SetActive(false);
        }

        private bool IsTouchOverUI(Vector2 screenPosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = screenPosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}
