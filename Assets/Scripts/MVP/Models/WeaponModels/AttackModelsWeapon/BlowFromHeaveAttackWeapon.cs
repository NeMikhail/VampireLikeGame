using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.EnemyViews;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class BlowFromHeaveAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private Vector3 _target = Vector3.zero;
        private const float OFFSET = 2.5f;

        public BlowFromHeaveAttackWeapon(PlayerView playerView) : base(playerView)
        {
        }


        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            DeSpawnProjectileLifeTime(weapon);

            if (!ReloadWeaponCompleted(weapon))
                return;

            if (weapon.CurrentProjectileInterval >= weapon.ProjectileInterval)
            {
                weapon.CurrentProjectileInterval = 0f;
                Collider[] collidersScreen = Physics.OverlapSphere(_playerView.PlayerTransform.position, SCREEN / 1.5f);

                for (int i = 0; i < collidersScreen.Length; i++)
                {
                    if (collidersScreen[Random.Range(i, collidersScreen.Length - 1)].
                        TryGetComponent<IDamageable>(out var enemy))
                    {
                        if ((enemy as EnemyView) != null)
                            if (enemy.IsCamVisible && _target != (enemy as EnemyView).transform.position)
                            {
                                _target = (enemy as EnemyView).transform.position;
                                weapon.EnemyList.Add(enemy);
                                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                                weapon.AddProjectile(projectile);
                                projectile.transform.position = _target;

                                Collider[] collidersWeapon = Physics.OverlapSphere(projectile.transform.position,
                                    weapon.Area * OFFSET);

                                for (int j = 0; j < collidersWeapon.Length; j++)
                                {
                                    if (collidersWeapon[j].TryGetComponent<IDamageable>(out var enemyAreaWeapon))
                                    {
                                        CauseDamage(enemyAreaWeapon, weapon);
                                    }
                                }
                                break;
                            }
                    }
                }

                if (weapon.ProjectileCurrrentCount >= weapon.ProjectileMaxCount)
                {
                    StartReloadWeapon(weapon);
                }
            }
        }

        public override void OnEnemyStartDamaging(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            return;
        }
    }
}