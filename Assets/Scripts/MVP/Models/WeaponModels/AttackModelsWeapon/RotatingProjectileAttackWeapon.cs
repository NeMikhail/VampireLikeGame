using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class RotatingProjectileAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private const float OFFSET = 3f;

        public RotatingProjectileAttackWeapon(PlayerView playerView) : base(playerView)
        {
        }


        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            var center = new Vector3(_playerView.transform.position.x, 1.5f, _playerView.transform.position.z);
            float radius = weapon.Area * OFFSET;
            float angularSpeed = weapon.ProjectileSpeed;

            DeSpawnProjectileLifeTime(weapon);

            for (var i = 0; i < weapon.ProjectileList.Count; i++)
            {
                ProjectileView item = weapon.ProjectileList[i];
                item.transform.LookAt(_playerView.transform);
                item.transform.rotation = new Quaternion(0f, item.transform.rotation.y, 0f, item.transform.rotation.w);
                float offset = (270f / (float)weapon.ProjectileMaxCount) * (float)i;
                float dx = Mathf.Cos(Angle + offset) * radius;
                float dz = Mathf.Sin(Angle + offset) * radius;
                var vector = new Vector3(dx, 0, dz);

                item.transform.position = center + vector;
                Angle += Time.deltaTime * (angularSpeed / (float)weapon.ProjectileMaxCount);
                if (Angle + (offset * (i)) >= 3600f)
                    Angle -= Angle + (offset * (i));
            }

            if (!ReloadWeaponCompleted(weapon))
                return;

            if (weapon.ProjectileCurrrentCount < weapon.ProjectileMaxCount)
                for (var i = 0; i < weapon.ProjectileMaxCount; i++)
                {
                    ProjectileView projectile = pool.Spawn(weapon.Projectile);
                    projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
                    weapon.AddProjectile(projectile);
                }

            if (weapon.ProjectileList.Count == 0)
            {
                StartReloadWeapon(weapon);
            }
        }
    }
}