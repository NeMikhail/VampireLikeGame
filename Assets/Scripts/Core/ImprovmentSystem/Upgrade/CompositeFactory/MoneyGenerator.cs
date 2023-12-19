using System.Collections.Generic;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade.CompositeFactory
{
    internal sealed class MoneyGenerator
    {
        private const int MAX_GANARATED_MONEY = 50;


        public void NormalizeUpgrades(ref List<IUpgradeInfo> upgradeInfos)
        {
            int money = UnityEngine.Random.Range(0, MAX_GANARATED_MONEY);
            var moneyStruct = new MoneyUpgradeInfo();
            
            moneyStruct.Caption = money + " Coins";
            moneyStruct.Money = money;
            moneyStruct.Icon = null;
            
            upgradeInfos.Add(moneyStruct);
        }
    }
}