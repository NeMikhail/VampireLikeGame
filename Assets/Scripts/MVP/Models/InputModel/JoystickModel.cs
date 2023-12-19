using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Core.Interface.IInputs;

namespace Infrastructure.InputSystem
{
    internal sealed class JoystickModel : IInputInitialisation
    {
        public event Action OnPauseActivated;

        private float _xInput;
        private float _zInput;
        private JoystickView _input;
        private readonly IUserButtonInputProxy _escKey = new PauseButtonProxy();

        public JoystickModel()
        {
            _input = Object.Instantiate(Resources.Load("UI/Joystick") as GameObject).GetComponent<JoystickView>();
        }


        public Vector3 GetInput()
        {
            _xInput = _input.Horizontal;
            _zInput = _input.Vertical;

            return new Vector3(_xInput, 0, _zInput);
        }

        public void CheckPauseActivation()
        {
            if (_escKey.GetButtonClick())
                OnPauseActivated?.Invoke();
        }
    }
}