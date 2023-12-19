using System.Collections.Generic;
using UnityEngine;
using Core.Interface.IModels;
using Core.Interface.ISynergy;

namespace MVP.Models.SynergyModels
{
    internal sealed class SynergyModel: ISynergyModel
    {
        private List<IWeaponModel> _activeSynergies;

        public List<IWeaponModel> ActiveSynergies => _activeSynergies;

        public SynergyModel()
        {
            _activeSynergies = new List<IWeaponModel>();
        }


        public void AddSynergy(IWeaponModel weapon)
        {
            _activeSynergies.Add(weapon);
        }

        public void RemoveSynergy(IWeaponModel weaponModel)
        {
            _activeSynergies.Remove(weaponModel);
        }
    }
}