using UnityEngine;
using Core.Animations.Enemy;
using MVP.Models.EnemyModels;
using MVP.Views.EnemyViews;
using MVP.Views.PlayerViews;

namespace MVP.Presenters.EnemyPresenters
{
    internal sealed class EnemyMovementInStraightLline : EnemyMovement
    {
        public Vector3 _direction
        {
            get; set;
        }

        public EnemyMovementInStraightLline(PlayerView player, EnemyView enemy, EnemyModel enemyModel,
            Rigidbody enemyRigidbody, EnemyAnimation enemyAnimation) : base(player, enemy, enemyModel, enemyRigidbody,
                enemyAnimation)
        {
            _direction = (_player.transform.position - _enemy.transform.position).normalized;
        }


        protected override Vector3 GetDirection()
        {
            return _direction;
        }
    }
}