using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Enums;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    internal struct PassiveItemUpgrade : IUpgrade
    {
        public string Name { get; }
        private PassiveItemsName _passiveItemName;
        private readonly IDataProvider _dataProvider;
        private List<ModifierUpgrade> _modifierUpgrades;

        public PassiveItemUpgrade(PassiveItemsName passiveItem, IDataProvider dataProvider, string name,
            List<ModifierUpgrade> modifierUpgrades)
        {
            Name = name;
            _passiveItemName = passiveItem;
            _dataProvider = dataProvider;
            _modifierUpgrades = modifierUpgrades;
        }


        public void Apply()
        {
            IPassiveItemModel passiveItem = GetPassiveItemModel(_passiveItemName);
            _dataProvider.PlayerModel.AddPassiveItem(passiveItem);
            for (var i = 0; i < _modifierUpgrades.Count; i++)
                _modifierUpgrades[i].Apply();
        }

        private IPassiveItemModel GetPassiveItemModel(PassiveItemsName passiveItemName)
        {
            return _dataProvider.PassiveItemModelsList.Find(element => element.Name == passiveItemName);
        }
    }
}