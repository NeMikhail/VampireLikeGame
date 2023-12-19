using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class BounceProjectileAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        public BounceProjectileAttackWeapon(PlayerView playerView) : base(playerView)
        {
        }


        public override void Init(IWeaponModel weapon)
        {
            if (!weapon.IsActive)
            {
                GameObject borders = GameObject.Instantiate(Resources.Load<GameObject>("Hero/Borders"));
                borders.transform.parent = _playerView.transform;
                borders.transform.position = _playerView.transform.position;

                weapon.OnActivateWeapon += Enabled;
                weapon.OnDeActivateWeapon += Disabled;
                weapon.ActivateWeapon();
            }
        }

        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;

            DeSpawnProjectileLifeTime(weapon);

            for (var i = 0; i < weapon.ProjectileList.Count; i++)
            {
                RotateObj(weapon.ProjectileList[i]);
            }

            if (!ReloadWeaponCompleted(weapon))
                return;

            if (weapon.CurrentProjectileInterval >= weapon.ProjectileInterval)
            {
                weapon.CurrentProjectileInterval = 0f;
                float xRand = Random.Range(-1f, 1f);
                float zRand = Random.Range(-1f, 1f);
                var forceVector = new Vector3(xRand, 0f, zRand);
                forceVector = forceVector.normalized;
                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                projectile.OnBounce += HitImpact;
                projectile.OnDeSpawnObject += DeSpawnProjectile;
                weapon.AddProjectile(projectile);
                projectile.transform.position = new Vector3(_playerView.transform.position.x, 1.5f,
                    _playerView.transform.position.z);
                projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
                projectile.RigidBodyObj.velocity = Vector3.zero;
                projectile.RigidBodyObj.angularVelocity = Vector3.zero;
                projectile.Direction = forceVector;
                projectile.RigidBodyObj.velocity = forceVector * weapon.ProjectileSpeed;
            }

            if (weapon.ProjectileCurrrentCount >= weapon.ProjectileMaxCount)
            {
                StartReloadWeapon(weapon);
            }
        }

        private void HitImpact(IWeaponModel weapon, ProjectileView projectile)
        {
            if (weapon.HitImpact == null) return;

            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            ProjectileView hit = pool.Spawn(weapon.HitImpact);
            weapon.AddProjectile(hit);
            weapon.ProjectileCurrrentCount--;
            hit.LifeTime = weapon.ProjectileDuration - 3f;
            hit.isHit = true;
            hit.transform.position = projectile.transform.position;

            Collider[] collidersWeapon = Physics.OverlapSphere(hit.transform.position, (hit.ColliderObj as SphereCollider).
                radius);

            for (int j = 0; j < collidersWeapon.Length; j++)
            {
                if (collidersWeapon[j].TryGetComponent<IDamageable>(out var enemyAreaWeapon))
                {
                    CauseDamage(enemyAreaWeapon, weapon);
                }
            }
        }

        private void DeSpawnProjectile(ProjectileView projectile)
        {
            projectile.OnBounce -= HitImpact;
            projectile.OnDeSpawnObject -= DeSpawnProjectile;
        }
    }
}