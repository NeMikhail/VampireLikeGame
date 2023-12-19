using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.WeaponPresenters
{
    internal sealed class UpgradeWeaponPresenter : IInitialisation, ICleanUp
    {
        private IPlayerModel _playerModel;

        public UpgradeWeaponPresenter(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }


        public void Initialisation()
        {
            _playerModel.OnUpgradeWeapon += UpgradeWeapon;
        }

        private void UpgradeWeapon(IWeaponModel weaponModel)
        {
            weaponModel.LevelUp();
        }

        public void Cleanup()
        {
            _playerModel.OnUpgradeWeapon -= UpgradeWeapon;
        }
    }
}