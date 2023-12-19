using Core.Interface.IViews;
using System;
using UnityEngine;
using MVP.Views.CollectableViews;

namespace MVP.Views.PlayerViews
{
    internal sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private SpriteRenderer _healthBarSpriteRenderer;
        [SerializeField] private DetectorPlayerCollision _detectorPlayerCollision;
        [SerializeField] private DetectorCollectableCollision _detectorCollectableCollision;
        [SerializeField] private SphereCollider _sphereColliderOfPickingSpheres;
        [SerializeField] private Animator _playerAnimator;

        public Action<Collider> OnCollision;

        public GameObject PlayerObject
        {
            get => _playerObject;
            set => _playerObject = value;
        }
        public Transform PlayerTransform
        {
            get => _playerTransform;
            set => _playerTransform = value;
        }
        public Rigidbody PlayerRigidbody
        {
            get => _playerRigidbody;
            set => _playerRigidbody = value;
        }
        public SpriteRenderer HealthBarSpriteRenderer => _healthBarSpriteRenderer;
        public DetectorPlayerCollision DetectorPlayerCollision => _detectorPlayerCollision;
        public DetectorCollectableCollision DetectorCollactableCollision => _detectorCollectableCollision;
        public Animator PlayerAnimator
        {
            get => _playerAnimator;
            set => _playerAnimator = value;
        }
        

        internal void SetPlayerViewData(GameObject playerObject, Transform playerTransform, Rigidbody playerRigidbody)
        {
            _playerObject = playerObject;
            _playerTransform = playerTransform;
            _playerRigidbody = playerRigidbody;
        }

        private void OnTriggerStay(Collider other)
        {
            OnCollision?.Invoke(other);
        }

        public void SetValue(float radius)
        {
            _sphereColliderOfPickingSpheres.radius = radius;
        }
    }
}