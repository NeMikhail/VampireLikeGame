using Core.Animations.Player;
using Core.Interface.IModels;
using Core.Interface.UnityLifecycle;
using MVP.Presenters.InputPresenters;
using MVP.Views.PlayerViews;
using UnityEngine;

namespace MVP.Presenters.PlayerPresenters
{
    internal sealed class MovementPresenter : IFixedExecute
    {
        private readonly InputPresenter _inputPresenter;
        private readonly Rigidbody _playerRigidbody;
        private readonly IPlayerModel _playerModel;
        private readonly PlayerAnimation _playerAnimation;
        private readonly Transform _playerViewTransform;
        private Vector3 _movementDirection;

        internal MovementPresenter(PlayerView playerView, IPlayerModel playerModel, InputPresenter inputPresenter,
            PlayerAnimation playerAnimation)
        {
            _inputPresenter = inputPresenter;
            _playerRigidbody = playerView.PlayerRigidbody;
            _playerModel = playerModel;
            _playerAnimation = playerAnimation;
            _playerViewTransform = playerView.PlayerObject.transform;
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            Move();
            _playerAnimation.Move(_playerRigidbody.velocity.magnitude);
            RotatePlayer(_movementDirection);
        }

        private void Move()
        {
            _movementDirection
                = new Vector3(_inputPresenter.InputVector.x, 0, _inputPresenter.InputVector.z);

            if (_movementDirection.sqrMagnitude > 1.0f)
                _movementDirection.Normalize();

            _playerRigidbody.velocity = _movementDirection * _playerModel.PlayerSpeed;
        }

        private void RotatePlayer(Vector3 movementDirection)
        {
            Vector3 rotationTarget = _playerViewTransform.position + movementDirection;
            _playerViewTransform.LookAt(rotationTarget);
        }
    }
}