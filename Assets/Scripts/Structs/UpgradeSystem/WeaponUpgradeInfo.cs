using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IModels;
using Core.Interface.IUpgrades;
using Enums;
using MVP.Models.PassiveItemModels;

namespace Structs.UpgradeSystem
{
    internal struct WeaponUpgradeInfo: IWeaponUpgrade
    {
        private string _caption;
        private string _description;
        private Sprite _icon;
        private WeaponsName _weapon;
        private int _weaponLevel;
        private float _probability;
        private List<PassiveBlock> _modifiers;

        private PassiveItemsName _passiveItemName;
        private IWeaponModel _synergyWeapon;
		
        public string Caption { get => _caption; set => _caption = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
        public string Description { get => _description; set => _description = value; }
        public float Probability { get => _probability; set => _probability = value; }
        public WeaponsName Weapon { get => _weapon; set => _weapon = value; }
        public int WeaponLevel { get => _weaponLevel; set => _weaponLevel = value; } 
        public PassiveItemsName PassiveItemName {get => _passiveItemName; set => _passiveItemName = value; }
        public IWeaponModel SynergyWeapon { get => _synergyWeapon; set => _synergyWeapon = value; }
        public List<PassiveBlock> Modifiers { get => _modifiers; set => _modifiers = value; }
    }
}