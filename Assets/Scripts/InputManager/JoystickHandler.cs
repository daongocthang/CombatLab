using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _joystick;
    private Image _joystickImage;
    private Vector2 _inputPoint;

    private const float DefaultAlpha = 0.1f;
    private const float TouchedAlpha = 1.0f;

    private void Awake()
    {
        _joystick = GetComponent<Image>();
        _joystickImage = transform.GetChild(0).GetComponent<Image>();

        ImageUtils.SetOpacity(_joystick, DefaultAlpha);
        ImageUtils.SetOpacity(_joystickImage, DefaultAlpha);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick.rectTransform, eventData.position,
            eventData.pressEventCamera, out _inputPoint))
        {
            var sizeDelta = _joystick.rectTransform.sizeDelta;
            _inputPoint.x /= sizeDelta.x;
            _inputPoint.y /= sizeDelta.y;

            // Normalized
            if (_inputPoint.magnitude > 1.0f)
                _inputPoint = _inputPoint.normalized;

            _joystickImage.rectTransform.anchoredPosition = new Vector2(
                _inputPoint.x * sizeDelta.x / 2, _inputPoint.y * sizeDelta.y / 2);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        ImageUtils.SetOpacity(_joystick, TouchedAlpha);
        ImageUtils.SetOpacity(_joystickImage, TouchedAlpha);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputPoint = Vector2.zero;
        _joystickImage.rectTransform.anchoredPosition = Vector2.zero;
        ImageUtils.SetOpacity(_joystick, DefaultAlpha);
        ImageUtils.SetOpacity(_joystickImage, DefaultAlpha);
    }

    public float InputHorizontal =>
        Mathf.Abs(_inputPoint.x) > 0.1f ? _inputPoint.x : Input.GetAxis("Horizontal");

    public float InputVertical =>
        Mathf.Abs(_inputPoint.y) > 0.1f ? _inputPoint.y : Input.GetAxis("Vertical");
}