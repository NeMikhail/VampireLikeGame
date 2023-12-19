using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Enums;

namespace Core.ImprovmentSystem.Upgrade.ConcreteUpgrades
{
    public struct WeaponUpgrade : IUpgrade
    {
        private readonly IDataProvider _dataProvider;

        private readonly WeaponsName _weaponType;
        public WeaponsName WeaponsName => _weaponType;

        public string Name { get; }

        #region Constructors

        internal WeaponUpgrade(IDataProvider dataProvider)
        {
            Name = string.Empty;
            _dataProvider = dataProvider;
            _weaponType = WeaponsName.None;
        }

        internal WeaponUpgrade(IDataProvider dataProvider, WeaponsName weaponType)
        {
            Name = string.Empty;
            _dataProvider = dataProvider;
            _weaponType = weaponType;
        }

        internal WeaponUpgrade(string name, IDataProvider dataProvider)
        {
            Name = name;
            _dataProvider = dataProvider;
            _weaponType = WeaponsName.None;
        }

        internal WeaponUpgrade(string name, IDataProvider dataProvider, WeaponsName weaponType)
        {
            Name = name;
            _dataProvider = dataProvider;
            _weaponType = weaponType;
        }

        #endregion

        public void Apply()
        {
            if (_dataProvider == null) 
                return;
            IWeaponModel weapon = _dataProvider.GetWeaponModel((int)_weaponType);
            _dataProvider.PlayerModel.AddWeapon(weapon);
        }
    }
}