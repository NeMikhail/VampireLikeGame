using Core.Interface.IModels;

namespace Core.Interface
{
    internal interface IItemsView
    {
        public void SetWeapon(IWeaponModel weapon);
        public void SetPassiveItem(IPassiveItemModel passiveItem);
    }
}