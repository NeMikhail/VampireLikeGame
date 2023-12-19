using System;
using Configs;
using Core.Interface.IModels;
using Core.Interface.IScriptable;
using UnityEngine;

namespace MVP.Models.PlayerModels
{
    internal sealed class ModifiersModel : IModifiersModel
    {
        public event Action<float> SpeedMultiplierChanged;
        public event Action<float> MaxHealthMultiplierChanged;
        public event Action<float> ArmorAdditionChanged;
        public event Action<float> RegenerationChanged;
        public event Action<float> RevivesCountChanged;
        public event Action<float> DamageMultiplierChanged;
        public event Action<float> AreaMultiplierChanged;
        public event Action<float> CooldownMultiplierChanged;
        public event Action<float> EffectDurationMultiplierChanged;
        public event Action<float> ProjectileSpeedMultiplierChanged;
        public event Action<int> ProjectilesAdditionalChanged;
        public event Action<float> LuckMultiplierChanged;
        public event Action<float> ExpirienceMultiplierChanged;
        public event Action<float> CoinsMultiplierChanged;
        public event Action<float> ExperienceAttractionAreaChanged;
        public event Action<float> EnemyHealthMultiplierChanged;
        public event Action<float> EnemySpeedMultiplierChanged;
        public event Action<float> EnemyDamageMultiplierChanged;
        public event Action<float> EnemyCountMultiplierChanged;

        private float _speedMultiplier;
        private float _maxHealthMultiplier;
        private float _armorAddition;
        private float _regeneration;
        private float _revivesCount;
        private float _weaponDamageMultiplier;
        private float _weaponAreaMultiplier;
        private float _weaponCooldownMultiplier;
        private float _weaponEffectDurationMultiplier;
        private float _projectileSpeedMultiplier;
        private int _weaponProjectilesAdditional;
        private float _luckMultiplier;
        private float _experienceMultiplier;
        private float _coinsMultiplier;
        private float _experienceAttractionArea;
        private float _enemyHealthMultiplier;
        private float _enemySpeedMultiplier;
        private float _enemyDamageMultiplier;
        private float _enemyCountMultiplier;

        public float SpeedMultiplier
        {
            get { return _speedMultiplier; }
            set
            {
                if (_speedMultiplier != value)
                {
                    _speedMultiplier = value;
                    SpeedMultiplierChanged?.Invoke(_speedMultiplier);
                }
            }
        }
        public float MaxHealthMultiplier
        {
            get { return _maxHealthMultiplier; }
            set
            {
                if (_maxHealthMultiplier != value)
                {
                    _maxHealthMultiplier = value;
                    MaxHealthMultiplierChanged?.Invoke(_maxHealthMultiplier);
                }
            }
        }
        public float ArmorAddition
        {
            get { return _armorAddition; } 
            set
            {
                if (_armorAddition != value)
                {
                    _armorAddition = value;
                    ArmorAdditionChanged?.Invoke(_armorAddition);
                }
            }
        }
        public float Regeneration
        {
            get { return  _regeneration; }
            set
            {
                if ( _regeneration != value )
                {
                    _regeneration = value;
                    RegenerationChanged?.Invoke(_regeneration);
                }
            }
        }
        public float RevivesCount
        {
            get { return _revivesCount;}
            set
            {
                if ( _revivesCount != value )
                {
                    _revivesCount = value;
                    RevivesCountChanged?.Invoke(_revivesCount);
                }

            }
        }
        public float WeaponDamageMultiplier
        {
            get { return _weaponDamageMultiplier; }
            set
            {
                if (_weaponDamageMultiplier != value)
                {
                    _weaponDamageMultiplier = value;
                    DamageMultiplierChanged?.Invoke(_weaponDamageMultiplier);
                }
            }
        }
        public float WeaponAreaMultiplier
        {
            get { return _weaponAreaMultiplier; }
            set
            {
                if (_weaponAreaMultiplier != value)
                {
                    _weaponAreaMultiplier = value;
                    AreaMultiplierChanged?.Invoke(_weaponAreaMultiplier);
                }
            }
        }
        public float WeaponCooldownMultiplier
        {
            get { return _weaponCooldownMultiplier; }
            set
            {
                if (_weaponCooldownMultiplier != value)
                {
                    _weaponCooldownMultiplier = value;
                    CooldownMultiplierChanged?.Invoke(_weaponCooldownMultiplier);
                }
            }
        }
        public float WeaponEffectDurationMultiplier
        {
            get { return _weaponEffectDurationMultiplier; }
            set
            {
                if (_weaponEffectDurationMultiplier != value)
                {
                    _weaponEffectDurationMultiplier = value;
                    EffectDurationMultiplierChanged?.Invoke(_weaponEffectDurationMultiplier);
                }
            }
        }
        public float ProjectileSpeedMultiplier
        {
            get { return _projectileSpeedMultiplier; }
            set
            {
                if (_projectileSpeedMultiplier != value)
                {
                    _projectileSpeedMultiplier = value;
                    ProjectileSpeedMultiplierChanged?.Invoke(_projectileSpeedMultiplier);
                }
            }
        }
        public int WeaponProjectilesAdditional
        {
            get { return _weaponProjectilesAdditional; }
            set
            {
                if (_weaponProjectilesAdditional != value)
                {
                    _weaponProjectilesAdditional = value;
                    ProjectilesAdditionalChanged?.Invoke(_weaponProjectilesAdditional);
                }
            }
        }
        public float LuckMultiplier
        {
            get { return _luckMultiplier; }
            set
            {
                if (_luckMultiplier != value)
                {
                    _luckMultiplier = value;
                    LuckMultiplierChanged?.Invoke(_luckMultiplier);
                }
            }
        }
        public float ExperienceMultiplier
        {
            get { return _experienceMultiplier; }
            set
            {
                if (_experienceMultiplier != value)
                {
                    _experienceMultiplier = value;
                    ExpirienceMultiplierChanged?.Invoke(_experienceMultiplier);
                }
            }
        }
        public float CoinsMultiplier
        {
            get { return _coinsMultiplier; }
            set
            {
                if (_coinsMultiplier != value)
                {
                    _coinsMultiplier = value;
                    CoinsMultiplierChanged?.Invoke(_coinsMultiplier);
                }
            }
        }
        public float ExperienceAttractionArea
        {
            get { return _experienceAttractionArea; }
            set
            {
                if (_experienceAttractionArea != value)
                {
                    _experienceAttractionArea = value;
                    ExperienceAttractionAreaChanged?.Invoke(_experienceAttractionArea);
                }
            }
        }
        public float EnemyHealthMultiplier
        {
            get { return _enemyHealthMultiplier; }
            set
            {
                if (_enemyHealthMultiplier != value)
                {
                    _enemyHealthMultiplier = value;
                    EnemyHealthMultiplierChanged?.Invoke(_enemyHealthMultiplier);
                }
            }
        }
        public float EnemySpeedMultiplier
        {
            get { return _enemySpeedMultiplier; }
            set
            {
                if (_enemySpeedMultiplier != value)
                {
                    _enemySpeedMultiplier = value;
                    EnemySpeedMultiplierChanged?.Invoke(_enemySpeedMultiplier);
                }
            }
        }
        public float EnemyDamageMultiplier
        {
            get { return _enemyDamageMultiplier;}
            set
            {
                if (_enemyDamageMultiplier != value)
                {
                    _enemyDamageMultiplier = value;
                    EnemyDamageMultiplierChanged?.Invoke(_enemyDamageMultiplier);
                }
            }
        }
        public float EnemyCountMultiplier
        {
            get { return _enemyCountMultiplier; }
            set
            {
                if (_enemyCountMultiplier != value)
                {
                    _enemyCountMultiplier = value;
                    EnemyCountMultiplierChanged?.Invoke(_enemyCountMultiplier);
                }
            }
        }

        internal ModifiersModel(LocationsPack config, UpgradeModifiersConfig upgradeConfig, IPlayerConfig playerConfig)
        {
            LocationModifiersConfig locationModifiersConfig = config.CurrentLocation.LocationModifiersConfig;
            ICharacterConfig characterConfig = playerConfig.Characters.GetValue(playerConfig.CurrentCharacterName);

            _speedMultiplier = locationModifiersConfig.SpeedMultiplier + upgradeConfig.SpeedMultiplier;
            _maxHealthMultiplier = locationModifiersConfig.MaxHealthMultiplier + upgradeConfig.MaxHealthMultiplier;
            _armorAddition = locationModifiersConfig.ArmorAddition + upgradeConfig.ArmorAddition;
            _regeneration = locationModifiersConfig.Regeneration + upgradeConfig.Regeneration;
            _revivesCount = locationModifiersConfig.RevivesCount + upgradeConfig.RevivesCount;
            _weaponDamageMultiplier = locationModifiersConfig.WeaponDamageMultiplier +
                upgradeConfig.WeaponDamageMultiplier + characterConfig.WeaponDamage;
            _weaponAreaMultiplier = locationModifiersConfig.WeaponAreaMultiplier + 
                upgradeConfig.WeaponAreaMultiplier + characterConfig.WeaponArea;
            _weaponCooldownMultiplier = locationModifiersConfig.WeaponReloadMultiplier +
                upgradeConfig.WeaponReloadMultiplier + characterConfig.WeaponCooldown;
            _weaponEffectDurationMultiplier = locationModifiersConfig.WeaponEffectDurationMultiplier +
                upgradeConfig.WeaponEffectDurationMultiplier + characterConfig.WeaponProjectileDuration;
            _projectileSpeedMultiplier = locationModifiersConfig.ProjectileSpeedMultiplier +
                upgradeConfig.ProjectileSpeedMultiplier + characterConfig.WeaponProjectileSpeed;
            _weaponProjectilesAdditional = locationModifiersConfig.WeaponProjectilesAdditional +
                upgradeConfig.WeaponProjectilesAdditional + characterConfig.WeaponProjectileAdditional;
            _luckMultiplier = locationModifiersConfig.LuckMultiplier + upgradeConfig.LuckMultiplier;
            _experienceMultiplier = locationModifiersConfig.ExperienceMultiplier + upgradeConfig.ExperienceMultiplier;
            _coinsMultiplier = locationModifiersConfig.CoinsMultiplier;
            _experienceAttractionArea = locationModifiersConfig.ExperienceAttractionArea + upgradeConfig.PickupRange;
            _enemyHealthMultiplier = locationModifiersConfig.EnemyHealthMultiplier;
            _enemySpeedMultiplier = locationModifiersConfig.EnemySpeedMultiplier;
            _enemyDamageMultiplier = locationModifiersConfig.EnemyDamageMultiplier;
            _enemyCountMultiplier = locationModifiersConfig.EnemyCountMultiplier;
        }


        public void ApplyModifiers()
        {
            SpeedMultiplierChanged?.Invoke(_speedMultiplier);
            MaxHealthMultiplierChanged?.Invoke(_maxHealthMultiplier);
            ArmorAdditionChanged?.Invoke(_armorAddition);
            RegenerationChanged?.Invoke(_regeneration);
            RevivesCountChanged?.Invoke(_revivesCount);
            DamageMultiplierChanged?.Invoke(_weaponDamageMultiplier);
            AreaMultiplierChanged?.Invoke(_weaponAreaMultiplier);
            CooldownMultiplierChanged?.Invoke(_weaponCooldownMultiplier);
            EffectDurationMultiplierChanged?.Invoke(_weaponEffectDurationMultiplier);
            ProjectileSpeedMultiplierChanged?.Invoke(_projectileSpeedMultiplier);
            ProjectilesAdditionalChanged?.Invoke(_weaponProjectilesAdditional);
            LuckMultiplierChanged?.Invoke(_luckMultiplier);
            ExpirienceMultiplierChanged?.Invoke(_experienceMultiplier);
            CoinsMultiplierChanged?.Invoke(_coinsMultiplier);
            ExperienceAttractionAreaChanged?.Invoke(_experienceAttractionArea);
            EnemyHealthMultiplierChanged?.Invoke(_enemyHealthMultiplier);
            EnemySpeedMultiplierChanged?.Invoke(_enemySpeedMultiplier);
            EnemyDamageMultiplierChanged?.Invoke(_enemyDamageMultiplier);
            EnemyCountMultiplierChanged?.Invoke(_enemyCountMultiplier);
        }
    }
}