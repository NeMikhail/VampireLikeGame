using System.Collections.Generic;
using UnityEngine;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    public struct UniversalUpgrade : IUpgrade
    {
        public string Name { get; }
        
        private readonly List<ModifierUpgrade> _modifierUpgrades;
        private readonly WeaponUpgrade _weaponUpgrade;

        public List<ModifierUpgrade> ModifiersUpgrade => _modifierUpgrades;
        public WeaponUpgrade WeaponUpgrade => _weaponUpgrade;

        #region Constructors

        public UniversalUpgrade(string name, List<ModifierUpgrade> modifierUpgrades)
        {
            Name = name;
            _modifierUpgrades = modifierUpgrades;
            _weaponUpgrade = new WeaponUpgrade();
        }

        public UniversalUpgrade(string name, WeaponUpgrade weaponUpgrade)
        {
            Name = name;
            _modifierUpgrades = new List<ModifierUpgrade>();
            _weaponUpgrade = weaponUpgrade;
        }

        public UniversalUpgrade(string name, List<ModifierUpgrade> modifierUpgrades, WeaponUpgrade weaponUpgrade)
        {
            Name = name;
            _modifierUpgrades = modifierUpgrades;
            _weaponUpgrade = weaponUpgrade;
        }

        #endregion

        public void Apply()
        {
            for(var i = 0; i < _modifierUpgrades.Count; i++)
                _modifierUpgrades[i].Apply();
            _weaponUpgrade.Apply(); 
        }
    }
}