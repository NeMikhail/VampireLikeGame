using UnityEngine;
using Core.Interface.IModels;

namespace Core.ImprovmentSystem.Upgrade
{
    public sealed class PlayerStatsChanger
    {      
        private readonly IModifiersModel _modifiersModel;

        internal PlayerStatsChanger(IModifiersModel modifiersModel)
        {
            _modifiersModel = modifiersModel;
        }


        public void ChangeSpeed(float deltaMultiplierSpeed)
        {
            _modifiersModel.SpeedMultiplier *= deltaMultiplierSpeed;
        }

        public void ChangeDamage(float deltaMultiplierDamage)
        {
            _modifiersModel.WeaponDamageMultiplier *= deltaMultiplierDamage;
        }

        public void ChangeArea(float deltaMultiplierRadius)
        {
            _modifiersModel.WeaponAreaMultiplier *= deltaMultiplierRadius;
        }

        public void ChangeWeaponCooldown(float deltaSubtractionWeaponCooldown)
        {
            _modifiersModel.WeaponCooldownMultiplier -= deltaSubtractionWeaponCooldown;
        }

        public void ChangeProjectileSpeed(float deltaMultiplierProjectileSpeed)
        {
            _modifiersModel.ProjectileSpeedMultiplier *= deltaMultiplierProjectileSpeed;
        }

        public void ChangeProjectileDuration(float deltaMultipliterProjectileDuration)
        {
            _modifiersModel.WeaponEffectDurationMultiplier *= deltaMultipliterProjectileDuration;
        }

        public void ChangeProjectileCount(int deltaAdditionProjectileCount)
        {
            _modifiersModel.WeaponProjectilesAdditional += deltaAdditionProjectileCount;
        }

        public void ChangeMaxHealth(float deltaMultiplierMaxHealth)
        {
            _modifiersModel.MaxHealthMultiplier *= deltaMultiplierMaxHealth;
        } 

        public void ChangeArmor(float deltaAdditionArmor)
        {
            _modifiersModel.ArmorAddition += deltaAdditionArmor;
        }

        public void ChangeRegeneration(float deltaAdditionRegeneration)
        {
            _modifiersModel.Regeneration += deltaAdditionRegeneration;
        }

        public void ChangeLuck(float deltaMultiplerLuck)
        {
            _modifiersModel.LuckMultiplier *= deltaMultiplerLuck;
        }

        public void ChangeExpirienceGrowth(float deltaMultiplerExpirience)
        {
            _modifiersModel.ExperienceMultiplier *= deltaMultiplerExpirience;
        }

        public void ChangeExpiriencePickupRange(float deltaMultiplerExpirienceRange)
        {
            _modifiersModel.ExperienceAttractionArea *= deltaMultiplerExpirienceRange;
        }

        public void ChangeRevivesCount(float deltamultiplerRevivesCount)
        {
            _modifiersModel.RevivesCount += deltamultiplerRevivesCount;
        }
    }
}