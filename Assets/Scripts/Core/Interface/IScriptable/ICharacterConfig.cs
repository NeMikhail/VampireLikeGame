using Enums;
using UnityEngine;

namespace Core.Interface.IScriptable
{
    public interface ICharacterConfig
    {
        public WeaponsName PlayerDefaultWeapon { get; }
        public GameObject PlayerPrefab { get; }
        public float PlayerSpeed { get; }
        public float PlayerMaxHealth { get; }
        public float PlayerHealth { get; }
        public float PlayerExpirienceMultiplier { get; }
        public float PlayerExpiriencePickupRange { get; }
        public float PlayerRegeneration { get; }
        public float PlayerArmor { get; }
        public float PlayerRevivesCount { get; }

        public float WeaponDamage { get; }
        public float WeaponArea { get; }
        public float WeaponCooldown { get; }
        public float WeaponProjectileSpeed { get; }
        public float WeaponProjectileDuration { get; }
        public int WeaponProjectileAdditional { get; }
    }
}