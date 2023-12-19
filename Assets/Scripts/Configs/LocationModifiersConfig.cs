using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(LocationModifiersConfig), menuName = "Configs/Location/" +
        nameof(LocationModifiersConfig))]
    internal sealed class LocationModifiersConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedMultiplier { get; private set; }
        [field: SerializeField] public float MaxHealthMultiplier { get; private set; }
        [field: SerializeField] public float ArmorAddition { get; private set; }
        [field: SerializeField] public float Regeneration { get; private set; }
        [field: SerializeField] public float RevivesCount { get; private set; }
        [field: SerializeField] public float WeaponDamageMultiplier { get; private set; }
        [field: SerializeField] public float WeaponAreaMultiplier { get; private set; }
        [field: SerializeField] public float WeaponReloadMultiplier { get; private set; }
        [field: SerializeField] public float WeaponEffectDurationMultiplier { get; private set; }
        [field: SerializeField] public float ProjectileSpeedMultiplier { get; private set; }
        [field: SerializeField] public int WeaponProjectilesAdditional { get; private set; }
        [field: SerializeField] public float LuckMultiplier { get; private set; }
        [field: SerializeField] public float ExperienceMultiplier { get; private set; }
        [field: SerializeField] public float CoinsMultiplier { get; private set; }
        [field: SerializeField] public float ExperienceAttractionArea { get; private set; }
        [field: SerializeField] public float EnemyHealthMultiplier { get; private set; }
        [field: SerializeField] public float EnemySpeedMultiplier { get; private set; }
        [field: SerializeField] public float EnemyDamageMultiplier { get; private set; }
        [field: SerializeField] public float EnemyCountMultiplier { get; private set; }
    }
}