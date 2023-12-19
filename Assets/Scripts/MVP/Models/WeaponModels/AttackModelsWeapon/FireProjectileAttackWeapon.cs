using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class FireProjectileAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        public FireProjectileAttackWeapon(PlayerView playerView) : base(playerView)
        {
        }


        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;

            DeSpawnProjectileLifeTime(weapon);

            if (!ReloadWeaponCompleted(weapon)) return;

            float xRand = Random.Range(-1f, 1f);
            float zRand = Random.Range(-1f, 1f);
            var forceVector = new Vector3(xRand, 0f, zRand);
            var deltaVelocity = new Vector3(0.2f, 0f, 0.2f);
            var target = new Vector3();

            for (int i = 0; i < weapon.ProjectileMaxCount; i++, forceVector += deltaVelocity)
            {
                forceVector = forceVector.normalized;
                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                weapon.AddProjectile(projectile);
                projectile.transform.position = new Vector3(_playerView.transform.position.x, 1.5f,
                    _playerView.transform.position.z);
                projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
                projectile.RigidBodyObj.velocity = Vector3.zero;
                projectile.RigidBodyObj.angularVelocity = Vector3.zero;
                target = forceVector * weapon.ProjectileSpeed;
                projectile.RigidBodyObj.AddForce(target, ForceMode.Impulse);
                projectile.transform.rotation = Quaternion.LookRotation(target);
                projectile.transform.rotation = new Quaternion(0f, projectile.transform.rotation.y, 0f,
                    projectile.transform.rotation.w);
            }
            StartReloadWeapon(weapon);
        }
    }
}