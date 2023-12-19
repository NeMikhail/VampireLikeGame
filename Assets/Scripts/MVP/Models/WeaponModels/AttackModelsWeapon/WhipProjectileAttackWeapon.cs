using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;
using UnityEngine;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class WhipProjectileAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private Transform _playerTransform;

        public WhipProjectileAttackWeapon(PlayerView playerView) : base(playerView)
        {
            _playerTransform = playerView.PlayerTransform;
        }


        public void Execute(IWeaponModel weapon)
        {
            for (int i = 0; i < weapon.ProjectileList.Count; i++)
                weapon.ProjectileList[i].transform.position = _playerTransform.position;

            DeSpawnProjectileLifeTime(weapon);

            if (!ReloadWeaponCompleted(weapon)) return;

            if (weapon.CurrentProjectileInterval >= weapon.ProjectileInterval)
            {
                Shoot(weapon);
                StartReloadWeapon(weapon);
            }
        }

        public override void OnEnemyStartDamaging(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            if (!weapon.EnemyList.Contains(enemy))
            {
                weapon.EnemyList.Add(enemy);
                CauseDamage(enemy, weapon);
            }
        }

        private void Shoot(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            float whipOrientation = 1f;

            weapon.CurrentProjectileInterval = 0f;
            for (int i = 0; i < weapon.ProjectileMaxCount; i++, whipOrientation *= -1)
            {
                if (i >= 2) return;
                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                weapon.AddProjectile(projectile);
                projectile.transform.position = new Vector3(_playerView.transform.position.x, 1f,
                    _playerView.transform.position.z);

                projectile.transform.rotation = Quaternion.AngleAxis(whipOrientation == 1 ? 0f : 180f, Vector3.up);

                projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
            }
        }
    }
}