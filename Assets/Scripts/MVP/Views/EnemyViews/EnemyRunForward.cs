using UnityEngine;
using Core.Animations.Enemy;
using MVP.Models.EnemyModels;
using MVP.Presenters.EnemyPresenters;
using MVP.Views.PlayerViews;

namespace MVP.Views.EnemyViews
{
    internal sealed class EnemyRunForward : EnemyView
    {
        public override void Init(PlayerView player, EnemyModel enemyModel)
        {
            _enemyModel = enemyModel;
            if (_enemyAnimator != null)
            {
                var enemyAnimation = new EnemyAnimation(_enemyAnimator);
                _enemyMovement = new EnemyMovementInStraightLline(player, this, _enemyModel, _enemyRigidbody,
                    enemyAnimation);
            }
            else
            {
                _enemyMovement = new EnemyMovementInStraightLline(player, this, _enemyModel, _enemyRigidbody, null);
            }
            _enemyModel.OnDie += Dead;
        }

        public void SetDirection(Vector3 line)
        {
            ((EnemyMovementInStraightLline)_enemyMovement)._direction = line.normalized;
        }
    }
}