using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Presenters.PlayerPresenters;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;
using MVP.Views.EnemyViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class AreaDamageAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private const float OFFSET = 8f;
        private IWeaponModel _currentWeaponModel;

        private HealthChangingPresenter _healthChangingPresenter;

        public AreaDamageAttackWeapon(PlayerView playerView, HealthChangingPresenter healthChanging) : base(playerView)
        {
            _healthChangingPresenter = healthChanging;
        }


        public override void Enabled(IWeaponModel weapon)
        {
            base.Enabled(weapon);
            weapon.OnKill += OnKillEnemy;
            _currentWeaponModel = weapon;
        }

        public override void Disabled(IWeaponModel weapon)
        {
            base.Disabled(weapon);
            weapon.OnKill -= OnKillEnemy;
            _currentWeaponModel = null;
        }


        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            if (weapon.ProjectileList.Count == 0)
            {
                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                projectile.transform.localScale = new Vector3(weapon.Area * OFFSET, weapon.Area * OFFSET,
                    weapon.Area * OFFSET);
                weapon.AddProjectile(projectile);
            }
            else
            {
                for (int i = 0; i < weapon.ProjectileList.Count; i++)
                {
                    Transform projectile = weapon.ProjectileList[i].transform;
                    projectile.localScale = new Vector3(weapon.Area * OFFSET, weapon.Area * OFFSET,
                        weapon.Area * OFFSET);
                    projectile.position = _playerView.transform.position;
                }

            }

            for (var i = 0; i < weapon.EnemyList.Count; i++)
            {
                IDamageable enemy = weapon.EnemyList[i];
                if (enemy.IsCamVisible)
                {
                    weapon.EnemyTimeAttackList[enemy] += Time.deltaTime;
                    if (weapon.EnemyTimeAttackList[enemy] >= weapon.DamageReplayTime)
                    {
                        CauseDamage(enemy, weapon);
                        weapon.EnemyTimeAttackList[enemy] = 0f;
                    }
                }
            }
        }

        public override void OnEnemyStartDamaging(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            enemy.OnStop += RemoveEnemy;
            base.OnEnemyStartDamaging(enemy, weapon, projectile);
        }

        public void OnKillEnemy(IWeaponModel weapon)
        {
            if (weapon.IsSinergyActive)
            {
                if (Random.Range(0f, 1f) < 0.07 * weapon.Damage / 7)
                {
                    _healthChangingPresenter.TakeHealth(1);
                } 
            }
        }

        public void RemoveEnemy(IDamageable enemy)
        {
            _currentWeaponModel.EnemyList.Remove(enemy);
            enemy.OnStop -= RemoveEnemy;
        }
    }
}