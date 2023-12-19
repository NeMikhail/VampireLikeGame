using UnityEngine;

public class JoystickView : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    public float Horizontal { get; set; }
    public float Vertical { get; set; }


    void Update()
    {
        Horizontal = _joystick.Horizontal;
        Vertical = _joystick.Vertical;
    }
}