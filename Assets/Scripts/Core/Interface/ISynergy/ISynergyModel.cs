using System.Collections.Generic;
using Core.Interface.IModels;

namespace Core.Interface.ISynergy
{
    internal interface ISynergyModel
    {
        public List<IWeaponModel> ActiveSynergies { get; }
        
        public void AddSynergy(IWeaponModel weapon);
        public void RemoveSynergy(IWeaponModel weaponModel);
    }
}