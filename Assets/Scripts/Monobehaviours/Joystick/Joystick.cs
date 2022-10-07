using UnityEngine;
using UnityEngine.EventSystems;
using ProjectE.Managers;

namespace ProjectE.Input
{
    public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [Range(0f, 2f)]
        public float _handleLimit = 1f;

        private Vector2 _inputVector = Vector2.zero;
        private Vector2 _joystickPosition = Vector2.zero;

        public RectTransform _background;
        public RectTransform _handle;
        public RectTransform _touchableArea;

        public float Horizontal { get { return _inputVector.x; } }
        public float Vertical { get { return _inputVector.y; } }
        public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - _joystickPosition;
            _inputVector = (direction.magnitude > _background.sizeDelta.x / 2f) ? direction.normalized : direction / (_background.sizeDelta.x / 2f);
            _handle.anchoredPosition = (_inputVector * _background.sizeDelta.x / 2f) * _handleLimit;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_touchableArea, eventData.position, null, out _joystickPosition);
            //_joystickPosition = eventData.pointerCurrentRaycast.screenPosition;
            //_joystickPosition = RectTransformUtility.WorldToScreenPoint(null, eventData.position);
            //_joystickPosition -= new Vector2(Screen.width/2, Screen.height/2);
            SetBackGroundPosToPointerPos(_joystickPosition);
            Debug.Log(_joystickPosition);
            Debug.Log(eventData.position);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
        }

        private void SetBackGroundPosToPointerPos(Vector2 pos)
        {
            _background.anchoredPosition = pos;
        }
    }
}