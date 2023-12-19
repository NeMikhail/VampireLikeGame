using System.Collections.Generic;
using Core.Interface.IFacroty;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade.CompositeFactory
{
    internal sealed class PassiveItemsUpgradeFactory : IUpgradeFactory
    {
        private IDataProvider _dataProvider;
        private readonly int _maxUpgrades;

        public PassiveItemsUpgradeFactory(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _maxUpgrades = ConstantsProvider.MAX_UPGRADES_ON_SELECT;
        }


        public List<IUpgradeInfo> GenerateUpgrades()
        {
            var generatedPassiveUpgrades = new List<IUpgradeInfo>(_maxUpgrades);
            var currentUpgrades = new List<IPassiveItemModel>(_dataProvider.PassiveItemModelsList);

            for (int i = 0; i < _dataProvider.PassiveItemModelsList.Count; i++)
            {
                if (currentUpgrades[i].Level < currentUpgrades[i].MaxLevel)
                {
                    IUpgradeInfo newUpgradeInfo = ConvertToUpgradeInfo(currentUpgrades[i]);
                    generatedPassiveUpgrades.Add(newUpgradeInfo);
                }
            }

            currentUpgrades.Clear();

            return generatedPassiveUpgrades;
        }

        private IUpgradeInfo ConvertToUpgradeInfo(IPassiveItemModel passiveItemModel)
        {
            var newUpgradeInfo = new WeaponUpgradeInfo();
            newUpgradeInfo.Caption = passiveItemModel.DisplayName;
            newUpgradeInfo.PassiveItemName = passiveItemModel.Name;
            newUpgradeInfo.Description = passiveItemModel.Description;
            newUpgradeInfo.Icon = passiveItemModel.Icon;
            newUpgradeInfo.Modifiers = passiveItemModel.Modifiers;
            newUpgradeInfo.Probability = passiveItemModel.Probability;
            return newUpgradeInfo;
        }

        public List<IUpgradeInfo> GenerateAppliedUpgrade()
        {
            var generatedPassiveUpgrades = new List<IUpgradeInfo>(_maxUpgrades);

            for (int i = 0; i < _dataProvider.PlayerModel.PassiveItemList.Count; i++)
            {
                if (_dataProvider.PlayerModel.PassiveItemList[i].Level < _dataProvider.PlayerModel.PassiveItemList[i].
                    MaxLevel)
                {
                    IUpgradeInfo newUpgradeInfo = ConvertToUpgradeInfo(_dataProvider.PlayerModel.PassiveItemList[i]);
                    generatedPassiveUpgrades.Add(newUpgradeInfo);
                }
            }

            return generatedPassiveUpgrades;
        }
    }
}