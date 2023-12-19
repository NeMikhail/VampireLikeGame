using Core.Interface.IModels;
using Core.Interface.IPresenters;
using Core.Interface.ISynergy;

namespace MVP.Presenters.WeaponPresenters
{
    internal sealed class SynergyListUpdatePresenter : IInitialisation, ICleanUp
    {
        private ISynergyModel _synergyModel;
        private IPlayerModel _playerModel;

        public SynergyListUpdatePresenter(IDataProvider dataProvider)
        {
            _synergyModel = dataProvider.SynergyModel;
            _playerModel = dataProvider.PlayerModel;
        }


        public void Initialisation()
        {
            _playerModel.OnAddWeaponSinergy += OnNewSynergy;
        }

        private void OnNewSynergy(IWeaponModel weaponModel)
        {
            _synergyModel.ActiveSynergies.Add(weaponModel);
        }

        public void Cleanup()
        {
            _playerModel.OnAddWeaponSinergy -= OnNewSynergy;
        }
    }
}