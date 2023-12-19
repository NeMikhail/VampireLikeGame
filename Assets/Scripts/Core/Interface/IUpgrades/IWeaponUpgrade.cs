using System.Collections.Generic;
using Core.Interface.IModels;
using Enums;
using MVP.Models.PassiveItemModels;
using Structs.UpgradeSystem;

namespace Core.Interface.IUpgrades
{
    internal interface IWeaponUpgrade : IUpgradeInfo
    {
        public WeaponsName Weapon { get; }
        public int WeaponLevel { get; set; }
        public PassiveItemsName PassiveItemName { get; set; }
        public IWeaponModel SynergyWeapon { get; set; }
        public List<PassiveBlock> Modifiers { get; set; }
    }
}