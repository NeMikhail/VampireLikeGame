using UnityEngine;
using Configs;
using MVP.Models.EnemyModels;
using MVP.Presenters.EnemyPresenters;
using MVP.Views.PlayerViews;

namespace MVP.Views.EnemyViews
{
    internal sealed class EnemyShooterView : EnemyView
    {
        [SerializeField] private EnemyBulletConfig _enemyBulletConfig;
        [SerializeField] private AnimatorEventsView _animatorEventsView;
        [SerializeField] private Transform _handForBullet;
        private EnemyShootPresenter _enemyShootPresenter;


        public override void Init(PlayerView player, EnemyModel enemyModel)
        {
            base.Init(player, enemyModel);
            _enemyShootPresenter = new EnemyShootPresenter(player, this, _enemyBulletConfig, EnemyAnimator, _handForBullet);
            _enemyShootPresenter.CreateTimerInterval();
            _animatorEventsView.OnShoot += _enemyShootPresenter.Shoot;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _enemyShootPresenter.RemoveTimerInterval();
            _animatorEventsView.OnShoot -= _enemyShootPresenter.Shoot;
        }
    }
}