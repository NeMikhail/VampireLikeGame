using UnityEngine;
using Configs;
using Infrastructure.PoolSystems.Pool;
using Infrastructure.TimeRemaningService;
using MVP.Views.EnemyViews;
using MVP.Views.PlayerViews;

namespace MVP.Presenters.EnemyPresenters
{
    internal sealed class EnemyShootPresenter
    {
        private readonly PlayerView _player;
        private readonly EnemyShooterView _enemy;
        private readonly Animator _animator;
        private readonly Transform _handForBullet;
        private EnemyBulletConfig _enemyBulletConfig;
        private readonly TimeRemaining _timeRemaining;

        public EnemyShootPresenter(PlayerView player, EnemyShooterView enemy, EnemyBulletConfig enemyBulletConfig, Animator animator, Transform handForBullet)
        {
            _player = player;
            _enemy = enemy;
            _enemyBulletConfig = enemyBulletConfig;
            _timeRemaining = new TimeRemaining(TimerInterval, _enemyBulletConfig.Interval, true);
            _animator = animator;
            _handForBullet = handForBullet;
        }


        public void CreateTimerInterval()
        {
            _timeRemaining.AddTimeRemaining();
        }

        public void RemoveTimerInterval()
        {
            _timeRemaining.RemoveTimeRemaining();
        }

        private void TimerInterval()
        {
            _animator.SetInteger("State", 1);
            _animator.SetTrigger("Attack");
        }

        public void Shoot()
        {
            EnemyBulletView currentBullet = GamePool.EnemyBullet.Pool.Spawn(_enemyBulletConfig.Prefab);
            currentBullet.transform.localPosition = Vector3.zero;
            currentBullet.transform.localRotation = Quaternion.identity;
            currentBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;

            currentBullet.transform.position = _handForBullet.position;

            Vector3 enemyPosition = _enemy.transform.position;
            Vector3 direction = (_player.transform.position - enemyPosition).normalized;
            currentBullet.transform.LookAt(_player.transform.position);
            currentBullet.transform.rotation = new Quaternion(0f, currentBullet.transform.rotation.y, 0f,
                currentBullet.transform.rotation.w);
            currentBullet.GetComponent<Rigidbody>().velocity = direction * _enemyBulletConfig.Speed;
        }
    }
}