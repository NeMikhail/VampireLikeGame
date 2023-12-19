using Core.Interface.IFeatures;
using Core.Interface.IPresenters;
using Cysharp.Threading.Tasks;
using MVP.Views.PlayerViews;
using UnityEngine;

namespace MVP.Presenters.PlayerPresenters
{
    internal class PlayerCollisionHandler : IInitialisation, ICleanUp
    {
        private const int INVULNERABILITY_TIME = 500;
        private readonly PlayerView _playerView;
        private readonly HealthChangingPresenter _healthChangingPresenter;
        private Renderer _playerRenderer;
        private Color _playerDefaultColor;
        private bool isVulnerable = true;

        public PlayerCollisionHandler(PlayerView playerView, HealthChangingPresenter healthChangingPresenter)
        {
            _playerView = playerView;
            _healthChangingPresenter = healthChangingPresenter;
        }

        public void Initialisation()
        {
            _playerRenderer = _playerView.gameObject.GetComponentInChildren<Renderer>();
            _playerDefaultColor = _playerRenderer.material.color;
            _playerView.OnCollision += OnCollision;
        }

        private void OnCollision(Collider coll)
        {
            if (isVulnerable && coll.gameObject.TryGetComponent<IAttackable>(out IAttackable enemy))
            {
                _healthChangingPresenter.TakeDamage(enemy.Damage);
                Invulnerable().Forget();
            }
        }

        private async UniTask Invulnerable()
        {
            isVulnerable = false;
            await UniTask.Delay(INVULNERABILITY_TIME);
            isVulnerable = true;
            if (_playerRenderer != null)
            {
                _playerRenderer.material.color = _playerDefaultColor;
            }
        }

        public void Cleanup()
        {
            _playerView.OnCollision -= OnCollision;
        }
    }
}