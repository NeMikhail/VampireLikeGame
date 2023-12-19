using System;
using UnityEngine;
using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;
using MVP.Views.PlayerViews;
using UI;
using Audio.Service;
using Audio;

namespace MVP.Presenters.PlayerPresenters
{
    internal sealed class HealthChangingPresenter : IInitialisation, ICleanUp, IExecute
    {
        private const float MIN_MAX_HEALTH = 1.0f;
        private const float REGENERATION_COOLDOWN = 1.0f;

        private readonly PlayerView _playerView;
        private readonly IPlayerModel _playerModel;

        private UiBattleScreen _battleScreen;
        private Transform _barHealthTransform;
        private SpriteRenderer _barHealthSpriteRenderer;

        private float _maxXScale;
        private float _maxHealth;
        private float _currentHealth;
        private float _currentRegenerationTime;

        private bool _hasHealthBar;

        public event Action<Transform, float, PopUpType> OnTakeHealth;

        public HealthChangingPresenter(PlayerView playerView, IPlayerModel playerModel, IViewProvider viewProvider)
        {
            _playerView = playerView;
            _playerModel = playerModel;
            _battleScreen = viewProvider.GetBattleScreen();
            _maxHealth = MIN_MAX_HEALTH;
        }


        public void Initialisation()
        {
            _currentHealth = _playerModel.PlayerHealth;
            _maxHealth = _playerModel.PlayerMaxHealth;
            SetSpriteRenderer(_playerView.HealthBarSpriteRenderer);
            _playerModel.HealthChanged += SetHealth;
            _playerModel.MaxHealthChanged += SetMaxHealth;
            _battleScreen.OnRespawnButtonClick += Respawn;
        }

        public void Execute(float deltaTime)
        {
            if (_playerModel.PlayerRegeneration > 0)
            {
                _currentRegenerationTime += deltaTime;
                if (_currentRegenerationTime >= REGENERATION_COOLDOWN)
                {
                    TakeHealth(_playerModel.PlayerRegeneration);
                    _currentRegenerationTime = 0;
                }
            }
        }

        private void SetMaxHealth()
        {
            _maxHealth = _playerModel.PlayerMaxHealth;
            if (_maxHealth < MIN_MAX_HEALTH)
            {
                _maxHealth = MIN_MAX_HEALTH;
            }

            UpdateBar();
        }

        public void Cleanup()
        {
            _playerModel.HealthChanged -= SetHealth;
            _playerModel.MaxHealthChanged -= SetMaxHealth;
            _battleScreen.OnRespawnButtonClick -= Respawn;
        }

        public void TakeDamage(float damage)
        {
            _playerModel.PlayerHealth -= damage * _playerModel.PlayerArmor;
            if (_playerModel.PlayerHealth <= 0)
            {
                Die();
                return;
            }
        }

        public void TakeHealth(float health)
        {
            if (_playerModel.PlayerHealth >= _playerModel.PlayerMaxHealth)
            {
                _playerModel.PlayerHealth = _playerModel.PlayerMaxHealth;
                return;
            }
            _playerModel.PlayerHealth += health;
        }

        private void Die()
        {
            _playerModel.IsDead = true;
        }

        private void Respawn()
        {
            _playerModel.PlayerHealth = _playerModel.PlayerMaxHealth;
            _playerModel.IsDead = false;
        }

        private void SetHealth()
        {
            float difference = _playerModel.PlayerHealth - _currentHealth;
            if (difference > 0)
                OnTakeHealth?.Invoke(_barHealthTransform, difference, PopUpType.PlayerHeal);
            else if (difference < 0)
            {
                AudioService.Instance.PlayAudioOneShot(AudioClipNames.Player_Damage);
                OnTakeHealth?.Invoke(_barHealthTransform, difference, PopUpType.PlayerDamage);
            }

            _currentHealth = _playerModel.PlayerHealth;
            UpdateBar();
        }

        private void UpdateBar()
        {
            if (_hasHealthBar)
            {
                float health = Mathf.Clamp(_currentHealth, 0.0f, _maxHealth);
                float scale = health / _maxHealth;
                Vector3 localScale = _barHealthTransform.localScale;
                localScale.x = scale * _maxXScale;
                _barHealthTransform.localScale = localScale;
            }
        }

        private void SetSpriteRenderer(SpriteRenderer healthBarSpriteRenderer)
        {
            _hasHealthBar = healthBarSpriteRenderer != null;
            if (_hasHealthBar)
            {
                _barHealthSpriteRenderer = healthBarSpriteRenderer;
                _barHealthTransform = _barHealthSpriteRenderer.transform;
                _maxXScale = _barHealthTransform.localScale.x;
                UpdateBar();
            }
            else
            {
                _barHealthTransform = null;
                _barHealthSpriteRenderer = null;
            }
        }
    }
}