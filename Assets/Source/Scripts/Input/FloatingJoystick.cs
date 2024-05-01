using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace SlimeScience.Input
{
    public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform _handler;
        [SerializeField] private OnScreenStick _screenStick;
        [SerializeField] private Vector2 _knobOffset;

        public void Init()
        {
            _handler.anchoredPosition = _knobOffset;
            _screenStick.movementRange = _handler.rect.size.x / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            ExecuteEvents.dragHandler(_screenStick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _handler.position = eventData.position;
            ExecuteEvents.pointerDownHandler(_screenStick, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ExecuteEvents.pointerUpHandler(_screenStick, eventData);
            _handler.anchoredPosition = _knobOffset;
        }
    }
}