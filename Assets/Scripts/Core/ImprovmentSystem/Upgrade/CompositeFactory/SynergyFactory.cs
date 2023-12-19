using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IFacroty;
using Core.Interface.IModels;
using Core.Interface.ISynergy;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade.CompositeFactory
{
    internal sealed class SynergyFactory : IUpgradeFactory
    {
        private ISynergyModel _synergyModel;
        private readonly int _maxUpgrades;

        public SynergyFactory(ISynergyModel synergyModel)
        {
            _synergyModel = synergyModel;
            _maxUpgrades = ConstantsProvider.MAX_UPGRADES_ON_SELECT;
        }


        public List<IUpgradeInfo> GenerateUpgrades()
        {
            var generatedSynergies = new List<IUpgradeInfo>();
            var currentUpgrades = new List<IWeaponModel>(_synergyModel.ActiveSynergies);
            int randomValue;

            if (currentUpgrades.Count <= 0)
            {
                return generatedSynergies;
            }

            for (int i = 0; i < _maxUpgrades; i++)
            {
                randomValue = Random.Range(0, currentUpgrades.Count - 1);

                IUpgradeInfo newUpgradeInfo = ConvertToUpgradeInfo(currentUpgrades[randomValue]);
                generatedSynergies.Add(newUpgradeInfo);
                currentUpgrades.Remove(currentUpgrades[randomValue]);
            }

            currentUpgrades.Clear();

            return generatedSynergies;
        }

        private IUpgradeInfo ConvertToUpgradeInfo(IWeaponModel synergyWeaponModel)
        {
            var newUpgradeInfo = new WeaponUpgradeInfo();
            newUpgradeInfo.Caption = synergyWeaponModel.DisplayName;
            newUpgradeInfo.SynergyWeapon = synergyWeaponModel;

            return newUpgradeInfo;
        }

        public List<IUpgradeInfo> GenerateAppliedUpgrade()
        {
            var generatedSynergies = new List<IUpgradeInfo>();

            if (_synergyModel.ActiveSynergies.Count <= 0)
            {
                return generatedSynergies;
            }

            for (int i = 0; i < _synergyModel.ActiveSynergies.Count; i++)
            {
                IUpgradeInfo newUpgradeInfo = ConvertToUpgradeInfo(_synergyModel.ActiveSynergies[i]);
                generatedSynergies.Add(newUpgradeInfo);
            }

            return generatedSynergies;
        }
    }
}