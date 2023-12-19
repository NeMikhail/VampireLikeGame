using System.Collections.Generic;
using Structs.UpgradeSystem;
using static Structs.UpgradeSystem.PassiveUpgradeInfo;

namespace Core.Interface.IUpgrades
{
    internal interface IPassiveUpgrade: IUpgradeInfo
    {
        public List<ModifierBlock> MultiplierList { get; }
    }
}