using UnityEngine;
using Core.Animations.Enemy;
using MVP.Models.EnemyModels;
using MVP.Views.EnemyViews;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Presenters.EnemyPresenters
{
    internal class EnemyMovement
    {
        protected readonly PlayerView _player;
        protected readonly EnemyView _enemy;
        private readonly EnemyModel _enemyModel;
        private readonly Rigidbody _enemyRigidbody;
        private readonly EnemyAnimation _enemyAnimation;

        private const  float ROTATION_SPEED = 100f;

        private const float KNOCKBACK_LENGTH = 0.1f;
        private const float MAX_KNOCKBACK = 10;
        private const float MIN_KNOCKBACK = 3;
        private float _knockBackLength;
        private Vector3 _knockBackDirection;
        private float _knockBackSpeed;

        public EnemyMovement(PlayerView player, EnemyView enemy, EnemyModel enemyModel, Rigidbody enemyRigidbody,
            EnemyAnimation enemyAnimation)
        {
            _player = player;
            _enemy = enemy;
            _enemyModel = enemyModel;
            _enemyRigidbody = enemyRigidbody;
            _enemyAnimation = enemyAnimation;
        }


        protected virtual Vector3 GetDirection() => GetVectorTowardsPlayer();

        private Vector3 GetVectorTowardsPlayer() => (_player.transform.position - _enemy.transform.position).normalized;

        public virtual void FixedExecute(float deltaTime)
        {
            Vector3 direction = GetDirection();
            Vector3 movementVelocity = direction * _enemyModel.EnemySpeed;

            if (_knockBackLength <= 0)
            {
                _enemyRigidbody.velocity = movementVelocity;
            }
            else
            {
                _knockBackLength -= deltaTime;
                if (_knockBackDirection == Vector3.zero)
                    _knockBackDirection = -1 * GetVectorTowardsPlayer();

                _enemyRigidbody.velocity = _knockBackDirection * _knockBackSpeed;
            }

            var rotation = Quaternion.LookRotation(direction);
            _enemy.EnemyObject.transform.rotation = Quaternion.Lerp(_enemy.transform.rotation, rotation, ROTATION_SPEED * Time.deltaTime);

            UpdateAnimation(movementVelocity.magnitude);
        }

        public void UpdateAnimation(float speed)
        {
            if (_enemyAnimation != null)
                _enemyAnimation.Move(speed);
        }

        public void CauseKnockBack(ProjectileView projectile, float projectileSpeed)
        {
            _knockBackLength = KNOCKBACK_LENGTH;
            _knockBackDirection = projectile.RigidBodyObj.velocity.normalized;
            _knockBackSpeed = (projectileSpeed - _enemyModel.EnemySpeed) / 2;

            if (_knockBackSpeed > MAX_KNOCKBACK)
                _knockBackSpeed = MAX_KNOCKBACK;

            if (_knockBackSpeed < MIN_KNOCKBACK)
                _knockBackSpeed = MIN_KNOCKBACK;
        }
    }
}