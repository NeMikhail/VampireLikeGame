using Core.Interface.IModels;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    internal struct SynergyWeaponUpgrade : IUpgrade
    {
        public string Name { get; }
        private IWeaponModel _weaponModelWithSynergy;

        public SynergyWeaponUpgrade(IWeaponModel weaponModelWithSynergy, string name)
        {
            Name = name;
            _weaponModelWithSynergy = weaponModelWithSynergy;
        }


        public void Apply()
        {
            _weaponModelWithSynergy.LevelUp();
        }
    }
}