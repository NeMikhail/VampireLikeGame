using Configs;
using Core.ResourceLoader;

namespace UI
{
    internal sealed class UIResetUpgrades
    {
        private UpgradeConfigPack _upgradeConfigPack;
        private UpgradeModifiersConfig _modifiersConfig;
        public int CostAllUpgrades { get; private set; }

        public UIResetUpgrades()
        {
            _upgradeConfigPack = ResourceLoadManager.GetConfig<UpgradeConfigPack>();
            _modifiersConfig = ResourceLoadManager.GetConfig<UpgradeModifiersConfig>();
            CostAllUpgrades = _modifiersConfig.CostAllUpgrades;
        }


        public void ResetUpgrades()
        {
            foreach (UpgradeConfig upgradeConfig in _upgradeConfigPack.UpgradeConfigs)
            {
                upgradeConfig.CurrentLevel = 1;
                upgradeConfig.UpgradeCost = upgradeConfig.BaseUpgradeCost;
            }

            _modifiersConfig.CostAllUpgrades = 0;

            _modifiersConfig.SpeedMultiplier = 0;
            _modifiersConfig.MaxHealthMultiplier = 0;
            _modifiersConfig.ArmorAddition = 0;
            _modifiersConfig.Regeneration = 0;
            _modifiersConfig.PickupRange = 0;
            _modifiersConfig.WeaponDamageMultiplier = 0;
            _modifiersConfig.WeaponAreaMultiplier = 0;
            _modifiersConfig.WeaponReloadMultiplier = 0;
            _modifiersConfig.WeaponEffectDurationMultiplier = 0;
            _modifiersConfig.ProjectileSpeedMultiplier = 0;
            _modifiersConfig.WeaponProjectilesAdditional = 0;
            _modifiersConfig.LuckMultiplier = 0;
            _modifiersConfig.ExperienceMultiplier = 0;
            _modifiersConfig.RevivesCount = 0;
        }
    }
}