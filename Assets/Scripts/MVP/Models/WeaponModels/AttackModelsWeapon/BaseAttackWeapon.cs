using System;
using UnityEngine;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using MVP.Views.PlayerViews;
using MVP.Views.WeaponViews;
using MVP.Views.EnemyViews;

namespace MVP.Models.WeaponModels.AttackModelsWeapon
{
    internal abstract class BaseAttackWeapon
    {
        public const float SCREEN = 35f;

        private float _angle = 0f;
        public PlayerView _playerView;
        private PopUpType _damagePopUpType;

        public float Angle { get => _angle; set => _angle = value; }

        public event Action<Transform, float, PopUpType> OnCauseDamage;

        public BaseAttackWeapon(PlayerView playerView)
        {
            _playerView = playerView;
        }


        public virtual void Init(IWeaponModel weapon)
        {
            if (!weapon.IsActive)
            {
                weapon.OnActivateWeapon += Enabled;
                weapon.OnDeActivateWeapon += Disabled;
                weapon.ActivateWeapon();
            }
        }

        public virtual void Enabled(IWeaponModel weapon)
        {
            weapon.OnStartDamageEnemy += OnEnemyStartDamaging;
            weapon.OnStopDamageEnemy += OnEnemyStopDamaging;
        }

        public virtual void Disabled(IWeaponModel weapon)
        {
            weapon.OnStartDamageEnemy -= OnEnemyStartDamaging;
            weapon.OnStopDamageEnemy -= OnEnemyStopDamaging;
            weapon.OnDeActivateWeapon -= Disabled;
            weapon.OnActivateWeapon -= Enabled;
        }

        public virtual void OnEnemyStartDamaging(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            if (!weapon.EnemyTimeAttackList.ContainsKey(enemy))
                weapon.EnemyTimeAttackList.Add(enemy, 0f);

            if (!weapon.EnemyList.Contains(enemy))
                weapon.EnemyList.Add(enemy);

            if (projectile != null && weapon.ProjectilePiercingMaxEnemies > 0)
            {
                CauseDamage(enemy, weapon);
                projectile.DamageEnemyCount++;

                if (projectile.DamageEnemyCount >= weapon.ProjectilePiercingMaxEnemies)
                {
                    GamePool.Projectile.Pool.DeSpawn(projectile.transform);
                    projectile.LifeTime = 0;
                    projectile.DamageEnemyCount = 0;
                    weapon.RemoveProjectile(projectile);
                }
                return;
            }
            CauseDamage(enemy, weapon);
        }

        public virtual void OnEnemyStopDamaging(IDamageable enemy, IWeaponModel weapon, ProjectileView projectile)
        {
            if (weapon.Name != Enums.WeaponsName.Whip && weapon.Name != Enums.WeaponsName.BloodyTear)
                weapon.EnemyList.Remove(enemy);
        }

        protected void DeSpawnProjectileLifeTime(IWeaponModel weapon)
        {
            if (weapon.ProjectileDuration == 0f)
                return;

            Pool<ProjectileView> pool = GamePool.Projectile.Pool;
            for (int i = 0; i < weapon.ProjectileList.Count; i++)
            {
                ProjectileView projectile = weapon.ProjectileList[i];
                projectile.LifeTime += Time.deltaTime;

                if (weapon.WeaponType == Enums.WeaponType.BounceProjectiles ||
                    weapon.WeaponType == Enums.WeaponType.SinergyBounceProjectiles)
                {
                    if (!projectile.isHit)
                    {
                        projectile.RigidBodyObj.velocity = projectile.Direction.normalized * weapon.ProjectileSpeed;
                        if (projectile.BounceCurrentCount >= weapon.ProjectileMaxBounceCount)
                        {
                            projectile.BounceCurrentCount = 0;
                            projectile.LifeTime = weapon.ProjectileDuration;
                        }
                    }
                }

                if (projectile.LifeTime >= weapon.ProjectileDuration)
                {
                    pool.DeSpawn(projectile.transform);
                    projectile.LifeTime = 0;
                    projectile.DamageEnemyCount = 0;
                    weapon.RemoveProjectile(projectile);
                }
            }
        }

        protected void StartReloadWeapon(IWeaponModel weapon)
        {
            weapon.CurrentCooldown = 0;
            weapon.ProjectileCurrrentCount = 0;
            weapon.EnemyList.Clear();
            weapon.EnemyTimeAttackList.Clear();
        }

        protected bool ReloadWeaponCompleted(IWeaponModel weapon)
        {
            if (weapon.ProjectileCurrrentCount == 0)
            {
                weapon.CurrentCooldown += Time.deltaTime;
            }

            if (weapon.CurrentCooldown < weapon.Cooldown)
            {
                return false;
            }
            else
            {
                if (weapon.ProjectileCurrrentCount == 0)
                    weapon.CurrentProjectileInterval = weapon.ProjectileInterval;
            }

            weapon.CurrentProjectileInterval += Time.deltaTime;
            return true;
        }

        protected void CauseDamage(IDamageable enemy, IWeaponModel weapon)
        {
            float currentDamage = GetCriticalChanceProbability(weapon);
            enemy.CauseDamage(currentDamage);

            if (enemy is EnemyView enemyView)
            {
                OnCauseDamage?.Invoke(enemyView.transform, currentDamage, _damagePopUpType);
            }
        }

        protected void RotateObj(ProjectileView projectile)
        {
            float speed = 8f;
            if (!projectile.isHit)
                projectile.transform.Rotate(new Vector3(0f, 10f, 0f), speed);
        }

        private float GetCriticalChanceProbability(IWeaponModel weapon)
        {
            _damagePopUpType = PopUpType.EnemyNormalDamage;
            float currentDamage = weapon.Damage;
            if (weapon.ChanceCritical > 0)
            {
                int randomNumber = UnityEngine.Random.Range(0, 100);
                float chanceInPercents = weapon.ChanceCritical * 100;

                if (randomNumber < chanceInPercents)
                {
                    currentDamage *= weapon.MultiplierCritical;
                    _damagePopUpType = PopUpType.EnemyCriticalDamage;
                }
            }
            
            return currentDamage;
        }
    }
}