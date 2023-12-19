using System.Collections.Generic;
using Core.Interface.IUpgrades;
using Structs.UpgradeSystem;

namespace Core.Interface.IUpgradeInfoModels
{
    internal interface IUpgradeInfoModel
    {
        public List<IPassiveUpgrade> PassiveUpgrades { get; }
        public int PassiveUpgradesCount { get; }

        public void RemovePassiveUpgrade(IUpgradeInfo upgradeInfo);
        public void AddPassiveUpgrade(IUpgradeInfo upgradeInfo);
    }
}