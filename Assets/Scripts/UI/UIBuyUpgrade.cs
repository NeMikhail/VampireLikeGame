using Configs;
using Core.ResourceLoader;
using Enums;
using UnityEngine;

namespace UI
{
    internal sealed class UIBuyUpgrade
    {
        private UpgradeConfig _upgradeConfig;
        private UpgradeModifiersConfig _modifiersConfig;

        public UIBuyUpgrade(UpgradeConfig upgradeConfig)
        {
            _upgradeConfig = upgradeConfig;
            _modifiersConfig = ResourceLoadManager.GetConfig<UpgradeModifiersConfig>();

            BuyUpgrade();
        }


        private void BuyUpgrade()
        {
            if (_upgradeConfig == null)
                return;

            _modifiersConfig.CostAllUpgrades += (int)(_upgradeConfig.BaseUpgradeCost + (_upgradeConfig.UpgradeCostMultiplier * (_upgradeConfig.CurrentLevel - 1) * _upgradeConfig.BaseUpgradeCost));
            switch (_upgradeConfig.ModifierType)
            {
                case ModifierType.PlayerSpeed:
                    _modifiersConfig.SpeedMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerHealth:
                    _modifiersConfig.MaxHealthMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerArmor:
                    _modifiersConfig.ArmorAddition += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerRegeneration:
                    _modifiersConfig.Regeneration += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerPickupRange:
                    _modifiersConfig.PickupRange += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.BaseDamage:
                    _modifiersConfig.WeaponDamageMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.WeaponArea:
                    _modifiersConfig.WeaponAreaMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.WeaponCooldown:
                    _modifiersConfig.WeaponReloadMultiplier -= _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.ProjectileDuration:
                    _modifiersConfig.WeaponEffectDurationMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.ProjectileSpeed:
                    _modifiersConfig.ProjectileSpeedMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.ProjectileAdditional:
                    _modifiersConfig.WeaponProjectilesAdditional += (int)_upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerLuck:
                    _modifiersConfig.LuckMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerExperienceGrowth:
                    _modifiersConfig.ExperienceMultiplier += _upgradeConfig.AdditionModifierPerLevel;
                    break;
                case ModifierType.PlayerRevivesCount:
                    _modifiersConfig.RevivesCount += (int)_upgradeConfig.AdditionModifierPerLevel;
                    break;
                default:
                    break;
            }
        }
    }
}