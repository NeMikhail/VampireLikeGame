using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(UpgradeModifiersConfig), menuName = "Configs/Upgrade/" +
        nameof(UpgradeModifiersConfig))]
    internal sealed class UpgradeModifiersConfig : ScriptableObject
    {
        [Header("Modifiers")]
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private float _maxHealthMultiplier;
        [SerializeField] private float _armorAddition;
        [SerializeField] private float _regeneration;
        [SerializeField] private float _pickupRange;
        [SerializeField] private float _weaponDamageMultiplier;
        [SerializeField] private float _weaponAreaMultiplier;
        [SerializeField] private float _weaponReloadMultiplier;
        [SerializeField] private float _weaponEffectDurationMultiplier;
        [SerializeField] private float _projectileSpeedMultiplier;
        [SerializeField] private int _weaponProjectilesAdditional;
        [SerializeField] private float _luckMultiplier;
        [SerializeField] private float _experienceMultiplier;
        [SerializeField] private int _revival;

        [Header("Coins")]
        [SerializeField] private int _costAllUpgrades;

        public float SpeedMultiplier
        {
            get => _speedMultiplier;
            set
            {
                _speedMultiplier = value;
            }
        }
        public float MaxHealthMultiplier
        {
            get => _maxHealthMultiplier;
            set
            {
                _maxHealthMultiplier = value;
            }
        }
        public float ArmorAddition
        {
            get => _armorAddition;
            set
            {
                _armorAddition = value;
            }
        }
        public float Regeneration
        {
            get => _regeneration;
            set
            {
                _regeneration = value;
            }
        }

        public float PickupRange
        {
            get => _pickupRange;
            set
            {
                _pickupRange = value;
            }
        }
        public float WeaponDamageMultiplier
        {
            get => _weaponDamageMultiplier;
            set
            {
                _weaponDamageMultiplier = value;
            }
        }
        public float WeaponAreaMultiplier
        {
            get => _weaponAreaMultiplier;
            set
            {
                _weaponAreaMultiplier = value;
            }
        }
        public float WeaponReloadMultiplier
        {
            get => _weaponReloadMultiplier;
            set
            {
                _weaponReloadMultiplier = value;
            }
        }
        public float WeaponEffectDurationMultiplier
        {
            get => _weaponEffectDurationMultiplier;
            set
            {
                _weaponEffectDurationMultiplier = value;
            }
        }
        public float ProjectileSpeedMultiplier
        {
            get => _projectileSpeedMultiplier;
            set
            {
                _projectileSpeedMultiplier = value;
            }
        }
        public int WeaponProjectilesAdditional
        {
            get => _weaponProjectilesAdditional;
            set
            {
                _weaponProjectilesAdditional = value;
            }
        }
        public float LuckMultiplier
        {
            get => _luckMultiplier;
            set
            {
                _luckMultiplier = value;
            }
        }
        public float ExperienceMultiplier
        {
            get => _experienceMultiplier;
            set
            {
                _experienceMultiplier = value;
            }
        }

        public int RevivesCount
        {
            get => _revival;
            set 
            {
                _revival = value;
            }
        }

        public int CostAllUpgrades
        {
            get => _costAllUpgrades;
            set
            {
                _costAllUpgrades = value;
            }
        }
    }
}