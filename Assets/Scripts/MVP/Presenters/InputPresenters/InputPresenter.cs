using Core.Interface.IInputs;
using Core.Interface.UnityLifecycle;
using UnityEngine;

namespace MVP.Presenters.InputPresenters
{
    internal class InputPresenter : IExecute
    {
        private IInputInitialisation _inputInitialisation;

        private Vector3 _inputVector;
        private float _deltaTime;
        public Vector3 InputVector { get => _inputVector; }
        public float DeltaTime { get => _deltaTime; }

        public InputPresenter(IInputInitialisation inputInitialisation)
        {
            _inputInitialisation = inputInitialisation;
        }

        public void Execute(float deltaTime)
        {
            _inputVector = _inputInitialisation.GetInput();
            _deltaTime = deltaTime;

            _inputInitialisation.CheckPauseActivation();
        }
    }
}