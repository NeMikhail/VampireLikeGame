using System;
using Enums;
using Structs.UpgradeSystem;

namespace Core.Interface.ICollectable
{
    internal interface IAnalyze
    {
        public event Action<IUpgradeInfo[]> OnAppliedUpgradesChanged;
        public event Action<IUpgradeInfo[]> OnAllUpgradesChanged;

        public IUpgradeInfo[] AppliedUpgrades { get; }
        public IUpgradeInfo[] AllUpgrades { get; }
        public void AnalyzeCollectable(CollectableType collectableType);
    }
}