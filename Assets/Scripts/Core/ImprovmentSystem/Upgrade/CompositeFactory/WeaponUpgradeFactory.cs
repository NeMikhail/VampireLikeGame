using System.Collections.Generic;
using Core.Interface.IFacroty;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade.CompositeFactory
{
    internal sealed class WeaponUpgradeFactory : IUpgradeFactory
    {
        private readonly IDataProvider _dataprovider;
        private readonly int _maxUpgrades;

        public WeaponUpgradeFactory(IDataProvider dataprovider)
        {
            _dataprovider = dataprovider;
            _maxUpgrades = ConstantsProvider.MAX_UPGRADES_ON_SELECT;
        }


        public List<IUpgradeInfo> GenerateUpgrades()
        {
            var generatingWeaponUpgrades = new List<IUpgradeInfo>(_maxUpgrades);
            var currentUpgrades = new List<IWeaponModel>(_dataprovider.WeaponModelsList);

            for (int i = 0; i < _dataprovider.WeaponModelsList.Count; i++)
            {
                if (currentUpgrades[i].Level < currentUpgrades[i].MaxLevel)
                {
                    generatingWeaponUpgrades.Add(GetWeaponUpgradeInfo(currentUpgrades[i]));
                }
            }

            currentUpgrades.Clear();

            return generatingWeaponUpgrades;
        }

        private IUpgradeInfo GetWeaponUpgradeInfo(IWeaponModel weaponModel)
        {
            var weaponUpgradeInfo = new WeaponUpgradeInfo();
            weaponUpgradeInfo.Caption = weaponModel.DisplayName;
            weaponUpgradeInfo.Weapon = weaponModel.Name;
            weaponUpgradeInfo.Description = weaponModel.Description;
            weaponUpgradeInfo.Icon = weaponModel.Icon;
            weaponUpgradeInfo.Probability = weaponModel.Probability;
            weaponUpgradeInfo.PassiveItemName = Enums.PassiveItemsName.None;
            return weaponUpgradeInfo;
        }

        public List<IUpgradeInfo> GenerateAppliedUpgrade()
        {
            var generatingWeaponUpgrades = new List<IUpgradeInfo>(_maxUpgrades);
            for (int i = 0; i < _dataprovider.PlayerModel.WeaponList.Count; i++)
            {
                if (_dataprovider.PlayerModel.WeaponList[i].Level < _dataprovider.PlayerModel.WeaponList[i].MaxLevel)
                {
                    generatingWeaponUpgrades.Add(GetWeaponUpgradeInfo(_dataprovider.PlayerModel.WeaponList[i]));
                }
            }

            return generatingWeaponUpgrades;
        }
    }
}