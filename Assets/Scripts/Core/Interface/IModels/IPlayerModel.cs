using System;
using System.Collections.Generic;
using Enums;

namespace Core.Interface.IModels
{
    public interface IPlayerModel : IModel, IModifyPlayer
    {
        public event Action HealthChanged;
        public event Action MaxHealthChanged;
        public event Action OnPlayerDeath;
        public event Action RegenerationChanged;
        public event Action PickupRangeChanged;
        public event Action<IWeaponModel> OnAddNewWeapon;
        public event Action<IWeaponModel> OnRemoveWeapon;
        public event Action<IWeaponModel> OnUpgradeWeapon;
        public event Action<IWeaponModel> OnAddWeaponSinergy;
        public event Action<IPassiveItemModel> OnAddNewPassiveItem;
        public event Action<IPassiveItemModel> OnRemovePassiveItem;
        public event Action<IPassiveItemModel> OnUpgradePassiveItem;

        public WeaponsName PlayerDefaultWeapon { get; }
        public float PlayerSpeed { get; set; }
        public float PlayerHealth{ get; set; }
        public float PlayerMaxHealth { get; set; }
        public float PlayerArmor{ get; set; }
        public float PlayerRegeneration { get; set; }
        public float PlayerExpirienceMultiplier { get; set; }
        public float PlayerExpiriencePickupRange { get; set; }
        public float PlayerRevivesCount { get; set; }
        public float PlayerWeaponDamage { get; set; }
        public float PlayerWeaponArea { get; set; }
        public float PlayerWeaponCooldown { get; set; }
        public float PlayerWeaponProjectileSpeed { get; set; }
        public float PlayerWeaponProjectileDuration { get; set; }
        public int PlayerWeaponProjectileCount { get; set; }

        public bool IsDead{ get; set; }
        public List<IWeaponModel> WeaponList { get; set;}
        public List<IPassiveItemModel> PassiveItemList { get; set; }

        public void AddWeapon(IWeaponModel weapon);
        public void RemoveWeapon(IWeaponModel weapon);
        public void AddPassiveItem(IPassiveItemModel passiveItem);
        public void RemovePassiveItem(IPassiveItemModel passiveItem);
        public void AddWeaponSinergy(IWeaponModel weapon);
    }
}