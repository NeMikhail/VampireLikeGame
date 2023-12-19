using System.Collections.Generic;
using Core.ImprovmentSystem.Upgrade.ConcreteUpgrades;
using Core.Interface.IPresenters;
using Core.Interface.IUpgrades;
using Enums;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade
{
    internal sealed class UpgradeConverter
    {       
        private readonly PlayerStatsChanger _playerStatsChanger;
        private readonly IDataProvider _dataProvider;

        public UpgradeConverter(PlayerStatsChanger playerStatsChanger, IDataProvider dataProvider)
        {
            _playerStatsChanger = playerStatsChanger;
            _dataProvider = dataProvider;
        }
        

        public IUpgrade Convert(IUpgradeInfo upgradeInfo)
        {
            var weaponUpgrade = upgradeInfo as IWeaponUpgrade;
            var moneyUpgrade = upgradeInfo as IMoneyUpgrade;

            if (weaponUpgrade != null)
            {
                if (weaponUpgrade.SynergyWeapon != null)
                {
                    return new SynergyWeaponUpgrade(weaponUpgrade.SynergyWeapon, weaponUpgrade.Caption);
                }

                if (weaponUpgrade.PassiveItemName != PassiveItemsName.None)
                {
                    int multipliersCount = weaponUpgrade.Modifiers.Count;

                    var modifierUpgrades = new List<ModifierUpgrade>();

                    for (var i = 0; i < multipliersCount; i++)
                    {
                        ModifierType modifier = weaponUpgrade.Modifiers[i].Modifier;
                        float deltaMultiplier = weaponUpgrade.Modifiers[i].DeltaMultiplier;
                        modifierUpgrades.Add(new ModifierUpgrade(_playerStatsChanger, modifier, deltaMultiplier));
                    }

                    return new PassiveItemUpgrade(weaponUpgrade.PassiveItemName, _dataProvider, weaponUpgrade.Caption, modifierUpgrades);
                }

                if (weaponUpgrade.Weapon != WeaponsName.None)
                {
                    var weaponUpgrade1 = new WeaponUpgrade(_dataProvider, weaponUpgrade.Weapon);
                    return new UniversalUpgrade(weaponUpgrade.Caption, weaponUpgrade1);
                }
            }

            if (moneyUpgrade != null)
            {
                return new MoneyUpgrade(_dataProvider, moneyUpgrade);
            }

            return null;
        }
    }
}