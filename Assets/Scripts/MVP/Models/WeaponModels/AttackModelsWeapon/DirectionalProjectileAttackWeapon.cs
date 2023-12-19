using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class DirectionalProjectileAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private readonly Transform _playerObjectView;

        public DirectionalProjectileAttackWeapon(PlayerView playerView) : base(playerView)
        {
            _playerObjectView = playerView.PlayerObject.transform;
        }


        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            DeSpawnProjectileLifeTime(weapon);

            if (!ReloadWeaponCompleted(weapon)) return;

            if (weapon.CurrentProjectileInterval >= weapon.ProjectileInterval)
            {
                weapon.CurrentProjectileInterval = 0f;
                Vector3 target = _playerObjectView.forward;
                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                weapon.AddProjectile(projectile);
                projectile.transform.rotation = _playerObjectView.rotation;
                projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
                projectile.transform.position = new Vector3(_playerView.PlayerObject.transform.position.x, 1.5f,
                    _playerView.transform.position.z);
                projectile.RigidBodyObj.velocity = Vector3.zero;
                projectile.RigidBodyObj.AddForce(target * weapon.ProjectileSpeed, ForceMode.Impulse);
            }

            if (weapon.ProjectileCurrrentCount >= weapon.ProjectileMaxCount)
            {
                StartReloadWeapon(weapon);
            }
        }
    }
}