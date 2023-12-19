using UnityEngine.UI;
using Core.Interface;
using Core.Interface.IModels;

namespace UI
{
    internal sealed class UIItemsView : IItemsView
    {
        private Image[] _weaponCells;
        private Image[] _passiveItemCells;

        private int _weponsCount;
        private int _passiveItemsCount;

        public UIItemsView(Image[] weapons, Image[] items)
        {
            _weaponCells = weapons;
            _passiveItemCells = items;
        }


        public void SetWeapon(IWeaponModel weapon)
        {
            if (_weponsCount > 5)
                return;
            
            _weaponCells[_weponsCount++].sprite = weapon.Icon;
        }

        public void SetPassiveItem(IPassiveItemModel passiveItem)
        {
            if (_passiveItemsCount > 5)
                return;

            _passiveItemCells[_passiveItemsCount++].sprite = passiveItem.Icon;
        }
    }
}