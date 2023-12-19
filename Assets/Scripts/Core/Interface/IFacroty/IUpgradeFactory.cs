using System.Collections.Generic;
using Structs.UpgradeSystem;

namespace Core.Interface.IFacroty
{
    internal interface IUpgradeFactory
    {
        public List<IUpgradeInfo> GenerateUpgrades();
        public List<IUpgradeInfo> GenerateAppliedUpgrade();
    }
}