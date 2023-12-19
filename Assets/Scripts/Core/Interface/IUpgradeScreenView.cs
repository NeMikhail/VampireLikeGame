using System;
using Structs.UpgradeSystem;

namespace Core.Interface
{
    internal interface IUpgradeScreenView
    {
        public event Action<int> OnUpgradeSelect;
        public void SetUpgrades(IUpgradeInfo[] upgrades);
    }
}