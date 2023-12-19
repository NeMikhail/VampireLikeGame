using Enums;
using UnityEngine;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    public struct ModifierUpgrade : IUpgrade
    {
        private readonly PlayerStatsChanger _upgradable;
        private readonly ModifierType _modifierType;
        private readonly float _multiplier;

        public ModifierType ModifierType => _modifierType;
        public float Multiplier => _multiplier;
        public string Name { get; }

        #region Constructors
        
        public ModifierUpgrade(PlayerStatsChanger upgradable)
        {
            Name = string.Empty;
            _upgradable = upgradable;
            _modifierType = ModifierType.None;
            _multiplier = 0;
        }

        public ModifierUpgrade(PlayerStatsChanger upgradable, ModifierType upgradeType, float delta)
        {
            Name = string.Empty;
            _upgradable = upgradable;
            _modifierType = upgradeType;
            _multiplier = delta;
        }

        public ModifierUpgrade(string name, PlayerStatsChanger upgradable)
        {
            Name = name;
            _upgradable = upgradable;
            _modifierType = ModifierType.None;
            _multiplier = 0;
        }

        public ModifierUpgrade(string name, PlayerStatsChanger upgradable, ModifierType upgradeType, float delta)
        {
            Name = name;
            _upgradable = upgradable;
            _modifierType = upgradeType;
            _multiplier = delta;
        }

        #endregion


        public void Apply()
        {
            if(_upgradable == null) 
                return;
            switch (_modifierType)
            {                       
                case ModifierType.PlayerSpeed:
                    _upgradable.ChangeSpeed(_multiplier);
                    break;
                case ModifierType.BaseDamage:
                    _upgradable.ChangeDamage(_multiplier);
                    break;
                case ModifierType.WeaponArea:
                    _upgradable.ChangeArea(_multiplier);
                    break;
                case ModifierType.WeaponCooldown:
                    _upgradable.ChangeWeaponCooldown(_multiplier);
                    break;
                case ModifierType.ProjectileSpeed:
                    _upgradable.ChangeProjectileSpeed(_multiplier);
                    break;
                case ModifierType.ProjectileDuration:
                    _upgradable.ChangeProjectileDuration(_multiplier);
                    break;
                case ModifierType.ProjectileAdditional:
                    _upgradable.ChangeProjectileCount((int)_multiplier);
                    break;
                case ModifierType.PlayerHealth:
                    _upgradable.ChangeMaxHealth(_multiplier);
                    break;
                case ModifierType.PlayerArmor:
                    _upgradable.ChangeArmor(_multiplier);
                    break;
                case ModifierType.PlayerRegeneration:
                    _upgradable.ChangeRegeneration(_multiplier);
                    break;
                case ModifierType.PlayerLuck:
                    _upgradable.ChangeLuck(_multiplier);
                    break;
                case ModifierType.PlayerExperienceGrowth:
                    _upgradable.ChangeExpirienceGrowth(_multiplier);
                    break;
                case ModifierType.PlayerPickupRange:
                    _upgradable.ChangeExpiriencePickupRange(_multiplier);
                    break;
                case ModifierType.PlayerRevivesCount:
                    _upgradable.ChangeRevivesCount(_multiplier);
                    break;
                default:
                    break;
            }           
        }
    }
}