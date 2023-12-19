using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.PassiveItemsUpgradePresenter
{
    internal class PassiveItemUpgradePresenter : IInitialisation, ICleanUp
    {
        private IPlayerModel _playerModel;

        public PassiveItemUpgradePresenter(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }


        public void Initialisation()
        {
            _playerModel.OnUpgradePassiveItem += UpgradePassiveItem;
        }

        private void UpgradePassiveItem(IPassiveItemModel passiveItemModel)
        {
            passiveItemModel.LevelUp();
        }

        public void Cleanup()
        {
            _playerModel.OnUpgradePassiveItem -= UpgradePassiveItem;
        }
    }
}