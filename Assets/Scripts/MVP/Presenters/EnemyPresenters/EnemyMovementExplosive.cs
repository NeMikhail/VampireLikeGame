using UnityEngine;
using Configs;
using Core.Animations.Enemy;
using Infrastructure.TimeRemaningService;
using MVP.Models.EnemyModels;
using MVP.Views.EnemyViews;
using MVP.Views.PlayerViews;

namespace MVP.Presenters.EnemyPresenters
{
    internal sealed class EnemyMovementExplosive : EnemyMovement
    {
        private bool _isMovable = true;
        private float _sqrMinDistance;
        private ExploseView _explosive;
        private float _timer;
        private Rigidbody _rigidbody;
        private EnemyExplosiveView _enemyView;

        public EnemyMovementExplosive(PlayerView player, EnemyExplosiveView enemy, EnemyModel enemyModel,
            Rigidbody enemyRigidbody, EnemyAnimation enemyAnimation) : base(player, enemy, enemyModel, enemyRigidbody,
                enemyAnimation)
        {
            EnemyExplosionConfig config = enemy._enemyExplosionConfig;
            _sqrMinDistance = config.Distance * config.Distance;
            _explosive = enemy._explosive;
            _explosive.Damage = config.Damage;
            _explosive.Radius = config.ExplosionRadius;
            _timer = config.Timer;
            _rigidbody = enemyRigidbody;
            _enemyView = enemy;
        }


        public override void FixedExecute(float deltaTime)
        {
            if (_isMovable)
            {
                base.FixedExecute(deltaTime);
                float sqrDistance = (_player.transform.position - _enemy.transform.position).sqrMagnitude;

                if(sqrDistance < _sqrMinDistance)
                {
                    _isMovable = false;
                    _rigidbody.velocity = Vector3.zero;

                    UpdateAnimation(_rigidbody.velocity.magnitude);
                    _enemyView.ShowPreExplosionArea();
                    
                    new TimeRemaining(() => { if (_explosive != null) _explosive.Explode(); }, _timer, false).
                        AddTimeRemaining();
                }
            }
        }
    }
}