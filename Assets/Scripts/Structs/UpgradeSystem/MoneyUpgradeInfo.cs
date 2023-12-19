using UnityEngine;
using Core.Interface.IUpgrades;

namespace Structs.UpgradeSystem
{
    internal struct MoneyUpgradeInfo : IMoneyUpgrade
    {
        private string _caption;
        private string _description;
        private Sprite _icon;
        private float _probability;
        private int _money;

        public int Money { get => _money; set => _money = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public string Description { get => _description; set => _description = value; }
        public float Probability { get => _probability; set => _probability = value; }
    }
}