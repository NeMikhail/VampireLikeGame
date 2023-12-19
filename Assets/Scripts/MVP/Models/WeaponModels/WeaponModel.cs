using System;
using System.Collections.Generic;
using UnityEngine;
using Configs;
using static Configs.WeaponScriptableObject;
using Core.Interface.IFeatures;
using Core.Interface.IModels;
using Infrastructure.PoolSystems.Pool;
using Enums;
using MVP.Models.PassiveItemModels;
using MVP.Views.WeaponViews;

namespace MVP.Models.WeaponModels
{
    public struct ItemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Sprite Icon { get; set; }
        public int MaxLevel { get; set; }
    }

    public sealed class WeaponModel : IWeaponModel
    {
        private bool _isActive;

        private WeaponsName _name;
        private string _displayName;
        private string _description;
        private Sprite _icon;
        private int _level;
        private int _maxLevel;

        private bool _isSynergy;
        private bool _isAvailableSynergy;
        private bool _isSinergyActive;
        private List<PassiveItemInfo> _passiveForSynergy;
        private WeaponScriptableObject _weaponSynergy;
        private WeaponScriptableObject _levelUpCfg;

        private float _damage;
        private float _damageReplayTime;
        private float _chanceCritical;
        private int _multiplierCritical;

        private float _area;
        private float _cooldown;
        private float _currentCooldown;
        private float _projectileDuration;
        private float _projectileSpeed;

        private float _projectileInterval;
        private float _currentProjectileInterval;
        private int _projectileCurrrentCount;
        private int _projectileMaxCount;
        private int _projectilePiercingMaxEnemies;
        private int _projectileMaxBounceCount;
        private bool _isKnockback;

        private float _modifierDamage;
        private float _modifierArea;
        private float _modifierCooldown;
        private float _modifierDuration;
        private float _modifierProjectileSpeed;
        private int _modifierProjectileMaxCount;

        private float _probability;

        private GameObject _projectile;
        private GameObject _hitImpact;
        private WeaponType _weaponType;
        private WeaponTypeTarget _target;
        
        private List<ProjectileView> _projectileList;
        private List<IDamageable> _enemyList = new List<IDamageable>();
        private Dictionary<IDamageable, float> _enemyTimeAttackList = new Dictionary<IDamageable, float>();

        public event Action<IDamageable, IWeaponModel, ProjectileView> OnStartDamageEnemy;
        public event Action<IDamageable, IWeaponModel, ProjectileView> OnStopDamageEnemy;
        public event Action<IWeaponModel> OnKill;
        public event Action<IWeaponModel> OnDeActivateWeapon;
        public event Action<IWeaponModel> OnActivateWeapon;
        public event Action OnLevelUp;

        public bool IsActive
        {
            get { return _isActive; }
        }
        public WeaponsName Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public float Damage
        {
            get { return _damage * _modifierDamage; }
        }
        public float DamageReplayTime
        {
            get { return _damageReplayTime; }
        }
        public float ProjectileInterval
        {
            get { return _projectileInterval; }
        }
        public float CurrentProjectileInterval
        {
            get => _currentProjectileInterval;
            set => _currentProjectileInterval = value;
        }
        public float Area
        {
            get { return _area * _modifierArea; }
        }
        public float Cooldown
        {
            get { return _cooldown * _modifierCooldown; }
        }
        public float CurrentCooldown
        {
            get => _currentCooldown;
            set => _currentCooldown = value;
        }
        public float ProjectileDuration
        {
            get { return _projectileDuration * _modifierDuration; }
        }
        public float ProjectileSpeed
        {
            get { return _projectileSpeed * _modifierProjectileSpeed; }
        }

        public int ProjectileCurrrentCount
        {
            get => _projectileCurrrentCount;
            set => _projectileCurrrentCount = value;
        }
        public int ProjectileMaxCount
        {
            get { return (int)(_projectileMaxCount + _modifierProjectileMaxCount); }
        }
        public int ProjectilePiercingMaxEnemies
        {
            get => _projectilePiercingMaxEnemies;
        }
        public int ProjectileMaxBounceCount
        {
            get => _projectileMaxBounceCount;
        }
        public float Probability
        {
            get => _probability;
        }
        public GameObject Projectile
        {
            get { return _projectile; }
        }
        public WeaponType WeaponType
        {
            get { return _weaponType; }
        }
        public WeaponTypeTarget Target
        {
            get => _target;
            set => _target = value;
        }
        public Sprite Icon
        {
            get { return _icon; }
        }

        public List<ProjectileView> ProjectileList
        {
            get => _projectileList;
            set => _projectileList = value;
        }
        public List<IDamageable> EnemyList
        {
            get => _enemyList;
            set => _enemyList = value;
        }
        public Dictionary<IDamageable, float> EnemyTimeAttackList
        {
            get => _enemyTimeAttackList;
            set => _enemyTimeAttackList = value;
        }
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        public WeaponScriptableObject LevelUpCfg
        {
            get => _levelUpCfg;
            set => _levelUpCfg = value;
        }
        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
        }
        public bool IsAvailableSynergy
        {
            get => _isAvailableSynergy;
            set => _isAvailableSynergy = value;
        }
        public bool IsKnockback
        {
            get => _isKnockback;
            set => _isKnockback = value;
        }
        public int MaxLevel
        {
            get => _maxLevel;
            set => _maxLevel = value;
        }
        public List<PassiveItemInfo> PassiveForSynergy
        {
            get => _passiveForSynergy;
            set => _passiveForSynergy = value;
        }
        public bool IsSynergy
        {
            get => _isSynergy;
            set => _isSynergy = value;
        }
        public bool IsSinergyActive
        {
            get => _isSinergyActive;
            set => _isSinergyActive = value;
        }
        public WeaponScriptableObject WeaponSynergy
        {
            get => _weaponSynergy;
            set => _weaponSynergy = value;
        }
        public GameObject HitImpact 
        { 
            get => _hitImpact; 
            set => _hitImpact = value; 
        }
        public float ModifierArea 
        { 
            get => _modifierArea; 
        }
        public float ChanceCritical
        {
            get => _chanceCritical;
        }
        public int MultiplierCritical
        {
            get => _multiplierCritical;
        }
        public WeaponModel(WeaponAttributes cfg)
        {
            _name = cfg.Name;
            _displayName = cfg.DisplayName;
            _description = cfg.Description;
            _level = 1;
            _levelUpCfg = cfg.LevelUpCfg;
            _maxLevel = cfg.LevelUpCfg.Items.Count + 1;
            _weaponType = cfg.WeaponType;
            _target = cfg.Target;
            _icon = cfg.Icon;
            _probability= cfg.Probability;

            _isSynergy = cfg.IsSynergy;
            _isAvailableSynergy = false;
            _passiveForSynergy = cfg.PassiveForSynergy;
            _weaponSynergy = cfg.WeaponSynergy;

            _damage = cfg.Damage;
            _damageReplayTime = cfg.DamageReplayTime;
            _chanceCritical = cfg.ChanceCritical;
            _multiplierCritical = cfg.MultiplierCritical;

            _projectileInterval = cfg.ProjectileInterval;
            _area = cfg.Area;
            _cooldown = cfg.Cooldown;

            _projectile = cfg.Projectile;
            _hitImpact = cfg.HitImpact;
            _projectileDuration = cfg.ProjectileDuration;
            _projectileSpeed = cfg.ProjectileSpeed;
            _projectileMaxCount = cfg.ProjectileMaxCount;
            _projectilePiercingMaxEnemies = cfg.ProjectilePiercingMaxEnemies;
            _projectileMaxBounceCount = cfg.ProjectileMaxBounceCount;
            _projectileList = new List<ProjectileView>();
            _isKnockback = cfg.IsKnockback;
        }


        public void ActivateWeapon()
        {
            if (!_isActive)
            {
                _isActive = true;
                OnActivateWeapon?.Invoke(this);
            }
        }

        public void DeActivateWeapon()
        {
            if (_isActive)
            {
                _isActive = false;
                ClearProjectile();
                _enemyList.Clear();
                _enemyTimeAttackList.Clear();
                OnDeActivateWeapon?.Invoke(this);
            }
        }

        public void AddProjectile(ProjectileView projectileView)
        {
            _projectileList.Add(projectileView);
            projectileView.Weapon = this;
            ProjectileCurrrentCount++;
            SubscribeProjectile(projectileView);
        }

        public void RemoveProjectile(ProjectileView projectileView)
        {
            projectileView.LifeTime = 0;
            projectileView.DamageEnemyCount = 0;
            _projectileList.Remove(projectileView);
            UnSubscribeProjectile(projectileView);
        }

        public void ClearProjectile()
        {
            for (int i = 0; i < _projectileList.Count; i++)
            {
                ProjectileView projectile = _projectileList[i];
                projectile.LifeTime = 0;
                projectile.DamageEnemyCount = 0;
                UnSubscribeProjectile(projectile);
                GamePool.Projectile.Pool.DeSpawn(projectile.transform);
            }
            _projectileList.Clear();
        }

        private void SubscribeProjectile(ProjectileView projectileView)
        {
            projectileView.OnStartDamageEnemy += EventStartDamageEnemy;
            projectileView.OnStopDamageEnemy += EventStopDamageEnemy;
        }

        private void UnSubscribeProjectile(ProjectileView projectileView)
        {
            projectileView.OnStartDamageEnemy -= EventStartDamageEnemy;
            projectileView.OnStopDamageEnemy -= EventStopDamageEnemy;
        }

        private void EventStartDamageEnemy(IDamageable enemy, ProjectileView projectile)
        {
            if (_isKnockback)
                OnStartDamageEnemy += enemy.CauseKnockback;

            OnStartDamageEnemy?.Invoke(enemy, this, projectile);
            enemy.OnStop += EnemyDead;
        }

        private void EventStopDamageEnemy(IDamageable enemy, ProjectileView projectile)
        {
            OnStopDamageEnemy?.Invoke(enemy, this, projectile);
            enemy.OnStop -= EnemyDead;
        }

        private void EnemyDead(IDamageable enemy)
        {
            OnStopDamageEnemy?.Invoke(enemy, this, null);
            OnKill?.Invoke(this);
        }

        public void ModifyDamage(float modifier) => _modifierDamage = modifier;
        public void ModifyArea(float modifier) => _modifierArea = modifier;
        public void ModifyProjectileSpeed(float modifier) => _modifierProjectileSpeed = modifier;
        public void ModifyCooldown(float modifier) => _modifierCooldown = modifier;
        public void ModifyEffectDuration(float modifier) => _modifierDuration = modifier;
        public void ModifyProjectilesAdditional(int modifier) => _modifierProjectileMaxCount = modifier;

        public void LevelUp()
        {
            if (_level >= _maxLevel && !_isSynergy)
                return;

            if (_level >= _maxLevel && _isSynergy && !_isAvailableSynergy)
                return;

            WeaponAttributes cfg = new();
            if (_level >= _maxLevel && _isAvailableSynergy && _isSynergy)
            {
                _isSinergyActive = true;
                _level = 1;
                _maxLevel = 1;
                cfg = _weaponSynergy.Items[0];
                _name = cfg.Name;
                _hitImpact = cfg.HitImpact;
                _weaponType = cfg.WeaponType;
                _target = cfg.Target;
                _icon = cfg.Icon;
                _projectile = cfg.Projectile;
            }
            else
            {
                _level++;
                cfg = _levelUpCfg.Items[_level - 2];
            }

            _damage = cfg.Damage;
            _damageReplayTime = cfg.DamageReplayTime;
            _projectileInterval = cfg.ProjectileInterval;
            _area = cfg.Area;
            _cooldown = cfg.Cooldown;

            _projectileDuration = cfg.ProjectileDuration;
            _projectileSpeed = cfg.ProjectileSpeed;
            _projectileMaxCount = cfg.ProjectileMaxCount;
            _projectilePiercingMaxEnemies = cfg.ProjectilePiercingMaxEnemies;
            _projectileMaxBounceCount = cfg.ProjectileMaxBounceCount;
            _isKnockback = cfg.IsKnockback;

            ClearProjectile();
            ProjectileCurrrentCount = 0;
            CurrentCooldown = _cooldown;
            OnLevelUp?.Invoke();
        }

        public bool LevelUpInfo(out ItemInfo info)
        {
            info = new ItemInfo();
            if (_level >= _maxLevel)
            {
                return false;
            }
            WeaponAttributes cfg = _levelUpCfg.Items[_level - 1];
            info.Name = cfg.DisplayName;
            info.Description = cfg.Description;
            info.Icon = cfg.Icon;
            return true;
        }
    }
}