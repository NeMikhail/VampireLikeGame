using Core.Interface.IModels;

namespace Core.Interface.IFeatures
{
    internal interface IAttackWeapon
    {
        public void Init(IWeaponModel weapon);
        public void Disabled(IWeaponModel weapon);
        public void Enabled(IWeaponModel weapon);
        public void Execute(IWeaponModel weapon);
    }
}
