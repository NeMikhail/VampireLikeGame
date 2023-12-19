using System;
using UnityEngine;
using Core.Interface.IScriptable;
using Enums;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/Player/CharacterScriptableConfig",
        order = 2)]
    public sealed class CharacterConfig : ScriptableObject, ICharacterConfig
    {
        [SerializeField] private bool _isUnlocked;
        [SerializeField] private bool _isCharachterOpen;
        [SerializeField] private int _characterCost;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private string _characterName;
        [SerializeField] private Sprite _characterImage;
        [SerializeField] private WeaponsName _playerDefaultWeapon;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _playerMaxHealth;
        [SerializeField] private float _playerHealth;
        [SerializeField] private float _playerArmor;
        [SerializeField] private float _playerRegeneration;
        [SerializeField] private float _playerExpirienceMultiplier;
        [SerializeField] private float _playerExpiriencePickupRange;
        [SerializeField] private float _playerRevivesCount;

        [SerializeField] private float _weaponDamage;
        [SerializeField] private float _weaponArea;
        [SerializeField] private float _weaponCooldown;
        [SerializeField] private float _weaponProjectileSpeed;
        [SerializeField] private float _weaponProjectileDuration;
        [SerializeField] private int _weaponProjectileAdditional;

        public event Action<bool> OnUnlockedStatusChanged;

        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                _isUnlocked = value;
                OnUnlockedStatusChanged?.Invoke(value);
            }
        }
        public bool IsCharachterOpen
        {
            get => _isCharachterOpen;
            set => _isCharachterOpen = value;
        }
        public int CharacterCost => _characterCost;
        public GameObject PlayerPrefab => _playerPrefab;
        public Sprite CharacterImage => _characterImage;
        public string CharacterName => _characterName;
        public WeaponsName PlayerDefaultWeapon => _playerDefaultWeapon;
        public float PlayerSpeed => _playerSpeed;
        public float PlayerMaxHealth => _playerMaxHealth;
        public float PlayerHealth => _playerHealth;
        public float PlayerArmor => _playerArmor;
        public float PlayerRegeneration => _playerRegeneration;
        public float PlayerExpirienceMultiplier => _playerExpirienceMultiplier;
        public float PlayerExpiriencePickupRange => _playerExpiriencePickupRange;
        public float PlayerRevivesCount => _playerRevivesCount;
        public float WeaponDamage => _weaponDamage;
        public float WeaponArea => _weaponArea;
        public float WeaponCooldown => _weaponCooldown;
        public float WeaponProjectileSpeed => _weaponProjectileSpeed;
        public float WeaponProjectileDuration => _weaponProjectileDuration;
        public int WeaponProjectileAdditional => _weaponProjectileAdditional;
    }
}