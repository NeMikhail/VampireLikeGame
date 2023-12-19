using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using Core.Interface.IFeatures;
using Enums;
using MVP.Models.PassiveItemModels;
using MVP.Models.WeaponModels;
using MVP.Views.WeaponViews;

namespace Core.Interface.IModels
{
    public interface IWeaponModel : IModel, IModifyWeapon
    {
        public bool IsActive { get; }

        public WeaponsName Name { get; }
        public string DisplayName { get; }
        public Sprite Icon { get; }
        public string Description { get; }
        public bool IsAvailableSynergy { get; set; }
        public bool IsSynergy { get; }
        public bool IsSinergyActive { get; }
        public WeaponScriptableObject WeaponSynergy { get; }
        public List<PassiveItemInfo> PassiveForSynergy { get; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public float Damage { get; }
        public float DamageReplayTime { get; }
        public float ChanceCritical { get; }
        public int MultiplierCritical { get; }
        public float Probability { get; }
        public bool IsKnockback { get; }
        public float ProjectileInterval { get; }
        public float CurrentProjectileInterval { get; set; }
        public float Area { get; }
        public float ModifierArea { get; }
        public float Cooldown { get; }
        public float CurrentCooldown { get; set; }
        public float ProjectileDuration { get; }
        public float ProjectileSpeed { get; }

        public int ProjectileCurrrentCount { get; set; }
        public int ProjectileMaxCount { get; }
        public int ProjectilePiercingMaxEnemies { get; }
        public int ProjectileMaxBounceCount { get; }

        public GameObject Projectile { get; }
        public GameObject HitImpact { get; }
        public WeaponType WeaponType { get; }
        public WeaponTypeTarget Target { get; }

        public List<ProjectileView> ProjectileList { get; set; }
        public List<IDamageable> EnemyList { get; set; }
        public Dictionary<IDamageable, float> EnemyTimeAttackList { get; set; }

        public event Action<IDamageable, IWeaponModel, ProjectileView> OnStartDamageEnemy;
        public event Action<IDamageable, IWeaponModel, ProjectileView> OnStopDamageEnemy;
        public event Action<IWeaponModel> OnKill;
        public event Action<IWeaponModel> OnActivateWeapon;
        public event Action<IWeaponModel> OnDeActivateWeapon;
        public event Action OnLevelUp;

        public void ActivateWeapon();
        public void DeActivateWeapon();
        public void AddProjectile(ProjectileView projectileView);
        public void RemoveProjectile(ProjectileView projectileView);
        public void ClearProjectile();
        public bool LevelUpInfo(out ItemInfo info);
        public void LevelUp();
    }
}