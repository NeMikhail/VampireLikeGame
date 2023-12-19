using Structs.UpgradeSystem;

namespace Core.Interface.IUpgrades
{
    internal interface IMoneyUpgrade : IUpgradeInfo
    {
        public int Money { get; set; }
    }
}