using System;
using UnityEngine;
using Core.Interface.IInputs;

namespace Infrastructure.InputSystem
{
    internal class KeyboardInput : IInputInitialisation
    {
        public event Action OnPauseActivated;

        private float _xInput;
        private float _zInput;

        private readonly IUserInputProxy xAxis = new XAxisProxy();
        private readonly IUserInputProxy zAxis = new ZAxisProxy();
        private readonly IUserButtonInputProxy escKey = new PauseButtonProxy();

        public Vector3 GetInput()
        {
            _xInput = xAxis.GetAxis();
            _zInput = zAxis.GetAxis();
            return new Vector3(_xInput, 0, _zInput);
        }

        public void CheckPauseActivation()
        {
            if (escKey.GetButtonClick())
                OnPauseActivated?.Invoke();
        }
    }
}