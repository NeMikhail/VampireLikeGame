using UnityEngine;
using System.Collections.Generic;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal sealed class ClosestEnemyAttackWeapon : BaseAttackWeapon, IAttackWeapon
    {
        private GameObject _enemyDetectorObject;
        private CloseEnemyDetector _enemyDetector;

        public ClosestEnemyAttackWeapon(PlayerView playerView) : base(playerView)
        {
        }


        public override void Init(IWeaponModel weapon)
        {
            if (!weapon.IsActive)
            {
                _enemyDetectorObject = new GameObject("CloseEnemyDetector");
                _enemyDetectorObject.transform.parent = _playerView.transform;
                _enemyDetectorObject.transform.position = _playerView.transform.position;
                var rb = _enemyDetectorObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                var boxColl = _enemyDetectorObject.AddComponent<BoxCollider>();
                boxColl.size = new Vector3(SCREEN, SCREEN / 2, SCREEN);
                boxColl.isTrigger = true;
                _enemyDetector = _enemyDetectorObject.AddComponent<CloseEnemyDetector>();

                weapon.OnActivateWeapon += Enabled;
                weapon.OnDeActivateWeapon += Disabled;
                weapon.ActivateWeapon();
            }
        }

        public void Execute(IWeaponModel weapon)
        {
            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            DeSpawnProjectileLifeTime(weapon);

            if (!ReloadWeaponCompleted(weapon)) return;

            if (weapon.CurrentProjectileInterval >= weapon.ProjectileInterval && _enemyDetector.CloseEnemies.Count > 0)
            {
                weapon.CurrentProjectileInterval = 0f;
                Transform closestEnemyPosition = GetClosestEnemy(_enemyDetector.CloseEnemies);

                if (closestEnemyPosition == null)
                    return;

                Vector3 forceVector = closestEnemyPosition.position - _playerView.transform.position;

                ProjectileView projectile = pool.Spawn(weapon.Projectile);
                weapon.AddProjectile(projectile);
                projectile.transform.position = new Vector3(_playerView.transform.position.x, 1.5f,
                    _playerView.transform.position.z);
                projectile.transform.localScale = new Vector3(weapon.Area, weapon.Area, weapon.Area);
                projectile.RigidBodyObj.velocity = Vector3.zero;
                projectile.RigidBodyObj.AddForce(forceVector.normalized * weapon.ProjectileSpeed, ForceMode.Impulse);

                projectile.transform.rotation = Quaternion.LookRotation(forceVector.normalized * weapon.ProjectileSpeed);
                projectile.transform.rotation = new Quaternion(0f, projectile.transform.rotation.y, 0f,
                    projectile.transform.rotation.w);
            }

            if (weapon.ProjectileCurrrentCount >= weapon.ProjectileMaxCount)
            {
                StartReloadWeapon(weapon);
            }
        }

        private Transform GetClosestEnemy(List<Transform> list)
        {
            Transform closestTarget = null;
            float closestDistSqr = Mathf.Infinity;
            for (int i = 0; i < list.Count; i++)
            {
                float currentDist = (list[i].position - _playerView.transform.position).sqrMagnitude;
                if (currentDist < closestDistSqr && list[i].gameObject.activeInHierarchy)
                {
                    closestDistSqr = currentDist;
                    closestTarget = list[i];
                }
                else if (!list[i].gameObject.activeInHierarchy)
                {
                    list.Remove(list[i]);
                }
            }
            return closestTarget;
        }
    }
}