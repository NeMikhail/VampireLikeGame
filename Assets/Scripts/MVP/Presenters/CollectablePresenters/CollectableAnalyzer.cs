using System;
using UnityEngine;
using Core;
using Core.ImprovmentSystem.Upgrade;
using Core.Interface;
using Core.Interface.ICollectable;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.IUpgrades;
using Enums;
using Structs.UpgradeSystem;

namespace MVP.Presenters.CollectablePresenters
{
    internal sealed class CollectableAnalyzer : IAnalyze, IInitialisation, ICleanUp
    {
        private IRunStatisticsSystemModel _runStatisticsSystemModel;
        private IDataProvider _dataProvider;
        private IViewProvider _viewProvider;
        private UpgradeFactory _upgradeFactory;
        private readonly UpgradeConverter _upgradeConverter;
        private IUpgradeInfo[] _appliedUpgrades;
        private IUpgradeInfo[] _allUpgrades;

        public event Action<IUpgradeInfo[]> OnAppliedUpgradesChanged;
        public event Action<IUpgradeInfo[]> OnAllUpgradesChanged;

        public IUpgradeInfo[] AppliedUpgrades => _appliedUpgrades;
        public IUpgradeInfo[] AllUpgrades => _allUpgrades;

        public CollectableAnalyzer(IDataProvider dataProvider, IViewProvider viewProvider, UpgradeFactory upgradeFactory)
        {
            _runStatisticsSystemModel = dataProvider.RunStatisticsModel;
            _dataProvider = dataProvider;
            _viewProvider = viewProvider;
            _upgradeFactory = upgradeFactory;
            _upgradeConverter = new UpgradeConverter(null, _dataProvider);
        }


        public void Initialisation()
        {
            OnAppliedUpgradesChanged += _viewProvider.GetBattleScreen().ShowChestUI;
        }

        public void AnalyzeCollectable(CollectableType collectableType) 
        {
            switch (collectableType)
            {
                case CollectableType.GoldCoin:
                    GotGoldCoin();
                    break;
                case CollectableType.BagOfCoins:
                    GotBagOfCoins();
                    break;
                case CollectableType.BigBagOfCoins:
                    GotBigBagOfCoins();
                    break;
                case CollectableType.CommonChest:
                    GotCommonChest();
                    break;
                case CollectableType.EpicChest:
                    GotEpicChest();
                    break;
                case CollectableType.RareChest:
                    GotRareChest();
                    break;
                case CollectableType.Healing:
                    GotHealing();
                    break;
                default:
                    Debug.Log("Nothing collected");
                    break;
            }
        }

        private void GotGoldCoin()
        {
            _runStatisticsSystemModel.CollectedCoins += 1;
        }

        private void GotBagOfCoins()
        {
            _runStatisticsSystemModel.CollectedCoins += 5;
        }

        private void GotBigBagOfCoins()
        {
            _runStatisticsSystemModel.CollectedCoins += 10;
        }

        private void GotCommonChest()
        {
            _appliedUpgrades = new IUpgradeInfo[1];
            _appliedUpgrades[0] = GetUpgrades();
            if (_appliedUpgrades[0] is not MoneyUpgradeInfo)
            {
                OnAllUpgradesChanged?.Invoke(_allUpgrades);
                OnAppliedUpgradesChanged?.Invoke(_appliedUpgrades);
            }
        }

        private void GotRareChest()
        {
            _appliedUpgrades = new IUpgradeInfo[3];
            for (int i = 0; i < 3; i++)
            {
                _appliedUpgrades[i] = GetUpgrades();
            }
            if (_appliedUpgrades[0] is not MoneyUpgradeInfo)
            {
                OnAllUpgradesChanged?.Invoke(_allUpgrades);
                OnAppliedUpgradesChanged?.Invoke(_appliedUpgrades);
            }
        }

        private void GotEpicChest()
        {
            _appliedUpgrades = new IUpgradeInfo[5];
            for (int i = 0; i < 5; i++)
            {
                _appliedUpgrades[i] = GetUpgrades();
            }
            if (_appliedUpgrades[0] is not MoneyUpgradeInfo)
            {
                OnAllUpgradesChanged?.Invoke(_allUpgrades);
                OnAppliedUpgradesChanged?.Invoke(_appliedUpgrades);
            }
            
        }

        private void GotHealing()
        {
            _dataProvider.PlayerModel.PlayerHealth += ConstantsProvider.HEALING_VALUE;
        }

        private IUpgradeInfo GetUpgrades()
        {
            _allUpgrades = _upgradeFactory.GenerateUpgradesWithSynergy();

            int randomValue = UnityEngine.Random.Range(0, _allUpgrades.Length - 1);
            IWeaponUpgrade synergyWeapon = (_allUpgrades[randomValue] as IWeaponUpgrade);
            if (synergyWeapon != null && synergyWeapon.SynergyWeapon != null)
            {
                _dataProvider.SynergyModel.RemoveSynergy(synergyWeapon.SynergyWeapon);
            }
            _upgradeConverter.Convert(_allUpgrades[randomValue]).Apply();

            return _allUpgrades[randomValue];
        }

        public void Cleanup()
        {
            OnAppliedUpgradesChanged -= _viewProvider.GetBattleScreen().ShowChestUI;
        }
    }
}