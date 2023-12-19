using System;
using System.Collections.Generic;
using Core.ImprovmentSystem.Upgrade.CompositeFactory;
using Core.Interface.IFacroty;
using Core.Interface.IPresenters;
using Structs.UpgradeSystem;

namespace Core.ImprovmentSystem.Upgrade
{
    internal sealed class UpgradeFactory
    {
        private List<IUpgradeFactory> _upgradeFactories;
        private readonly int _maxUpgrades = ConstantsProvider.MAX_UPGRADES_ON_SELECT;
        private MoneyGenerator _moneyGenerator;

        private List<IUpgradeInfo> _shuffledProbabilityList;
        private List<IUpgradeInfo> _sameProbabilityList;

        public UpgradeFactory(IDataProvider dataProvider)
        {
            _moneyGenerator = new MoneyGenerator();
            _upgradeFactories = new List<IUpgradeFactory>
            {
                new WeaponUpgradeFactory(dataProvider),
                new PassiveItemsUpgradeFactory(dataProvider),
                new SynergyFactory(dataProvider.SynergyModel)
            };
            _shuffledProbabilityList = new List<IUpgradeInfo>();
            _sameProbabilityList = new List<IUpgradeInfo>();
        }


        public IUpgradeInfo[] GenerateUpgrades()
        {        
            var generatedUpgrades = new List<IUpgradeInfo>();
          
            for(int i = 0; i < _upgradeFactories.Count - 1; i++)
            {
                generatedUpgrades.AddRange(_upgradeFactories[i].GenerateUpgrades());
            }

            List<IUpgradeInfo> curerentUpgrades = GenerateUpgradesWithReference(generatedUpgrades);

            generatedUpgrades.Clear();

            if(curerentUpgrades.Count == 0)
            {
                _moneyGenerator.NormalizeUpgrades(ref curerentUpgrades);
            }
            
            return curerentUpgrades.ToArray();
        }

        private List<IUpgradeInfo> GenerateUpgradesWithReference(List<IUpgradeInfo> upgradeInfos)
        {
            upgradeInfos.Sort(new Comparison<IUpgradeInfo>((x, y) => x.Probability.CompareTo(y.Probability)));

            ShuffleByProbability(ref upgradeInfos);

            var upgradesToPlayer = new List<IUpgradeInfo>();

            float sum = GetSumOfProbability(upgradeInfos);
            NormalizeProbability(ref upgradeInfos, sum);
            AccumulateProbability(ref upgradeInfos);

            for (int i = upgradeInfos.Count - 1; i >= 0; i--)
            {
                if (upgradeInfos[i].Probability > 0)
                    upgradesToPlayer.Add(upgradeInfos[i]);
            }

            if (upgradesToPlayer.Count < _maxUpgrades)
            {
                return upgradesToPlayer;
            }

            return upgradesToPlayer.GetRange(0, _maxUpgrades);
        }

        private void ShuffleByProbability(ref List<IUpgradeInfo> list)
        {
            _shuffledProbabilityList.Clear();
            List<float> uniqueProbabilityList = CreateUniqueProbabilityList(list);

            for(int uniqueProbability = 0; uniqueProbability < uniqueProbabilityList.Count; uniqueProbability++)
            {
                _sameProbabilityList.Clear();
                for(int upgrade = 0; upgrade < list.Count; upgrade++)
                {
                    if (list[upgrade].Probability == uniqueProbabilityList[uniqueProbability])
                        _sameProbabilityList.Add(list[upgrade]);
                }
                _shuffledProbabilityList.AddRange(ShuffleSimilarProbabilities(_sameProbabilityList));
            }
            list = _shuffledProbabilityList;
        }

        private List<float> CreateUniqueProbabilityList(List<IUpgradeInfo> list)
        {
            List<float> uniqueProbabilityList = new List<float>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!uniqueProbabilityList.Contains(list[i].Probability))
                    uniqueProbabilityList.Add(list[i].Probability);
            }

            return uniqueProbabilityList;
        }

        private List<IUpgradeInfo> ShuffleSimilarProbabilities(List<IUpgradeInfo> list)
        {
            var random = new Random();
            int listCount = list.Count;
            List<IUpgradeInfo> shuffledList = new List<IUpgradeInfo>();

            for (int i = 0; i < listCount; i++)
            {
                var randomElement = random.Next(0, list.Count);
                shuffledList.Add(list[randomElement]);
                list.Remove(list[randomElement]);
            }

            return shuffledList;
        }

        private void AccumulateProbability(ref List<IUpgradeInfo> upgradeInfos)
        {
            for (int i = 1; i < upgradeInfos.Count; i++)
            {
                upgradeInfos[i].Probability += upgradeInfos[i - 1].Probability;
            }
        }

        private void NormalizeProbability(ref List<IUpgradeInfo> upgradeInfos, float sumOfProbability)
        {
            for(int i = 0; i < upgradeInfos.Count;i++)
            {
                upgradeInfos[i].Probability /= sumOfProbability;
            }
        }

        private float GetSumOfProbability(List<IUpgradeInfo> upgradeInfos)
        {
            float sum = 0f;
            for (int i = 0; i < upgradeInfos.Count; i++)
            {
                sum += upgradeInfos[i].Probability;
            }
            return sum;
        }

        public List<IUpgradeInfo> GenerateAllUpgrades()
        {
            var generatedUpgrades = new List<IUpgradeInfo>();

            for (int i = 0; i < _upgradeFactories.Count - 1; i++)
            {
                generatedUpgrades.AddRange(_upgradeFactories[i].GenerateAppliedUpgrade());
            }

            return generatedUpgrades;
        }

        public IUpgradeInfo[] GenerateUpgradesWithSynergy()
        {
            var generatedUpgrades = new List<IUpgradeInfo>(_upgradeFactories[_upgradeFactories.Count-1].
                GenerateAppliedUpgrade());
            if(generatedUpgrades.Count > 0) 
            {
                return generatedUpgrades.ToArray();
            }
            generatedUpgrades = GenerateAllUpgrades();

            if(generatedUpgrades.Count == 0)
            {
                _moneyGenerator.NormalizeUpgrades(ref generatedUpgrades);
            }

            return generatedUpgrades.ToArray();
        }
    }
}