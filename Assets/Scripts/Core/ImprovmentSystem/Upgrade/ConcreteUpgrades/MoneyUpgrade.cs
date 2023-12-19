using Core.Interface.IPresenters;
using Core.Interface.IUpgrades;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    internal struct MoneyUpgrade : IUpgrade
    {
        public string Name { get; }
        private IMoneyUpgrade _moneyUpgrade;
        private readonly IDataProvider _dataProvider;

        public MoneyUpgrade(IDataProvider dataProvider, IMoneyUpgrade moneyUpgrade)
        {
            Name = moneyUpgrade.Caption;
            _moneyUpgrade = moneyUpgrade;
            _dataProvider = dataProvider;
        }


        public void Apply()
        {
            _dataProvider.RunStatisticsModel.CollectedCoins += _moneyUpgrade.Money;
        }
    }
}