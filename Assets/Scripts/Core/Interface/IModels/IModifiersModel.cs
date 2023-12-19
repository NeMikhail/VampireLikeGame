using System;

namespace Core.Interface.IModels
{
    internal interface IModifiersModel
    {
        public event Action<float> SpeedMultiplierChanged;
        public event Action<float> MaxHealthMultiplierChanged;
        public event Action<float> ArmorAdditionChanged;
        public event Action<float> RegenerationChanged;
        public event Action<float> RevivesCountChanged;
        public event Action<float> DamageMultiplierChanged;
        public event Action<float> AreaMultiplierChanged;
        public event Action<float> CooldownMultiplierChanged;
        public event Action<float> EffectDurationMultiplierChanged;
        public event Action<float> ProjectileSpeedMultiplierChanged;
        public event Action<int> ProjectilesAdditionalChanged;
        public event Action<float> LuckMultiplierChanged;
        public event Action<float> ExpirienceMultiplierChanged;
        public event Action<float> CoinsMultiplierChanged;
        public event Action<float> ExperienceAttractionAreaChanged;
        public event Action<float> EnemyHealthMultiplierChanged;
        public event Action<float> EnemySpeedMultiplierChanged;
        public event Action<float> EnemyDamageMultiplierChanged;
        public event Action<float> EnemyCountMultiplierChanged;

        public float SpeedMultiplier { get; set; }
        public float MaxHealthMultiplier { get; set; }
        public float ArmorAddition { get; set; }
        public float Regeneration { get; set; }
        public float RevivesCount { get; set; }
        
        public float WeaponDamageMultiplier { get; set; }
        public float WeaponAreaMultiplier { get; set; }
        public float WeaponCooldownMultiplier { get; set; }
        public float WeaponEffectDurationMultiplier { get; set; }
        public float ProjectileSpeedMultiplier { get; set; }
        public int WeaponProjectilesAdditional { get; set; }

        public float LuckMultiplier { get; set; }
        public float ExperienceMultiplier { get; set; }
        public float CoinsMultiplier { get; set; }
        public float ExperienceAttractionArea { get; set; }
        
        public float EnemyHealthMultiplier { get; set; }
        public float EnemySpeedMultiplier { get; set; }
        public float EnemyDamageMultiplier { get; set; }
        public float EnemyCountMultiplier { get; set; }

        public void ApplyModifiers();
    }
}