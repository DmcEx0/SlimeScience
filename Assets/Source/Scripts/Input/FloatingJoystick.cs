using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UIElements;

namespace SlimeScience
{
    public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _handler;
        [SerializeField] private RectTransform _knob;
        [SerializeField] private OnScreenStick _onScreenStick;

        private Vector2 _startPosition;

        public void Init()
        {
            _onScreenStick.movementRange = _handler.rect.width / 2;
            _handler.gameObject.SetActive(false);

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            Vector2 position = eventData.position;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_handler, position, null, out Vector2 localPoint))
            {
                _handler.gameObject.SetActive(true);
                _handler.position = _handler.TransformPoint(localPoint);
                _knob.position = _handler.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp");
            _knob.position = _handler.position;
            // _handler.gameObject.SetActive(false);
        }
    }
}
