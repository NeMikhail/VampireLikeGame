using System;
using System.Collections.Generic;
using Core.Interface.IModels;
using Core.Interface.IPresenters;

namespace MVP.Presenters.PlayerPresenters
{
    internal sealed class ModifiersPresenter : IInitialisation, ICleanUp
    {
        private IModifiersModel _modifiersModel;
        private IModifyPlayer _modifyPlayer;
        private List<IWeaponModel> _weaponModelList;

        internal ModifiersPresenter(IModifiersModel modifiersModel, IModifyPlayer modifyable,
            List<IWeaponModel> weaponModelList)
        {
            _modifiersModel = modifiersModel;
            _modifyPlayer = modifyable;
            _weaponModelList = weaponModelList;
        }


        public void Initialisation()
        {
            _modifiersModel.SpeedMultiplierChanged += _modifyPlayer.ModifySpeed;
            _modifiersModel.MaxHealthMultiplierChanged += _modifyPlayer.ModifyMaxHealth;
            _modifiersModel.ArmorAdditionChanged += _modifyPlayer.ModifyArmor;
            _modifiersModel.RegenerationChanged += _modifyPlayer.ModifyRegeneration;
            _modifiersModel.ExpirienceMultiplierChanged += _modifyPlayer.ModifyExpirienceMultiplier;
            _modifiersModel.ExperienceAttractionAreaChanged += _modifyPlayer.ModifyExpiriencePickupRange;
            _modifiersModel.RevivesCountChanged += _modifyPlayer.ModifyRevivesCount;

            foreach(IModifyWeapon modifyWeapon in _weaponModelList)
            {
                _modifiersModel.DamageMultiplierChanged += modifyWeapon.ModifyDamage;
                _modifiersModel.AreaMultiplierChanged += modifyWeapon.ModifyArea;
                _modifiersModel.ProjectileSpeedMultiplierChanged += modifyWeapon.ModifyProjectileSpeed;
                _modifiersModel.CooldownMultiplierChanged += modifyWeapon.ModifyCooldown;
                _modifiersModel.EffectDurationMultiplierChanged += modifyWeapon.ModifyEffectDuration;
                _modifiersModel.ProjectilesAdditionalChanged += modifyWeapon.ModifyProjectilesAdditional;
            }
            
            _modifiersModel.ApplyModifiers();
        }

        public void Cleanup()
        {
            _modifiersModel.SpeedMultiplierChanged -= _modifyPlayer.ModifySpeed;
            _modifiersModel.MaxHealthMultiplierChanged -= _modifyPlayer.ModifyMaxHealth;
            _modifiersModel.ArmorAdditionChanged -= _modifyPlayer.ModifyArmor;
            _modifiersModel.RegenerationChanged -= _modifyPlayer.ModifyRegeneration;
            _modifiersModel.ExpirienceMultiplierChanged -= _modifyPlayer.ModifyExpirienceMultiplier;
            _modifiersModel.ExperienceAttractionAreaChanged -= _modifyPlayer.ModifyExpiriencePickupRange;
            _modifiersModel.RevivesCountChanged -= _modifyPlayer.ModifyRevivesCount;

            foreach (IModifyWeapon modifyWeapon in _weaponModelList)
            {
                _modifiersModel.DamageMultiplierChanged -= modifyWeapon.ModifyDamage;
                _modifiersModel.AreaMultiplierChanged -= modifyWeapon.ModifyArea;
                _modifiersModel.ProjectileSpeedMultiplierChanged -= modifyWeapon.ModifyProjectileSpeed;
                _modifiersModel.CooldownMultiplierChanged -= modifyWeapon.ModifyCooldown;
                _modifiersModel.EffectDurationMultiplierChanged -= modifyWeapon.ModifyEffectDuration;
                _modifiersModel.ProjectilesAdditionalChanged -= modifyWeapon.ModifyProjectilesAdditional;
            }
        }
    }
}