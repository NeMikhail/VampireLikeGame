using System;
using System.Collections.Generic;
using Core;
using Core.Interface.IModels;
using Core.Interface.IScriptable;
using Enums;
using UnityEngine;

namespace MVP.Models.PlayerModels
{
    internal sealed class PlayerModel : IPlayerModel
    {
        public event Action SpeedChanged;
        public event Action MaxHealthChanged;
        public event Action HealthChanged;
        public event Action RegenerationChanged;
        public event Action PickupRangeChanged;
        public event Action OnPlayerDeath;
        public event Action<IWeaponModel> OnAddNewWeapon;
        public event Action<IWeaponModel> OnRemoveWeapon;
        public event Action<IWeaponModel> OnUpgradeWeapon;
        public event Action<IWeaponModel> OnAddWeaponSinergy;
        public event Action<IPassiveItemModel> OnAddNewPassiveItem;
        public event Action<IPassiveItemModel> OnRemovePassiveItem;
        public event Action<IPassiveItemModel> OnUpgradePassiveItem;

        private WeaponsName _playerDefaultWeapon;
        private float _playerSpeed;
        private float _playerHealth;
        private float _playerMaxHealth;
        private float _playerArmor;
        private float _playerRegeneration;
        private float _playerExpirienceMultiplier;
        private float _playerExpiriencePickupRange;
        private float _playerRevivesCount;
        private float _playerWeaponDamage;
        private float _playerWeaponArea;
        private float _playerWeaponCooldown;
        private float _playerWeaponProjectileSpeed;
        private float _playerWeaponProjectileDuration;
        private int _playerWeaponProjectileCount;
        private bool _isDead;
        private float _defaultPlayerSpeed;
        private float _defaultPlayerMaxHealth;
        private float _defaultPlayerExpirienceMultiplier;
        private float _defaultPlayerExpiriencePickupRange;
        private List<IWeaponModel> _weaponList;
        private List<IPassiveItemModel> _passiveItemList;

        public WeaponsName PlayerDefaultWeapon
        {
            get { return _playerDefaultWeapon; }
        }
        public float PlayerSpeed
        {
            get { return _playerSpeed; }
            set
            {
                if (_playerSpeed != value)
                {
                    _playerSpeed = value;
                    SpeedChanged?.Invoke();
                }
            }
        }
        public float PlayerHealth
        {
            get { return _playerHealth; }
            set
            {
                if (_playerHealth != value)
                {
                    if (value <= PlayerMaxHealth)
                    {
                        _playerHealth = value;
                    }
                    else
                    {
                        _playerHealth = _playerMaxHealth;
                    }
                    HealthChanged?.Invoke();
                }
            }
        }
        public float PlayerMaxHealth
        {
            get { return _playerMaxHealth; }
            set
            {
                if (_playerMaxHealth != value)
                {
                    _playerMaxHealth = value;
                    MaxHealthChanged?.Invoke();
                }
            }
        }
        public float PlayerArmor
        {
            get
            {
                return (1 - _playerArmor / 10);
            }
            set
            {
                if (_playerArmor != value)
                {
                    _playerArmor = value;
                }

                if (_playerArmor > 9)
                {
                    _playerArmor = 9;
                    Debug.Log("Armor error : current armor > max armor");
                }
            }
        }
        public float PlayerRegeneration
        {
            get { return _playerRegeneration; }
            set
            {
                if (_playerRegeneration != value)
                {
                    _playerRegeneration = value;
                    RegenerationChanged?.Invoke();
                }
            }
        }
        public float PlayerExpirienceMultiplier
        {
            get { return _playerExpirienceMultiplier; }
            set
            {
                if (_playerExpirienceMultiplier != value)
                {
                    _playerExpirienceMultiplier = value;
                }
            }
        }
        public float PlayerExpiriencePickupRange
        {
            get { return _playerExpiriencePickupRange; }
            set
            {
                if (_playerExpiriencePickupRange != value)
                {
                    _playerExpiriencePickupRange = value;
                    PickupRangeChanged?.Invoke();
                }
            }
        }
        public float PlayerRevivesCount
        {
            get { return _playerRevivesCount; }
            set
            {
                if (_playerRevivesCount != value)
                {
                    _playerRevivesCount = value;
                }
            }
        }

        public float PlayerWeaponDamage
        {
            get { return _playerWeaponDamage; }
            set
            {
                if (_playerWeaponDamage != value)
                {
                    _playerWeaponDamage = value;
                }
            }
        }
        public float PlayerWeaponArea
        {
            get { return _playerWeaponArea; }
            set
            {
                if (_playerWeaponArea != value)
                {
                    _playerWeaponArea = value;
                }
            }
        }
        public float PlayerWeaponCooldown
        {
            get { return _playerWeaponCooldown; }
            set
            {
                if (_playerWeaponCooldown != value)
                {
                    _playerWeaponCooldown = value;
                }
            }
        }
        public float PlayerWeaponProjectileSpeed
        {
            get { return _playerWeaponProjectileSpeed; }
            set
            {
                if (_playerWeaponProjectileSpeed != value)
                {
                    _playerWeaponProjectileSpeed = value;
                }
            }
        }
        public float PlayerWeaponProjectileDuration
        {
            get { return _playerWeaponProjectileDuration; }
            set
            {
                if (_playerWeaponProjectileDuration != value)
                {
                    _playerWeaponProjectileDuration = value;
                }
            }
        }
        public int PlayerWeaponProjectileCount
        {
            get { return _playerWeaponProjectileCount; }
            set
            {
                if (_playerWeaponProjectileCount != value)
                {
                    _playerWeaponProjectileCount = value;
                }
            }
        }

        public bool IsDead
        {
            get => _isDead;
            set
            {
                if (_isDead == value)
                    return;
                _isDead = value;
                if (_isDead)
                {
                    OnPlayerDeath?.Invoke();
                }
            }
        }

        public List<IWeaponModel> WeaponList
        {
            get => _weaponList;
            set => _weaponList = value;
        }
        public List<IPassiveItemModel> PassiveItemList
        {
            get => _passiveItemList;
            set => _passiveItemList = value;
        }

        internal PlayerModel(IPlayerConfig playerConfig)
        {
            ICharacterConfig characterConfig = playerConfig.Characters.GetValue(playerConfig.CurrentCharacterName);
            _playerDefaultWeapon = characterConfig.PlayerDefaultWeapon;
            _playerSpeed = characterConfig.PlayerSpeed * ConstantsProvider.GLOBAL_SPEED_MULTIPLER;
            _playerHealth = characterConfig.PlayerHealth;
            _playerMaxHealth = characterConfig.PlayerMaxHealth;
            _playerExpiriencePickupRange = characterConfig.PlayerExpiriencePickupRange;
            _playerExpirienceMultiplier = characterConfig.PlayerExpirienceMultiplier;
            _defaultPlayerExpirienceMultiplier = characterConfig.PlayerExpirienceMultiplier;
            _defaultPlayerExpiriencePickupRange = characterConfig.PlayerExpiriencePickupRange;
            _playerRegeneration = characterConfig.PlayerRegeneration;
            _playerArmor = characterConfig.PlayerArmor;
            _playerRevivesCount = characterConfig.PlayerRevivesCount;
            _playerWeaponDamage = characterConfig.WeaponDamage;
            _playerWeaponArea = characterConfig.WeaponArea;
            _playerWeaponCooldown = characterConfig.WeaponCooldown;
            _playerWeaponProjectileSpeed = characterConfig.WeaponProjectileSpeed;
            _playerWeaponProjectileDuration = characterConfig.WeaponProjectileDuration;
            _playerWeaponProjectileCount = characterConfig.WeaponProjectileAdditional;
            _weaponList = new List<IWeaponModel>();
            _passiveItemList = new List<IPassiveItemModel>();
            _defaultPlayerMaxHealth = _playerMaxHealth;
            _defaultPlayerSpeed = _playerSpeed;
        }


        public void AddWeapon(IWeaponModel weapon)
        {
            if (_weaponList.Contains(weapon))
            {
                OnUpgradeWeapon?.Invoke(weapon);
                return;
            }
            _weaponList.Add(weapon);
            OnAddNewWeapon?.Invoke(weapon);
        }

        public void AddWeaponSinergy(IWeaponModel weapon)
        {
            OnAddWeaponSinergy?.Invoke(weapon);
        }

        public void RemoveWeapon(IWeaponModel weapon)
        {
            _weaponList.Remove(weapon);
            OnRemoveWeapon?.Invoke(weapon);
        }

        public void AddPassiveItem(IPassiveItemModel passiveItem)
        {
            if (_passiveItemList.Contains(passiveItem))
            {
                OnUpgradePassiveItem?.Invoke(passiveItem);
                return;
            }
            _passiveItemList.Add(passiveItem);
            OnAddNewPassiveItem?.Invoke(passiveItem);
        }

        public void RemovePassiveItem(IPassiveItemModel passiveItem)
        {
            _passiveItemList.Remove(passiveItem);
            OnRemovePassiveItem?.Invoke(passiveItem);
        }

        public void ModifySpeed(float modifier)
        {
            PlayerSpeed = _defaultPlayerSpeed * modifier;
        }

        public void ModifyMaxHealth(float modifier)
        {
            PlayerMaxHealth = _defaultPlayerMaxHealth * modifier;
        }

        public void ModifyRegeneration(float modifier)
        {
            PlayerRegeneration = modifier;
        }

        public void ModifyArmor(float modifier)
        {
            float result = _playerArmor + modifier;
            if (result < 10)
                PlayerArmor = _playerArmor + modifier;
        }

        public void ModifyExpirienceMultiplier(float modifier)
        {
            PlayerExpirienceMultiplier = _defaultPlayerExpirienceMultiplier * modifier;
        }

        public void ModifyExpiriencePickupRange (float modifier)
        {
            PlayerExpiriencePickupRange = _defaultPlayerExpiriencePickupRange * modifier;
        }

        public void ModifyRevivesCount(float modifier)
        {
            PlayerRevivesCount = _playerRevivesCount + modifier;
        }

    }
}