using Core.Interface;
using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.UIPresenters
{
    internal sealed class UIItemsPresenter : IInitialisation, ICleanUp
    {
        private IPlayerModel _playerModel;
        private IItemsView _itemsView;

        public UIItemsPresenter(IDataProvider dataProvider, IViewProvider viewProvider)
        {
            _playerModel = dataProvider.PlayerModel;
            _itemsView = viewProvider.GetItemsView();
            SetNewWeapon(dataProvider.GetWeaponModel((int)_playerModel.PlayerDefaultWeapon));
        }


        public void Initialisation()
        {
            _playerModel.OnAddNewWeapon += SetNewWeapon;
            _playerModel.OnAddNewPassiveItem += SetNewPassiveItem;
        }

        public void SetNewWeapon(IWeaponModel weapon)
        {
            _itemsView.SetWeapon(weapon);
        }

        public void SetNewPassiveItem(IPassiveItemModel passiveItem)
        {
            _itemsView.SetPassiveItem(passiveItem);
        }

        public void Cleanup()
        {
            _playerModel.OnAddNewWeapon -= SetNewWeapon;
            _playerModel.OnAddNewPassiveItem -= SetNewPassiveItem;
        }
    }
}