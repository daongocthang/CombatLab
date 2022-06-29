using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameInput : CoreComponent
{
    [SerializeField] private Button attackButton;
    private JoystickHandler _joystick;
    private Vector3 _vector3;


    private void Start()
    {
        _joystick = GameObject.FindWithTag("Joystick").GetComponent<JoystickHandler>();
        _vector3 = Vector3.zero;
    }

    public Vector3 GetAxis()
    {
        _vector3.x = _joystick.InputHorizontal;
        _vector3.z = _joystick.InputVertical;
        return _vector3;
    }

    public void OnAttack(UnityAction action)
    {
        attackButton.onClick.AddListener(action);
    }
}